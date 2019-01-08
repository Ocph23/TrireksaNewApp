using Ocph.DAL;

namespace ModelsShared.Models
{
    [TableName("photos")]
    public class photo : BaseNotifyProperty,Iphoto
    {
       

        [PrimaryKey("Id")]
        [DbColumn("Id")]
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChange("Id");
            }
        }

        [DbColumn("PenjualanId")]
        public int PenjualanId
        {
            get { return _PenjualanId; }
            set
            {
                _PenjualanId = value;
                OnPropertyChange("PenjualanId");
            }
        }

        [DbColumn("Ext")]
        public string Ext
        {
            get { return _ext; }
            set
            {
                _ext = value;
                OnPropertyChange("Ext");
            }
        }

       

        [DbColumn("Path")]
        public string Path
        {
            get { return _Path; }
            set
            {
                _Path = value;
                OnPropertyChange("Path");
            }
        }

        [DbColumn("File")]
        public string File
        {
            get { return _File; }
            set
            {
                _File = value;
                OnPropertyChange("File");
            }
        }
        private int _id;
        private int _PenjualanId;
        private string _ext;
        private string _Path;
        private string _File;
    }
}
