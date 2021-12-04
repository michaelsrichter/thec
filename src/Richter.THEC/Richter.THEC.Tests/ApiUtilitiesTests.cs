using Microsoft.VisualStudio.TestTools.UnitTesting;
using Richter.THEC.Api;
using Richter.THEC.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Richter.THEC.Api.Tests
{
    [TestClass()]
    public class ApiUtilitiesTests
    {
        [TestMethod()]
        public void ConvertTripInputToTotalByDistanceTest()
        {
            var tripInput = new TripInput() { Distance = 1, Provider = "Yellow", When = new DateTime(2021, 1, 1, 1, 1, 1) };
            var modelinput = ApiUtilities.ConvertTripInputToTotalByDistance(tripInput);

            Assert.AreEqual("5-1", modelinput.DayAndHour);
        }

        [TestMethod()]
        public void ConvertTripInputToTotalByDistanceTest1()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ConvertTripInputToDurationByDistanceTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GetOptionsTest()
        {
            throw new NotImplementedException();
        }
    }
}