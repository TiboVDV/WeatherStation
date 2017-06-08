using System;
using System.Collections.Generic;
using System.IO.Ports;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DispatcherTimer GetDataTimer = new DispatcherTimer();//Timer om de gegevens van de poortpagina te gaan halen

        private List<Grid> controlsSelector = new List<Grid>();//Lijst met alle selector controls
        private List<Label> controlsLabelSelector = new List<Label>();//Lijst met alle labels van de selector controls;

        private WeatherStationPage weatherStationPage;//Page voor de grafieken
        private PortConnectionPage portConnectionPage;//Page voor de poort
        private ImportDataPage importDataPage;//Page om data te importeren
        private ExportDataPage exportDataPage;//Page om data te exporteren

        //Alle gebruikte kleuren
        private SolidColorBrush whiteColorBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        private SolidColorBrush grayColorBrush = new SolidColorBrush(Color.FromRgb(70, 70, 70));
        private SolidColorBrush blueColorBrush = new SolidColorBrush(Color.FromRgb(66, 133, 244));
        private SolidColorBrush redColorBrush = new SolidColorBrush(Color.FromRgb(219, 68, 71));
        private SolidColorBrush greenColorBrush = new SolidColorBrush(Color.FromRgb(15, 157, 88));

        //Alle lijsten met de metingen
        private List<TemperatuurMeasurement> temperatuurMeasurements = new List<TemperatuurMeasurement>();
        private List<CO2Measurement> co2Measurements = new List<CO2Measurement>();
        private List<InfraroodstralingMeasurement> infraroodstralingMeasurements = new List<InfraroodstralingMeasurement>();
        private List<LichtMeasurement> lichtMeasurements = new List<LichtMeasurement>();
        private List<LuchtdrukMeasurement> luchtdrukMeasurements = new List<LuchtdrukMeasurement>();
        private List<LuchtvochtigheidMeasurement> luchtvochtigheidMeasurements = new List<LuchtvochtigheidMeasurement>();
        private List<UVindexMeasurement> uvindexMeasurements = new List<UVindexMeasurement>();

        public MainWindow()
        {
            InitializeComponent();

            GetDataTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);//Tijdsinterval geven aan de timer
            GetDataTimer.Tick += GetDataTimer_Tick;

            //Pagina's aanmaken
            weatherStationPage = new WeatherStationPage();
            portConnectionPage = new PortConnectionPage();
            importDataPage = new ImportDataPage();
            exportDataPage = new ExportDataPage(this.temperatuurMeasurements, this.co2Measurements, this.infraroodstralingMeasurements, this.lichtMeasurements, this.luchtdrukMeasurements, this.luchtvochtigheidMeasurements, this.uvindexMeasurements);

        }

        //Functie die opgeroepen wordt wanneer het programma gedaan is met laden
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Toevoegen van de verschillende selectors naar een lijst
            controlsSelector.Add(weatherStationSelector);
            controlsSelector.Add(portConnectionSelector);
            controlsSelector.Add(importDataSelector);
            controlsSelector.Add(exportDataSelector);

            //Toevoegen van de verschillende labels naar een lijst
            controlsLabelSelector.Add(lblWeatherstationSelector);
            controlsLabelSelector.Add(lblPortConnection);
            controlsLabelSelector.Add(lblImportData);
            controlsLabelSelector.Add(lblExportData);

            contentViewer.Content = weatherStationPage; //Eerste form is met alle resultaten

            GetDataTimer.Start(); //Starten van de timer
            
        }

        //Functie die opgeroepen wordt wanneer er op de knop is geduwd om naar de grafieken te gaan
        private void weatherStationform_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChangeSelectorsColors(0);
        }

        //Functie die opgeroepen wordt wanneer er op de knop is geduwd om naar de poort pagina te gaan
        private void portConnectionForm_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChangeSelectorsColors(1);
        }

        //Functie die opgeroepen wordt wanneer er op de knop is geduwd om naar de import pagina te gaan
        private void importDataForm_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChangeSelectorsColors(2);
        }

        //Functie die opgeroepen wordt wanneer er op de knop is geduwd om naar de export pagina te gaan
        private void exportDataFrom_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChangeSelectorsColors(3);
        }

        //Functie om de kleuren te veranderen van de selectors, met de labels
        private void ChangeSelectorsColors(int selectedSelector)
        {
            foreach (Grid controlObject in controlsSelector)
            {
                controlObject.Background = whiteColorBrush;
            }

            foreach (Label controlObject in controlsLabelSelector)
            {
                controlObject.Foreground = grayColorBrush;
            }

            switch (selectedSelector)
            {
                case 0:
                    //Blauwe achtergrond voor de selector
                    weatherStationSelector.Background = blueColorBrush;
                    lblWeatherstationSelector.Foreground = whiteColorBrush;//Witte text
                    contentViewer.Content = weatherStationPage;//Content van de pagina moet de grafieken zijn
                    CheckConnectionLabel();
                    break;
                case 1:
                    portConnectionSelector.Background = blueColorBrush;
                    lblPortConnection.Foreground = whiteColorBrush;
                    lblPortSatusSelector.Foreground = whiteColorBrush;
                    contentViewer.Content = portConnectionPage;
                    break;
                case 2:
                    importDataSelector.Background = blueColorBrush;
                    lblImportData.Foreground = whiteColorBrush;
                    contentViewer.Content = importDataPage;
                    CheckConnectionLabel();
                    break;
                case 3:
                    exportDataSelector.Background = blueColorBrush;
                    lblExportData.Foreground = whiteColorBrush;
                    contentViewer.Content = exportDataPage;
                    CheckConnectionLabel();
                    break;
            }
        }

        //Functie om de label van kleur te doen veranderen die de status van de poort aanduid
        private void CheckConnectionLabel()
        {
            if (lblPortSatusSelector.Content.ToString() == "Closed")
            {
                lblPortSatusSelector.Foreground = redColorBrush;
            }
            else
            {
                lblPortSatusSelector.Foreground = greenColorBrush;
            }
        }

        //Timer functie om de gegevens te halen bij de poort
        private void GetDataTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (portConnectionPage.DataTextBox.LineCount > 100)
                    portConnectionPage.DataTextBox.Clear();

                string receivedData = portConnectionPage.GetDataBuffer();

                DetectMeasurementsFromData(receivedData);

                if(receivedData != "")
                {
                    portConnectionPage.DataTextBox.Text += receivedData;
                    portConnectionPage.DataTextBox.ScrollToEnd();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            CheckPortStatus();

        }

        public void DetectMeasurementsFromData(string dataBuffer)
        {
            if(dataBuffer != "")
            {
                string[] dataBufferLineSplit = dataBuffer.Split(new string[] { Environment.NewLine, "\n", "\r\n" }, StringSplitOptions.None);
                
                if(dataBufferLineSplit.Count() > 0)
                {
                    for(int i = 0; i < dataBufferLineSplit.Count(); i++)
                    {
                        try
                        {
                            if (dataBufferLineSplit[i].Contains("Temperature"))
                            {
                                string value = dataBufferLineSplit[i].Split(' ')[1].Replace('.', ',');
                                TemperatuurMeasurement tempM = new TemperatuurMeasurement("°C", double.Parse(value));
                                temperatuurMeasurements.Add(tempM);
                                weatherStationPage.TempGraph.AddMeasurement(tempM);
                            }
                            else if (dataBufferLineSplit[i].Contains("UV index"))
                            {
                                string value = dataBufferLineSplit[i].Split(' ')[2].Replace('.', ',');
                                UVindexMeasurement uvindexM = new UVindexMeasurement("", double.Parse(value));
                                uvindexMeasurements.Add(uvindexM);
                                weatherStationPage.UVIndexGauge.CurrentValue = uvindexM.Value;
                            }
                            else if (dataBufferLineSplit[i].Contains("Humidity"))
                            {
                                string value = dataBufferLineSplit[i].Split(' ')[1].Replace('.', ',');
                                LuchtvochtigheidMeasurement lvm = new LuchtvochtigheidMeasurement("%", double.Parse(value));
                                luchtvochtigheidMeasurements.Add(lvm);
                                weatherStationPage.HumidityGauge.CurrentValue = lvm.Value;
                            } else if (dataBufferLineSplit[i].Contains("CO2"))
                            {
                                string value = dataBufferLineSplit[i].Split(' ')[1].Replace('.', ',');
                                CO2Measurement co2M = new CO2Measurement("ppm", double.Parse(value));
                                co2Measurements.Add(co2M);
                                weatherStationPage.CO2Gauge.CurrentValue = co2M.Value;
                            } else if (dataBufferLineSplit[i].Contains("Visible"))
                            {
                                string value = dataBufferLineSplit[i].Split(' ')[2].Replace('.', ',');
                                LichtMeasurement lM = new LichtMeasurement("lx", double.Parse(value));
                                lichtMeasurements.Add(lM);
                                weatherStationPage.LightGauge.CurrentValue = lM.Value;
                            } else if (dataBufferLineSplit[1].Contains("Infrared radiation"))
                            {
                                string value = dataBufferLineSplit[i].Split(' ')[2].Replace('.', ',');
                                InfraroodstralingMeasurement irM = new InfraroodstralingMeasurement("W/m^2", double.Parse(value));
                                infraroodstralingMeasurements.Add(irM);
                            } else if (dataBufferLineSplit[i].Contains("Pressure"))
                            {
                                string value = dataBufferLineSplit[i].Split(' ')[1].Replace('.', ',');
                                LuchtdrukMeasurement ldM = new LuchtdrukMeasurement("Pa", double.Parse(value));
                                luchtdrukMeasurements.Add(ldM);
                            }

                        } catch(Exception ex)
                        {
                            //MessageBox.Show(ex.Message);
                        }
                        
                    }
                }

            }
            
        }

        //Controleren van de status van de COM-port
        public void CheckPortStatus()
        {
            if (portConnectionPage.Port != null)
            {
                if (portConnectionPage.Port.IsOpen)
                {
                    lblPortSatusSelector.Content = "Open";
                } else
                {
                    lblPortSatusSelector.Content = "Closed";
                }
                
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            portConnectionPage.Port.Close();
        }
    }
}
