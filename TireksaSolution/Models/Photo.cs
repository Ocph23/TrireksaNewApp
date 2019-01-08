using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsShared
{
    public class Photo :BaseNotifyProperty, IPhoto
    {
        private byte[] _picture;
        private byte[] _thumb;

        public byte[] Picture {
            get { return _picture; }
            set
            {
                _picture = value;
                OnPropertyChange("Picture");
            }
        }
        public byte[] Thumb {

            get { return _thumb; }
            set
            {
                _thumb = value;
                OnPropertyChange("Thumb");
            }
        }
        public int Id { get; set; }
        public int STT { get; set; }
        public int PenjualanId { get; set; }
        public string Ext { get; set; }
        public string Path { get; set; }
        public string File { get; set; }
    }



    public interface IPhoto:Iphoto
    {
        byte[] Picture { get; set; }

        byte[] Thumb { get; set; }

        int STT { get; set; }
    }

    public interface Iphoto
    {
        int Id { get; set; }

        int PenjualanId { get; set; }

        string Ext { get; set; }


        string Path { get; set; }

        string File { get; set; }


    }


}
