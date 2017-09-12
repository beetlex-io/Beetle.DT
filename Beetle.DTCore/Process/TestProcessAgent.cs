using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore.Process
{
    public class TestProcessAgent : IDisposable
    {
        public TestProcessAgent(Node.NodeApp app, string name, string path, int nodePort)
        {

            DTProcessFile = "Beetle.DTProcess.exe";
            NodePort = nodePort;
            Path = path;
            App = app;
            Name = name;
            Started = false;
            this.Folder = new DTCore.Folder(Path);
        }

        public Folder Folder { get; set; }

        public Node.NodeApp App { get; set; }

        public string Name { get; set; }

        public int NodePort
        {
            get;
            set;
        }

        public string Path { get; set; }

        public BeetleX.ISession Session { get; set; }

        public System.Diagnostics.Process Process { get; set; }

        public string Config { get; set; }

        public bool Started { get; set; }

        public StatisticalInfo StatisticalInfo { get; set; }

        public void Start()
        {
            try
            {

                Success = 0;
                Errors = 0;
                string file = Path + DTProcessFile;

                System.IO.File.Copy(
                    AppDomain.CurrentDomain.BaseDirectory + DTProcessFile, file, true);

                System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
                info.FileName = file;
                info.Arguments = string.Format("{0} {1} {2} {3}", NodePort, Name, null, Config);
                this.Process = System.Diagnostics.Process.Start(info);

                Started = true;
                Loger.Process(LogType.INFO, "{0} node {1}  test process start!", App.Name, Name);
            }
            catch (Exception e_)
            {
                Loger.Process(LogType.ERROR, "{0} node {1}  test process start error {2}!", App.Name, Name, e_.Message);
            }
        }

        public void Run(string casename, int users, string config)
        {
            if (Session != null)
            {
                this.StatisticalInfo = new StatisticalInfo();
                Network.RunTestcase run = new Network.RunTestcase();
                run.UnitTest = Name;
                run.TestCase = casename;
                run.Users = users;
                run.Config = config;
                Session.Send(run);
            }
            else
            {
                Loger.Process(LogType.ERROR, "{0} network session notfound", Name);
            }
        }

        public void Stop()
        {
            try
            {
                Started = false;
                if (this.Process != null)
                {
                    this.Process.Kill();
                    Loger.Process(LogType.INFO, "{0} node {1}  test process stop!", App.Name, Name);
                    this.Process = null;
                }

            }
            catch (Exception e_)
            {
                Loger.Process(LogType.ERROR, "{0} node {1}  test process stop error {2}!", App.Name, Name, e_.Message);

            }
        }

        public ILogHandler Loger { get; set; }

        public string DTProcessFile { get; set; }

        public Network.RunTestcase TestCase { get; set; }

        public long Success { get; set; }

        public long Errors { get; set; }

        public void Dispose()
        {
            Stop();
        }
    }
}
