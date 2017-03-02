using System;
using ParcelForce.Test.Common.Domain.Common;
using ParcelForce.Test.Common.Domain.Loc;
using ParcelForce.Test.Domain.Lawn;
using ParcelForce.Test.Domain.Machine.Commands;

namespace ParcelForce.Test.Domain.Machine.Events
{
    public class LawnMowerMachineEventArgs : EventArgs
    {
        public LawnMowerMachineEventArgs(Direction Direction, int width, int height, ILocation location, string cmdLog, Boolean running, ILawnMowerMachineCommand cmd,
            int timeToRun = -1)
        {
            MachineDirection = Direction;
            MachineLocation = location;
            MachineRunning = running;
            CurrentCommandTimeExpectedToFinish = timeToRun;
            CommandLog = cmdLog;
            LastCommand = cmd;
            MaxX = width;
            MaxY = height;
        }

        public int MaxX { get; set; }

        public int MaxY { get; set; }

        public String CommandLog { get; private set; }

        public Direction MachineDirection { get; private set; }

        public ILocation MachineLocation { get; private set; }

        public Boolean MachineRunning { get; private set; }

        public ILawnMowerMachineCommand LastCommand { get; private set; }


        public int CurrentCommandTimeExpectedToFinish { get; private set; }

    }

}