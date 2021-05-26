using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class AuthenticationHeaderHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                               CancellationToken cancellationToken)
        {
            IEnumerable<string> apiKeyHeaderValues = null;

            if (request.Headers.TryGetValues("x-apikey", out apiKeyHeaderValues))
            {
                var apiKeyHeaderValue = apiKeyHeaderValues.FirstOrDefault();

                if (apiKeyHeaderValue == "12345")
                {
                    // Allow the traffic.  Call SendAsync on the base class.
                    return await base.SendAsync(request, cancellationToken);
                }
            } 

            // Disallow
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            var tcs = new TaskCompletionSource<HttpResponseMessage>();

            tcs.SetResult(response);
            return await tcs.Task;

        }
    }
}
