using CsvHelper.Configuration;

namespace Richter.THEC.Data
{
    public class GreenMap : ClassMap<Trip>
    {
        public GreenMap()
        {
            Map(m => m.Provider).Constant("Green");
            Map(m => m.StartLocation).Name("PULocationID");
            Map(m => m.EndLocation).Name("DOLocationID");
            Map(m => m.Distance).Name("trip_distance");
            Map(m => m.StartTime).Name("lpep_pickup_datetime");
            Map(m => m.EndTime).Name("lpep_dropoff_datetime");
            Map(m => m.Total).Name("total_amount");
        }
    }
}
