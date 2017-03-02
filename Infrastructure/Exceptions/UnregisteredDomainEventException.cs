using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ParcelForce.Test.Infrastructure.Exceptions
{
    public class UnregisteredDomainEventException : Exception
    {
        public UnregisteredDomainEventException(string message) : base(message) { }
    }
}
