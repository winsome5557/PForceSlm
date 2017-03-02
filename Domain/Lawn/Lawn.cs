using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParcelForce.Test.Common.Domain.Common;
using ParcelForce.Test.Common.Domain.Loc;
using ParcelForce.Test.Domain.Lawn.Events;
using ParcelForce.Test.Domain.Machine;
using ParcelForce.Test.Domain.Machine.Events;
using ParcelForce.Test.Domain.Repository;

namespace ParcelForce.Test.Domain.Lawn
{

    public class Lawn : ILawn, IEntity,ILawnEvents, IDisposable
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public event EventHandler<LawnEventArgs> StatusChanged;

        public ILawnMowerMachine MowingMachine { get; private set; }

        public Lawn(ILawnMowerMachine mower)
        {
            Id = Guid.NewGuid();
            Width = 0;
            Height = 0;
            MowingMachine = mower;
            MowingMachine.StatusChanged += MowingMachine_StatusChanged;
        }

        private void MowingMachine_StatusChanged(object sender, Machine.Events.LawnMowerMachineEventArgs e)
        {
            // Create Dto and update Repository
            SlmmDto dto = new SlmmDto()
            {
                X = e.MachineLocation.X,
                Y = e.MachineLocation.Y,
                CommandLog = e.CommandLog,
                MaxX = e.MaxX,
                MaxY = e.MaxY,
                MowerDirection = e.MachineDirection,
                MowerRunning = e.MachineRunning
            };

            StatusChanged?.Invoke(this, new LawnEventArgs(dto,e.LastCommand));
        }


        public Guid Id { get; set; }

        public void Dispose()
        {
            // Cleanup
            MowingMachine = null;
            
        }

        
    }    
}
