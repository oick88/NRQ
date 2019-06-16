using NRQ.LOG;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NRQ
{
    [Serializable]
    public class Logs : ILogNotity
    {
        public delegate void EventLogHandler(object sender, LogInfo e);
         readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");

         readonly log4net.ILog SysInfo = log4net.LogManager.GetLogger("SysInfo");

         readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");


        public event EventLogHandler LogNotify;

        public void SetConfig()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
        public Logs()
        {
            SetConfig();
        }
        public Logs(FileInfo configFile)
        {
            SetConfig(configFile);
        }
        public void SetConfig(FileInfo configFile)
        {
            log4net.Config.XmlConfigurator.Configure(configFile);
        }

        public void Write(LogInfo e)
        {
            switch (e.Type)
            {
                case LogType.System:
                    if (SysInfo.IsInfoEnabled)
                    {
                        SysInfo.Info(e.ToString());
                    }
                    break;
                case LogType.Unknown:
                case LogType.Custom:
                    if (e.Exception == null )
                    {
                        if (loginfo.IsInfoEnabled)
                        {
                            loginfo.Info(e.ToString());
                        }
                      
                    }
                    else if(loginfo.IsErrorEnabled)
                    {
                        loginfo.Error(e.ToString());
  
                    }
                    break;
                case LogType.Error:

                        if (logerror.IsErrorEnabled)
                        {

                            logerror.Error(e.Exception);

                        }
                    break;
                default:
                    break;
            }
            this.OnNotity(this, e);
        }

        public void OnNotity(object sender, LogInfo e)
        {
            LogNotify?.BeginInvoke(sender, e,null,null);
        }
        public void WriteLog(string mssage,Exception exception, LogType type)
        {
            Write(new LogInfo(DateTime.Now, mssage, exception, type));
            if (exception != null && exception.InnerException!=null)
            {
                Write(new LogInfo(DateTime.Now, mssage, exception.InnerException, type));
            }
        }

        public void WriteSystemLog(string mssage)
        {
            WriteSystemLog(mssage, null);
        }
        public void WriteSystemLog( Exception exception)
        {
            WriteSystemLog(null, exception);
        }
        public void WriteSystemLog(string mssage,Exception exception)
        {
            WriteLog(mssage, exception, LogType.System);
        }
        public void WriteLog(string mssage)
        {
            WriteLog(mssage, null);
        }
        public void WriteLog(Exception exception)
        {
            WriteLog(null, exception);
        }
        public void WriteLog(string mssage, Exception exception)
        {
            WriteLog(mssage, exception, LogType.Custom);
        }
        public void WriteError(String mssage)
        {
            WriteError(mssage, null);
        }
        public void WriteError(Exception exception)
        {
            WriteError(null, exception);
        }
        public void WriteError(string mssage, Exception exception)
        {

            WriteLog(mssage, exception, LogType.Error);
        }
    }

}
