using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using TrireksaAppContext;
using ModelsShared.Models;

namespace TrireksaWebsite.Api
{

  [Authorize]
    public class CityController : ApiController
    {
        private CityContext context = CityContext.Instance;

        // GET: api/City
       
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

        // GET: api/City/5
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

        // POST: api/City
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Post([FromBody]city value)
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
                return Content(HttpStatusCode.NotFound, ex.Message);
            }

        }

        // PUT: api/City/5

        [Authorize(Roles = "Admin")]
        public IHttpActionResult Put(int id, [FromBody]city value)
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

        // DELETE: api/City/5
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
