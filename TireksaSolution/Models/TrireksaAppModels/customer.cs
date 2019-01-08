using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;namespace ModelsShared.Models 
{ 
     [TableName("customer")] 
     public class customer : BaseNotifyProperty,IModelValidate
    {
          [PrimaryKey("Id")] 
          [DbColumn("Id")] 
          public int Id 
          { 
               get{return _id;} 
               set{ 
                      _id=value; 
                     OnPropertyChange("Id");
                     }
          } 

          [DbColumn("Name")] 
          public string Name 
          { 
               get{return _name;} 
               set{ 
                      _name=value; 
                     OnPropertyChange("Name");
                     }
          } 

          [DbColumn("CustomerType")] 
          public CustomerType CustomerType 
          { 
               get{return _customertype;} 
               set{ 
                      _customertype=value; 
                     OnPropertyChange("CustomerType");
                     }
          } 

          [DbColumn("ContactName")] 
          public string ContactName 
          { 
               get{return _contactname;} 
               set{ 
                      _contactname=value; 
                     OnPropertyChange("ContactName");
                     }
          } 

          [DbColumn("Phone1")] 
          public string Phone1 
          { 
               get{return _phone1;} 
               set{ 
                      _phone1=value; 
                     OnPropertyChange("Phone1");
                     }
          }

        [DbColumn("Phone2")]
        public string Phone2
        {
            get { return _phone2; }
            set
            {
                _phone2 = value;
                OnPropertyChange("Phone2");
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

        [DbColumn("Address")] 
          public string Address 
          { 
               get{return _address;} 
               set{ 
                      _address=value; 
                     OnPropertyChange("Address");
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

        public bool IsValid
        {
            get
            {
                return ValidatedAction();
            }
        }

        public city City { get;  set; }

        public bool ValidatedAction()
        {
            bool result = true;
            if (this.CustomerType == CustomerType.None || this.CityID == 0 || string.IsNullOrEmpty(this.Name) || 
                string.IsNullOrEmpty(this.ContactName) || string.IsNullOrEmpty(this.Address))
            {
                result = false;
            }

            return result;

        }

      

        private int _id;
        private string _name;
        private CustomerType _customertype;
        private string _contactname;
        private string _phone1;
        private string _address;
        private string _phone2;
        private string _handphone;
        private string _email;
        private int _cityid;
    }
}


