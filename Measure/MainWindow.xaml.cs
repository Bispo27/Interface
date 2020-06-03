using System;
using System.Windows;
using System.Windows.Controls;
using System.IO.Ports;
using System.Timers;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;





namespace Measure
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    /// 



    public partial class MainWindow 
    {
        private static System.Timers.Timer aTimer;
        string RxString1, RxString2, RxString3, RxString4, RxString5, RxString6, RxString;
        SerialPort serialPort1 = new SerialPort();
        Chart Grafic = new Chart();
        bool isconnected = false;
        private delegate void SafeCallDelegate(string text);
        StreamWriter X;
        string caminhoNome = "C:\\Users\\João Paulo Bispo\\Desktop\\Desktop JP\\Batata\\Leittura.m";

        public MainWindow()
        {
            
            serialPort1.BaudRate = 115200;
            serialPort1.Parity = Parity.None;
            serialPort1.DataBits = int.Parse("8");
            serialPort1.StopBits = StopBits.One;
            serialPort1.Handshake = Handshake.None;
            serialPort1.ReadTimeout = 500;
            serialPort1.WriteTimeout = 500;





            X = File.CreateText(caminhoNome);
            //Console.ReadKey();

            InitializeComponent();


            Command.Items.Add("Reiniciar");
            Command.Items.Add("Off");
            Command.Items.Add("Init");
            Command.Items.Add("Monofásico");
            Command.Items.Add("Bifásico");
            Command.Items.Add("Trífásico");

            Samples.Items.Add("0.1");
            Samples.Items.Add("1");
            Samples.Items.Add("2");
            Samples.Items.Add("5");
            Samples.Items.Add("10");
            Samples.Items.Add("50");
            Samples.Items.Add("100");
            Samples.Items.Add("1000");
        }


        private void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(20);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Start();
        }

        bool ControlRcvd = true;
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            
            if (serialPort1.IsOpen == true)
            {
               // serialPort1.Write("a");
                //for (Int32 i = 0; i < 3000; i++) ;
                //this.Dispatcher.Invoke(upDateData);
            }

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                //serialPort1.Write
            }
        }

        private void PORT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*Samples.Items.Add("0.1");
            Samples.Items.Add("1");
            Samples.Items.Add("2");
            Samples.Items.Add("5");
            Samples.Items.Add("10");
            Samples.Items.Add("50");
            Samples.Items.Add("100");
            Samples.Items.Add("1000");*/

            String s = Samples.Text;
            string nb = "a";
            byte ch;
            int aux;

            if (serialPort1.IsOpen)
            {
               
                if (s.Equals("5"))
                {
                    aux = 0x03c7;
                    ch = Convert.ToByte(aux);
                    nb = ch.ToString();
                    WriteByte(ch);
                }
                if (s.Equals("10"))
                {
                    aux = 0x0463;
                    ch = Convert.ToByte(aux);
                    nb = ch.ToString();
                    WriteByte(ch);
                }
                if (s.Equals("50"))
                {
                    aux = 0x0513;
                    ch = Convert.ToByte(aux);
                    nb = ch.ToString();
                    WriteByte(ch);
                }
                if (s.Equals("100"))
                {
                    aux = 0x9009;
                    ch = Convert.ToByte(aux);
                    nb = ch.ToString();
                    WriteByte(ch);
                }
                if (s.Equals("1000"))
                {
                    aux = 0x9000;
                    ch = Convert.ToByte(aux);
                    nb = ch.ToString();
                    WriteByte(ch);
                }


            }

        }

        private void SendCommand_Click(object sender, RoutedEventArgs e)
        {
            
            String s = Command.Text;
            System.Windows.Forms.MessageBox.Show(s, s, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            string nb = "a";
            byte ch;
            int aux;
            if (serialPort1.IsOpen)
            {
                if (s.Equals("Reiniciar"))
                {
                    aux = 0x00;
                    ch = Convert.ToByte(aux);
                    nb = ch.ToString(); 
                    WriteByte(ch);
                }
                if (s.Equals("Off"))
                {
                    aux = 0x01;
                    ch = Convert.ToByte(aux);
                    nb = ch.ToString();
                    WriteByte(ch);
                }
                if (s.Equals("Init"))
                {
                    aux = 0x02;
                    ch = Convert.ToByte(aux);
                    nb = ch.ToString();
                    WriteByte(ch);
                }
                if (s.Equals("Monofásico"))
                {
                    aux = 0x03;
                    ch = Convert.ToByte(aux);
                    nb = ch.ToString();
                    WriteByte(ch);
                }
                if (s.Equals("Bifásico"))
                {
                    aux = 0x04;
                    ch = Convert.ToByte(aux);
                    nb = ch.ToString();
                    WriteByte(ch);
                }
                if (s.Equals("Trífásico"))
                {
                    aux = 0x05;
                    ch = Convert.ToByte(aux);
                    nb = ch.ToString();
                    WriteByte(ch);
                }

                
            }


        }


        public void WriteByte(byte data)
        {
            //change data to array
            //byte[] dataArray = new byte[1];
            var dataArray = new byte[] { data };
            //dataArray[0] = data;
            serialPort1.Write(dataArray, 0, 1);   // <-- Exception is thrown here
        }



        private void UpdateListCOMs()
        {
            
            bool valueChangePort = false;

            if(PORT.Items.Count == SerialPort.GetPortNames().Length)
            {
                foreach(string s in SerialPort.GetPortNames())
                {
                    int i = 0;
                    if (PORT.Items[i++].Equals(s) == false)
                    {
                        valueChangePort = true;
                    }
                }
            }
            else
            {
                valueChangePort = true;
            }

            if(valueChangePort == false)
            {
                return;
            }

            PORT.Items.Clear();

            foreach(string s in SerialPort.GetPortNames())
            {
                PORT.Items.Add(s);
            }
            PORT.SelectedIndex = 0;
        }

        

        private void btConectar_Click(object sender, RoutedEventArgs e)
        {
            

            if (serialPort1.IsOpen == false && PORT.Text.Length > 0)
            {
                serialPort1.PortName = PORT.Text;

                try
                {
                    serialPort1.PortName = PORT.Items[PORT.SelectedIndex].ToString();
                    serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
                    serialPort1.Open();
                    SetTimer();

                }
                catch
                {
                    return;

                }
                if (serialPort1.IsOpen)
                {
                    btConectar.Content = "Desconectar";
                    PORT.IsEnabled = false;

                }
            }
            else
            {

                try
                {
                    aTimer.Stop();
                    serialPort1.Close();
                    PORT.IsEnabled = true;
                    X.Close();
                    btConectar.Content = "Conectar";
                }
                catch
                {
                    return;
                }

            }
        }


        private void VOLTAGE_A1_Click(object sender, RoutedEventArgs e)
        {
            int i;
            bool quantDiferente;    //flag para sinalizar que a quantidade de portas mudou

            i = 0;
            quantDiferente = false;

            //se a quantidade de portas mudou
            if (PORT.Items.Count == SerialPort.GetPortNames().Length)
            {
                foreach (string s in SerialPort.GetPortNames())
                {
                    if (PORT.Items[i++].Equals(s) == false)
                    {
                        quantDiferente = true;
                    }
                }
            }
            else
            {
                quantDiferente = true;
            }

            //Se não foi detectado diferença
            if (quantDiferente == false)
            {
                return;                     //retorna
            }

            //limpa comboBox
            PORT.Items.Clear();

            //adiciona todas as COM diponíveis na lista
            foreach (string s in SerialPort.GetPortNames())
            {
                PORT.Items.Add(s);
            }
            //seleciona a primeira posição da lista
            PORT.SelectedIndex = 0;
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            this.Dispatcher.Invoke(upDateData);

        }

        private void upDateData() {

            
            if (serialPort1.IsOpen) {
                RxString = serialPort1.ReadLine();
                 if (RxString.First() == '+')
                {
                    try
                    {
                        RxString1 = RxString.Substring(RxString.IndexOf("!") + 1, (RxString.IndexOf("$") - 2));
                        //RxString2 = RxString.Substring(RxString.IndexOf("@") + 1, (RxString.IndexOf(".") + 1));
                        //RxString3 = RxString.Substring(RxString.IndexOf("#") + 1, (RxString.IndexOf(".")));
                        RxString4 = RxString.Substring(RxString.IndexOf("$") + 1, (RxString.IndexOf(".")));
                        //RxString5 = RxString.Substring(RxString.IndexOf("%") + 1, (RxString.IndexOf(".") - 1));
                        //RxString6 = RxString.Substring(RxString.IndexOf("&") + 1, (RxString.IndexOf(".") - 1));

                        ControlRcvd = false;


                        X.Write(RxString1); X.Write(", "); X.Write(RxString4);
                        X.WriteLine();
                    }
                    catch {

                        X.Write(""); X.Write(", "); X.Write("");
                        X.WriteLine();
                        return; 
                    
                    }
                    
                    //this.Dispatcher.Invoke(upDateData);
                }

                //serialPort1.Close();
                //serialPort1.Open();
                RxString = "";

            }

            

            

            /*Voltage_A.Content = "Voltage A: " + RxString1 + " V";
            Voltage_B.Content = "Voltage B: " + RxString2 + " V";
            Voltage_C.Content = "Voltage C: " + RxString3 + " V";
            Current_A.Content = "Current A: " + RxString4 + " A";
            Current_B.Content = "Current B: " + RxString5 + " A";
            Current_C.Content = "Current C: " + RxString6 + " A";
            Power_A.Content = "Power A: " + Convert.ToString(potA) + " W";
            Power_B.Content = "Power B: " + Convert.ToString(potB) + " W";
            Power_C.Content = "Power C: " + Convert.ToString(potC) + " W";*/
            ControlRcvd = true;
        }
    }


}
