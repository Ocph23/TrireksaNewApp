//using OcphSMSLib;
//using OcphSMSLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace TrireksaWebsite.Api
{
    public class SMSController : ApiController
    {

        //public async System.Threading.Tasks.Task<IHttpActionResult> SendSMSAsync()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    using (var modem = new Modem("COM7"))
        //    {
        //        modem.WriteTimeOut = 5000;
        //        modem.OnSendingSMS += (OutMessage message, EventArgs args) => {
        //           sb.AppendLine(message.MessageText);
        //        };

        //        modem.OnErrorMessage += Modem_OnErrorMessage;
        //        StringBuilder sb1 = new StringBuilder();
        //        sb1.AppendLine("Silahkan Masukkan Kode Token Ini:");
        //        sb1.AppendLine(new Random().Next(1000, 9999).ToString());

        //         var res=  await  modem.Connect();
        //       await modem.SendMessage(new OutMessage { Destination = "+628114801030", MessageText = sb1.ToString()});
        //    }
        //    return Ok(sb.ToString());
        //}

        //private void Modem_OnErrorMessage(Exception ex)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
