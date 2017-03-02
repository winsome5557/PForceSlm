using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ParcelForce.Test.Common.Domain.Common;
using ParcelForce.Test.Common.Domain.Loc;
using NUnit.Framework;
using ParcelForce.Test.Application.Lawn.Commands;
using ParcelForce.Test.Application.Lawn.Queries;
using ParcelForce.Test.Common.Application.Lawn.Commands;
using ParcelForce.Test.Domain.Lawn;
using ParcelForce.Test.Domain.Machine;
using ParcelForce.Test.Domain.Machine.Commands;
using ParcelForce.Test.Domain.Repository;

namespace ParcelForce.Test.Common.Application.Wall
{
    [TestFixture]
    class WallCommandsTest
    {
        private ISlmmRepository _repository;
        private ILawnCommandsService _lawnCmds;
        private ILawnQueyService _lawnQuery;
        private ILawn _lawn;
        private ILawnMowerMachine _machine;
        private Direction orient;
        private Location loc;

        [SetUp]
        public void Setup()
        {
            _repository = new SlmmInMemoryRepository();
            _machine = new LawnMowerMachine(loc);
            _lawn = new Test.Domain.Lawn.Lawn(_machine);
            _lawnCmds = new LawnCommandsService(_lawn,_repository);
            orient = Direction.North;
            loc = new Location() { X = 0, Y = 0 };
            _lawnQuery = new LawnQueyService(_repository);
        }

        [Test]
        public void Should_Create_NonNull_lawn_With_Valid_Size()
        {
            // Arrange Act
            _lawnCmds.SetSize(0,0,10, 10);

            // Assert
            Assert.IsNotNull(_lawnQuery.MachineLocation);
            Assert.AreEqual(_lawnQuery.LawnWidth,10);
            Assert.AreEqual(_lawnQuery.LawnHeight, 10);
            Assert.AreEqual(_lawnQuery.MachineLocation.X, 0);
            Assert.AreEqual(_lawnQuery.MachineLocation.Y, 0);
        }


        [Test]
        public void Should_Execute_Move_Forward_Command_Y_Axis()
        {
            // Arrange 
            _lawnCmds.SetSize(0, 0, 10, 10);


            // Act
            _lawnCmds.AddCommandForLawnMower(new MachineMoveForwardCommand() {MoveBy = 1});

            Thread.Sleep(20000);

            /// Assert
            Assert.AreEqual(_machine.CurrentLocation.X, 0);
            Assert.AreEqual(_machine.CurrentLocation.Y, 1);
        }

 

    }
}
