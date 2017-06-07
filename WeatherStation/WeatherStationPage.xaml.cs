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

        //Used gauges
        private VerticalGauge temperatureGauge;
        private VerticalGauge uvGauge;
        private HorizontalGauge tempGauge;

        public VerticalGauge TemperatureGauge
        {
            get { return temperatureGauge; }
        }

        public VerticalGauge UVGauge
        {
            get { return uvGauge; }
        }

        public HorizontalGauge TempGauge
        {
            get { return tempGauge; }
        }

        public WeatherStationPage()
        {
            InitializeComponent();
            DrawingTimer.Interval = new TimeSpan(0, 0, 0, 0, 1 / 60);
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
            temperatureGauge = new VerticalGauge("Temperatuur", "°C", yellowColorBrush, grayColorBrush, lightGrayColorBrush, grayColorBrush, 16, robotoFontFamily, TemperatureGaugeUsercontrol, 100, 0, 5);

            uvGauge = new VerticalGauge("UV Index", "", yellowColorBrush, grayColorBrush, lightGrayColorBrush, grayColorBrush, 16, robotoFontFamily, UVIndexUsercontrol, 12, 0, 12);

            tempGauge = new HorizontalGauge("Temperatuur", "°C", yellowColorBrush, grayColorBrush, lightGrayColorBrush, grayColorBrush, 16, robotoFontFamily, TemperatureHorizontalGaugeUsercontrol, 70, 0, 14);

            gauges.Add(temperatureGauge);
            gauges.Add(uvGauge);
            gauges.Add(tempGauge);
        }
    }
}
