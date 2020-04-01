using System;
using System.IO;
using LadeskabSWT;
using NUnit.Framework;

namespace TestProject
{
    public class LogSimTest
    {
        private LogSimulator _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new LogSimulator();
        }

        [Test]
        public void LogFuncTest1()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _uut.LogLocked(1234);
                string expected = string.Format("Not Implemented{0}", Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());
            }
        }
        [Test]
        public void LogFuncTest2()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _uut.LogUnLocked(1234);
                string expected = string.Format("Not Implemented{0}", Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());
            }
        }
    }
}