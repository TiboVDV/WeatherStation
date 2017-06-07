using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStation
{
    public class LuchtdrukMeasurement : Measurement
    {
        public LuchtdrukMeasurement(string unit, double value) : base(MeasurementTypes.luchtdruk, unit, value)
        {

        }

        public override string ToString()
        {
            return "Luchtdruk: " + this.Value + this.Unit;
        }
    }
}
