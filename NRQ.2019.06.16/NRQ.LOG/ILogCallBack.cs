using System;

namespace NRQ.LOG
{

    
    public interface ILogNotity
    {
        void OnNotity(object sender, LogInfo e);
    }
    [Serializable]
    public class LogInfo
    {
        public DateTime DateTime { get;private set; }
        public Object Message { get; private set; }
        public Exception Exception { get; private set; }
        public LogType Type { get; private set; }
        public String CorpName { get; set; }

        public LogInfo()
        {
            DateTime = DateTime.Now;
        }
        public LogInfo(DateTime dateTime, String message) : this(dateTime, message, null)
        {
        }
        public LogInfo( Exception exception) : this(DateTime.Now, null, exception)
        {
        }
        public LogInfo(String message) : this(DateTime.Now, message, null)
        {
        }
        public LogInfo(string message, Exception exception):this(DateTime.Now,message,exception)
        {
        }
        public LogInfo(DateTime dateTime, string message, Exception exception):this(dateTime,message,exception,LogType.Custom)
        {
        }
        public LogInfo(DateTime dateTime, string message, Exception exception, LogType logType)
        {
            this.DateTime = dateTime;
            this.Message = message;
            this.Exception = exception;
            this.Type = logType;
        }
        public override string ToString()
        {
            String tmpString;
            if (this.Exception == null)
            {
                tmpString = String.Format("{0:yyyy-MM-dd HH:mm:ss.fff}\t{1}",
                    this.DateTime,
                    this.Message
                    );
            }
            else
            {
                 tmpString = String.Format("{0:yyyy-MM-dd HH:mm:ss.fff}\t{1}\t{2}",
                    this.DateTime,
                    this.Message,
                    this.Exception.Message);
            }
            return tmpString;

        }
    }
    [Serializable]
    public enum LogType
    {
        Unknown,
        System,
        Custom,
        Error,
    }

}
