using System;
using ParcelForce.Test.Domain.Machine;
using ParcelForce.Test.Domain.Machine.Commands;

namespace ParcelForce.Test.Domain.Lawn.Events
{
    public class LawnEventArgs : EventArgs
    {
        public LawnEventArgs(SlmmDto data, ILawnMowerMachineCommand cmd)
        {
            lawnData = data;
            LastCommand = cmd;
        }

        public SlmmDto lawnData { get; private set; }

        public ILawnMowerMachineCommand LastCommand { get; set; }
    }

}