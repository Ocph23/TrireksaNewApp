using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ModelsShared
{
    public static class MessageCollection
    {
        public static string Message(MessageType type)
        {
            switch (type)
            {
                case MessageType.SaveOk:
                    return "Berhasil Disimpan";
                case MessageType.SaveFail:
                    return "Tidak Tersimpan";
                case MessageType.DeleteOk:
                    return "Berhasil Dihapus";
                case MessageType.DeleteFail:
                    return "Gagagal Dihapus";
                case MessageType.UpdateOk:
                    return "Berhasil Diuubah";
                case MessageType.UpdateFaild:
                    return "Gagal Diubah";
                case MessageType.NotFound:
                    return "Data Tidak Ditemukan";
                default:
                    return "";
            }
        }
    }

    public enum MessageType
    {
        SaveOk,SaveFail,DeleteOk,DeleteFail,UpdateOk,UpdateFaild,
        NotFound
    }
}
