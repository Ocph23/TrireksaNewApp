using ModelsShared.Models;
using System;
using System.Net;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TrireksaAppContext;
using System.Threading.Tasks;

namespace TrireksaWebsite.Api
{
    [Authorize]
    public class AgentsController : ApiController
    {
        // GET: api/Agents
        private AgentsContext context = AgentsContext.Instance;

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

        // GET: api/Agents/5


        public IHttpActionResult  Get(int id)
        {
            try
            {
                return Ok( context.Get(id));

            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // POST: api/Agents
        [HttpPost]
        [Authorize(Roles ="Manager, Admin") ]
        public IHttpActionResult Post(agent value)
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

        // PUT: api/Agents/5
        [Authorize(Roles = "Manager")]
        public IHttpActionResult Put(int id, [FromBody]agent  value)
        {
            try
            {
                return Ok(context.Put(id,value));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // DELETE: api/Agents/5
        [Authorize(Roles ="Manager")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                return Ok(context.Delete(id));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

    

      
       
    }
}
