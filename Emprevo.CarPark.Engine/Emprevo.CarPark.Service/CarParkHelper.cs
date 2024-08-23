using Emprevo.CarPark.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Emprevo.CarPark.Service
{
    /// <summary>
    /// A Car Park helper class...
    /// </summary>
    public static class CarParkHelper
    {
        /// <summary>
        /// Validates that the entry time is before the exit time.
        /// </summary>
        /// <param name="entryTime">The time the patron entered the car park.</param>
        /// <param name="exitTime">The time the patron exited the car park.</param>
        /// <returns>Returns true if the entry time is before the exit time, otherwise false.</returns>
        public static bool IsEntryBeforeExit(DateTime entryTime, DateTime exitTime)
        {
            return entryTime < exitTime;
        }

        /// <summary>
        /// Check input datetime is a week day or not
        /// </summary>
        /// <param name="date">Input date</param>
        /// <returns>Returns it is a week day or not.</returns>
        public static bool IsWeekday(DateTime date)
        {
            return date.DayOfWeek >= DayOfWeek.Monday && date.DayOfWeek <= DayOfWeek.Friday;
        }

        /// <summary>
        /// Check inputs are on the same day or not
        /// </summary>
        /// <param name="exitTime">Exit Time</param>
        /// <param name="entryTime">Entry Time</param>
        /// <returns>Returns it is a week day or not.</returns>
        public static int GetDaysDifference(DateTime exitTime, DateTime entryTime)
        {
            return (exitTime.Date - entryTime.Date).Days;
        }
    }
}
