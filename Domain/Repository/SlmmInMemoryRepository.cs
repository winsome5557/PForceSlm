using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParcelForce.Test.Common.Domain.Common;
using ParcelForce.Test.Common.Domain.Loc;
using ParcelForce.Test.Domain.Lawn.Events;
using ParcelForce.Test.Domain.Machine.Commands;
using ParcelForce.Test.Domain.Repository.Events;

namespace ParcelForce.Test.Domain.Repository
{
    public class SlmmInMemoryRepository: ISlmmRepository
    {
        public SlmmInMemoryRepository()
        {
            MachineLocation = new Location() {X=0,Y=0};
            MachineDirection=Direction.North;
            LawnWidth = 0;
            LawnHeight = 0;
            MachineRunning = false;
            CommandHistory = new List<ILawnMowerMachineCommand>();
        }
        public event EventHandler<RepositoryEventArgs> StatusChanged;

        public Direction MachineDirection { get; set; }
        public ILocation MachineLocation { get; set; }
        public int LawnWidth { get; set; }
        public int LawnHeight { get; set; }
        public bool MachineRunning { get; set; }

        public IList<ILawnMowerMachineCommand> CommandHistory { get; set; }



        public void Update(SlmmDto dto)
        {
            MachineDirection = dto.MowerDirection;
            MachineLocation = new Location() {X = dto.X, Y = dto.Y};
            LawnWidth = dto.MaxX;
            LawnHeight = dto.MaxX;
            MachineRunning = dto.MowerRunning;
            if (StatusChanged != null)
            {
                
                StatusChanged(this,new RepositoryEventArgs(dto,dto.CommandLog));
            }
        }

        public void AddCommandToHistory(ILawnMowerMachineCommand command)
        {
            CommandHistory.Add(command);
        }


    }
}
