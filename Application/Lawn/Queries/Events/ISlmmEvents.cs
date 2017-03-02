using System;
using ParcelForce.Test.Domain.Lawn.Events;

namespace ParcelForce.Test.Application.Lawn.Queries.Events
{ 
    public interface ISlmmEvent
{
        event EventHandler<SlmmEventArg> StatusChanged;
    }
}