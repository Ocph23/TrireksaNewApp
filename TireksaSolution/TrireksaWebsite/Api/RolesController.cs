using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrireksaAppContext;
using ModelsShared.Models;

namespace TrireksaWebsite.Api
{
    [Authorize]
    public class RolesController : ApiController
    {
        RolesContext context = new RolesContext();
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var res = context.Get();
                return Ok(res);
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.NotAcceptable, ex.Message);
            }
        }



    }
}
