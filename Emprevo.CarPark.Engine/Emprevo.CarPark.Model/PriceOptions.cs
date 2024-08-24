using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emprevo.CarPark.Impl
{
    public sealed class PriceOptions
    {
        public const string SectionName = "PriceOptions";
        public double EarlyBird { get; set; }
        public double NightRate { get; set; }
        public double WeekendRate { get; set; }
        public double StandardRateOneHour { get; set; }
        public double StandardRateTwoHour { get; set; }
        public double StandardRateThreeHour { get; set; }
        public double StandardRateFlatRate { get; set; }
    }
}
