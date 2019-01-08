using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsShared.Models
{
    public enum PayType
    {
       None,Credit,COD,Chash
    }

    public enum CustomerIsPay
    {
        None,Shiper,Reciver,Other
    }

    public enum TypeOfWeight
    {
        None, Weight, WeightVolume, Volume
    }

    public enum CustomerType
    {
        None, Person, Organization
    }

    public enum PortType
    {
        None, Sea,Air,Land
    }


    public enum ManifestType
    {
        Incomming, Outgoing
    }

    public enum InvoicePayType
    {
        None,Transfer, Chash
    }
    public enum InvoiceStatus
    {
       Unpaid, Pending, Paid, Cancel, None
    }
}
