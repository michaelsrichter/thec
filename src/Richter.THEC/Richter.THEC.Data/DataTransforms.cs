using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Transactions;

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

        public static string GetDayAndHour(DateTime datetime)
        {
            return $"{GetDayOfWeek(datetime)}-{GetHourOfDay(datetime)}";
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
            trainingtrip.Duration = GetDurationMinutes(trip.StartTime, trip.EndTime);
            trainingtrip.Distance = trip.Distance;
            trainingtrip.DayAndHour = GetDayAndHour(trip.StartTime);
            trainingtrip.Provider = trip.Provider;
            trainingtrip.Total = trip.Total;
            return trainingtrip;
        }

        public static void CreateTrainingDistanceTripFile(string yellowPath, string greenPath, string trainingFile)
        {
            var trips = new List<Trip>();
            //TODO: Abstract this to accept any number of File/Map combos
            trips.AddRange(GetTrips<YellowMap>(yellowPath));
            trips.AddRange(GetTrips<GreenMap>(greenPath));
            trips = LimitTrainingData(trips, 100).ToList();
            var trainingTrips = trips.Where(t => (t.Distance >= 0 && t.Total >= 0)).Select(GetTrainingDistanceTripFromTrip);
            using var writer = new StreamWriter(trainingFile);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(trainingTrips);
        }

        public static IEnumerable<Trip> LimitTrainingData(IEnumerable<Trip> Trips, int maxPerCategory)
        {
            var LessTrips = new List<Trip>();

            //Create a smaller training
            var categoryCounter = new Dictionary<string, int>();
            foreach (var trip in Trips)
            {
                var category = $"{trip.Provider}-{GetDayAndHour(trip.StartTime)}";
                if (categoryCounter.ContainsKey(category))
                {
                    if (categoryCounter[category] == maxPerCategory)
                    {
                        continue;
                    }
                    LessTrips.Add(trip);
                    categoryCounter[category]++;
                }
                else
                {
                    categoryCounter.Add(category, 1);
                    LessTrips.Add(trip);
                }
            }
            return LessTrips;
        }
    }
}
