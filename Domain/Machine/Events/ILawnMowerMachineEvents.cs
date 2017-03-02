using System;

namespace ParcelForce.Test.Domain.Machine.Events
{
    public interface ILawnMowerMachineEvents
    {
        event EventHandler<LawnMowerMachineEventArgs> StatusChanged;
        
    }
}