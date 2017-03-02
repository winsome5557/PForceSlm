using System;
using ParcelForce.Test.Domain.Lawn.Events;

namespace ParcelForce.Test.Domain.Repository.Events
{ 
    public interface IRepositoryEvents
{
        event EventHandler<RepositoryEventArgs> StatusChanged;
    }
}