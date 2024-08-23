using Moq;
using Microsoft.Extensions.Options;
using Emprevo.CarPark.Impl;
using Emprevo.CarPark.Impl.Services;
using Emprevo.CarPark.Model;

namespace Emprevo.CarPark.Test
{
    public class ParkingRateCalculatorTests
    {
        private Mock<IOptions<TimeOptions>> _mockTimeOptions;
        private Mock<IOptions<PriceOptions>> _mockPriceOptions;
        private RateCalculatorService _rateCalculatorService;

        [SetUp]
        public void Setup()
        {
            // Setup TimeOptions
            _mockTimeOptions = new Mock<IOptions<TimeOptions>>();
            _mockTimeOptions.Setup(x => x.Value).Returns(new TimeOptions
            {
                EarlyBirdEntryStartTime = 6.00,
                EarlyBirdEntryEndTime = 9.00,
                EarlyBirdExitStartTime = 15.50,
                EarlyBirdExitEndTime = 23.50,
                NightRateEntryStartTime = 18.00,
                NightRateEntryEndTime = 24.00,
                NightRateExitNextDateLastTime = 6.00,
            });

            // Setup PriceOptions
            _mockPriceOptions = new Mock<IOptions<PriceOptions>>();
            _mockPriceOptions.Setup(x => x.Value).Returns(new PriceOptions
            {
                EarlyBird = 13.00,
                NightRate = 6.50,
                WeekendRate = 10.00,
                StandardRateOneHour = 5.00,
                StandardRateTwoHour = 10.00,
                StandardRateThreeHour = 15.00,
                StandardRateFlatRate = 20.00,
            });

            //setup the RateCalculatorService
            _rateCalculatorService = new RateCalculatorService(_mockTimeOptions.Object, _mockPriceOptions.Object);
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
        [TestCase("2024-08-23 23:30", "2024-08-24 05:30", ParkingRateName.NightRate, 6.50, ParkingRateType.FlatRate)]
        public void CalculateRate_ShouldReturnCorrectRate(string entryTimeStr, string exitTimeStr, string expectedRateName, double expectedPrice, ParkingRateType expectedRateType)
        {
            //prepare
            var entryTime = DateTime.Parse(entryTimeStr);
            var exitTime = DateTime.Parse(exitTimeStr);

            //act
            var rate = _rateCalculatorService.CalculateRate(entryTime, exitTime);

            //assert
            Assert.That(rate.Name, Is.EqualTo(expectedRateName));
            Assert.That(rate.Price, Is.EqualTo(expectedPrice));
            Assert.That(rate.RateType, Is.EqualTo(expectedRateType));
        }

        [TestCase("2024-08-22 06:00", "2024-08-21 06:30")]
        public void CalculateRate_ShouldThrowArgumentException(string entryTimeStr, string exitTimeStr)
        {
            //prepare
            var entryTime = DateTime.Parse(entryTimeStr);
            var exitTime = DateTime.Parse(exitTimeStr);

            //assert
            Assert.Throws<ArgumentException>(() =>
                _rateCalculatorService.CalculateRate(entryTime, exitTime));
        }
    }
}