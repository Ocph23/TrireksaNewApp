using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TrireksaAppContext;

namespace TrireksaWebsite.Api
{
    [Authorize]
    public class ColliesController : ApiController
    {

        ColliesContext context = new ColliesContext();
        // GET: api/Collies
      

        // GET: api/Collies/5
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                return Ok(await context.Get(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // POST: api/Collies
        public async Task<IHttpActionResult> Post(colly value)
        {
            try
            {
                return Ok(await context.Post(value));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Collies/5
        public async Task<IHttpActionResult>  Put(int id, colly value)
        {
            try
            {
                return Ok(await context.Put(value));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Collies/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                return Ok(await context.Delete(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
