using System;

namespace ParcelForce.Test.Domain.Lawn.Events
{
    public interface ILawnEvents
    {
        event EventHandler<LawnEventArgs> StatusChanged;
    }
}