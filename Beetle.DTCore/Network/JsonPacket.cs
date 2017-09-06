using BeetleX;
using BeetleX.Buffers;
using BeetleX.Clients;
using BeetleX.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Network
{

	public interface IMessageTypeHandler
	{
		Type ReadType(BeetleX.Buffers.IBinaryReader reader);

		void WriteType(object data, BeetleX.Buffers.IBinaryWriter writer);

		string GetTypeName(Type type);

		Type GetType(string typeName);
	}

	public class MessageTypeHandler : IMessageTypeHandler
	{
		private System.Collections.Concurrent.ConcurrentDictionary<Type, string> mTypeNames = new System.Collections.Concurrent.ConcurrentDictionary<Type, string>();

		private System.Collections.Concurrent.ConcurrentDictionary<string, Type> mNameTypes = new System.Collections.Concurrent.ConcurrentDictionary<string, Type>();

		public Type GetType(string typeName)
		{
			Type result;
			if (!mNameTypes.TryGetValue(typeName, out result))
			{
				if (typeName == null)
					throw new BeetleX.BXException("{0} type not found!", typeName);
				result = Type.GetType(typeName);
				if (result == null)
					throw new BXException("{0} type not found!", typeName);

				mNameTypes[typeName] = result;
			}
			return result;
		}

		public Type ReadType(IBinaryReader reader)
		{
			string typeName = reader.ReadLine();
			return GetType(typeName);
		}

		public string GetTypeName(Type type)
		{
			string result;
			if (!mTypeNames.TryGetValue(type, out result))
			{
				TypeInfo info = type.GetTypeInfo();
				if (info.FullName.IndexOf("System") >= 0)
					result = info.FullName;
				else
					result = string.Format("{0},{1}", info.FullName, info.Assembly.GetName().Name);
				mTypeNames[type] = result;
			}
			return result;
		}

		public void WriteType(object data, IBinaryWriter writer)
		{
			string name = GetTypeName(data.GetType());
			writer.WriteLine(name);
		}
	}

	public class ClientPacket : BeetleX.Clients.IClientPacket
	{

		public ClientPacket()
		{
			TypeHandler = new MessageTypeHandler();
		}

		private System.Collections.Concurrent.ConcurrentDictionary<Type, string> mTypeNames = new System.Collections.Concurrent.ConcurrentDictionary<Type, string>();

		public IMessageTypeHandler TypeHandler
		{
			get;
			set;
		}

		public EventClientPacketCompleted Completed
		{
			get;
			set;
		}

		private int mSize = 0;

		public IClientPacket Clone()
		{
			ClientPacket result = new ClientPacket();
			result.TypeHandler = TypeHandler;
			return result;
		}

		public void Decode(IClient client, IBinaryReader reader)
		{
			START:
			try
			{
				object data;
				if (mSize == 0)
				{
					if (reader.Length < 4)
						return;
					mSize = reader.ReadInt32();
				}
				if (reader.Length < mSize)
					return;
				Type type = TypeHandler.ReadType(reader);
				int bodySize = reader.ReadInt32();
				data = Newtonsoft.Json.JsonConvert.DeserializeObject(reader.ReadString(bodySize), type);
				mSize = 0;
				try
				{
					if (Completed != null)
					{
						Completed(client, data);
					}
				}
				catch (Exception e_)
				{
					client.ProcessError(e_, "client packet process object error!");
				}
				goto START;
			}
			catch (Exception e_)
			{
				client.ProcessError(e_, "client packet decode error!");
				client.DisConnect();
			}
		}

		public void Dispose()
		{

		}

		public void Encode(object data, IClient client, IBinaryWriter writer)
		{
			using (IWriteBlock msgsize = writer.Allocate4Bytes())
			{
				int length = (int)writer.Length;
				TypeHandler.WriteType(data, writer);
				using (IWriteBlock bodysize = writer.Allocate4Bytes())
				{
					int bodyStartlegnth = (int)writer.Length;
					string strData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
					writer.Write(strData);

					bodysize.SetData((int)writer.Length - bodyStartlegnth);
				}
				msgsize.SetData((int)writer.Length - length);

			}
		}
	}

	public class Packet : BeetleX.IPacket
	{
		public Packet()
		{
			TypeHandler = new MessageTypeHandler();
		}

		private PacketDecodeCompletedEventArgs mCompletedEventArgs = new PacketDecodeCompletedEventArgs();

		public EventHandler<PacketDecodeCompletedEventArgs> Completed
		{
			get;
			set;
		}

		public IMessageTypeHandler TypeHandler
		{
			get;
			set;
		}

		private int mSize = 0;

		public IPacket Clone()
		{
			Packet result = new Packet();
			result.TypeHandler = TypeHandler;
			return result;
		}

		public void Decode(ISession session, IBinaryReader reader)
		{
			START:
			try
			{
				object data;
				if (mSize == 0)
				{
					if (reader.Length < 4)
						return;
					mSize = reader.ReadInt32();
				}
				if (reader.Length < mSize)
					return;
				Type type = TypeHandler.ReadType(reader);
				int bodySize = reader.ReadInt32();
				string strData = reader.ReadString(bodySize);
				//if (type.Name.IndexOf("StatisticalReport") >= 0)
				//{
				//	Console.WriteLine(strData);
				//}
				data = Newtonsoft.Json.JsonConvert.DeserializeObject(strData, type); //reader.Stream.Deserialize(bodySize, type);
				mSize = 0;
				try
				{
					if (Completed != null)
					{
						Completed(this, mCompletedEventArgs.SetInfo(session, data));
					}
				}
				catch (Exception e_)
				{
					session.Server.Error(e_, session, "session packet process object error!");
				}

				goto START;
			}
			catch (Exception e_)
			{
				session.Server.Error(e_, session, "session packet decode error!");
				session.Dispose();
			}
		}

		private void OnEncode(object data, IBinaryWriter writer)
		{

			using (IWriteBlock msgsize = writer.Allocate4Bytes())
			{
				int length = (int)writer.Length;
				TypeHandler.WriteType(data, writer);
				using (IWriteBlock bodysize = writer.Allocate4Bytes())
				{
					int bodyStartlegnth = (int)writer.Length;
					writer.Write(Newtonsoft.Json.JsonConvert.SerializeObject(data));
					bodysize.SetData((int)writer.Length - bodyStartlegnth);
				}
				msgsize.SetData((int)writer.Length - length);

			}
		}

		public byte[] Encode(object data, IServer server)
		{
			byte[] result = null;
			using (XStream stream = new XStream(server.BufferPool))
			{
				IBinaryWriter writer = ServerFactory.CreateWriter(stream, server.Config.Encoding,
				 server.Config.LittleEndian);
				OnEncode(data, writer);
				stream.Position = 0;
				result = new byte[stream.Length];
				stream.Read(result, 0, result.Length);

			}
			return result;
		}

		public ArraySegment<byte> Encode(object data, IServer server, byte[] buffer)
		{
			using (XStream stream = new XStream(server.BufferPool))
			{
				IBinaryWriter writer = ServerFactory.CreateWriter(stream, server.Config.Encoding,
				  server.Config.LittleEndian);
				OnEncode(data, writer);
				stream.Position = 0;
				int count = (int)writer.Length;
				stream.Read(buffer, 0, count);
				return new ArraySegment<byte>(buffer, 0, count);

			}
		}

		public void Encode(object data, ISession session, IBinaryWriter writer)
		{
			OnEncode(data, writer);
		}

		public void Dispose()
		{

		}
	}
}
