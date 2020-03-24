using NUnit.Framework;


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
