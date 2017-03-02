using System;
using ParcelForce.Test.Domain.Lawn.Events;
using ParcelForce.Test.Domain.Machine;
using ParcelForce.Test.Domain.Machine.Commands;

namespace ParcelForce.Test.Application.Lawn.Queries.Events
{
    public class SlmmEventArg : EventArgs
    {
        public SlmmEventArg(SlmmDto data, string cmdLog)
        {
            lawnData = data;
            CommandLog = cmdLog;
        }

        public SlmmDto lawnData { get;  set; }

        public string CommandLog { get; set; }
    }

}