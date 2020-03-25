using LadeskabSWT;
using NUnit.Framework;

namespace TestProject
{
    [TestFixture]
    class TestDoorSimulator
    {
        private DoorSimulator _uut;
        private DoorStateChangeEventArgs _myEventArgs;
        
        

        #region Setup
        [SetUp]
        public void Setup()
        {
            _uut = new DoorSimulator();

            _uut.DoorStateChange += (o, args) =>
            {
                _myEventArgs = args;
            };
        }
        #endregion
        
        #region testDoorLock
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

        [Test]
        public void TestDoorStateEventTrue()
        {
            _myEventArgs.Opened = true;
            
            Assert.That(_myEventArgs.Opened, Is.True);

        }

        [Test]
        public void TestDoorStateEventFalse()
        {
            _myEventArgs.Opened = false;

            Assert.That(_myEventArgs.Opened, Is.False);
        }
        #endregion

        #region Events
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
        #endregion

    }
}
