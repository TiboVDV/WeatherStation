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

        public ExportDataPage(List<TemperatuurMeasurement> temperatuurMeasurements)
        {
            InitializeComponent();
            this.temperatuurMeasurements = temperatuurMeasurements;
        }

        private void btnExportData_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SaveFileDialog safeFD = new SaveFileDialog();

            safeFD.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            safeFD.FilterIndex = 2;
            safeFD.RestoreDirectory = true;

            if (safeFD.ShowDialog() == DialogResult.OK)
            {
                if(temperatuurMeasurements.Count > 0)
                {
                    if (!File.Exists("temperatuur.csv"))
                    {
                        File.Create("temperatuur.csv");
                    }

                    StreamWriter sw = new StreamWriter("temperatuur.csv");
                    
                    foreach(Measurement m in temperatuurMeasurements)
                    {
                        sw.WriteLine(m.Value.ToString().Replace(',','.') + "," + m.DateTimeOfMeasurement.ToString("dd/MM/yyyy HH:mm:ss"));
                    }

                    sw.Close();
                }
            }

        }
    }
}
