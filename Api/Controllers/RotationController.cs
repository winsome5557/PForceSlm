using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ParcelForce.Test.Application.Lawn.Commands;
using ParcelForce.Test.Application.Lawn.Queries;
using ParcelForce.Test.Common.Domain.Common;
using ParcelForce.Test.Domain.Machine.Commands;
using ParcelForce.Test.WebApi.Mappers;
using ParcelForce.Test.WebApi.ViewModel;
using Serilog;

namespace ParcelForce.Test.WebApi.Controllers
{
    public class RotationController : ApiController
    {
        private ILawnCommandsService _lawnCommands;
        private ILawnQueyService _lawnQuey;

        public RotationController(ILawnCommandsService commandService, ILawnQueyService queryService)
        {
            _lawnCommands = commandService;
            _lawnQuey = queryService;
        }

        // Rotation
        [HttpPut]
        public async Task PutRotation([FromBody]Rotation direction)
        {
            Direction currentDir = _lawnQuey.MachineDirection;
            IList<ILawnMowerMachineCommand> command = CommandMapper.GetLawnMowerMachineCommand(currentDir, direction);
            await Task.Run(() => _lawnCommands.AddCommandsForLawnMower(command)).ConfigureAwait(false);
        }

        [HttpGet]
        public IHttpActionResult GetRotation()
        {
            return Ok(_lawnQuey.MachineDirection);
        }

    }
}