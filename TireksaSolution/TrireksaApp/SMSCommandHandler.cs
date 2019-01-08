using ModelsShared.Models;
using OcphSMSLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrireksaApp.Common;

namespace TrireksaApp
{
    public class SMSCommandHandler
    {
       

        public CommandHandler SendToShiper { get;  set; }
        public CommandHandler SendToReciver { get; set; }


    }
}
