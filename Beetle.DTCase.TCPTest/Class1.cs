using Beetle.DTCore;
using BeetleX.Buffers;
using BeetleX.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCase.Tcp
{
	public class SocketTcpUtf : Beetle.DTCore.TestCase<TcpConfig>
	{
		public byte[] mData;

		private BeetleX.Clients.IClient mClient;

		public override void Init()
		{
			base.Init();
			mData = GetData();
			mClient = BeetleX.ServerFactory.CreateTcpClient(Config.Host, Config.Port);
			mClient.ClientError = OnNetError;
			mClient.Receive = OnReceive;
		}

		private void OnReceive(IClient c, IBinaryReader reader)
		{
			foreach (IBuffer item in reader.GetBuffers())
			{
				item.Free();
			}
			Completed();
		}

		private void OnNetError(IClient c, Exception e, string message)
		{
			Completed(e);
		}

		protected virtual byte[] GetData()
		{
			return Encoding.UTF8.GetBytes(Config.Data);
		}

		public override string Name
		{
			get
			{
				return "TCP_UTF_DATA";
			}

		}
		public override void Execute()
		{
			mClient.Send(mData);
		}
	}

	public class SocketTcpHex : SocketTcpUtf
	{
		public override string Name
		{
			get
			{
				return "TCP_HEX_DATA";
			}
		}

		protected override byte[] GetData()
		{
			return Enumerable.Range(0, Config.Data.Length)
				  .Where(x => x % 2 == 0)
				  .Select(x => Convert.ToByte(Config.Data.Substring(x, 2), 16))
				  .ToArray();
		}

	}




	public class TcpConfig
	{
		[PropertyLabel("服务地址", "对应的服务器地址，IP或域名")]
		public string Host { get; set; }
		[PropertyLabel("端口", "对应的服务端口")]
		public int Port { get; set; }
		[PropertyLabel("数据", "进行压力测试发送的数据内容")]
		[Property(Type = PropertyType.Remark)]
		public string Data { get; set; }
	}

}
