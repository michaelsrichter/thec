using Microsoft.VisualStudio.TestTools.UnitTesting;
using Richter.THEC.Data;
using System;
using System.IO;
using System.Linq;

namespace Richter.THEC.Tests
{
    [TestClass]
    public class DataTransformsTests
    {
        [TestMethod]
        public void GetDurationMinutesReturnCorrectResult()
        {
            double minutes = 5;
            var duration = DataTransforms.GetDurationMinutes(DateTime.Now, DateTime.Now.AddMinutes(minutes));

            Assert.AreEqual(minutes, duration, 0.0001);
        }

        [TestMethod]
        public void GetHourReturnsCorrectResult()
        {
            var hour = 23;
            var datetime = new DateTime(2021, 12, 1, hour, 1, 1);
            var hourofday = DataTransforms.GetHourOfDay(datetime);

            Assert.AreEqual(hour, hourofday);
        }

        [TestMethod]
        public void GeDayOfWeekReturnsCorrectResult()
        {
            //Friday = 5th day of the week
            var datetime = new DateTime(2021, 12, 3, 1, 1, 1);
            var dayOfWeek = DataTransforms.GetDayOfWeek(datetime);

            Assert.AreEqual(5, dayOfWeek);
        }

        [TestMethod]
        public void GetTripsParsesYellowFileCorrectly()
        {
            //Friday = 5th day of the week
            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var path = Path.Combine(directory, "yellow.csv");
            var trips = DataTransforms.GetTrips<YellowMap>(path).ToList();
            Assert.AreEqual("Yellow", trips[0].Provider);
            Assert.AreEqual(264, trips[0].StartLocation);
            Assert.AreEqual(3.8, trips[1].Total, 0.001);
            Assert.AreEqual(new DateTime(2021,3,1,0,25,17), trips[2].StartTime);
        }

        [TestMethod]
        public void GetTripsParsesGreenFileCorrectly()
        {
            //Friday = 5th day of the week
            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var path = Path.Combine(directory, "green.csv");
            var trips = DataTransforms.GetTrips<GreenMap>(path).ToList();
            Assert.AreEqual("Green", trips[0].Provider);
            Assert.AreEqual(83, trips[0].StartLocation);
            Assert.AreEqual(7.3, trips[1].Total, 0.001);
            Assert.AreEqual(new DateTime(2021, 3, 1, 0, 2, 6), trips[2].StartTime);
        }

        [TestMethod]
        public void GetBoroughsReturnsCorrectDictionary()
        {
            var boroughs = DataTransforms.GetBoroughs();
            Assert.AreEqual("Queens", boroughs[2]);
        }

        [TestMethod]
        public void GetTrainingDistanceTripFromTripReturnsExpectedResult()
        {
            var trip = new Trip() 
            { 
                Distance = 11.11, 
                StartLocation = 12, 
                EndLocation = 14, 
                Provider = "Yellow", 
                EndTime = new DateTime(2021, 12, 3, 14, 2, 6), 
                StartTime = new DateTime(2021, 12, 3, 14, 42, 6), 
                Total = 25.7 };
            var trainingdistancetrip = DataTransforms.GetTrainingDistanceTripFromTrip(trip);

            Assert.AreEqual(40, trainingdistancetrip.Duration, 0.001);
            Assert.AreEqual(14, trainingdistancetrip.HourOfDay);
            Assert.AreEqual(5, trainingdistancetrip.DayOfWeek);
        }

        [TestMethod]
        public void CreateTrainingDistanceTripFileCreatesFileAsExpected()
        {
            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var greenpath = Path.Combine(directory, "green.csv");
            var yellowpath = Path.Combine(directory, "yellow.csv");
            var trainingpath = Path.Combine(directory, "training.csv");

            File.Delete(trainingpath);

            DataTransforms.CreateTrainingDistanceTripFile(yellowpath, greenpath, trainingpath);

            Assert.IsTrue(File.Exists(trainingpath));
        }

    }
}