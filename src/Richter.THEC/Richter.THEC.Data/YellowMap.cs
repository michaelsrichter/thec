using CsvHelper.Configuration;

namespace Richter.THEC.Data
{
    public class YellowMap : ClassMap<Trip>
    {
        public YellowMap()
        {
            Map(m => m.Provider).Constant("Yellow");
            Map(m => m.StartLocation).Name("PULocationID");
            Map(m => m.EndLocation).Name("DOLocationID");
            Map(m => m.Distance).Name("trip_distance");
            Map(m => m.StartTime).Name("tpep_pickup_datetime");
            Map(m => m.EndTime).Name("tpep_dropoff_datetime");
            Map(m => m.Total).Name("total_amount");
        }
    }
}
