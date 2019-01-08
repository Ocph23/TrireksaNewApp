using System.Threading.Tasks;
using ModelsShared.Models;
using TrireksaApp.Common;

namespace TrireksaApp.CollectionsBase
{
    public  class PricesCollection
    {
        private Client client = new Client("Prices");

       
        internal Task<Prices> SetPrices(Prices prices)
        {
            return client.PostAsync<Prices>("SetPrices",prices);
        }

        internal async Task<double> GetPricesByCustomer(Prices prices)
        {
            var resulr= await  client.PostAsync<Prices>("GetPricesByCustomer",prices);
            if (resulr != default(Prices))
            {
                return resulr.Price;
            }
            else
                return 0;
        }
    }
}
