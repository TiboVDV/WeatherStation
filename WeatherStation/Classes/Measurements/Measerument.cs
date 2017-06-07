using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WeatherStation
{
    public abstract class Measurement
    {
        public enum MeasurementTypes
        {
            luchtvochtigheid,
            luchtdruk,
            uvindex,
            co2,
            infraroodstraling,
            temperatuur,
            licht
        }

        protected MeasurementTypes type;
        protected string unit;
        protected double value;
        protected DateTime dateTimeOfMeasurement;

        public MeasurementTypes Type
        {
            get { return this.type; }
        }

        public string Unit
        {
            get { return this.unit; }
        }

        public double Value
        {
            get { return this.value; }
        }

        public DateTime DateTimeOfMeasurement
        {
            get { return this.dateTimeOfMeasurement; }
        }

        public Measurement(MeasurementTypes type, string unit, double value)
        {
            this.type = type;
            this.unit = unit;
            this.value = value;
            this.dateTimeOfMeasurement = DateTime.Now;
        }
    }
}
