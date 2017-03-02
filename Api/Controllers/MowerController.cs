using System.Threading.Tasks;
using System.Web.Http;
using ParcelForce.Test.Application.Lawn.Commands;
using ParcelForce.Test.Application.Lawn.Queries;
using ParcelForce.Test.Domain.Machine.Commands;
using ParcelForce.Test.WebApi.ViewModel;
using Serilog;

namespace ParcelForce.Test.WebApi.Controllers
{
    public class MowerController : ApiController
    {
        private ILawnCommandsService _lawnCommands;
        private ILawnQueyService _lawnQuey;

        public MowerController(ILawnCommandsService commandService, ILawnQueyService queryService)
        {
            _lawnCommands = commandService;
            _lawnQuey = queryService;
        }

        // Mower
        [HttpPut]
        public async Task PutMower([FromBody]bool on)
        {
            await Task.Run(() => _lawnCommands.AddCommandForLawnMower(new MachineMowLawnCommand())).ConfigureAwait(false);
        }

        [HttpGet]
        public IHttpActionResult GetMower()
        {
            return Ok(_lawnQuey.MowerRunning);
        }

    }
}