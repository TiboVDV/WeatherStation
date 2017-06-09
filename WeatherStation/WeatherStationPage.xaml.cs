using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WeatherStation
{
    /// <summary>
    /// Interaction logic for WeatherStationPage.xaml
    /// </summary>
    public partial class WeatherStationPage : UserControl
    {

        private DispatcherTimer DrawingTimer = new DispatcherTimer();
        private List<Gauge> gauges = new List<Gauge>();

        //Alle gebruikte kleuren
        private SolidColorBrush whiteColorBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        private SolidColorBrush grayColorBrush = new SolidColorBrush(Color.FromRgb(70, 70, 70));
        private SolidColorBrush lightGrayColorBrush = new SolidColorBrush(Color.FromRgb(200, 200, 200));
        private SolidColorBrush blueColorBrush = new SolidColorBrush(Color.FromRgb(66, 133, 244));
        private SolidColorBrush redColorBrush = new SolidColorBrush(Color.FromRgb(219, 68, 71));
        private SolidColorBrush greenColorBrush = new SolidColorBrush(Color.FromRgb(15, 157, 88));
        private SolidColorBrush yellowColorBrush = new SolidColorBrush(Color.FromRgb(255, 235, 59));

        //Used fonts
        private FontFamily robotoFontFamily = new FontFamily("Roboto");

        //Used gauges and graphs
        private UVIndexGauge uvIndexGauge;
        private VerticalGauge humidityGauge;
        private VerticalGauge co2Gauge;
        private VerticalGauge lightGauge;
        private GraphGauge tempGraph;

        public UVIndexGauge UVIndexGauge
        {
            get { return uvIndexGauge; }
        }

        public VerticalGauge HumidityGauge
        {
            get { return humidityGauge; }
        }

        public VerticalGauge CO2Gauge
        {
            get { return co2Gauge; }
        }

        public VerticalGauge LightGauge
        {
            get { return lightGauge; }
        }

        public GraphGauge TempGraph
        {
            get { return tempGraph; }
        }

        public WeatherStationPage()
        {
            InitializeComponent();
            DrawingTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            DrawingTimer.Tick += DrawingTimer_Tick;

            DrawingTimer.Start();
        }

        private void DrawingTimer_Tick(object sender, EventArgs e)
        {
            foreach(Gauge g in gauges)
            {
                g.Update();
            }
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
            //TemperatureGauge
            //temperatureGauge = new VerticalGauge("Temperatuur", "°C", yellowColorBrush, grayColorBrush, lightGrayColorBrush, grayColorBrush, 16, robotoFontFamily, TemperatureGaugeUsercontrol, 100, 0, 5);

            //uvGauge = new VerticalGauge("UV Index", "", yellowColorBrush, grayColorBrush, lightGrayColorBrush, grayColorBrush, 16, robotoFontFamily, UVIndexUsercontrol, 12, 0, 12);

            //tempGauge = new HorizontalGauge("Temperatuur", "°C", yellowColorBrush, grayColorBrush, lightGrayColorBrush, grayColorBrush, 16, robotoFontFamily, UVIndexGaugeUsercontrol, 70, 0, 14);

            uvIndexGauge = new UVIndexGauge("UV Index", "", lightGrayColorBrush, grayColorBrush, lightGrayColorBrush, grayColorBrush, 16, robotoFontFamily, UVIndexGaugeUsercontrol, 11, 0, 11);
            humidityGauge = new VerticalGauge("Humidity", "%", blueColorBrush, grayColorBrush, lightGrayColorBrush, grayColorBrush, 16, robotoFontFamily, HumidityGaugeUsercontrol, 100, 0, 5);
            co2Gauge = new VerticalGauge("CO2", "ppm", new SolidColorBrush(Color.FromRgb(114, 144, 58)), grayColorBrush, lightGrayColorBrush, grayColorBrush, 16, robotoFontFamily, CO2GaugeUsercontrol, 800, 100, 7);
            lightGauge = new VerticalGauge("Light", "lx", yellowColorBrush, grayColorBrush, lightGrayColorBrush, grayColorBrush, 16, robotoFontFamily, LightGaugeUsercontrol, 10000, 0, 10);

            tempGraph = new GraphGauge("Temperature", "°C", yellowColorBrush, grayColorBrush, lightGrayColorBrush, grayColorBrush, 16, robotoFontFamily, 0, 30, 0, 50, "Timespan (s)", "Temperature (°C)", TemperatureChartUserControl);


            gauges.Add(uvIndexGauge);
            gauges.Add(humidityGauge);
            gauges.Add(co2Gauge);
            gauges.Add(lightGauge);
            gauges.Add(tempGraph);
        }
    }
}
