using LadeskabSWT;
using NUnit.Framework;

namespace TestProject
{
    [TestFixture]
    class RFIDSimulationTest
    {
        private RFIDSimulator _RFID_Test;
        private RfidEventArgs _rfidEvent_Test;
        int TEST_ID = 123;

        [SetUp]
        public void Setup()
        {
            _RFID_Test = new RFIDSimulator();
            _RFID_Test.RFIDEvent +=
                (o, args) => { _rfidEvent_Test = args; };
        }
        
        [Test]

        public void CorrectIDTest()
        {
            _RFID_Test.RfidDetected(123);
            Assert.That(_rfidEvent_Test.ID, Is.EqualTo(TEST_ID));
        }
        
        [Test]

        public void RFIDErrorTest()
        {
            _RFID_Test.RfidDetected(456);
            Assert.That(_rfidEvent_Test.ID, Is.Not.EqualTo(TEST_ID));
        }
    }
}