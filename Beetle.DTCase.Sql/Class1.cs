using Beetle.DTCore;
using MySql.Data.MySqlClient;
using Peanut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCase.Sql
{
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
}
