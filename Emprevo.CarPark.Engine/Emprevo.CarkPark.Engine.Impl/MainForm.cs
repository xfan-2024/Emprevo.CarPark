using Emprevo.CarPark.Impl.Services;
using Emprevo.CarPark.Interface;
using Emprevo.CarPark.Model;
using Emprevo.CarPark.Service;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Emprevo.CarkPark.Engine.Impl
{
    public partial class MainForm : Form
    {
        private readonly IRateCalculatorService _rateCalculatorService;
        public MainForm(IRateCalculatorService rateCalculatorService)
        {
            _rateCalculatorService = rateCalculatorService;
            InitializeComponent();

            entryDateTimePicker.Format = DateTimePickerFormat.Custom;
            entryDateTimePicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            entryDateTimePicker.ShowUpDown = true;

            exitDateTimePicker.Format = DateTimePickerFormat.Custom;
            exitDateTimePicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            exitDateTimePicker.ShowUpDown = true;

            rateResultBox.ReadOnly = true;
        }

        /// <summary>
        /// calculate button clicked
        /// </summary>
        private void calculateBtn_Click(object sender, EventArgs e)
        {
            if(!CarParkHelper.IsEntryBeforeExit(entryDateTimePicker.Value, exitDateTimePicker.Value))
            {
                MessageBox.Show("Entry Time should be earlier than Exit Time", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var result = _rateCalculatorService.CalculateRate(entryDateTimePicker.Value, exitDateTimePicker.Value);
            RenderParkingFeeResult(result);
        }

        /// <summary>
        /// Render the calculation Result
        /// </summary>
        private void RenderParkingFeeResult(ParkingRate rate)
        {
            rateResultBox.Text = $"Entry Time: " + entryDateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss") + $"\n" +
                $"Exit Time: " + exitDateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss") + $"\n\n" +
                $"Rate Name: " + rate.Name + $"\n" +
                $"Rate Type: " + rate.RateType.ToString() + $"\n" +
                $"Total Price: " + rate.Price.ToString()
            ;
        }
    }
}
