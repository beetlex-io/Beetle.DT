# Beetle.DT（分布式压力测试工具）
  基于.NET实现的分布式压力测试工具，用户可以根据需求编写相关的测试用例；通过工具的管理界面即可以把测试用例推送到服务中心，
  再根据实际压测的需求把测试用例分配到不同节点上运行。工具会根据测试的情况实时获取测试结果，测试完成后用户还可以查询具体
  的测试报告。节点采用进程隔离的方式运行测试用例，所以测试用例的运行都是相互独立。
  ![image](https://github.com/IKende/Beetle.DT/blob/master/IMG_5106.PNG)
  
### 运行环境要求
现有版本的Beetle.DT只能运行在windows+.net 4.5的基础上（暂只支持Console模试运行）在功能完善后会进行win service服务和.net core版的扩展开发。工具运行主要部署两大应用服务（已编译在Public目录下）分别是：测试管理中心和测试运行节点，通过配置相关网络信息确保节点和中心有效地进行交互通讯。
### 测试管理中心
项目Beetle.DTCenter是测试管理中心的运行程度，中心需要配置两个服务地址分别是节点通讯端口和管理通讯端，对应端口是9091和9092;如果想修改服务地址和端则通过配置文件修改（Beetle.DTCenter.exe）
```xml
  <appSettings>
    <!-- ... -->
    <add key="server-host" value=""/>
    <add key="server-port" value="9091"/>

    <add key="manager-host" value=""/>
    <add key="manager-port" value="9092"/>
    <!-- ... -->
  </appSettings>
```
### 节点服务
项目Beetle.DTNode是测试节点服务，节点服务除了包括自身的服务外目录下还包括了Beetle.DTProcess；Beetle.DTProcess的主要用途是用于加载运行测试用例；Beetle.DTProcess并不需要配置任何信息，不过它必须存放在Beetle.DTNode对应的目录下。Beetle.DTNode主要是配置Beetle.DTCenter对应的server-host和server-port，确认节点能有效的指向对应的测试中心。
``` xml
  <appSettings>
    <!-- ... -->
    <add key="server-host" value="127.0.0.1"/>
    <add key="server-port" value="9091"/>
    <!-- ... -->
  </appSettings>
```
### 管理工具
当测试中心和节点都部署完成后，就可以使用管理工具登陆到测试中心进行测试用例的创建，文件上传，节点监控和测试运行等相关操作。
打开管理工具后输入管理端的服务地址，然后连接进入管理端；进入后就可以创建测试目录和上传文件，在选择测试目录和运行节点后点击测试按钮就进行测试界面。
![](https://raw.githubusercontent.com/wiki/IKende/Beetle.DT/beetledt_manager.png)
在测试界面上点击同步即可以把测试用例同步到所选择的节点上，然后输入相应的配置信息点击运行进行测试用例即可；在测试运行过程中下面的图表会显示当前不同时间段内测试请求响应的并发结果
![](https://raw.githubusercontent.com/wiki/IKende/Beetle.DT/beetledt_runtest.png)
测试完成后可以通过查看详细报表来查看测试的情况
![](https://raw.githubusercontent.com/wiki/IKende/Beetle.DT/beetledt_report.png)
### HTTP,SQL和TCP测试
Beetle.DT自身只是一个测试工具，所以在没有测试用例的情况工具是完全起不了任何作用。为让大家更好的了解工具的用途，所以编写了几个基础的测试用例，通过这些测试用例可以对http,sql和tcp进行简单的压力测试。
![](https://raw.githubusercontent.com/wiki/IKende/Beetle.DT/beetledt_http.png)
![](https://raw.githubusercontent.com/wiki/IKende/Beetle.DT/beetledt_sql.png)
![](https://raw.githubusercontent.com/wiki/IKende/Beetle.DT/beetledt_tcp.png)
### 编写测试用例
实际业务都存在多样性，对于简单的测试用例是不可能满足不同业务的需要，所以在使用工具的时候就不得不进行测试用例编写。由于需要符合工具运行要求所以编写测试用例也需要遵循着工具制定的规则；在编写测试用例的时候必须引用Beetle.DTCore项目，然后承继TestCase重写OnExecute写入需要测试的代码取可；如果测试代码是异步的情况则需要重写Execute的主方法，手动调用Completed方法来告诉工具测试用例什么时候完成。以下是几个基础测试用例的实现代码
* HTTP
``` c#
	public class HttpGet : TestCase<Config>
	{
		public override string Name
		{
			get
			{
				return "HTTP_GET";
			}
		}

		private long mIndex = 0;

		private List<string> mUrls = new List<string>();

		public override void Init()
		{
			base.Init();
			mUrls.AddRange(this.Config.Urls.Split(';'));
		}

		public string GetUrl()
		{
			mIndex++;
			return mUrls[(int)(mIndex % mUrls.Count)];
		}

		protected override void OnExecute()
		{
			System.Net.WebRequest wReq = System.Net.WebRequest.Create(GetUrl());
			System.Net.WebResponse wResp = wReq.GetResponse();
			System.IO.Stream respStream = wResp.GetResponseStream();
			using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.UTF8))
			{
				reader.ReadToEnd();
			}
		}
	}

	public class Config
	{

		public string Urls { get; set; }
	}
```
* SQL
``` c#
	public class MySqlDriver : DriverTemplate<MySqlConnection, MySqlCommand, MySqlDataAdapter, MySqlParameter, MysqlBuilder>
	{
	}

	public abstract class sqlbase : TestCase<SQLConfig>
	{
		public override void Init()
		{
			Peanut.DBContext.SetConnectionString(DB.DB1, Config.Connection);
			base.Init();
		}
		protected override void OnExecute()
		{
			base.OnExecute();
			Peanut.SQL sql = Config.Sql;
			sql.Execute();
		}
	}
	public class MSSQL : sqlbase
	{
		public override void Init()
		{
			Peanut.DBContext.SetConnectionDriver<Peanut.MSSQL>(DB.DB1);

			base.Init();
		}

		public override string Name
		{
			get
			{
				return "mssql";
			}
		}

	}

	public class MYSQL : sqlbase
	{
		public override void Init()
		{
			Peanut.DBContext.SetConnectionDriver<MySqlDriver>(DB.DB1);
			base.Init();
		}

		public override string Name
		{
			get
			{
				return "mysql";
			}
		}

	}

	public class SQLConfig
	{
		public string Connection { get; set; }
		public string Sql { get; set; }
	}
```
* TCP
``` c#
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
		public string Host { get; set; }

		public int Port { get; set; }

		public string Data { get; set; }
	}

```
