using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelForce.Test.Common.Application.Lawn.Commands
{
    public class LawnCommandOutOfBoundsException : Exception
    {
        public LawnCommandOutOfBoundsException(string message)
        {
            Message = message;
        }

        public override string Message { get; }
    }
}
