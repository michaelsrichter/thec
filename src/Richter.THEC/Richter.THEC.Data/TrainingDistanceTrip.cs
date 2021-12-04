namespace Richter.THEC.Data
{
    public class TrainingDistanceTrip
    {
        public string Provider { get; set; }
        public double Distance { get; set; }
        public double Duration { get; set; }
        public double Total { get; set; }
        public string DayAndHour { get; internal set; }
    }
}
