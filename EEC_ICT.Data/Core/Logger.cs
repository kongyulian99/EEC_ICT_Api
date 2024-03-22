using System;

namespace EEC_ICT.Data.Core
{
    public static class Logger
    {
        private static readonly ILogService logService = new Log4NetService();

        public static void Error(Exception ex)
        {
            logService.Error(ex);
        }

        public static void Info(string message)
        {
            logService.Information(message);
        }

        public static void InfoFormat(string message, string[] dataformat)
        {
            var infolog = string.Format(message, dataformat);
            logService.Information(infolog);
        }

        public static void Debug(string message)
        {
            logService.Debug(message);
        }
    }
}