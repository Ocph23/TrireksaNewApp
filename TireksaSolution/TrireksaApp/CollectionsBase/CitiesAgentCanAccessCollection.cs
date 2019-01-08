using System.Threading.Tasks;
using ModelsShared.Models;
using TrireksaApp.Common;

namespace TrireksaApp.CollectionsBase
{
    public class CitiesAgentCanAccessCollection
    {
        private Client client = new Client("CitiesAgentCanAccess");


        public async Task<CityAgentCanAccess> OnChangeItemTrue(CityAgentCanAccess item)
        {
          return await client.PostAsync<CityAgentCanAccess>("OnChangeItemTrue", item);
        }

        public async Task<CityAgentCanAccess> OnChangeItemFalse(CityAgentCanAccess item)
        {
            return await client.PostAsync<CityAgentCanAccess>("OnChangeItemFalse", item);
        }
    }
}
