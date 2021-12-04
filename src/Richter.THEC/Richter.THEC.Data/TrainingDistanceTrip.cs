namespace Richter.THEC.Data
{
    public class TrainingDistanceTrip
    {
        public string Provider { get; set; }
        public double Distance { get; set; }
        public double Duration { get; set; }
        public int HourOfDay { get; set; }
        public int DayOfWeek { get; set; }
        public double Total { get; set; }
    }
}
