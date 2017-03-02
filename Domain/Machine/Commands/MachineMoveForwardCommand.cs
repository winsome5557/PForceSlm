using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelForce.Test.Domain.Machine.Commands
{


    public class MachineMoveForwardCommand : ILawnMowerMachineCommand
    {
        public int MoveBy { get; set; }
        public void Execute(ILawnMowerMachine machine)
        {
            machine.MoveForward(MoveBy);
        }
    }
}
