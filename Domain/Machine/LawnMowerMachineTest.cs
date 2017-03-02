using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParcelForce.Test.Common.Domain.Common;
using ParcelForce.Test.Common.Domain.Loc;
using NUnit.Framework.Internal;
using NUnit.Framework;
using ParcelForce.Test.Domain.Machine;

namespace ParcelForce.Test.Domain.Machine
{
   

    [TestFixture()]
    public class machineTestup
    {
        private ILawnMowerMachine _machine;
        private Direction orient;
        private Location loc;

        [SetUp]
        public void Setup()
        {
            orient = Direction.North;
            loc = new Location() {X=0,Y=0};
            _machine = new LawnMowerMachine(loc);
        }

        [Test]
        public void Should_Have_ID()
        {
            Assert.IsNotNull(_machine.Id);
        }



        [Test]
        public void Should_Have_NonNull_Location_And_Direction()
        {
            Assert.IsNotNull(_machine.CurrentLocation);
            Assert.IsNotNull(_machine.direction);
        }


        [Test]
        public void Should_MoveForward_OneStep()
        {
            // Arrange
            _machine.CurrentLocation = new Location() {X = 0, Y = 0};
            _machine.direction =   Direction.North;
            _machine.MaxX = 10;
            _machine.MaxY = 10;
           // Act
           _machine.MoveForward(1);

            // Assert
            Assert.AreEqual(_machine.CurrentLocation.X,0);
            Assert.AreEqual(_machine.CurrentLocation.Y, 1);
        }


        [Test]
        public void Should_No_MoveOutOfBound()
        {
            // Arrange
            _machine.CurrentLocation = new Location() { X = 0, Y = 0 };
            _machine.direction = Direction.North;
            _machine.MaxX = 1;
            _machine.MaxY = 1;
            // Act
            _machine.MoveForward(10);

            // Assert
            Assert.AreEqual(_machine.CurrentLocation.X, 0);
            Assert.AreEqual(_machine.CurrentLocation.Y, 0);
        }


        [Test]
        public void Should_Rotate_Left_With_No_Location_Change()
        {
            // Arrange
            _machine.CurrentLocation = new Location() { X = 0, Y = 0 };
            _machine.direction = Direction.North;

            // Act
            _machine.RotateLeft();

            // Assert
            Assert.AreEqual(_machine.direction, Direction.West);
            Assert.AreEqual(_machine.CurrentLocation.X, 0);
            Assert.AreEqual(_machine.CurrentLocation.Y, 0);
        }

        [Test]
        public void Should_Rotate_Right_With_No_Location_Change()
        {
            // Arrange
            _machine.CurrentLocation = new Location() { X = 0, Y = 0 };
            _machine.direction = Direction.North;

            // Act
            _machine.RotateRight();

            // Assert
            Assert.AreEqual(_machine.direction, Direction.East);
            Assert.AreEqual(_machine.CurrentLocation.X, 0);
            Assert.AreEqual(_machine.CurrentLocation.Y, 0);
        }

    }
}
