using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStation
{
    public class LuchtvochtigheidMeasurement : Measurement
    {
        public LuchtvochtigheidMeasurement(string unit, double value) : base(MeasurementTypes.luchtvochtigheid, unit, value)
        {

        }

        public override string ToString()
        {
            return "Luchtvochtigheid: " + this.Value + this.Unit;
        }
    }
}
