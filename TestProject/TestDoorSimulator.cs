using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab;
using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab;
using NUnit.Framework;

namespace TestProject
{
    class TestDoorSimulator
    {
        private DoorSimulator _uut;
        private DoorStateChangeEventArgs _myEventArgs;

        [SetUp]
        public void Setup()
        {
            _myEventArgs = null;
            _uut = new DoorSimulator();

            _uut.DoorStateChange += (o, args) =>
            {
                _myEventArgs = args;
            };
        }

        [Test]
        public void doNothing_TestNoEventsTriggered()
        {
            Assert.That(_myEventArgs, Is.Null);
        }

        [Test]
        public void setNewDoorstate_TestEventTriggered()
        {
            _uut.SetDoorState(true);
            Assert.That(_myEventArgs, Is.Not.Null);
        }
    }
}
