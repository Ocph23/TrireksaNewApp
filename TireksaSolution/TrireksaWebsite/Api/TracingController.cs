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
    public class TracingController : ApiController
    {
        private TracingContext context = new TracingContext();
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetPenjualan(int STT)
        {
            try
            {
                return Ok(context.GetPenjualan(STT));
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }


    }
}
