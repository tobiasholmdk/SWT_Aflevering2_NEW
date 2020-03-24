using LadeskabSWT;
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
        public void setNewDoorstate_Opened_TestEventTriggered()
        {
            _uut.DoorOpen();
            Assert.That(_myEventArgs, Is.Not.Null);
        }
        [Test]
        public void setNewDoorstate_Closed_TestEventTriggered()
        {
            _uut.DoorClosed();
            Assert.That(_myEventArgs, Is.Not.Null);
        }

        [Test]
        public void doNothing_testDoorLock()
        {
            Assert.That(_uut._unlocked, Is.True);
        }
        [Test]
        public void LockDoor_testDoorLock()
        {
            _uut.LockDoor();
            Assert.That(_uut._unlocked, Is.False);

        }
        [Test]
        public void UnlockDoor_testDoorUnLock()
        {
            _uut.UnlockDoor();
            Assert.That(_uut._unlocked, Is.True);
        }
    }
}
