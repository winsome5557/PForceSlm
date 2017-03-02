using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelForce.Test.Domain.Machine.Commands
{


    public class MachineRotateLeftCommand : ILawnMowerMachineCommand
    {
        public void Execute(ILawnMowerMachine machine)
        {
            machine.RotateLeft();
        }
    }
}
