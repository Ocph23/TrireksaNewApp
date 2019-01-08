using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OcphSMSLib
{

    public enum SMSMOde
    {
        Text,
        Decoder
    }
    public enum ReadType
    {
        READ,
        UNREAD
    }

    public enum SendingStatus
    {
        SendingOK,
        SendingOKNoReport,
        SendingError,
        DeliveryOK,
        DeliveryFailed,
        DeliveryPending,
        DeliveryUnknown,
        Error
    }


    public enum Religion
    {
        Islam=1,
        Kristen,
        Katolik,
        Hindu,
        Budha,
        KongHuchu
    }


    public enum PhoneType
    {


    }

    public enum MessageMode
    {
        None,
        Text
    }


    public enum RowStatus
    { 
        New,
        Old,
        Change
    }
}
