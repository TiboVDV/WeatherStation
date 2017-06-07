using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStation
{
    class MinValueBiggerThanMaxValueException : Exception
    {
        protected double minValue;
        protected double maxValue;

        public MinValueBiggerThanMaxValueException(double minValue, double maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public override string Message
        {
            get
            {
                if(maxValue <= minValue)
                {
                    return "MaxValue is groter dan MinValue";
                }
                else
                {
                    return base.Message;
                }
            }
        }
    }
}
