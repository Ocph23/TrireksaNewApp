using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsShared.Models
{
    [TableName("ships")]
    public class ship:BaseNotifyProperty
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

        [DbColumn("Description")]
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChange("Description");
            }
        }

        [DbColumn("Route")]
        public string Route
        {
            get { return _route; }
            set
            {
                _route = value;
                OnPropertyChange("Route");
            }
        }

        public List<string> RouteView {

            get
            {
                if (!string.IsNullOrEmpty(this.Route)) {
                    var x= this.Route.Split(';');
                    var list = new List<string>();
                    foreach (var item in x)
                    {
                        list.Add(item);
                    }
                    return list;
                }
                   
                else
                    return null;
            }
            set
            {
                Route = string.Empty;
                if (value != null)
                {
                    foreach (var item in value)
                    {
                        Route += item;
                        if (item.LastIndexOf(item) <= (value.Count -2))
                        {
                            Route += ";";
                        }
                    }
                }
            }

        }

        private int _id;
        private string _name;
        private string _description;
        private string _route;
    }

}
