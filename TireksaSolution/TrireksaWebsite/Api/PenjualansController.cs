using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using ModelsShared.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using ModelsShared;
using TrireksaAppContext;
using System.Net.Http;
using System.IO;

namespace TrireksaWebsite.Api
{
     [Authorize]
    public class PenjualansController : ApiController
    {
        // GET: api/Penjualans
        PenjualanContext context= new PenjualanContext();

        public IHttpActionResult Get(DateTime startDate,DateTime endDate)
        {
            try
            {
                return Ok(context.Get(startDate,endDate));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }



        // POST: api/Penjualans
        [HttpGet]
        public IHttpActionResult NewSTT()
        {
            try
            {
                    return Ok(context.NewSTT());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotImplemented, ex.Message);
            }
        }


        [Roles("Admin")]
        [HttpPost]
        public IHttpActionResult Post(penjualan value)
        {
            try
            {
                return Ok(context.InsertAndGetItem(value));
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [Roles("Manager")]
        public IHttpActionResult Put(int id,penjualan value)
        {
            try
            {
                return Ok(context.Update(id,value));
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }



        [HttpGet]
        public IHttpActionResult  GetByParameter(int agentId ,PortType type)
        {
            try
            {
                var result = context.GetByParameter(agentId,type);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.NotImplemented, ex.Message);
            }
        }

        public IHttpActionResult Get(int Id)
        {
            try
            {
                return Ok(context.Get(Id));
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.NotImplemented, ex.Message);
            }
        }

        public IHttpActionResult GetById(int Id)
        {
            try
            {
                return Ok(context.GetById(Id));
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.NotImplemented, ex.Message);
            }
        }


        [Authorize(Roles = "Manager")]
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult GetPenjualanNotPaid(int Id)
        {
            try
            {
                return Ok(context.GetPenjualanNotPaid(Id));
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.NotImplemented, ex.Message);
            }
        }



       [Roles("Agent","Operational")]
       [HttpPut]
        public async Task<IHttpActionResult> UpdateDeliveryStatus(deliverystatus obj)
        {
         
            var UserManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            obj.UserID = user.Id;
            try
            {
                return Ok(context.UpdateDeliveryStatus(obj));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotModified, ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateDeliveryStatusBySTT(int id, deliverystatus obj)
        {

            var UserManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            obj.UserID = user.Id;

            try
            {
                return Ok(context.UpdateDeliveryStatusBySTT(id,obj));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        
        [HttpGet]
        public  IHttpActionResult IsSended(int Id)
        {
            try
            {
                return Ok(context.IsSended(Id));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotModified, ex.Message);
            }
        }

        [Roles("Admin","Manager")]
        [HttpGet]
        public IHttpActionResult GetPenjualanFromTo(DateTime start,DateTime ended)
        {
            try
            {
                return Ok(context.GetPenjualanFromTo(start,ended));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotModified, ex.Message);
            }
        }

        
    }
}
