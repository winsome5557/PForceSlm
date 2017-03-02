using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ParcelForce.Test.Domain.Machine.Commands
{
    public  interface ILawnMowerMachineCommand
    {
        void Execute(ILawnMowerMachine machine);
    }
}
