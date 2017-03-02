using System.Threading.Tasks;
using System.Web.Http;
using ParcelForce.Test.Application.Lawn.Commands;
using ParcelForce.Test.Application.Lawn.Queries;
using ParcelForce.Test.Common.Domain.Common;
using ParcelForce.Test.Common.Domain.Loc;
using ParcelForce.Test.Domain.Machine.Commands;
using ParcelForce.Test.WebApi.ViewModel;
using Serilog;

namespace ParcelForce.Test.WebApi.Controllers
{
    public class LocationController : ApiController
    {

        private ILawnCommandsService _lawnCommands;
        private ILawnQueyService _lawnQuey;

        public LocationController(ILawnCommandsService commandService, ILawnQueyService queryService)
        {
            _lawnCommands = commandService;
            _lawnQuey = queryService;
        }

        // Location
        [HttpPut]
        public async Task PutLocation([FromBody]int moveBy)
        {
            await Task.Run(() => _lawnCommands.AddCommandForLawnMower(new MachineMoveForwardCommand() { MoveBy = moveBy })).ConfigureAwait(false);
        }

        [HttpGet]
        public IHttpActionResult GetLocation()
        {
            ILocation loc = _lawnQuey.MachineLocation;
            MachineLocation machineLoc = new MachineLocation() {X = loc.X, Y=loc.Y };

            return Ok(machineLoc);
        }


    }
}
