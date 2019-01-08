using TrireksaAppContext;
using ModelsShared.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace TrireksaWebsite.Api
{

    [Authorize]
    public class PricesController : ApiController
    {
        private PricesContext context = new PricesContext();

        [HttpPost]
        public async  Task<IHttpActionResult> GetPricesByCustomer(Prices prices)
        {
            try
            {
             //   var str = await Request.Content.ReadAsStringAsync();
               // var price = JsonConvert.DeserializeObject<Prices>(str);
                return Ok( await context.GetPricesByCustomer(prices));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [Roles("Admin","Manager")]
        [HttpPost]
        public async Task<IHttpActionResult> SetPrices()
        {
            try
            {
                var str = await Request.Content.ReadAsStringAsync();
                var price = JsonConvert.DeserializeObject<Prices>(str);
                return Ok(context.SetPrices(price));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotModified, ex.Message);
            }
        }
    }
}
