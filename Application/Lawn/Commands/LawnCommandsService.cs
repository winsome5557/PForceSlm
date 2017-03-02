using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParcelForce.Test.Common.Domain.Common;
using ParcelForce.Test.Common.Domain.Loc;
using ParcelForce.Test.Domain.Lawn;
using ParcelForce.Test.Domain.Lawn.Events;
using ParcelForce.Test.Domain.Machine.Commands;
using ParcelForce.Test.Domain.Repository;

namespace ParcelForce.Test.Application.Lawn.Commands
{
    public class LawnCommandsService : ILawnCommandsService
    {

        private ILawn lawnToMow = null;
        private ISlmmRepository _repository;

        public LawnCommandsService(ILawn lawnToMow, ISlmmRepository repository)
        {
            this.lawnToMow = lawnToMow;
            this._repository = repository;
        }

        public void SetSize(int startX, int startY, int width, int height)
        {
                    
            this.lawnToMow.StatusChanged += LawnToMow_StatusChanged;
            this.lawnToMow.MowingMachine.CurrentLocation = new Location(){X =startX, Y=startY};
            this.lawnToMow.MowingMachine.MaxX = width;
            this.lawnToMow.MowingMachine.MaxY = height;
            this.lawnToMow.MowingMachine.direction = Direction.North; // Default
            this.lawnToMow.Height = height;
            this.lawnToMow.Width = width;
            this.lawnToMow.MowingMachine.StartConsumingCommands(); // Start thread
        }

        private void LawnToMow_StatusChanged(object sender, Domain.Lawn.Events.LawnEventArgs e)
        {
            _repository.Update(e.lawnData);
            _repository.CommandHistory.Add(e.LastCommand);
        }

        public void AddCommandForLawnMower(ILawnMowerMachineCommand command)
        {
            this.lawnToMow.MowingMachine.AddCommand(command);
        }

        public void AddCommandsForLawnMower(IList<ILawnMowerMachineCommand> commands)
        {
            foreach (var cmd in commands)
            {
                this.lawnToMow.MowingMachine.AddCommand(cmd);
            }
        }
    }
}
