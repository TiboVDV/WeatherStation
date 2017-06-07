using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStation
{
    public class TemperatuurMeasurement : Measurement
    {
        public TemperatuurMeasurement(string unit, double value) : base(MeasurementTypes.temperatuur, unit, value)
        {

        }

        public override string ToString()
        {
            return "Temperatuur: " + this.Value + this.Unit;
        }
    }
}
