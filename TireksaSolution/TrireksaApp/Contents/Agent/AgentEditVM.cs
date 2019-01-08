using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsShared.Models;
using System.ComponentModel;
using System.Windows.Data;

namespace TrireksaApp.Contents.Agent
{
    public class AgentEditVM:ModelsShared.Models.agent,IDataErrorInfo
    {
        private Common.ErrorHandler errors = new Common.ErrorHandler();

        public AgentEditVM(agent item)
        {
            this.CitySourceView = Common.ResourcesBase.GetMainWindowViewModel().CityCollection.SourceView;
            this.Address = item.Address;
            this.ContactName = item.ContactName;
            this.Email = item.Email;
            this.Handphone = item.Handphone;
            this.Id = item.Id;
            this.Name = item.Name;
            this.Phone = item.Phone;
            this.CityID= item.CityID;
            this.NPWP = item.NPWP;
        }


        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(this.Name))
                    {
                        var err = "Name Is Requered !";
                        errors.AddError(columnName, err);
                        return err;
                    }
                    else
                    {
                        errors.DeleteError(columnName);
                        return null;
                    }
                }



                if (columnName == "ContactName")
                {
                    if (string.IsNullOrEmpty(this.ContactName))
                    {
                        var err = "Contact Name Is Requered";
                        errors.AddError(columnName, err);
                        return err;

                    }
                    else
                    {
                        errors.DeleteError(columnName);
                        return null;
                    }

                }


                if (columnName == "Address")
                {
                    if (string.IsNullOrEmpty(this.Address))
                    {
                        var err = "Address Is Requered !";
                        errors.AddError(columnName, err);
                        return err;
                    }
                    else
                    {
                        errors.DeleteError(columnName);
                        return null;
                    }
                }

                return null;
            }
        }

        public CollectionView CitySourceView { get; private set; }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }

        }
    }
}
