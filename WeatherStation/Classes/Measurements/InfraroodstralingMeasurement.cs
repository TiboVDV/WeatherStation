using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStation
{
    public class InfraroodstralingMeasurement : Measurement
    {
        public InfraroodstralingMeasurement(string unit, double value) : base(MeasurementTypes.infraroodstraling, unit, value)
        {

        }

        public override string ToString()
        {
            return "Infraroodstraling: " + this.Value + this.Unit;
        }
    }
}
