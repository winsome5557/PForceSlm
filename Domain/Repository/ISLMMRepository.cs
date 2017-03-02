using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParcelForce.Test.Common.Domain.Common;
using ParcelForce.Test.Common.Domain.Loc;
using ParcelForce.Test.Domain.Lawn.Events;
using ParcelForce.Test.Domain.Machine.Commands;
using ParcelForce.Test.Domain.Repository.Events;

namespace ParcelForce.Test.Domain.Repository
{
    public interface ISlmmRepository: IRepositoryEvents
    {

        Direction MachineDirection { get; set; }
        ILocation MachineLocation { get; set; }

        int LawnWidth { get; set; }

        int LawnHeight { get; set; }

        Boolean MachineRunning { get; set; }

        IList<ILawnMowerMachineCommand> CommandHistory { get; set; }

        void Update(SlmmDto dto);

        void AddCommandToHistory(ILawnMowerMachineCommand command);
    }
}
