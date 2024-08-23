using Emprevo.CarPark.Interface;
using Emprevo.CarPark.Model;
using Emprevo.CarPark.Service;
using Microsoft.Extensions.Options;

namespace Emprevo.CarPark.Impl.Services
{
    public class RateCalculatorService : IRateCalculatorService
    {
        private readonly TimeOptions _timeOptions;
        private readonly PriceOptions _priceOptions;
        public RateCalculatorService(IOptions<TimeOptions> timeOptions, IOptions<PriceOptions> priceOptions)
        {
            _timeOptions = timeOptions.Value;
            _priceOptions = priceOptions.Value;
        }

        #region public method
        /// <summary>
        /// Calculates parking rate by given info.
        /// </summary>
        /// <param name="entryTime">The time the patron entered the car park.</param>
        /// <param name="exitTime">The time the patron exited the car park.</param>
        /// <returns>Returns a <see cref="ParkingRate"/> Calculated price,type and name.</returns>
        public ParkingRate CalculateRate(DateTime entryTime, DateTime exitTime)
        {
            //Validate the input
            if (!CarParkHelper.IsEntryBeforeExit(entryTime, exitTime))
            {
                throw new ArgumentException("Entry time must be before exit time.");
            }

            //check it meets EarlyBird Condition
            if (IsEarlyBird(entryTime, exitTime))
            {
                return new ParkingRate { Name = ParkingRateName.EarlyBird, Price = _priceOptions.EarlyBird, RateType = ParkingRateType.FlatRate };
            }

            //check it meets NightRate Condition
            if (IsNightRate(entryTime, exitTime))
            {
                return new ParkingRate { Name = ParkingRateName.NightRate, Price = _priceOptions.NightRate, RateType = ParkingRateType.FlatRate };
            }

            //check it meets WeekEnd Condition
            if (IsWeekendRate(entryTime, exitTime))
            {
                return new ParkingRate { Name = ParkingRateName.WeekendRate, Price = _priceOptions.WeekendRate, RateType = ParkingRateType.FlatRate };
            }

            //StandRate
            return CalculateStandardRate(entryTime, exitTime);
        }
        #endregion

        #region private method
        /// <summary>
        /// Determine whether it's early bird rate or not
        /// </summary>
        /// <param name="entryTime">The time the patron entered the car park.</param>
        /// <param name="exitTime">The time the patron exited the car park.</param>
        /// <returns>Boolean result</returns>
        private bool IsEarlyBird(DateTime entryTime, DateTime exitTime)
        {
            return entryTime.TimeOfDay >= TimeSpan.FromHours(_timeOptions.EarlyBirdEntryStartTime) &&
                   entryTime.TimeOfDay <= TimeSpan.FromHours(_timeOptions.EarlyBirdEntryEndTime) &&
                   exitTime.TimeOfDay >= TimeSpan.FromHours(_timeOptions.EarlyBirdExitStartTime) &&
                   exitTime.TimeOfDay <= TimeSpan.FromHours(_timeOptions.EarlyBirdExitEndTime) &&
                   CarParkHelper.IsWeekday(entryTime) &&
                   CarParkHelper.IsWeekday(exitTime) &&
                   CarParkHelper.GetDaysDifference(exitTime.Date, entryTime.Date) == 0;//make sure they are on the same day
        }

        /// <summary>
        /// Determine whether it's night rate or not
        /// </summary>
        /// <param name="entryTime">The time the patron entered the car park.</param>
        /// <param name="exitTime">The time the patron exited the car park.</param>
        /// <returns>Boolean result</returns>
        private bool IsNightRate(DateTime entryTime, DateTime exitTime)
        {
            //Entry date need to be a week day
            //Entry time need to between 6pm-24
            bool isEntryTimeValid = entryTime.TimeOfDay >= TimeSpan.FromHours(_timeOptions.NightRateEntryStartTime) &&
                                entryTime.TimeOfDay < TimeSpan.FromHours(_timeOptions.NightRateEntryEndTime) &&
                                CarParkHelper.IsWeekday(entryTime);

            //Exit date need to be less than next date of entry date 6am
            bool isExitTimeValid = exitTime <= entryTime.Date.AddDays(1).Add(TimeSpan.FromHours(_timeOptions.NightRateExitNextDateLastTime));

            return isEntryTimeValid && isExitTimeValid;
        }

        /// <summary>
        /// Determine whether it's weekend rate or not
        /// </summary>
        /// <param name="entryTime">The time the patron entered the car park.</param>
        /// <param name="exitTime">The time the patron exited the car park.</param>
        /// <returns>Boolean result</returns>
        private bool IsWeekendRate(DateTime entryTime, DateTime exitTime)
        {
            //Entry datetime on weekend
            //Exit datetime on weekend
            bool isEntryOnWeekend = !CarParkHelper.IsWeekday(entryTime);
            bool isExitOnWeekend = !CarParkHelper.IsWeekday(exitTime);
            bool isOnTheSameWeek = CarParkHelper.GetDaysDifference(exitTime.Date, entryTime.Date) <= 1;//make sure entry and exit on the same week

            return isEntryOnWeekend && isExitOnWeekend && isOnTheSameWeek;
        }

        /// <summary>
        /// Get Standard rate
        /// </summary>
        /// <param name="entryTime">The time the patron entered the car park.</param>
        /// <param name="exitTime">The time the patron exited the car park.</param>
        /// <returns>Please <see cref="ParkingRate"/></returns>
        private ParkingRate CalculateStandardRate(DateTime entryTime, DateTime exitTime)
        {
            var duration = exitTime - entryTime;
            if (duration.TotalHours <= 1)
            {
                return new ParkingRate { Name = ParkingRateName.StandardRate, Price = _priceOptions.StandardRateOneHour, RateType = ParkingRateType.HourlyRate };
            }
            else if (duration.TotalHours <= 2)
            {
                return new ParkingRate { Name = ParkingRateName.StandardRate, Price = _priceOptions.StandardRateTwoHour, RateType = ParkingRateType.HourlyRate };
            }
            else if (duration.TotalHours <= 3)
            {
                return new ParkingRate { Name = ParkingRateName.StandardRate, Price = _priceOptions.StandardRateThreeHour, RateType = ParkingRateType.HourlyRate };
            }
            else
            {
                int days = (int)Math.Ceiling(duration.TotalDays);
                return new ParkingRate { Name = ParkingRateName.StandardRate, Price = _priceOptions.StandardRateFlatRate * days, RateType = ParkingRateType.HourlyRate };
            }
        }
        #endregion
    }
}
