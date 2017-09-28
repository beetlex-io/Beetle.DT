using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Network
{
	public class Register : MessageBase
	{

		public string Name { get; set; }

		public string Token { get; set; }

		public void CreateToken(string key)
		{
			string tokenvalue = Name + key + DateTime.Now.ToString("yyyyMMdd");
			Token = CreateMD5(tokenvalue);
		}

		public bool VerifyToken(string key)
		{
			string tokenvalue = Name + key + DateTime.Now.ToString("yyyyMMdd");
			return this.Token == CreateMD5(tokenvalue);
		}

		public static string CreateMD5(string input)
		{
			// Use input string to calculate MD5 hash
			using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
			{
				byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
				byte[] hashBytes = md5.ComputeHash(inputBytes);

				// Convert the byte array to hexadecimal string
				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < hashBytes.Length; i++)
				{
					sb.Append(hashBytes[i].ToString("X2"));
				}
				return sb.ToString();
			}
		}
	}
}
