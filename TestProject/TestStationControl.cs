using Ladeskab;
using NUnit.Framework;

namespace TestProject
{
    [TestFixture]
    public class TestStationControl
    {
        
        public class Tests
        {
            private StationControl _uut;
            [SetUp]
            public void Setup()
            {
                _uut = new StationControl();
            }
            
            [Test]
            public void Test1()
            {
                Assert.Pass();
            }
        }
    }
    
    
}