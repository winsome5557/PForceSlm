using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelForce.Test.Infrastructure.Messaging
{
    public interface ICommandBus
    {
        Task Send<T>(T command) where T : Command;
    }
}
