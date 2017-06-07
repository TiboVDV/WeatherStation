using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStation
{
    public class UVindexMeasurement : Measurement
    {
        public UVindexMeasurement(string unit, double value) : base(MeasurementTypes.uvindex, unit, Math.Round(value, 0))
        {
            
        }

        public override string ToString()
        {
            return "UV-index: " + this.Value + this.Unit;
        }
    }
}
