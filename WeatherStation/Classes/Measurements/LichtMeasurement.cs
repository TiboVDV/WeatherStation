using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStation
{
    public class LichtMeasurement : Measurement
    {
        public LichtMeasurement(string unit, double value) : base(MeasurementTypes.licht, unit, value)
        {

        }

        public override string ToString()
        {
            return "Licht: " + this.Value + this.Unit;
        }
    }
}
