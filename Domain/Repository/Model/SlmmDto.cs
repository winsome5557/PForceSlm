using System;
using System.Collections.Generic;
using ParcelForce.Test.Common.Domain.Common;
using ParcelForce.Test.Domain.Machine;

namespace ParcelForce.Test.Domain.Lawn.Events
{
    public class SlmmDto 
    {
        public SlmmDto()
        {
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int MaxX { get; set; }

        public int MaxY { get; set; }


        public Boolean MowerRunning { get; set; }

       public Direction MowerDirection { get; set; }

       public string CommandLog { get; set; }

    }

}