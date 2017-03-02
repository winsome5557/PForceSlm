﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace ParcelForce.Test.WebApi.Hubs
{
    public class SLMMEventHub : Hub
    {
        public void Connect()
        {
            Clients.Caller.onConnected(Context.ConnectionId + " has connected."); 
        }
    }
}
