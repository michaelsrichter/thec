using Microsoft.VisualStudio.TestTools.UnitTesting;
using Richter.THEC.Api.Models;
using System;
using System.Linq;

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
        public void ConvertTripInputToDurationByDistanceTest()
        {
            var tripInput = new TripInput() { Distance = 1, Provider = "Yellow", When = new DateTime(2021, 1, 1, 1, 1, 1) };
            var modelinput = ApiUtilities.ConvertTripInputToTotalByDistance(tripInput);

            Assert.AreEqual("5-1", modelinput.DayAndHour);
        }

        [TestMethod()]
        public void GetOptionsTest()
        {
            var tripInput = new TripInput() { Distance = 1, Provider = "Yellow", When = new DateTime(2021, 1, 1, 1, 1, 1) };
            var options = ApiUtilities.GetOptions(tripInput);

            Assert.AreEqual(22, options.Count());
        }       
    }
}