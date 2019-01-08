using ModelsShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TrireksaAppContext;

namespace TrireksaWebsite.Api
{
    [Authorize]
    public class PhotosController : ApiController
    {
        // GET: api/Photos
        public async Task<IHttpActionResult> GetPhotosByPenjualanId(int Id)
        {
            PhotoContext context = new PhotoContext();
            List<IPhoto> photos = context.GetPhotoByPenjualanId(Id);
           

            foreach(var item in photos)
            {
                var path = HttpContext.Current.Server.MapPath(string.Format("~/{0}/",item.Path));
                context = new PhotoContext(path);
               item.Thumb = await context.GetThumb(item);
            }
           return Ok(photos);
        }


        public async Task<IHttpActionResult> GetPictureById(int id)
        {
            PhotoContext context = new PhotoContext();
            try
            {
                var result = context.GetPhotoById(id);
                var path = HttpContext.Current.Server.MapPath(string.Format("~/{0}/", result.Path));
                context = new PhotoContext(path);
                return Ok(await context.GetPicture(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> AddNewPhoto(Photo ph)
        {
            var pathName = "PenjualanPhotoGalery";
            ph.Path = pathName;
            var path = HttpContext.Current.Server.MapPath(string.Format("~/{0}/", pathName));
            //if (!Request.Content.IsMimeMultipartContent())
            //    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable,
            //    "This request is not properly formatted"));
            //var streamProvider = new MultipartFileStreamProvider(path);
            //var res= await Request.Content.ReadAsMultipartAsync(streamProvider);
            try
            {
                if (ph != null)
                {
                    var context = new PhotoContext(path);
                    return Ok(await context.AddNewPhoto(ph, pathName));
                }
                else
                    throw new SystemException("Tidak ada gambar yang dikirim");

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles ="Admin")]
        public async Task<IHttpActionResult> DeletePhoto(int id)
        {
            PhotoContext context = new PhotoContext();
            try
            {
                var result = context.GetPhotoById(id);
                var path = HttpContext.Current.Server.MapPath(string.Format("~/{0}/", result.Path));
                context = new PhotoContext(path);
                return Ok(await context.DeletePhoto(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
