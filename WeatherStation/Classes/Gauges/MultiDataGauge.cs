using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WeatherStation
{
    public abstract class MultiDataGauge : Gauge
    {
        public MultiDataGauge(string name, string unit, SolidColorBrush foreGround, SolidColorBrush backGround, SolidColorBrush gridColor, SolidColorBrush fontColor, int fontSize, FontFamily fontFamily) : base(name, unit, foreGround, backGround, gridColor, fontColor, fontSize, fontFamily)
        {

        }
    }
}
