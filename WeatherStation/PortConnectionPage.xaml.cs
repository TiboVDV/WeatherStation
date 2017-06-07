using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for PortConnectionPage.xaml
    /// </summary>
    public partial class PortConnectionPage : UserControl
    {
        private DispatcherTimer RefreshPortsTimer = new DispatcherTimer();
        private SerialPort port;
        private ObservableCollection<string> allSerialPortNamesInList = new ObservableCollection<string>();

        //Alle gebruikte kleuren
        private SolidColorBrush whiteColorBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        private SolidColorBrush grayColorBrush = new SolidColorBrush(Color.FromRgb(70, 70, 70));
        private SolidColorBrush blueColorBrush = new SolidColorBrush(Color.FromRgb(66, 133, 244));
        private SolidColorBrush redColorBrush = new SolidColorBrush(Color.FromRgb(219, 68, 71));
        private SolidColorBrush greenColorBrush = new SolidColorBrush(Color.FromRgb(15, 157, 88));

        private string dataBuffer1 = "";
        private string dataBuffer2 = "";

        public SerialPort Port
        {
            get { return port; }
        }

        public string DataBuffer1
        {
            get { return dataBuffer1; }
        }

        public string DataBuffer2
        {
            get { return dataBuffer2; }
        }

        public TextBox DataTextBox
        {
            get { return txtPortOutput; }
            set { txtPortOutput = value; }
        }

        public PortConnectionPage()
        {
            InitializeComponent();

            comboBPortNames.ItemsSource = allSerialPortNamesInList;

            port = new SerialPort();
            port.DataReceived += PortDataReceived;

            RefreshPortsTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            RefreshPortsTimer.Tick += RefreshPortsTimer_Tick;

            RefreshPortsTimer.Start();

            comboBBaudRates.Items.Add("9600");
            comboBBaudRates.Items.Add("115200");
            comboBBaudRates.SelectedIndex = 1;

            
        }

        //Timer die de mogelijke poortnamen kan detecteren
        private void RefreshPortsTimer_Tick(object sender, EventArgs e)
        {
            RefreshAvailablePorts();

            try
            {
                if (!port.IsOpen)
                {
                    CheckPortConnectionForButton();
                }
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                CheckPortConnectionForButton();
            }
        }

        //Kijken welke COM-poorten verbonden zijn
        private void RefreshAvailablePorts()
        {
            try
            {
                //Array met alle namen van de verbonden COM-poorten
                string[] allSerialPortNames = SerialPort.GetPortNames();

                //Voor elke nieuwe naam: toevoegen aan de lijst die weergegeven wordt
                foreach (string portName in allSerialPortNames)
                {
                    if (!allSerialPortNamesInList.Contains(portName))
                    {
                        allSerialPortNamesInList.Add(portName);
                    }

                }

                //Voor elke oude naam: verwijderen uit de lijst die weergegeven wordt
                foreach (string oldPort in allSerialPortNamesInList)
                {
                    if (!allSerialPortNames.Contains(oldPort))
                    {
                        allSerialPortNamesInList.Remove(oldPort);
                    }

                    //Als de lijst gelijk is aan 0, dan kan de loop niet meer gedaan worden => break
                    if (allSerialPortNamesInList.Count == 0)
                    {
                        break;
                    }
                }

                //Als er maar 1 naam is, zet die dan als geselecteerde
                if (allSerialPortNamesInList.Count == 1)
                {
                    comboBPortNames.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Knop voor het openen en sluiten van de verbinding
        private void btnOpenCloseConnection_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(port != null)
            {
                if (!port.IsOpen)
                {
                    try
                    {
                        port.PortName = comboBPortNames.SelectedValue.ToString();
                        port.BaudRate = int.Parse(comboBBaudRates.SelectedValue.ToString());
                        port.StopBits = StopBits.One;
                        port.DataBits = 8;
                        port.Parity = Parity.None;
                        port.Open();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                } else
                {
                    port.Close();
                }
            }

            CheckPortConnectionForButton();


        }

        //Functie om te de kleur van de knop te veranderen en de tekst
        private void CheckPortConnectionForButton()
        {
            if(port != null)
            {
                if (port.IsOpen)
                {
                    btnOpenCloseConnection.Background = redColorBrush;
                    lblCurrentPortName.Content = port.PortName;
                    lblCurrentBaudRate.Content = port.BaudRate.ToString();
                    btnOpenCloseConnectionLabel.Content = "Close connection";
                }
                else
                {
                    btnOpenCloseConnection.Background = greenColorBrush;
                    lblCurrentPortName.Content = "not connected";
                    lblCurrentBaudRate.Content = "not connected";
                    btnOpenCloseConnectionLabel.Content = "Open connection";
                }
            }
            
        }

        //Functie die uitgevoerd wordt wanneer er data ontvangen wordt van het weerstation
        private void PortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                byte[] buffer = new byte[1024];
                int len = port.Read(buffer, 0, buffer.Length);

                if(len > 0)
                {
                    for (int i = 0; i < len; i++)
                    {
                        char c = (char)buffer[i];
                        if (c == '\n')
                        {
                            dataBuffer1 += c;
                            dataBuffer2 = dataBuffer1;
                        } else
                        {
                            dataBuffer1 += c;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string GetDataBuffer()
        {
            string tempDataBuffer = dataBuffer1;
            dataBuffer1 = "";

            return tempDataBuffer;
        }
    }
}
