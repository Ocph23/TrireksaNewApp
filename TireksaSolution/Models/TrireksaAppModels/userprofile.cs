using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsShared.Models
{

    [TableName("userprofile")]
    public class userprofile : ModelsShared.Models.user
    {


        [DbColumn("UserId")]
        public int UserId
        {
            get { return _userid; }
            set
            {
                _userid = value;
                OnPropertyChange("UserId");
            }
        }


        [DbColumn("UserCode")]
        public string UserCode
        {
            get { return _userCode; }
            set
            {
                _userCode = value;
                OnPropertyChange("UserCode");
            }
        }


        [DbColumn("FirstName")]
        public string FirstName
        {
            get { return _firstname; }
            set
            {
                _firstname = value;
                OnPropertyChange("FirstName");
            }
        }

        [DbColumn("LastName")]
        public string LastName
        {
            get { return _lastname; }
            set
            {
                _lastname = value;
                OnPropertyChange("LastName");
            }
        }

        [DbColumn("Address")]
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChange("Address");
            }
        }

        [DbColumn("Photo")]
        public byte[] Photo
        {
            get { return _photo; }
            set
            {
                _photo = value;
                OnPropertyChange("Photo");
            }
        }

        public List<roles> Roles { get; set; }

        private int _userid;
        private string _firstname;
        private string _lastname;
        private string _address;
        private byte[] _photo;
        private string _userCode;
    }
}