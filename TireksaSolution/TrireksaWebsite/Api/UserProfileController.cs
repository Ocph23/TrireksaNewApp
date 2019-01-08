using Microsoft.AspNet.Identity.Owin;
using ModelsShared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrireksaWebsite.Models;
using System.Web;
using TrireksaAppContext;

namespace TrireksaWebsite.Api
{
  
   [Authorize]
    public class UserProfileController : ApiController
    {
        private UserProfileContext context = new UserProfileContext();


        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(context.Get());
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.NotModified, ex.Message);
            }

        }




        [Authorize]
        [HttpGet]
        public IHttpActionResult GetProfile()
        {
            var identity = User.Identity.Name;
            var result = context.GetProfile(identity);

            try
            {
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotModified, ex.Message);
            }

        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IHttpActionResult AddNewRole(string userId,roles role)
        {
            try
            {
                return Ok(context.AddNewRole(userId, role.Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        public IHttpActionResult RemoveRole(string userId, string roleId)
        {
            try
            {
                return Ok(context.RemoveRole(userId, roleId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [Roles("Admin", "Manager")]
        public async Task<IHttpActionResult> RegisterCustomer(customer cust)
        {
            var email = cust.Email;
            string code = string.Empty;
            var UserManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var SignInManager = HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = email, Email = email};
                    var result = await UserManager.CreateAsync(user, string.Concat(email, "#3Rp"));
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        //  var callbackUrl = Request.GetUrlHelper().Link  Request..Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + "\">here</a>");
                        await UserManager.AddToRoleAsync(user.Id, "Customer");
                        return Ok(code);
                    }
                   
                }
            return Content(HttpStatusCode.NotAcceptable, code);
            // If we got this far, something failed, redisplay form


        }


        [Roles("Admin", "Manager")]
        public async Task<IHttpActionResult> RegisterAgent(agent cust)
        {
            var email = cust.Email;
            string code = string.Empty;
            var UserManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var SignInManager = HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = email, Email = email };
                var result = await UserManager.CreateAsync(user, string.Concat(email, "#3Rp"));
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    //  var callbackUrl = Request.GetUrlHelper().Link  Request..Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + "\">here</a>");
                    await UserManager.AddToRoleAsync(user.Id, "Agent");
                    return Ok(code);
                }

            }
            return Content(HttpStatusCode.NotAcceptable, code);
            // If we got this far, something failed, redisplay form


        }

        public IHttpActionResult Get(int Id)
        {
            var identity = User.Identity.Name;
            try
            {
                return Ok(context.Get(Id,identity));
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.NotModified, ex.Message);
            }
        }


        [Authorize(Roles = "Administrator")]
        public IHttpActionResult Post([FromBody] userprofile t)
        {
            var identity = User.Identity.Name;
            try
            {
                return Ok(context.Post(t, identity));
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.NotModified, ex.Message);
            }

        }


        public IHttpActionResult Put(int id, [FromBody] userprofile value)
        {
            var identity = User.Identity.Name;
            try
            {
                return Ok(context.Put(id,value, identity));
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.NotModified, ex.Message);
            }

        }
        
    }
}
