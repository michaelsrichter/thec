namespace Richter.THEC.Api.Models
{
    public class TripOption
    {
        public string Provider { get; set; }
        public float Duration { get; set; }
        public float Total { get; set; }
        public DateTime When { get; set; }

        public bool Requested { get; set; }

        public bool LowestTotal { get; set; }
        public bool LowestDuration { get; set; }
    }
}
