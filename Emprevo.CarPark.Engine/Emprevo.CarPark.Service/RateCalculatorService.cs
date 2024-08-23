using Emprevo.CarPark.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Emprevo.CarPark.Service
{
    public class RateCalculatorService : IRateCalculatorService
    {
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
                return new ParkingRate { Name = ParkingRateName.EarlyBird, Price = 13.00, RateType = ParkingRateType.FlatRate };
            }

            //check it meets NightRate Condition
            if (IsNightRate(entryTime, exitTime))
            {
                return new ParkingRate { Name = ParkingRateName.NightRate, Price = 6.50, RateType = ParkingRateType.FlatRate };
            }

            //check it meets WeekEnd Condition
            if (IsWeekendRate(entryTime, exitTime))
            {
                return new ParkingRate { Name = ParkingRateName.WeekendRate, Price = 10.00, RateType = ParkingRateType.FlatRate };
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
            return entryTime.TimeOfDay >= TimeSpan.FromHours(6) &&
                   entryTime.TimeOfDay <= TimeSpan.FromHours(9) &&
                   exitTime.TimeOfDay >= TimeSpan.FromHours(15.5) &&
                   exitTime.TimeOfDay <= TimeSpan.FromHours(23.5) && 
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
            bool isEntryTimeValid = entryTime.TimeOfDay >= TimeSpan.FromHours(18) &&
                                entryTime.TimeOfDay < TimeSpan.FromHours(24) &&
                                CarParkHelper.IsWeekday(entryTime);

            //Exit date need to be less than next date of entry date 6am
            bool isExitTimeValid = exitTime <= entryTime.Date.AddDays(1).Add(TimeSpan.FromHours(6));

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
                return new ParkingRate { Name = ParkingRateName.StandardRate, Price = 5.00, RateType = ParkingRateType.HourlyRate };
            }
            else if (duration.TotalHours <= 2)
            {
                return new ParkingRate { Name = ParkingRateName.StandardRate, Price = 10.00, RateType = ParkingRateType.HourlyRate };
            }
            else if (duration.TotalHours <= 3)
            {
                return new ParkingRate { Name = ParkingRateName.StandardRate, Price = 15.00, RateType = ParkingRateType.HourlyRate };
            }
            else
            {
                int days = (int)Math.Ceiling(duration.TotalDays);
                return new ParkingRate { Name = ParkingRateName.StandardRate, Price = 20.00 * days, RateType = ParkingRateType.HourlyRate };
            }
        }
        #endregion
    }
}
