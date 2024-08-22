using Emprevo.CarPark.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Emprevo.CarPark.Service
{
    public interface IRateCalculatorService
    {
        public ParkingRate CalculateRate(DateTime entryTime, DateTime exitTime);
    }
}
