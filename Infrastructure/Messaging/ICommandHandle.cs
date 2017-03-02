using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelForce.Test.Infrastructure.Messaging
{
    public interface ICommandHandler<TCommand> where TCommand : Command
    {
        Task Execute(TCommand command);
    }
}
