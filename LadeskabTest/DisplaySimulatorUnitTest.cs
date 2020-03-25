using NUnit.Framework;

using LadeskabSWT;
using System.IO;
using System;

namespace TestProject
{
    [TestFixture]
    class DisplaySimulatorUnitTest
    {

        private DisplaySimulator _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new DisplaySimulator();
        }

        [Test]
        public void ValidateConsoleOutput_IsReady()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _uut = new DisplaySimulator();
                _uut.IsReady();
                string expected = string.Format("Tilslut telefon{0}", Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());
            }
        }
        [Test]
        public void ValidateConsoleOutput_PresentRFID()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _uut = new DisplaySimulator();
                _uut.PresentRFID();
                string expected = string.Format("Indlæs RFID{0}", Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());
            }
        }
        [Test]
        public void ValidateConsoleOutput_ChargeError()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _uut = new DisplaySimulator();
                _uut.ChargeError();
                string expected = string.Format("Tilslutningsfejl{0}", Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());
            }
        }
        [Test]
        public void ValidateConsoleOutput_IsCharging()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _uut = new DisplaySimulator();
                _uut.IsCharging();
                string expected = string.Format("Ladeskab optaget{0}", Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());
            }
        }
        [Test]
        public void ValidateConsoleOutput_RFIDError()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _uut = new DisplaySimulator();
                _uut.RFIDError();
                string expected = string.Format("RFID fejl, Prøv igen{0}", Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());
            }
        }
        [Test]
        public void ValidateConsoleOutput_IsCharged()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _uut = new DisplaySimulator();
                _uut.IsCharged();
                string expected = string.Format("Fjern telefon{0}", Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());
            }
        }
    }
}
