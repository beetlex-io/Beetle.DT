using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Beetle.DTCore.Domains
{
	public class FileCompiler
	{
		private CompilerErrorCollection compilerErrors = null;

		public Assembly CreateAssembly(string filename)
		{
			return CreateAssembly(filename, new ArrayList());
		}

		public Assembly CreateAssembly(string filename, IList references)
		{

			compilerErrors = null;

			string extension = Path.GetExtension(filename);
			CodeDomProvider codeProvider = null;
			switch (extension)
			{
				case ".cs":
					codeProvider = new Microsoft.CSharp.CSharpCodeProvider();
					break;
				case ".vb":
					codeProvider = new Microsoft.CSharp.CSharpCodeProvider();
					break;
				default:
					throw new InvalidOperationException("Script files must have a .cs or .vb.");
			}

			CompilerParameters compilerParams = new CompilerParameters();
			compilerParams.CompilerOptions = "/target:library /optimize";
			compilerParams.GenerateExecutable = false;
			compilerParams.GenerateInMemory = true;
			compilerParams.IncludeDebugInformation = false;

			compilerParams.ReferencedAssemblies.Add("mscorlib.dll");
			compilerParams.ReferencedAssemblies.Add("System.dll");


			foreach (string reference in references)
			{
				if (!compilerParams.ReferencedAssemblies.Contains(reference))
				{
					compilerParams.ReferencedAssemblies.Add(reference);
				}
			}

			CompilerResults results = codeProvider.CompileAssemblyFromFile(compilerParams,
				filename);

			if (results.Errors.Count > 0)
			{
				StringBuilder sb = new StringBuilder();
				foreach (CompilerError item in results.Errors)
				{
					sb.AppendFormat("{0} line:{1}   {2}\r\n", item.FileName, item.Line, item.ErrorText);
				}
				compilerErrors = results.Errors;
				throw new Exception(
					"Compiler error(s)\r\n" + sb.ToString());
			}

			Assembly createdAssembly = results.CompiledAssembly;
			return createdAssembly;
		}


		public Assembly CreateAssembly(IList filenames)
		{
			return CreateAssembly(filenames, new ArrayList());
		}


		public Assembly CreateAssembly(IList filenames, IList references)
		{
			string fileType = null;
			foreach (string filename in filenames)
			{
				string extension = Path.GetExtension(filename);
				if (fileType == null)
				{
					fileType = extension;
				}
				else if (fileType != extension)
				{
					throw new ArgumentException("All files in the file list must be of the same type.");
				}
			}


			compilerErrors = null;


			CodeDomProvider codeProvider = null;
			switch (fileType)
			{
				case ".cs":
					codeProvider = new Microsoft.CSharp.CSharpCodeProvider();
					break;
				case ".vb":
					codeProvider = new Microsoft.CSharp.CSharpCodeProvider();
					break;
				default:
					throw new InvalidOperationException("Script files must have a .cs, .vb, or .js extension, for C#, Visual Basic.NET, or JScript respectively.");
			}



			CompilerParameters compilerParams = new CompilerParameters();
			compilerParams.CompilerOptions = "/target:library /optimize";
			compilerParams.GenerateExecutable = false;
			compilerParams.GenerateInMemory = true;
			compilerParams.IncludeDebugInformation = false;
			compilerParams.ReferencedAssemblies.Add("mscorlib.dll");
			compilerParams.ReferencedAssemblies.Add("System.dll");


			foreach (string reference in references)
			{
				if (!compilerParams.ReferencedAssemblies.Contains(reference))
				{
					compilerParams.ReferencedAssemblies.Add(reference);
				}
			}


			CompilerResults results = codeProvider.CompileAssemblyFromFile(
				compilerParams, (string[])ArrayList.Adapter(filenames).ToArray(typeof(string)));


			if (results.Errors.Count > 0)
			{
				StringBuilder sb = new StringBuilder();
				foreach (CompilerError item in results.Errors)
				{
					sb.AppendFormat("{0} line:{1}   {2}\r\n", item.FileName, item.Line, item.ErrorText);
				}
				compilerErrors = results.Errors;
				throw new Exception(
					"Compiler error(s)\r\n" + sb.ToString());
			}

			Assembly createdAssembly = results.CompiledAssembly;
			return createdAssembly;
		}


		public Assembly CreateAsssemblyFromCodes(bool iscs, params string[] codes)
		{
			compilerErrors = null;
			CodeDomProvider codeProvider = null;
			if (iscs)
			{
				codeProvider = new Microsoft.CSharp.CSharpCodeProvider();
			}
			else
			{
				codeProvider = new Microsoft.CSharp.CSharpCodeProvider();
			}

			CompilerParameters compilerParams = new CompilerParameters();
			compilerParams.CompilerOptions = "/target:library /optimize";
			compilerParams.GenerateExecutable = false;
			compilerParams.GenerateInMemory = true;
			compilerParams.IncludeDebugInformation = false;

			compilerParams.ReferencedAssemblies.Add("mscorlib.dll");
			compilerParams.ReferencedAssemblies.Add("System.dll");
			CompilerResults results = codeProvider.CompileAssemblyFromSource(compilerParams,
				codes);

			if (results.Errors.Count > 0)
			{
				StringBuilder sb = new StringBuilder();
				foreach (CompilerError item in results.Errors)
				{
					sb.AppendFormat("{0} line:{1}   {2}\r\n", item.FileName, item.Line, item.ErrorText);
				}
				compilerErrors = results.Errors;
				throw new Exception(
					"Compiler error(s)\r\n" + sb.ToString());
			}

			Assembly createdAssembly = results.CompiledAssembly;
			return createdAssembly;
		}



		public CompilerErrorCollection CompilerErrors
		{
			get
			{
				return compilerErrors;
			}
		}
	}
}
