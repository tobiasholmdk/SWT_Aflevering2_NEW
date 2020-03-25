using System.Runtime.CompilerServices;
using LadeskabSWT;
using NUnit.Framework;
using NSubstitute;
using System;
using NSubstitute.ReceivedExtensions;

namespace TestProject
{
    [TestFixture]
    public class TestStationControl
    {
        
        public class Tests
        {
            private StationControl _uut;
            private IDisplay _display;
            private IDoor _door;
            private IRFIDReader _rfidReader;
            private IUsbCharger _usbCharger;
          

            [SetUp]
            public void Setup()
            {
                _display = Substitute.For<IDisplay>();
                _door = Substitute.For<IDoor>();
                _rfidReader = Substitute.For<IRFIDReader>();
                _usbCharger = Substitute.For<IUsbCharger>();
                _uut = new StationControl(_display, _door, _rfidReader, _usbCharger);

            }
            

            #region state
            [Test]
            public void DoNothing_TestForAvailableState()
            {
                Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Available));
            }

            [Test]
            public void SetStateDoorOpen_TestForDoorOpenState()
            {
                _uut._state = StationControl.LadeskabState.DoorOpen;
                Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.DoorOpen));
            }
            [Test]
            public void SetStateLocked_TestForLockedState()
            {
                _uut._state = StationControl.LadeskabState.Locked;
                Assert.That(_uut._state, Is.Not.EqualTo(StationControl.LadeskabState.Available));
            }

            [Test]
            public void DoorChange_TestForDoorOpen()
            {
                _uut._state = StationControl.LadeskabState.DoorOpen;
                Assert.AreEqual(_uut._state, StationControl.LadeskabState.DoorOpen);
            }

            [Test]
            public void DoorChange_TestForAvailable()
            {
                _uut._state = StationControl.LadeskabState.Available;
                Assert.AreEqual(_uut._state, StationControl.LadeskabState.Available);
            }

            [Test]
            public void DoorChange_TestForLocked()
            {
                _uut._state = StationControl.LadeskabState.Locked;
                Assert.AreEqual(_uut._state, StationControl.LadeskabState.Locked);
            }
            #endregion

            #region oldId
            
            [Test]
            public void doNothing_testDefaultOldId()
            {
                Assert.That(_uut._oldId, Is.EqualTo(0));
            }

            [Test]
            public void setIdTo9942_testOldIdIsNotEqualToDefault0()
            {
                _uut._oldId = 9942;
                Assert.That(_uut._oldId, Is.Not.EqualTo(0));
            }
            
            [Test]
            public void setIdTo9942_testOldIdIsEqualTo9942()
            {
                _uut._oldId = 9942;
                Assert.That(_uut._oldId, Is.EqualTo(9942));
            }

            #endregion

            #region DoorChangeEvent



            #endregion

            #region RfidDetectedEvent



            #endregion

        }
    }
    
    
}