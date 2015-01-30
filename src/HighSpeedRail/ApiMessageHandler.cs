using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using log4net;

namespace HighSpeedRail
{
    public class ApiMessageHandler : DelegatingHandler
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ApiMessageHandler));

        protected async override Task<HttpResponseMessage> SendAsync(
    HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Method == HttpMethod.Get)
                _logger.InfoFormat("[SendAsync] , Request Message : {0}", request.RequestUri.AbsoluteUri);
            else
            {
                var requestContent = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
                _logger.InfoFormat("[SendAsync] Method:{0} Uri:{1}, Content-Type : {2}, Request Message : {3}", request.Method, request.RequestUri.AbsoluteUri, request.Content.Headers.ContentType, requestContent);
            }

            var response = await base.SendAsync(request, cancellationToken);
            _logger.InfoFormat("Response: {0}", await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            return response;
        }
    }
}