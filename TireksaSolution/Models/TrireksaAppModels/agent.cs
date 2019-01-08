using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsShared.Models
{

    [TableName("agents")]
    public class agent:BaseNotifyProperty,IModelValidate
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

        [DbColumn("Name")]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChange("Name");
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

        [DbColumn("NPWP")]
        public string NPWP
        {
            get { return _npwp; }
            set
            {
                _npwp = value;
                OnPropertyChange("NPWP");
            }
        }


        [DbColumn("ContactName")]
        public string ContactName
        {
            get { return _contactname; }
            set
            {
                _contactname = value;
                OnPropertyChange("ContactName");
            }
        }

        [DbColumn("Phone")]
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChange("Phone");
            }
        }

        [DbColumn("Handphone")]
        public string Handphone
        {
            get { return _handphone; }
            set
            {
                _handphone = value;
                OnPropertyChange("Handphone");
            }
        }

        [DbColumn("Email")]
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChange("Email");
            }
        }

        [DbColumn("CityID")]
        public int CityID
        {
            get { return _cityid; }
            set
            {
                _cityid = value;
                OnPropertyChange("CityID");
            }
        }

        public List<CityAgentCanAccess> CitiesCanAccess { get; set; }

        public bool IsValid
        {
            get
            {
                return ValidatedAction();
            }
        }

        public bool ValidatedAction()
        {
            var result = true;
            if (string.IsNullOrEmpty(this.Address) || string.IsNullOrEmpty(this.ContactName) || CityID <= 0 || string.IsNullOrEmpty(this.Handphone) || string.IsNullOrEmpty(this.Name))
                result = false;

            return result;
        }

        private int _id;
        private string _name;
        private string _address;
        private string _npwp;
        private string _contactname;
        private string _phone;
        private string _handphone;
        private string _email;
        private int _cityid;

  
    }
}
