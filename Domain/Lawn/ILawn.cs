using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParcelForce.Test.Common.Domain.Common;
using ParcelForce.Test.Common.Domain.Loc;
using ParcelForce.Test.Domain.Lawn.Events;
using ParcelForce.Test.Domain.Machine;


namespace ParcelForce.Test.Domain.Lawn
{
    public interface ILawn : IEntity, ILawnEvents
    {

        int Width { get; set; }

        int Height { get; set; }

        ILawnMowerMachine MowingMachine { get;  }

    }
}
