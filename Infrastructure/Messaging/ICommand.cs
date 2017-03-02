using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;


namespace ParcelForce.Test.Infrastructure.Messaging
{
    public interface ICommand
    {
        int Id { get; }
    }
}