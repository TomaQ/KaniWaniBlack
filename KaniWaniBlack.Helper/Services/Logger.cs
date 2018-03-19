using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace KaniWaniBlack.Helper.Services
{
    public class Logger
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //TODO: email and logging levels
        public static void HandleException(Exception ex)
        {
            log.ErrorFormat("An error has occurred, Exception: {0}", ex);
        }

        public static void LogError(string error)
        {
            log.ErrorFormat("An error has occurred: " + error);
        }

        public static void LogInfo(string info)
        {
            log.InfoFormat(info);
        }
    }
}