using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcphSMSLIBWithWCF
{
    class Program
    {
        static  void Main(string[] args)
        {
            var list = OcphSMSLib.Win32DeviceMgmt.GetAllCOMPorts();
            foreach(var item in list)
            {
                Display(string.Format("{0}|{1}", item.DeviceName, item.Port));
            }
            OcphSMSLib.Modem modem = new OcphSMSLib.Modem("COM4");
            modem.OnErrorMessage += Modem_OnErrorMessage;
            modem.OnReciveSMS += Modem_OnReciveSMS;
            modem.OnRecive += Modem_OnRecive;
             var isconect= modem.Connect();
            CompleteConnected(isconect);
            modem.SetModeSMS(OcphSMSLib.SMSMOde.Text);
            Console.ReadKey();
          
        }

        private static void Modem_OnRecive(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            Display(sp.ReadExisting());
        }

        private static void Modem_OnReciveSMS(OcphSMSLib.Models.InMessage inbox, EventArgs args)
        {
            Display(string.Format("{0} | {1}", inbox, inbox.Sender));
        }

        private static async void CompleteConnected(Task<bool> isconect)
        {
            var iscon = await isconect;
            Display(iscon.ToString());
        }

        private static void Modem_OnErrorMessage(Exception ex)
        {
            Display(ex.Message);
        }

        private static void Display(string v)
        {
            Console.WriteLine(v);
        }
    }
}
