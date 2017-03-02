using System;
using ParcelForce.Test.Domain.Lawn.Events;
using ParcelForce.Test.Domain.Machine;
using ParcelForce.Test.Domain.Machine.Commands;

namespace ParcelForce.Test.Domain.Repository.Events
{
    public class RepositoryEventArgs : EventArgs
    {
        public RepositoryEventArgs(SlmmDto data, string cmd)
        {
            lawnData = data;
            LastCommandLog = cmd;
        }

        public SlmmDto lawnData { get; set; }

        public string LastCommandLog { get; set; }
    }

}