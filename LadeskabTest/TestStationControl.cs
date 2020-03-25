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

            [Test]
            public void DoNothing_TestForAvailableState()
            {
                Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Available));
            }


        }
    }
    
    
}