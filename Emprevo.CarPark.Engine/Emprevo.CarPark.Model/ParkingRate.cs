using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emprevo.CarPark.Model
{
    /// <summary>
    /// Represents a parking rate with relevant info.
    /// </summary>
    public class ParkingRate
    {
        /// <summary>
        /// This could be "Early Bird", "Night Rate", "Weekend Rate", etc.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// parking fee
        /// </summary>
        public required double Price { get; set; }

        /// <summary>
        /// Please <see cref="ParkingRateType"/>
        /// </summary>
        public required ParkingRateType RateType { get; set; }
    }
}
