using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Timers;
namespace port_yazma
{
    public partial class Form1 : Form
    {
       

        private string veri;
        public Form1()
        {
            InitializeComponent();
        }

        private void serialPort2_DataReceived(Object sender,
               System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            veri = serialPort2.ReadExisting();
            this.Invoke(new EventHandler(DisplayText));
        }


        private void DisplayText(object sender, EventArgs e)
        {
            richTextBox1.AppendText(veri);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string bilgi = textBox1.Text;
            string rumuz = textBox2.Text;
            if(!serialPort1.IsOpen)
            {
                serialPort1.PortName = "COM1";
                serialPort1.BaudRate = 9600;
                serialPort1.StopBits = StopBits.One;
                serialPort1.DataBits = 8;
                serialPort1.Parity = Parity.None;
                serialPort1.Handshake = Handshake.None;

                serialPort1.Open();
                serialPort1.WriteLine(rumuz+" : "+bilgi);
                serialPort1.Close();
                textBox1.Clear();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Hide();
            if (!serialPort2.IsOpen)
            {
                serialPort2.PortName = "COM4";
                serialPort2.BaudRate = 9600;
                serialPort2.DataBits = 8;
                serialPort2.Handshake = Handshake.None;

                serialPort2.Open();
                serialPort2.ReadTimeout = 500;

                serialPort2.DataReceived += new SerialDataReceivedEventHandler(serialPort2_DataReceived);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Hide();
            label1.Hide();
            button2.Hide();
            label2.Show();

            timer1.Interval = 2000;
            timer1.Enabled = true;
                    

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Hide();
            timer1.Enabled = false;
        }

    }
}
