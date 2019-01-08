using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace TrireksaWebsite.Api
{
    public class UploadPhotoController : ApiController
    {
        [HttpPost]
        public async Task Post()
        {
         
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                "This request is not properly formatted"));
            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            foreach (HttpContent ctnt in provider.Contents)
            {

                var type = ctnt.Headers.ContentType;
                var filename = ctnt.Headers.ContentDisposition.FileName;
              

                //now read individual part into STREAM
                var stream = await ctnt.ReadAsStreamAsync();

                byte[] data = new byte[stream.Length];


                if (stream.Length != 0)
                {
                   await stream.ReadAsync(data, 0, (int)stream.Length);
                }
            }
        }

    }
}
