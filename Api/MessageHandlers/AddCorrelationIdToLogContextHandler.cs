﻿using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Serilog.Context;

namespace ParcelForce.Test.WebApi.MessageHandlers
{
    public class AddCorrelationIdToLogContextHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using (LogContext.PushProperty("CorrelationId", request.GetCorrelationId()))
            {
                return base.SendAsync(request, cancellationToken);
            }
        }
    }
}
