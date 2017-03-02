using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using ParcelForce.Test.Application.Lawn.Queries.Events;
using ParcelForce.Test.Common.Domain.Common;
using ParcelForce.Test.Common.Domain.Loc;
using ParcelForce.Test.Domain.Lawn;
using ParcelForce.Test.Domain.Lawn.Events;
using ParcelForce.Test.Domain.Repository;

namespace ParcelForce.Test.Application.Lawn.Queries
{
    public class LawnQueyService : ILawnQueyService
    {

        private ISlmmRepository _repository;

        public LawnQueyService(ISlmmRepository repository)
        {
            this._repository = repository;
            repository.StatusChanged += Repository_StatusChanged;
        }

        private void Repository_StatusChanged(object sender, Domain.Repository.Events.RepositoryEventArgs e)
        {
            if(StatusChanged!= null)
                StatusChanged(this,new SlmmEventArg(e.lawnData,e.LastCommandLog));

            LastCommandLog = e.LastCommandLog + $". Current Mower Location ({e.lawnData.X},{e.lawnData.Y}). Current Direction {e.lawnData.MowerDirection.ToString()}";
        }

        public Direction MachineDirection
        {
            get { return _repository.MachineDirection; }
        }

        public ILocation MachineLocation  
        {
                get { return _repository.MachineLocation; }
        }


        public int LawnWidth
        {
            get { return _repository.LawnWidth; }
        }

        public int LawnHeight
        {
            get { return _repository.LawnHeight; }

        }

        public Boolean MowerRunning
        {
            get { return _repository.MachineRunning; }

        }

        public string LastCommandLog { get; private set; }

        public event EventHandler<SlmmEventArg> StatusChanged;
    }
}
