using TrireksaAppContext;
using ModelsShared.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;

namespace TrireksaWebsite.Api
{
    [Authorize]
    public class customersController : ApiController
    {
        // GET: api/customers


        private CustomersContext context = CustomersContext.Instance;
        
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(context.Get());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // GET: api/customers/5
        [Authorize]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(context.Get(id));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // POST: api/customers
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Post([FromBody] customer value)
        {
            try
            {
                var result = context.Post(value);
                string username = User.Identity.Name;
                context.BroadcastData(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotAcceptable, ex.Message);
            }
        }

        // PUT: api/customers/5
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Put(int id, [FromBody] ModelsShared.Models.customer value)
        {
            try
            {
                return Ok(context.Put(id,value));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotModified, ex.Message);
            }
        }

        // DELETE: api/customers/5
        [Authorize(Roles = "Manager")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                return Ok(context.Delete(id));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotModified, ex.Message);
            }
        }
    }
}
