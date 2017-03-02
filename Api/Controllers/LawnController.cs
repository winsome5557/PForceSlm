using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR.Hubs;
using ParcelForce.Test.WebApi.ViewModel;
using Serilog;
using ParcelForce.Test.Application.Lawn.Commands;
using ParcelForce.Test.Application.Lawn.Queries;
using ParcelForce.Test.Common.Domain.Common;
using ParcelForce.Test.Common.Domain.Loc;
using ParcelForce.Test.Domain.Machine.Commands;
using ParcelForce.Test.WebApi.Hubs;

namespace ParcelForce.Test.WebApi.Controllers
{
    public class LawnController : ApiControllerWithHub<SLMMEventHub>
    {

        private ILawnCommandsService _lawnCommands;
        private ILawnQueyService _lawnQuey;
        private string lastLog; 
        public LawnController(ILawnCommandsService commandService, ILawnQueyService queryService)
        {
            _lawnCommands = commandService;
            _lawnQuey = queryService;
            queryService.StatusChanged += QueryService_StatusChanged;

        }

        // LAWN
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetLawn()
        {
            Direction orient= _lawnQuey.MachineDirection;
            ILocation loc =  _lawnQuey.MachineLocation; 
            int height = _lawnQuey.LawnHeight;
            int width = _lawnQuey.LawnWidth;
            LawnSize lawnInfo = new LawnSize() {SizeX = width, SizeY = height, StartX = loc.X, StartY = loc.Y};

            return Ok(lawnInfo);
        }

        [System.Web.Http.HttpPost]
        public async Task PostLawn([FromBody]LawnSize lawnData)
        {
            if (!ModelState.IsValid)
                throw new InvalidOperationException("Invalid lawn data provided.");

            await Task.Run(() => _lawnCommands.SetSize(lawnData.StartX, lawnData.StartX, lawnData.SizeX, lawnData.SizeY)).ConfigureAwait(false);
        }

        [Route("CommandLog")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetCommandLog(int id )
        {
            string cmdLog = _lawnQuey.LastCommandLog;
            string result = string.Empty;
            if (cmdLog != null)
                if (!cmdLog.ToLower().Equals(lastLog))
                {
                    result = cmdLog;
                    lastLog = cmdLog;
                }

            return Ok(result);
        }

        private void QueryService_StatusChanged(object sender, Application.Lawn.Queries.Events.SlmmEventArg e)
        {
            lastLog= e.CommandLog;
            if(e.CommandLog!= null)
                NotifyClients(e.CommandLog);
        }

        internal void NotifyClients(string actionLog)
        {
            var subscribed = Hub.Clients;
            subscribed.All.StateChanged(actionLog);
        }

    }
}
