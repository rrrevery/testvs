using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace TestLog4Net
{
    public enum LogLevel
    {
        DEBUG, INFO, WARN, ERROR, FATAL
    }

    public class Log4Net
    {
        public static void WriteLog(LogLevel loglv, string msg, Exception ex = null)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("bflog");
            switch (loglv)
            {
                case LogLevel.DEBUG:
                    log.Debug(msg, ex);
                    break;
                case LogLevel.INFO:
                    log.Info(msg, ex);
                    break;
                case LogLevel.WARN:
                    log.Warn(msg, ex);
                    break;
                case LogLevel.ERROR:
                    log.Error(msg, ex);
                    break;
                case LogLevel.FATAL:
                    log.Fatal(msg, ex);
                    break;
            }
        }
        public static void D(string msg, Exception ex = null)
        {
            WriteLog(LogLevel.DEBUG, msg, ex);
        }
        public static void I(string msg, Exception ex = null)
        {
            WriteLog(LogLevel.INFO, msg, ex);
        }
        public static void W(string msg, Exception ex = null)
        {
            WriteLog(LogLevel.WARN, msg, ex);
        }
        public static void E(string msg, Exception ex = null)
        {
            WriteLog(LogLevel.ERROR, msg, ex);
        }
        public static void F(string msg, Exception ex = null)
        {
            WriteLog(LogLevel.FATAL, msg, ex);
        }

    }
}
