using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emprevo.CarPark.Impl
{
    public sealed class TimeOptions
    {
        public const string SectionName = "TimeOptions";
        public double EarlyBirdEntryStartTime { get; set; }
        public double EarlyBirdEntryEndTime { get; set; }
        public double EarlyBirdExitStartTime { get; set; }
        public double EarlyBirdExitEndTime { get; set; }
        public double NightRateEntryStartTime { get; set; }
        public double NightRateEntryEndTime { get; set; }
        public double NightRateExitNextDateLastTime { get; set; }
    }
}
