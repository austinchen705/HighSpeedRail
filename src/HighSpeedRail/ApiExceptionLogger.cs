using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;
using log4net;

namespace HighSpeedRail
{
    public class ApiExceptionLogger : ExceptionLogger
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ApiExceptionLogger).Name);

        public override void Log(ExceptionLoggerContext context)
        {
            _logger.Error(string.Format("{0} General Error", context.Request.Method), context.Exception);
        }
    }
}