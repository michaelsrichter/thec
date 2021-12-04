using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace Richter.THEC.Data
{
    public static class DataTransforms
    {
        public static double GetDurationMinutes(DateTime start, DateTime end)
        {
            return (end - start).TotalMinutes;
        }

        public static int GetHourOfDay(DateTime datetime)
        {
            return datetime.Hour;
        }

        public static int GetDayOfWeek(DateTime datetime)
        {
            return (int)datetime.DayOfWeek;
        }

        public static IEnumerable<Trip> GetTrips<T>(string file) where T : ClassMap<Trip>
        {
            var results = new List<Trip>();
            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<T>();
                results = csv.GetRecords<Trip>().ToList();
            }
            return results;
        }

        public static Dictionary<int, string> GetBoroughs()
        {
            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var path = Path.Combine(directory, "zonelookup.csv");
            var boroughs = new Dictionary<int, string>();
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    boroughs.Add(csv.GetField<int>("LocationID"), csv.GetField<string>("Borough"));
                }
            }
            return boroughs;
        }


        public static TrainingDistanceTrip GetTrainingDistanceTripFromTrip(Trip trip)
        {
            var trainingtrip = new TrainingDistanceTrip();
            trainingtrip.Duration = GetDurationMinutes(trip.EndTime, trip.StartTime);
            trainingtrip.DayOfWeek = GetDayOfWeek(trip.StartTime);
            trainingtrip.Distance = trip.Distance;
            trainingtrip.Total = trip.Total;
            trainingtrip.HourOfDay = GetHourOfDay(trip.StartTime);
            trainingtrip.Provider = trip.Provider;
            return trainingtrip;
        }

        public static void CreateTrainingDistanceTripFile(string yellowPath, string greenPath, string trainingFile)
        {
            var trips = new List<Trip>();
            //TODO: Abstract this to accept any number of File/Map combos
            trips.AddRange(GetTrips<YellowMap>(yellowPath));
            trips.AddRange(GetTrips<GreenMap>(greenPath));

            using var writer = new StreamWriter(trainingFile);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(trips);
        }
    }
}
