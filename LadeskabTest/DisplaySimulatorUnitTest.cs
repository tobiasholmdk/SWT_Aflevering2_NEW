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
        public void ValidateConsoleOutput()
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


    }
}
