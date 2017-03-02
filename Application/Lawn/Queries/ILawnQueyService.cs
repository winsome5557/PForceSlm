using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParcelForce.Test.Application.Lawn.Queries.Events;
using ParcelForce.Test.Common.Domain.Common;
using ParcelForce.Test.Common.Domain.Loc;
using ParcelForce.Test.Domain.Lawn.Events;

namespace ParcelForce.Test.Application.Lawn.Queries
{

    public interface ILawnQueyService: ISlmmEvent
    {
        Direction MachineDirection { get; }
        ILocation MachineLocation { get; }
        int LawnWidth { get; }
        int LawnHeight { get; }
        Boolean MowerRunning { get; }

        string LastCommandLog { get; }

   }
}
