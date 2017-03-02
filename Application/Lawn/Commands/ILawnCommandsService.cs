using System.Collections.Generic;
using System.Threading.Tasks;
using ParcelForce.Test.Domain.Lawn;
using ParcelForce.Test.Domain.Machine.Commands;

namespace ParcelForce.Test.Application.Lawn.Commands
{
    public interface ILawnCommandsService
    {

        void SetSize(int startX, int startY, int width, int height);

        void AddCommandForLawnMower(ILawnMowerMachineCommand command);
        void AddCommandsForLawnMower(IList<ILawnMowerMachineCommand> commands);

    }
}