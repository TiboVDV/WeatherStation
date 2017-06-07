using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStation
{
    public class CO2Measurement : Measurement
    {
        public CO2Measurement(string unit, double value) : base(MeasurementTypes.co2, unit, value)
        {

        }

        public override string ToString()
        {
            return "CO2: " + this.Value + this.Unit;
        }
    }
}
