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
using System.Windows.Forms;
using System.IO;

namespace WeatherStation
{
    /// <summary>
    /// Interaction logic for ExportDataPage.xaml
    /// </summary>
    public partial class ExportDataPage : System.Windows.Controls.UserControl
    {

        //Alle lijsten met de metingen
        private List<TemperatuurMeasurement> temperatuurMeasurements = new List<TemperatuurMeasurement>();
        private List<CO2Measurement> co2Measurements = new List<CO2Measurement>();
        private List<InfraroodstralingMeasurement> infraroodstralingMeasurements = new List<InfraroodstralingMeasurement>();
        private List<LichtMeasurement> lichtMeasurements = new List<LichtMeasurement>();
        private List<LuchtdrukMeasurement> luchtdrukMeasurements = new List<LuchtdrukMeasurement>();
        private List<LuchtvochtigheidMeasurement> luchtvochtigheidMeasurements = new List<LuchtvochtigheidMeasurement>();
        private List<UVindexMeasurement> uvindexMeasurements = new List<UVindexMeasurement>();

        public ExportDataPage(List<TemperatuurMeasurement> temperatuurMeasurements, List<CO2Measurement> co2Measurements, List<InfraroodstralingMeasurement> infraroodMeasurements, List<LichtMeasurement> lichtMeasurements, List<LuchtdrukMeasurement> luchtdrukMeasurements, List<LuchtvochtigheidMeasurement> luchtvochtigheidMeasurements, List<UVindexMeasurement> uvindexMeasurements)
        {
            InitializeComponent();
            this.temperatuurMeasurements = temperatuurMeasurements;
            this.co2Measurements = co2Measurements;
            this.infraroodstralingMeasurements = infraroodMeasurements;
            this.lichtMeasurements = lichtMeasurements;
            this.luchtdrukMeasurements = luchtdrukMeasurements;
            this.luchtvochtigheidMeasurements = luchtvochtigheidMeasurements;
            this.uvindexMeasurements = uvindexMeasurements;
        }

        private void btnExportData_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            DateTime dateToday = DateTime.Today;
            string fileName = "weatherstation_" + dateToday.ToString("dd_MM_yyyy") + ".csv";

            if (!File.Exists(fileName))
            {
                File.Create(fileName).Close();
            }

            try
            {
                StreamWriter sw = new StreamWriter(fileName);
                sw.WriteLine("Date,Time,Type,Value,Unit");

                if (temperatuurMeasurements.Count > 0)
                {
                    foreach (Measurement m in temperatuurMeasurements)
                    {

                        sw.WriteLine(m.DateTimeOfMeasurement.ToString("dd/MM/yyyy") + "," + m.DateTimeOfMeasurement.ToString("HH:mm:ss.fff") + "," + "temperature," + m.Value.ToString().Replace(',', '.') + "," + m.Unit);
                    }
                }

                if (co2Measurements.Count > 0)
                {
                    foreach (Measurement m in co2Measurements)
                    {
                        sw.WriteLine(m.DateTimeOfMeasurement.ToString("dd/MM/yyyy") + "," + m.DateTimeOfMeasurement.ToString("HH:mm:ss.fff") + "," + "co2," + m.Value.ToString().Replace(',', '.') + "," + m.Unit);
                    }
                }

                if (infraroodstralingMeasurements.Count > 0)
                {
                    foreach (Measurement m in infraroodstralingMeasurements)
                    {
                        sw.WriteLine(m.DateTimeOfMeasurement.ToString("dd/MM/yyyy") + "," + m.DateTimeOfMeasurement.ToString("HH:mm:ss.fff") + "," + "infrarood," + m.Value.ToString().Replace(',', '.') + "," + m.Unit);
                    }
                }


                if (lichtMeasurements.Count > 0)
                {
                    foreach (Measurement m in lichtMeasurements)
                    {
                        sw.WriteLine(m.DateTimeOfMeasurement.ToString("dd/MM/yyyy") + "," + m.DateTimeOfMeasurement.ToString("HH:mm:ss.fff") + "," + "licht," + m.Value.ToString().Replace(',', '.') + "," + m.Unit);
                    }
                }


                if (luchtdrukMeasurements.Count > 0)
                {
                    foreach (Measurement m in luchtdrukMeasurements)
                    {
                        sw.WriteLine(m.DateTimeOfMeasurement.ToString("dd/MM/yyyy") + "," + m.DateTimeOfMeasurement.ToString("HH:mm:ss.fff") + "," + "luchtdruk," + m.Value.ToString().Replace(',', '.') + "," + m.Unit);
                    }
                }


                if (luchtvochtigheidMeasurements.Count > 0)
                {
                    foreach (Measurement m in luchtvochtigheidMeasurements)
                    {
                        sw.WriteLine(m.DateTimeOfMeasurement.ToString("dd/MM/yyyy") + "," + m.DateTimeOfMeasurement.ToString("HH:mm:ss.fff") + "," + "luchtvochtigheid," + m.Value.ToString().Replace(',', '.') + "," + m.Unit);
                    }
                }


                if (uvindexMeasurements.Count > 0)
                {
                    foreach (Measurement m in uvindexMeasurements)
                    {
                        sw.WriteLine(m.DateTimeOfMeasurement.ToString("dd/MM/yyyy") + "," + m.DateTimeOfMeasurement.ToString("HH:mm:ss.fff") + "," + "uvindex," + m.Value.ToString().Replace(',', '.') + "," + m.Unit);
                    }
                }


                sw.Close();
            } catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            
        }
    }
}
