using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using log4net;

namespace HighSpeedRail
{
    public class ApiExceptionAttribute : ExceptionFilterAttribute
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ApiExceptionAttribute).Name);

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
        }
    }
}