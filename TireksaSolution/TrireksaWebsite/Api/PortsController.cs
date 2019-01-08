using TrireksaAppContext;
using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TrireksaWebsite.Api
{
    [Authorize]
    public class PortsController : ApiController
    {
        private PortsContext context = PortsContext.Instance;
        // GET: api/Ports
        [HttpGet]
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

        // GET: api/Ports/5
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

        // POST: api/Ports
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Post(port value)
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

        // PUT: api/Ports/5
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Put(int id, port value)
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

        // DELETE: api/Ports/5
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
