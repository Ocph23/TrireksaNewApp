using OcphSMSLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsShared.Models;
using OcphSMSLib.Models;
using ModelsShared;

namespace TrireksaApp
{
   public class SMSClient
    {
        public Modem device;
        public SMSClient()
        {
           // var config = new AppConfiguration();
            
            device = new Modem("80");
            device.OnErrorMessage +=  Device_OnErrorMessageAsync;
            device.OnReciveSMS += Device_OnReciveSMS;
            device.OnSendingSMS += Device_OnSendingSMS;
            device.Signature = "Trireksa";
            var result = device.Connect();
            CompleteTask(result);
        }

       
        private async void CompleteTask(Task<bool> result)
        {
            var x=   await result;
            device.SetModeSMS(SMSMOde.Text);
        }

        public void SendMessage(OcphSMSLib.Models.OutMessage message)
        {
            try
            {
                device.SendMessage(message);
            }
            catch (Exception ex)
            {
                throw new SystemException("Pesan Gagal Dikirim ;"+ex.Message);
            }
           
        }




        //Event

        private void Device_OnSendingSMS(OcphSMSLib.Models.OutMessage message, EventArgs args)
        {
            throw new NotImplementedException();
        }

        private void Device_OnReciveSMS(OcphSMSLib.Models.InMessage inbox, EventArgs args)
        {
            throw new NotImplementedException();
        }

        private async void Device_OnErrorMessageAsync(Exception ex)
        {
          var  MainVM = Common.ResourcesBase.GetMainWindowViewModel();
            await MainVM.MessageCollection.Add(new Message { MessageText = ex.Message });
        }


        internal void SendSMSPenjualan(penjualan item, customer shiperSelected, customer reciverSelected)
        {
            item.Reciver = reciverSelected;
            item.Shiper = shiperSelected;
            OutMessage msg = new OutMessage { Destination = shiperSelected.Handphone, MessageText = GetPenjualanFormatSMS(item) };
            this.SendMessage(msg);
            msg = new OutMessage { Destination = reciverSelected.Handphone, MessageText = GetPenjualanFormatSMS(item) };
        }

        private string GetPenjualanFormatSMS(penjualan item)
        {
            return string.Format("STT:{0:D5}, Reciver={1}, Cly={2}, Weight={3}", item.STT,
                item.Shiper.Name, item.Details.Count, item.Details.Sum(O => O.Weight));
        }
    }
}
