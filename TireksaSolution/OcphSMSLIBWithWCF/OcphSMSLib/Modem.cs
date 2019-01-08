using OcphSMSLib.Models;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OcphSMSLib
{

    public delegate void ExecMessage(int index);
    public delegate void CheckMessageIsSend();
    public delegate void DelegateSendMessage(OutMessage message);
    public delegate void OnIncomming(InMessage inbox, EventArgs args);
    public delegate void OnSending(OutMessage message, EventArgs args);
    public delegate void OnDeleteOutbox(OutMessage outbox, EventArgs args);
    public delegate void OnError(Exception ex);
    public delegate void OnReciveFromSerial(object sender, SerialDataReceivedEventArgs e);


    public class Modem:IDisposable
    {
        private SerialPort serial;
        private string portName="";
        private int boundRate=115200;
        private int dataBits=8;
        private int stopBits=1;
        private int readTimeOut=300;
        private int writeTimeOut=300;
        private String indata;
        private OutMessage SMSData;
        public event OnIncomming OnReciveSMS;
        public event OnSending OnSendingSMS;
        public event OnDeleteOutbox OnDeletingSMS;
        public event OnError OnErrorMessage;
        public event OnReciveFromSerial OnRecive;
        
        public Modem(string portName)
        {
            this.portName = portName;
            serial = new SerialPort();
        }
        public SerialPort Port
        {
            get { return serial; }
            set { serial = value; }
        }
        public string PortName
        {
            get { return portName; }
            set { portName = value; }
        }
        public int BoundRate
        {
            get { return boundRate; }
            set { boundRate = value; }
        }
        public int DataBits
        {
            get { return dataBits; }
            set { dataBits = value; }
        }
        public int StopBits
        {
            get { return stopBits; }
            set { stopBits = value; }
        }
        public int ReadTimeOut
        {
            get { return readTimeOut; }
            set { readTimeOut = value; }
        }
        public int WriteTimeOut
        {
            get { return writeTimeOut; }
            set { writeTimeOut = value; }
        }
        public string Signature { get; set; }


        public async Task<bool> Connect()
        {
            Signature = "Applikasi Penilaian Mahasiswa";
            bool Connected = false;
            await Task.Delay(1000);
          
            try
            {
                serial.PortName = PortName;
                serial.BaudRate = this.BoundRate;
                serial.DataBits = this.DataBits;
                serial.StopBits = System.IO.Ports.StopBits.One;
                serial.ReadTimeout = this.ReadTimeOut;
                serial.WriteTimeout = this.WriteTimeOut;
                serial.DataReceived += new SerialDataReceivedEventHandler(serial_DataReceived);
                serial.Open();
                serial.DtrEnable = true;
                serial.RtsEnable = true;
                Connected = true;
                SetModeSMS(SMSMOde.Text);
            }
            catch (Exception Ex)
            {
               if(OnErrorMessage!=null)
                {
                    OnErrorMessage(Ex);
                }
                
            }
            return Connected;
        }

        private void serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (OnRecive != null)
                OnRecive(sender, e);
            SerialPort sp = (SerialPort)sender;
            if (sp.ReadBufferSize > 0)
            {
                indata = sp.ReadExisting();
                if (OnSendingSMS != null)
                    OnSendingSMS(new OutMessage { MessageText = indata }, null);
                indata = string.Empty;
            }

        }

        public void SetModeSMS(SMSMOde mode)
        {
            if (mode == SMSMOde.Text)
            {
                this.ExecuteCommand("AT+CMGF=1\r");
                this.ExecuteCommand("AT+CSCA=\"" + "081100000" + "\"\r\n");
            }
        }

        public  Task SendMessage(OutMessage message)
        {
           
            try
            {
                serial.BaseStream.Flush();
                string cb = char.ConvertFromUtf32(26);
               // this.ExecuteCommand("AT+CMGF=1\r");
               // this.ExecuteCommand("AT+CSCA=\""+"081100000"+"\"\r\n");//Ufone              Service Center   
                this.SMSData = message; 
                this.ExecuteCommand("AT+CMGS=\"" + message.Destination+ "\"\r\n");// 
                this.ExecuteCommand((message.MessageText+"\r\n#"+this.Signature+"#") + cb);//message text message sending
                System.Threading.Thread.Sleep(5000);
                return Task.FromResult(1);
            }
            catch (Exception ex)
            {
                var text = new Message { MessageText = ex.Message };
                OnErrorMessage(ex);
                return Task.FromResult(0);
            }
        
        }
        public void TestExecute()
        {

            try
            {
                serial.BaseStream.Flush();
                string cb = char.ConvertFromUtf32(26);
                // this.ExecuteCommand("AT+CMGF=1\r");
                // this.ExecuteCommand("AT+CSCA=\""+"081100000"+"\"\r\n");//Ufone              Service Center   
                this.ExecuteCommand("AT+CMSS=112" + "\"\r\n");// 
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }

        }

        public void ReadMessageFromSimAll()
        {
            try
            {
                serial.BaseStream.Flush();
                string cb = char.ConvertFromUtf32(26);
               //Ufone              Service Center                    
                this.ExecuteCommand("AT+CMGL=\"" + "ALL" + "\"\r\n");
                System.Threading.Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
                throw new SystemException( ex.Message);
            }
        
        }

        internal void ReadMessageFromSim(int index)
        {
            try
            {
                serial.BaseStream.Flush();
                System.Threading.Thread.Sleep(3500);
                string cb = char.ConvertFromUtf32(26);

            //    this.ExecuteCommand("AT+CMGF=1\r");
                this.ExecuteCommand("AT+CMGR="+index.ToString() + "\r");
                System.Threading.Thread.Sleep(3500);
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }

        }

        internal void ExecuteCommand(string command)
        {
            try
            {
                if (this.serial.IsOpen)
                {
                    // to send bulk sms
                    serial.WriteLine(command);
                }
            }
            catch (Exception ex)
            {
                serial.Close();
                throw ex;
            }
        }


        public  void Close()
        {
            serial.Close();
        }

        internal void DeleteMessageInSIM(int index)
        {
            try
            {
                serial.BaseStream.Flush();
                System.Threading.Thread.Sleep(3500);
                string cb = char.ConvertFromUtf32(26);

                this.ExecuteCommand("AT+CMGD="+index+ "\r\n"); 
                System.Threading.Thread.Sleep(3500);
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

        public void Dispose()
        {
            if (serial.IsOpen)
                serial.Close();
        }
    }
}
