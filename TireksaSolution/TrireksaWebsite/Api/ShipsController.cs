using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ModelsShared.Models;
using TrireksaAppContext;

namespace TrireksaWebsite.Api
{


    [Authorize]
    public class ShipsController : ApiController
    {
        private ShipsContext context = new ShipsContext();
        // GET: api/Ships
        [Authorize(Roles = "Manager")]
        [HttpDelete]
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

        [HttpGet]
        public IHttpActionResult Get(int Id)
        {
            try
            {
                return Ok(context.GetById(Id));
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

      


        [Authorize(Roles = "Admin")]
        public IHttpActionResult Post([FromBody] ship t)
        {
            try
            {
                return Ok(context.InsertAndGetItem(t));
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.NotAcceptable, ex.Message);
            }
        }



        [Roles("Admin", "Manager")]
        public IHttpActionResult Put(int id, [FromBody] ship value)
        {
            try
            {
                return Ok(context.UpdateAndGetItem(value));
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.NotModified, ex.Message);
            }
        }

    
    }
}
