using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Presentation;
using ModelsShared.Models;
using TrireksaApp.Common;
using System.Threading;
using TrireksaApp.CollectionsBase;
using OcphSMSLib;

namespace TrireksaApp
{
    public class MainWindowVM:NotifyPropertyChanged
    {

        public MainWindowVM()
        {
            //  ResourcesBase.Token = new AuthenticationToken { access_token = @"_qJDNt55FGkP5Swzg7lSOAVU-aGe6mzaxjvTQJ2mZeb_O6AMEK0sgfkFFLc2aC8JHmV2SPgO1jk5LS3AqUaKhGiwKPz5JCsYywyHL8HaF2GdYiG6n7cgPuOQRgeeQpzAEqtWQ1QBSpv-p_cbbUKSfURLZAp5n1Opje1p-YG5dmQy6oT-zb4lq9aTYJaCWszLiJKed1ufWl1iYRYIadX6Ets_DkcgsCf_QgofKE2XXtMQavjkJQvXiBz2-CvQYZYL57sfS_7jXtOanPjIKAoO8_5ooZV8GPYvKHwHnH95005hbuOeQQvsEWu6jOzky0IALR0o1L0e5iY0Az8MufJzh_ywow8FXjuguOU6VatN-cZTIJU-VhcIA9I-94gX2PyoDjwCIsHU3pu0qp1CpDtj7hs6Oh4ysrhYN4mAsneNjhTBSbgKRzrxNJr7M7dTmjUuzH1WoKdl6aGbVSAggFYWncT6a_Y-d2Iw_bnAl0tCtyc21GuA7hgDXkMj4QhUvujrsCRR_k4EsHONwtfQQ8jRxY63u8Z87xUtWH0U7xfG_7k",
            //     token_type = "bearer", expires_in = 120955 };

            SystemConfiguration = new AppConfiguration();
            MessageCollection = new MessageCollection();
            CityCollection = new CollectionsBase.CityCollection();
            CustomerCollection = new CollectionsBase.CustomerCollection();
            PortCollection = new CollectionsBase.PortCollection();
            AgentCollection = new CollectionsBase.AgentCollection();
            ShipCollection = new CollectionsBase.ShipCollection();
            ManifestOutgoingCollection = new CollectionsBase.ManifestOutGoingCollection();
            UserProfileCollections = new CollectionsBase.UserPorfileCollection();
            InvoiceCollections = new CollectionsBase.InvoiceCollection();
            PenjualanCollection = new CollectionsBase.PenjualanCollection();
            PricesCollection = new CollectionsBase.PricesCollection();
            this.SMS = new SMSClient();
            this.signalClient = new SignalRClient();
            
          
            
        }

        public CollectionsBase.MessageCollection MessageCollection { get; set; }
        public CollectionsBase.CityCollection CityCollection { get; set; }
        public CollectionsBase.CustomerCollection CustomerCollection { get; set; }
        public CollectionsBase.PenjualanCollection PenjualanCollection { get; set; }
        public PricesCollection PricesCollection { get; private set; }
        public CollectionsBase.PortCollection PortCollection { get; set; }
        public CollectionsBase.AgentCollection AgentCollection { get; private set; }
        public CollectionsBase.ShipCollection ShipCollection { get; set; }

        public CollectionsBase.ManifestOutGoingCollection ManifestOutgoingCollection { get; set; }
        public CollectionsBase.UserPorfileCollection UserProfileCollections { get; set; }
        public InvoiceCollection InvoiceCollections { get; private set; }
        public SMSClient SMS { get; private set; }

        public SignalRClient signalClient { get; set; }

        public AppConfiguration SystemConfiguration { get;  set; }
    }
}
