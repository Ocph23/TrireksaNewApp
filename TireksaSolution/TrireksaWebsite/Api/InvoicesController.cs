using TrireksaAppContext;
using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace TrireksaWebsite.Api
{
   [Authorize]
    public class InvoicesController : ApiController
    {
        private InvoicesContext context = new InvoicesContext();

        // GET: api/Invoices
        [HttpDelete]
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


        [HttpGet]
        public IHttpActionResult Get(DateTime start, DateTime end)
        {
            try
            {
                return Ok(context.Get(start,end));
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
                return Ok(context.Get(Id));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }


        [HttpPost]
        [Authorize(Roles = "Accounting")]
        public IHttpActionResult Post(invoice t)
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

        [HttpPut]
        [Authorize(Roles = "Accounting")]
        public IHttpActionResult UpdateDeliveryDataAction(int Id, invoice t)
        {
            try
            {
                return Ok(context.UpdateDeliveryDataAction(Id,t));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotAcceptable, ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Accounting")]
        public IHttpActionResult UpdateInvoiceStatusAction(int Id, invoice t)
        {
            try
            {
                return Ok(context.UpdateInvoiceStatusAction(Id,t));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotModified, ex.Message);
            }
        }

       
        [HttpGet]
        public async Task<IHttpActionResult> GetInvoiceForPenjualanInfo(int Id)
        {
            try
            {
                return Ok(await context.GetInvoiceForPenjualanInfo(Id));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

    }
}
