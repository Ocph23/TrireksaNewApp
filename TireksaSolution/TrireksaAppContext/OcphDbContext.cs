using ModelsShared.Models;
using Ocph.DAL.Provider.MySql;
using Ocph.DAL.Repository;
using System;
using System.Configuration;

namespace TrireksaAppContext
{
    public class OcphDbContext : MySqlDbConnection, IDisposable
    {
        //private string connectionString = "Server=localhost;database=trireksapenjualan;UID=root;password=;SslMode=none;CharSet=utf8;Persist Security Info=True";
        // private string connectionString = "Server=localhost;database=trireksapenjualan;UID=root;password=;CharSet=utf8;Persist Security Info=True";

        public OcphDbContext()
        {
            this.ConnectionString =   ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            
            
        }
        

        public IRepository<user> Users { get { return new Repository<user>(this); } }
        public IRepository<userroles> UserRoles { get { return new Repository<userroles>(this); } }
        public IRepository<roles> Roles { get { return new Repository<roles>(this); } }



        public IRepository<userprofile> UserProfiles { get { return new Repository<userprofile>(this); } }
        public IRepository<penjualan> Penjualans { get { return new Repository<penjualan>(this); } }
        public IRepository<colly> Collies { get { return new Repository<colly>(this); } }
        public IRepository<customer> Customers { get { return new Repository<customer>(this); } }
        public IRepository<city> Cities{ get { return new Repository<city>(this); } }
        public IRepository<port> Ports{ get { return new Repository<port>(this); } }
        public IRepository<agent> Agents{ get { return new Repository<agent>(this); } }
        public IRepository<CityAgentCanAccess> CitiesAgentCanAccess { get { return new Repository<CityAgentCanAccess>(this); } }
        public IRepository<ship> Ships{ get { return new Repository<ship>(this); } }
        public IRepository<manifestoutgoing> Outgoing { get { return new Repository<manifestoutgoing>(this); } }
        public IRepository<ManifestInformation> ManifestInformations { get { return new Repository<ManifestInformation>(this); } }
        public IRepository<packinglist> PackingLists { get { return new Repository<packinglist>(this); } }
        public IRepository<invoice> Invoices { get { return new Repository<invoice>(this); } }
        public IRepository<invoicedetail> InvoiceDetails { get { return new Repository<invoicedetail>(this); } }
        public IRepository<deliverystatus> DeliveryStatusses { get { return new Repository<deliverystatus>(this); } }
        public IRepository<Prices> Priceses { get { return new Repository<Prices>(this); } }
        public IRepository<TracingModel> Tracings { get { return new Repository<TracingModel>(this); } }
        public IRepository<photo> Photos { get { return new Repository<photo>(this); } }

    }
}
