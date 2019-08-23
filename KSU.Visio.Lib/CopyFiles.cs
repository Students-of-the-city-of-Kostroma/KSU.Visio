using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace KSU.Visio.Lib
{
    class CopyFiles
    {
        string pathInto, pathTo;
        FileStream into;
        FileStream to;
        Thread copyThread;
        long num = 0;
        byte[] b = new byte[2048 * 10240];
        char state = 'n';
        

        public string PathTo
        {
            get { return pathTo; }
            set { pathTo = value; }
        }

        public string PathInto
        {
            get { return pathInto; }
            set { pathInto = value; }
        }

        public void StartCopy() 
        {
            copyThread = new Thread(new ThreadStart(CopyFile));
            copyThread.Start();
        }


        public void CopyFile() 
        {
            state = 'c';
            
            while (num < into.Length)
            {
                num += into.Read(b, 0, b.Length);
                to.Write(b, 0, b.Length);
            }
            to.Close();
            state = 'n';  
            into.Close();
            copyThread.Abort();
            num = 0;
        }

        public void stop()
        {
            if (state != 'n')
            {
                copyThread.Abort();
                into.Dispose();
                while (copyThread.ThreadState != ThreadState.Aborted) ;
                to.Dispose();
                into.Close();
                to.Close();
                File.Delete(to.Name);
                num = 0;
            }
        }
        public void pause() 
        {
            if (state != 'n')
            {
                if (state == 'c')
                {
                    copyThread.Abort();
                    state = 'p';
                }
                else
                    if (state == 'p')
                    {
                        StartCopy();
                        state = 'c';
                    }
            }
        }
        public int Step()
        {
            try
            {
                return (int)(100 * num / into.Length);
            }
            catch 
            {
                return 0;
            }
                
            
        }

        public CopyFiles(string path1, string path2) 
        {
            pathInto = path1;
            pathTo = path2;
            into = new FileStream(pathInto, FileMode.Open);
            string s = pathTo + pathInto.Substring(pathInto.LastIndexOf('\\'));
            to = new FileStream(s, FileMode.Create);
        }
        public CopyFiles(string path1, string path2, bool b)
        {
            pathInto = path1;
            pathTo = path2;
            into = new FileStream(pathInto, FileMode.Open);
            string s = pathTo + pathInto.Substring(pathInto.LastIndexOf('\\'));
            s=s.Insert(s.LastIndexOf('.'), "Copy");
            to = new FileStream(s, FileMode.Create);
        }
        
    }
}
