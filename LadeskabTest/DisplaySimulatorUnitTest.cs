using System;
using System.Collections.Generic;
using System.Text;


using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

using LadeskabSWT;

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

        //[Test]
        //public void test_showAddPhone_whenIsReady()
        //{

        //}
        /*[Test]
        public void Test_IsReady()
        {
            string expectedMessage = "Tilslut telefon";
            var actualMessage = _uut.IsReady();

            Assert.AreEqual(actualMessage, expectedMessage);
        }*/


    }
}
