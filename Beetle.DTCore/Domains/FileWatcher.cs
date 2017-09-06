using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Beetle.DTCore.Domains
{
    public class FileWatcher
    {
        public FileWatcher(string path, string[] filter)
        {
            if (filter == null || filter.Length == 0)
            {
                FileSystemWatcher mWather = new FileSystemWatcher(path);
                mWather.Changed += new FileSystemEventHandler(fileSystemWatcher_Changed);
                mWather.Deleted += new FileSystemEventHandler(fileSystemWatcher_Changed);
                mWather.Created += new FileSystemEventHandler(fileSystemWatcher_Changed);
                mWather.EnableRaisingEvents = true;
                mWather.IncludeSubdirectories = false;
                mWathers.Add(mWather);
            }
            else
            {
                foreach (string item in filter)
                {
                    FileSystemWatcher mWather = new FileSystemWatcher(path, item);
                    mWather.Changed += new FileSystemEventHandler(fileSystemWatcher_Changed);
                    mWather.Deleted += new FileSystemEventHandler(fileSystemWatcher_Changed);
                    mWather.Created += new FileSystemEventHandler(fileSystemWatcher_Changed);
                    mWather.EnableRaisingEvents = true;
                    mWather.IncludeSubdirectories = false;
                    mWathers.Add(mWather);
                }
            }
            mTimer = new System.Threading.Timer(OnDetect, null, 5000, 5000);

        }

        private IList<FileSystemWatcher> mWathers = new List<FileSystemWatcher>();

        private System.Threading.Timer mTimer;

        private DateTime mLastChangeTime;

        private bool mIsChange = false;

        public event Action<FileWatcher> Change;


        private void OnDetect(object state)
        {
            if (mIsChange && (DateTime.Now - mLastChangeTime).TotalSeconds > 10)
            {
                mIsChange = false;
                if (Change != null)
                    this.Change(this);
            }
        }

        private void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            mLastChangeTime = DateTime.Now;
            mIsChange = true;
        }
    }
}
