using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParcelForce.Test.Common.Domain.Common;
using ParcelForce.Test.Common.Domain.Loc;
using NUnit.Framework;
using ParcelForce.Test.Domain.Machine;

namespace ParcelForce.Test.Domain.Lawn
{
    public class WallTest
    {
        private ILawn _lawn;
        private ILawnMowerMachine _machine;
        private Direction orient;
        private Location loc;

        [SetUp]
        public void Setup()
        {
            orient = Direction.North;
            loc = new Location() { X = 0, Y = 0 };
            _machine = new LawnMowerMachine(loc);
            _lawn = new Lawn(_machine);
        }

        [Test]
        public void Should_Have_ID()
        {
            Assert.IsNotNull(_lawn.Id);
        }

        [Test]
        public void Should_Have_Valid_Mowing_Machine()
        {
            // Arrange Act
            Assert.IsNotNull(_lawn.MowingMachine);
        }

        [Test]
        public void Should_Have_Valid_Mowing_Machine_Location()
        {
            // Arrange Act
            Assert.AreEqual(_lawn.MowingMachine.CurrentLocation.X,0);
            Assert.AreEqual(_lawn.MowingMachine.CurrentLocation.Y, 0);
        }

        [Test]
        public void Should_Have_Valid_Mowing_Machine_Direction()
        {
            // Arrange Act
            Assert.AreEqual(_lawn.MowingMachine.direction, Direction.North);
        }

    }
}
