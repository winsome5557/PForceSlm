using System;
using ParcelForce.Test.Common.Domain.Common;
using ParcelForce.Test.Common.Domain.Loc;
using ParcelForce.Test.Domain.Machine.Commands;
using ParcelForce.Test.Domain.Machine.Events;


namespace ParcelForce.Test.Domain.Machine
{
    public interface ILawnMowerMachine: IEntity, IDisposable, ILawnMowerMachineEvents
    {
        Direction direction { get; set;  }
        ILocation CurrentLocation { get; set; }

        int MaxX { get; set; }

        int MaxY { get; set; }

        void RotateRight();
        void RotateLeft();
        void MoveForward(int moveBy);
        void MowLawn();
        string CurrentPositionAndDirection { get; }

        void AddCommand(ILawnMowerMachineCommand command);

        void StartConsumingCommands();

        ILawnMowerMachineCommand LastCommand { get;}

        string LastCommandLog { get; }
    }
}