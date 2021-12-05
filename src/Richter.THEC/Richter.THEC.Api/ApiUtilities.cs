using Richter.THEC.Api.Models;
using Richter.THEC.Data;
using Richter_THEC_Model;

namespace Richter.THEC.Api
{
    public static class ApiUtilities
    {
        public static TotalByDistance.ModelInput ConvertTripInputToTotalByDistance(TripInput tripInput)
        {
            var input = new TotalByDistance.ModelInput();
            input.Provider = tripInput.Provider;
            input.DayAndHour = DataTransforms.GetDayAndHour(tripInput.When);
            input.Distance = (float)tripInput.Distance;
            return input;
        }

        public static DurationByDistance.ModelInput ConvertTripInputToDurationByDistance(TripInput tripInput)
        {
            var input = new DurationByDistance.ModelInput();
            input.Provider = tripInput.Provider;
            input.DayAndHour = DataTransforms.GetDayAndHour(tripInput.When);
            input.Distance = (float)tripInput.Distance;
            return input;
        }

        public static IEnumerable<TripOption> GetOptions(TripInput tripInput)
        {
            //when the api is called, suggest other times (5 hours before and 5 hours ahead)
            //and see what the other providers offer for the same times. 
            //calculate the best option.
            var options = new List<TripOption>();
            var when = tripInput.When;
            var provider = tripInput.Provider;
            for (int i = -5; i < 6; i++)
            {
                var option = new TripOption();
                option.Provider = tripInput.Provider;
                tripInput.When = when.AddHours(i);
                option.Total = TotalByDistance.Predict(ConvertTripInputToTotalByDistance(tripInput)).Score;
                option.Duration = DurationByDistance.Predict(ConvertTripInputToDurationByDistance(tripInput)).Score;
                option.Requested = i == 0;
                option.When = tripInput.When;
                options.Add(option);
                
                var otherOption = new TripOption();
                tripInput.Provider = tripInput.Provider.ToLower() == "green" ? "Yellow" : "Green";
                otherOption.Provider = tripInput.Provider;
                tripInput.When = when.AddHours(i);
                otherOption.Total = TotalByDistance.Predict(ConvertTripInputToTotalByDistance(tripInput)).Score;
                otherOption.Duration = DurationByDistance.Predict(ConvertTripInputToDurationByDistance(tripInput)).Score;
                otherOption.Requested = false;
                otherOption.When = tripInput.When;
                options.Add(otherOption);
                tripInput.Provider = provider;
            }
            return options.OrderBy(x => x.Total).ToList();
        }
    }
}
