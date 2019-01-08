using ModelsShared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TrireksaAppContext;
using ModelsShared;

namespace TrireksaWebsite.Api
{
    [Authorize]
    public class ManifestOutgoingController : ApiController
    {
        private OutgoingContext context = new OutgoingContext();

        [Authorize(Roles = "Manager")]
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    
        public IEnumerable<manifestoutgoing> Get()
        {
            return context.Get();
        }


        public IEnumerable<manifestoutgoing> GetByMount(int month)
        {
            return context.GetByMount(month);
        }

        public manifestoutgoing Get(int Id)
        {
            return context.Get(Id);
        }

        [HttpPost]
        [Authorize(Roles = "Operational")]
        public manifestoutgoing Post(manifestoutgoing t)
        {
            return context.InsertAndGetItem(t);
        }

        [HttpPost]
        [Authorize(Roles = "Operational")]
        public IHttpActionResult UpdateInformation(ManifestInformation obj)
        {
            try
            {
                var result = context.InsertInformation(obj);
                  return Ok(result);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadGateway, ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult ManifestsByPenjualanId(int Id)
        {
            var result= context.ManifestsByPenjualanId(Id);
            if (result!=null)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NotAcceptable, "Can Not Insert/Update Information");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Operational")]
        public async Task<IHttpActionResult> UpdateOrigin()
        {
            try
            {
                var result =await Request.Content.ReadAsStringAsync();
                var manifest = JsonConvert.DeserializeObject<manifestoutgoing>(result);
               return Ok(context.UpdateOrigin(manifest));
               
            }
            catch (Exception ex)
            {
                return Content( HttpStatusCode.NotAcceptable, ex.Message);
            }
        }

        [HttpPut]
        [Roles("Operational", "Agent")]
        public async Task<IHttpActionResult> UpdateDestination()
        {
            try
            {
                var result = await Request.Content.ReadAsStringAsync();
                var manifest = JsonConvert.DeserializeObject<manifestoutgoing>(result);
                return Ok(context.UpdateDestination(manifest));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotAcceptable, ex.Message);
            }
        }


        [HttpGet]
      
        public async Task<IHttpActionResult> GetTitipanKapal (int Id)
        {
            var result = await context.GetTitipanKapal(Id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NoContent,"Data Tidak Ditemukan");
            }
        }



        [HttpGet]
      
        public async Task<IHttpActionResult> GetPackingList(int Id)
        {
            var result = await context.GetPackingList(Id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NoContent, "Data Tidak Ditemukan");
            }
        }




    }
}
