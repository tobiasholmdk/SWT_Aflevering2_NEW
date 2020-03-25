using System.Runtime.CompilerServices;
using LadeskabSWT;
using NUnit.Framework;
using NSubstitute;

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

            [TestCase(false)]
            public void DoorChangeHandler_DoorIsOpen_ClosedDoorEventRecieved(bool doorOpenedTest)
            {
                _uut._state = StationControl.LadeskabState.DoorOpen;
                _door.DoorStateChange += Raise.EventWith(new DoorStateChangeEventArgs() {Opened = doorOpenedTest});

                _display.Received().IsCharging();
                Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Available));
            }

            [TestCase(true)]
            public void DoorChangeHandler_DoorIsOpen_OpenedDoorEventRecieved(bool doorOpenedTest)
            {
                _uut._state = StationControl.LadeskabState.DoorOpen;
                _door.DoorStateChange += Raise.EventWith(new DoorStateChangeEventArgs() { Opened = doorOpenedTest });

                _display.Received().IsReady();
            }

            [TestCase(true)]
            public void DoorChangeHandler_StationIsAvailable_OpenedDoorEventRecieved(bool doorOpenedTest)
            {
                _door.DoorStateChange += Raise.EventWith(new DoorStateChangeEventArgs() { Opened = doorOpenedTest });
                _display.Received().IsReady();
                _uut._state = StationControl.LadeskabState.DoorOpen;
            }

            [TestCase(false)]
            public void DoorChangeHandler_StationIsAvailable_ClosedDoorEventRecieved(bool doorOpenedTest)
            {
                _door.DoorStateChange += Raise.EventWith(new DoorStateChangeEventArgs() { Opened = doorOpenedTest });
                _display.Received().PresentRFID();
            }





            //[TestCase(null)]
            //public void DoorChangeHandler_DoorIsOpen_InvalidDoorEvent(bool doorOpenedTest)
            //{
            //    _uut._state = StationControl.LadeskabState.DoorOpen;
            //    _door.DoorStateChange += Raise.EventWith(new DoorStateChangeEventArgs() { Opened = doorOpenedTest });

            //    _display.Received().IsReady();
            //}











            #endregion

            #region RfidDetectedEvent

            [TestCase(123)]
            
            public void RFID_AvaliableTest(int testID)
            {
                _usbCharger.Connected.Returns(true);
                _rfidReader.RFIDEvent += Raise.EventWith(new RfidEventArgs() {ID = testID});
                _door.Received().LockDoor();
                _usbCharger.Received().StartCharge();
                _display.Received().IsCharging();
                _display.DidNotReceive().ChargeError();
                Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Locked));
            }
            
            [TestCase(123)]
            public void RFID_AvaliableElseTest(int testID)
            {
                _usbCharger.Connected.Returns(false);
                _rfidReader.RFIDEvent += Raise.EventWith(new RfidEventArgs() {ID = testID});
                _display.Received().ChargeError();
            }

            [TestCase(123)]
            public void RFID_LockedTestCorrectID(int testID)
            {
                _usbCharger.Connected.Returns(true);
                _rfidReader.RFIDEvent += Raise.EventWith(new RfidEventArgs() {ID = testID});
                _rfidReader.RFIDEvent += Raise.EventWith(new RfidEventArgs() {ID = testID});
                _door.Received().UnlockDoor();
                _usbCharger.Received().StopCharge();
                _display.Received().IsCharged();
                Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Available));
                Assert.That(_uut._oldId, Is.EqualTo(testID));
            }
            
            [TestCase(123,124)]
            public void RFID_LockedTestCorrectID(int testID, int wrongID)
            {
                _usbCharger.Connected.Returns(true);
                _rfidReader.RFIDEvent += Raise.EventWith(new RfidEventArgs() {ID = testID});
                _rfidReader.RFIDEvent += Raise.EventWith(new RfidEventArgs() {ID = wrongID});
                _display.Received().RFIDError();
            }

            [TestCase(123)]
            public void RFID_DoorOpenStateTest(int testID)
            {
                _uut._state = StationControl.LadeskabState.DoorOpen;
                _rfidReader.RFIDEvent += Raise.EventWith(new RfidEventArgs() {ID = testID});
                _display.IsReady();
            }
            #endregion

        }
    }
}