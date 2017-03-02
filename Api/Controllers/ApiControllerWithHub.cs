using System;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Messaging;
using ParcelForce.Test.Application.Lawn.Commands;
using ParcelForce.Test.Application.Lawn.Queries;
using ParcelForce.Test.WebApi.Hubs;

namespace ParcelForce.Test.WebApi.Controllers
{

    public abstract class ApiControllerWithHub<THub> : System.Web.Http.ApiController where THub : IHub
    {
        System.Lazy<IHubContext> hub = new Lazy<IHubContext>(
            () => GlobalHost.ConnectionManager.GetHubContext<THub>()
            );

        protected IHubContext Hub
        {
            get { return hub.Value; }
        }
    }
}