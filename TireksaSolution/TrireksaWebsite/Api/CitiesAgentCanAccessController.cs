using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using ModelsShared.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TrireksaAppContext;

namespace TrireksaWebsite.Api
{
    [Authorize(Roles = "Admin")]
    public class CitiesAgentCanAccessController : ApiController
    {
        // GET: api/CitiesAgentCanAccess
        private CitiesAgentCanAccessContext context = new CitiesAgentCanAccessContext();
      
        // GET: api/CitiesAgentCanAccess/5
        [Authorize]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(context.Get(id));
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.NotAcceptable, ex.Message);
            }
        }

        // POST: api/CitiesAgentCanAccess
        public IHttpActionResult Post([FromBody]CityAgentCanAccess value)
        {
            try
            {
                return Ok(context.Post(value));
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.NotAcceptable, ex.Message);
            }
        }

        // PUT: api/CitiesAgentCanAccess/5
     
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/CitiesAgentCanAccess/5
     
        public IHttpActionResult Delete(int id)
        {
            try
            {
                return Ok(context.Delete(id));
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.NotAcceptable, ex.Message);
            }
        }

        public IHttpActionResult OnChangeItemTrue (CityAgentCanAccess obj)
        {
            try
            {
                return Ok(context.OnChangeItemTrue(obj));
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.NotAcceptable, ex.Message);
            }

        }

        public  IHttpActionResult OnChangeItemFalse(CityAgentCanAccess obj)
        {

            try
            {
                return Ok(context.OnChangeItemFalse(obj));
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.NotAcceptable, ex.Message);
            }

        }

    }
}
