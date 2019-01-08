using TrireksaAppContext;
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
    public class DashboardController : ApiController
    {
        DashboardContext context = new DashboardContext();

        
        public async Task<IHttpActionResult> GetInvoiceNotYetPaid()
        {
            try
            {
                return Ok(await context.GetInvoiceNotYetPaid());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        public async Task<IHttpActionResult> GetPenjualanBulan(int month, int year)
        {
            try
            {
                return Ok(await context.GetPenjualanBulan(month, year));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }
        public async Task<IHttpActionResult> GetPenjualanNotHaveStatus()
        {
            try
            {
                return Ok(await context.GetPenjualanNotHaveStatus());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }

        }


        public async Task<IHttpActionResult> GetPenjualanNotYetSend()
        {
            try
            {
                return Ok(await context.GetPenjualanNotYetSend());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }


        }


        public async Task<IHttpActionResult> GetPenjualanNotPaid()
        {
            try
            {
                return Ok(await context.GetPenjualanNotPaid());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        public async Task<IHttpActionResult> GetInvoiceNotYetDelivery()
        {
            try
            {
                return Ok(await context.GetInvoiceNotYetDelivery());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }

        }

        public async Task<IHttpActionResult> GetInvoiceJatuhTempo()
        {
            try
            {
                return Ok(await context.GetInvoiceJatuhTempo());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }

        }

        public async Task<IHttpActionResult> GetInvoiceNotYetRecive()
        {
            try
            {
                return Ok(await context.GetInvoiceNotYetRecive());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }

        }


        public async Task<IHttpActionResult> GetPenjualanThreeYear()
        {
            try
            {
                return Ok(await context.GetPenjualanThreeYear());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        private async Task<IHttpActionResult> GetPenjualanOfMonth(int tahun)
        {
            try
            {
                return Ok(await context.GetInvoiceNotYetPaid());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }


    }
}
