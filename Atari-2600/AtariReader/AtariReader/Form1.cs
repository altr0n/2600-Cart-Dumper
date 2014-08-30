using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox2.Items.Add("2k");
            comboBox2.Items.Add("4k");
            comboBox2.Items.Add("8k");
        }
         int RomSize = 0;
        string[] ROM;
          int[] ByteRom; 
        byte[] test;
        int index = 0;
        SerialPort comm = new SerialPort();
        int baud = 9600;
        string PortName= "";
       
        private void button1_Click(object sender, EventArgs e)
        {
            foreach(string element in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(element);
            }
            
        }
        public bool recieved = true;
        bool reading = false;
        public void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //if(reading)
            //{
            //    if (index >= RomSize)
            //    {
            //        MessageBox.Show("Done!");
            //        comm.Close();
            //        reading = false;
                    
            //        //printRom();
            //    }
                try
                {
                        
                        string element = comm.ReadLine();
                        //string[] st = element.Split(new char[] {':'});
                        //int a = int.Parse(st[0], System.Globalization.NumberStyles.HexNumber);
                        //int b = int.Parse(st[1], System.Globalization.NumberStyles.HexNumber);
                    // MessageBox.Show(element.ToString());
                        //MessageBox.Show(element.ToString("X2"));
                        ROM[index] = element.ToString();
                        index++;
                        Invoke(new Action(() =>
                        {
                            label3.Text = "Value:" + element;
                            progressBar1.Increment(1);
                        }));
 
                    
                    
                }
                catch (IOException testing)
                {
                    MessageBox.Show("TEST");
                }
                catch(InvalidOperationException rc)
                {

                    MessageBox.Show("YUT");
               }
                
            }
               
        

        public void printRom()
        {

 
        }

        public void Connect()
        {
            progressBar1.Value = 0;
            PortName = comboBox1.SelectedItem.ToString();
            comm = new SerialPort(PortName, baud);
            try
            {
                label2.Text = "Attempting to open port";
                comm.Open();
            }
            catch (IOException exc)
            {
                label2.Text = "Unable to open port";
                MessageBox.Show("Unable to open port: " + exc.ToString());
            }
            if (comm.IsOpen)
            {
                label2.Text = "Connected!";
            }
            comm.DataReceived += new SerialDataReceivedEventHandler(DataReceived);

        }

        public void Close()
        {
            comm.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comm.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string fetch = comboBox2.SelectedItem.ToString();
            switch (fetch)
            {
                case "4k":
                    RomSize = 4096;
                    break;
                case "2k":
                    RomSize = 2048;
                    break;
                case "8k":
                    RomSize = 8192;
                    break;
            }
            ROM = new string[RomSize];
            ByteRom = new int[RomSize];
            test = new byte[RomSize] ;
            comm.WriteLine("FF"); // Break the arduino out of it's loop!

        }

        private void button5_Click(object sender, EventArgs e)
        {
            convertRom();
        }
      
        
        public void convertRom()
        {
            DataTable testData = new DataTable();
            progressBar1.Value = 0;
            for (int t = 0; t < ROM.Length-1; t++)
            {
                ByteRom[t] = byte.Parse(ROM[t]);

            }
            for (int t = 0; t < ROM.Length-1; t++)
            {

              
                    test[t] = (byte)ByteRom[t];
                  
                    progressBar1.Increment(1);
                  
            }
            //for (int t = 0; t < test.GetLength(0); t++)
            //{
            //    string column = string.Format("Column{0}", t + 1);
            //    dataGridView1.Columns.Add(column,column);
            //}

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ByteArrayToFile(saveFileDialog1.FileName, test);
            }
            MessageBox.Show("Save Complete!");
        }

        private void button6_Click(object sender, EventArgs e)
        {         
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ByteArrayToFile(saveFileDialog1.FileName, test);
            }
            MessageBox.Show("Save Complete!");
        }

        public bool ByteArrayToFile(string _FileName, byte[] _ByteArray)
        {
            label2.Text = "Writing to "+ _FileName+ " please wait...";
            try
            {
                // Open file for reading
                System.IO.FileStream _FileStream =
                   new System.IO.FileStream(_FileName, System.IO.FileMode.Create,
                                            System.IO.FileAccess.Write);
                // Writes a block of bytes to this stream using data from
                // a byte array.
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

                // close file stream
                _FileStream.Close();
                label2.Text = "Wrote to " + _FileName + " enjoy!";
                return true;
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}",
                                  _Exception.ToString());
            }

            // error occured, return false
            return false;
        }

       
      
    }
}
