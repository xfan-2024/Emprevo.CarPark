using Emprevo.CarPark.Model;
using Emprevo.CarPark.Service;

namespace Emprevo.CarPark.Test
{
    public class ParkingRateCalculatorTests
    {
        private IRateCalculatorService calculator;

        [SetUp]
        public void Setup()
        {
            calculator = new RateCalculatorService();
        }

        [TestCase("2024-08-22 06:00", "2024-08-22 06:30", ParkingRateName.StandardRate, 5.00, ParkingRateType.HourlyRate)]
        [TestCase("2024-08-22 07:30", "2024-08-22 17:00", ParkingRateName.EarlyBird, 13.00, ParkingRateType.FlatRate)]
        [TestCase("2024-08-20 19:30", "2024-08-20 20:00", ParkingRateName.NightRate, 6.50, ParkingRateType.FlatRate)]
        [TestCase("2024-08-23 00:30", "2024-08-23 05:30", ParkingRateName.StandardRate, 20.00, ParkingRateType.HourlyRate)]
        [TestCase("2024-08-24 14:00", "2024-08-24 18:00", ParkingRateName.WeekendRate, 10.00, ParkingRateType.FlatRate)]
        [TestCase("2024-08-24 14:00", "2024-08-31 23:00", ParkingRateName.StandardRate, 160.00, ParkingRateType.HourlyRate)]
        [TestCase("2024-08-22 06:30", "2024-08-23 16:00", ParkingRateName.StandardRate, 40.00, ParkingRateType.HourlyRate)]
        [TestCase("2024-08-24 05:00", "2024-08-25 23:00", ParkingRateName.WeekendRate, 10.00, ParkingRateType.FlatRate)]
        [TestCase("2024-08-24 10:00", "2024-08-24 10:30", ParkingRateName.WeekendRate, 10.00, ParkingRateType.FlatRate)]

        public void TestCalculateRate(string entryTimeStr, string exitTimeStr, string expectedRateName, double expectedPrice, ParkingRateType expectedRateType)
        {
            //prepare
            var entryTime = DateTime.Parse(entryTimeStr);
            var exitTime = DateTime.Parse(exitTimeStr);

            //act
            var rate = calculator.CalculateRate(entryTime, exitTime);

            //assert
            Assert.AreEqual(expectedRateName, rate.Name);
            Assert.AreEqual(expectedPrice, rate.Price);
            Assert.AreEqual(expectedRateType, rate.RateType);
        }
    }
}