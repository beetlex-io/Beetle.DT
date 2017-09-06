using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Network
{
	public class MessageBase
	{
		public MessageBase()
		{
			ID = Guid.NewGuid().ToString("N");
		}
		public string ID { get; set; }

		public void Reply(MessageBase msg, BeetleX.ISession session)
		{
			msg.ID = ID;
			session.Send(msg);
		}
		public void Reply(MessageBase msg, BeetleX.Clients.IClient client)
		{
			msg.ID = ID;
			client.Send(msg);
		}

		public void ReplySuccess(BeetleX.ISession session)
		{
			NetWorkResult result = new NetWorkResult();
			result.Success = true;
			Reply(result, session);
		}

		public void ReplySuccess(BeetleX.Clients.IClient client)
		{
			NetWorkResult result = new NetWorkResult();
			result.Success = true;
			Reply(result, client);
		}

		public void ReplyError(string message, BeetleX.ISession session)
		{
			NetWorkResult result = new NetWorkResult();
			result.Success = false;
			result.Message = message;
			Reply(result, session);
		}

		public void ReplyError(string message, BeetleX.Clients.IClient client)
		{
			NetWorkResult result = new NetWorkResult();
			result.Success = false;
			result.Message = message;
			Reply(result, client);
		}
	}
}
