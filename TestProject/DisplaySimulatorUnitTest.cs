﻿using System;
using System.Collections.Generic;
using System.Text;


using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UsbSimulator;
using Ladeskab;

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

    }
}
