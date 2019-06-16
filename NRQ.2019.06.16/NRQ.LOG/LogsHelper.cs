using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace NRQ.LOG
{
    public class LogHelper
    {

        static readonly Hashtable hashtable = new Hashtable();
        public static Logs Initialization()
        {
           return Initialization(null);
        }
        public static Logs Initialization(FileInfo fileInfo)
        {
            Logs logs = hashtable[AppDomain.CurrentDomain.Id] as Logs;
            if (logs == null)
            {
                lock (hashtable.SyncRoot)
                {
                    logs = hashtable[AppDomain.CurrentDomain.Id] as Logs;
                    if (logs == null)
                    {
                        if (fileInfo == null)
                        {
                            logs=new Logs();
                        }
                        else
                        {
                            logs = new Logs(fileInfo);
                        }
                        hashtable[AppDomain.CurrentDomain.Id] = logs;
                        AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
                        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                    }
                }
            }
            return logs;
        }

        private static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            lock (hashtable.SyncRoot)
            {
                var Id = AppDomain.CurrentDomain.Id;
                var logs = hashtable[AppDomain.CurrentDomain.Id] as Logs;
                if (logs!= null)
                {
                    hashtable[AppDomain.CurrentDomain.Id] = null;
                    hashtable.Remove(Id);
                    
                }
            }
            
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            
        }


        public static Logs GetInstance()
        {
            var logs = hashtable[AppDomain.CurrentDomain.Id] as Logs;
            if (logs == null)
            {
                logs = Initialization();
            }
            return logs;

        }
        
    }

    [Serializable]
    internal class NonInitialization : Exception
    {
        public NonInitialization():this("未初始化域")
        {
        }

        public NonInitialization(string message) : base(message)
        {
        }

        public NonInitialization(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NonInitialization(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
