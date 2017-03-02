using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParcelForce.Test.Infrastructure.Exceptions;

namespace ParcelForce.Test.Infrastructure.Messaging
{
    public class CommandBus : ICommandBus
    {
        private readonly ICommandHandlerFactory _commandHandlerFactory;

        public CommandBus(ICommandHandlerFactory commandHandlerFactory)
        {
            _commandHandlerFactory = commandHandlerFactory;
        }

        public async Task Send<T>(T command) where T : Command
        {
            var handler = _commandHandlerFactory.GetHandler<T>();
            if (handler != null)
            {
                await handler.Execute(command).ConfigureAwait(false);
            }
            else
            {
                throw new UnregisteredDomainEventException("no handler registered");
            }
        }
    }
}
