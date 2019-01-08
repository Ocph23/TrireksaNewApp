using Ocph.DAL.Provider.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Incomming;
using Ocph.DAL.Repository;

namespace TrireksaAppContext.IncommingContext
{
   public  class InccomingDbContext:MySqlDbConnection, IDisposable
    {
        private string connectionString = "Server=localhost;database=trireksaincomming;UID=root;password=;CharSet=utf8;Persist Security Info=True";
        // private string connectionString = "Server=localhost;database=trireksapenjualan;UID=root;password=;CharSet=utf8;Persist Security Info=True";

        public InccomingDbContext()
        {
            this.ConnectionString = connectionString;
        }


        public IRepository<agents> Agents { get { return new Repository<agents>(this); } }
        public IRepository<customers> Customers { get { return new Repository<customers>(this); } }
        public IRepository<details> Details { get { return new Repository<details>(this); } }
        public IRepository<shipinginformation> Shiping { get { return new Repository<shipinginformation>(this); } }
        public IRepository<manifestsincomming> Manifests { get { return new Repository<manifestsincomming>(this); } }




    }
}
