using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TrireksaApp.Repository;
using Newtonsoft.Json;
using System.Net;
using System.Configuration;

namespace TrireksaApp.Common
{
    public class Client : IDisposable
    {

        public HttpClient ClientContext { get; set; }
       
        public Client()
        {
            string BaseUri = ConfigurationManager.AppSettings.Get("host");
            ClientContext = new HttpClient();
         //   ClientContext.Timeout = TimeSpan.FromMilliseconds(10);
            ClientContext.BaseAddress = ClientContext.BaseAddress = new Uri(BaseUri);
            ClientContext.DefaultRequestHeaders.Accept.Clear();
            if (ResourcesBase.Token != null)
            {
                ClientContext.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                ClientContext.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(ResourcesBase.Token.token_type, ResourcesBase.Token.access_token);
            }
        }

        public Client(string controller)
        {
            string BaseUri = ConfigurationManager.AppSettings.Get("host")+ "/api/" + controller + "/";
            ClientContext = new HttpClient();
         //   ClientContext.Timeout = TimeSpan.FromMilliseconds(10);
            ClientContext.BaseAddress = new Uri(BaseUri);
            ClientContext.DefaultRequestHeaders.Accept.Clear();

            if (ResourcesBase.Token != null)
            {
                ClientContext.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                ClientContext.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(ResourcesBase.Token.token_type, ResourcesBase.Token.access_token);
            }
        }


        public void Dispose()
        {
           
        }

        internal async Task<T> GetAsync<T>(string uri,int id)
        {
            try
            {
                var response = await ClientContext.GetAsync(uri + "/" + id);
                if (response.IsSuccessStatusCode)
                {
                    return ConvertResponseToObject<T>(response);
                }
            }
            catch (AggregateException ex)
            {
                ResourcesBase.ShowMessageError(ex.Message);
            }
            catch (WebException ex)
            {
                ResourcesBase.ShowMessageError(ex.Message);
            }
            catch (Exception ex)
            {
                ResourcesBase.ShowMessageError(ex.Message);
            }

            return default(T);
        }

        internal async Task<T> GetAsync<T>(string uri)
        {
           
            try
            {
                var response = await ClientContext.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return ConvertResponseToObject<T>(response);
                }
            }
            catch (AggregateException ex)
            {
                ResourcesBase.ShowMessageError(ex.Message);
            }
            catch (WebException ex)
            {
                ResourcesBase.ShowMessageError(ex.Message);
            }
            catch (Exception ex)
            {
                ResourcesBase.ShowMessageError(ex.Message);
            }

            return default(T);
        }

        internal async Task<T> PostAsync<T>(string uri, T item)
        {
            try
            {
                var response = await ClientContext.PostAsJsonAsync<T>(uri, item);
                if (response.IsSuccessStatusCode)
                {
                    return ConvertResponseToObject<T>(response);
                }
                AnotherResponse(response);
            }
            catch (AggregateException ex)
            {
                ResourcesBase.ShowMessageError(ex.Message);
            }
            catch (WebException ex)
            {
                ResourcesBase.ShowMessageError(ex.Message);
            }
            catch (Exception ex)
            {
                ResourcesBase.ShowMessageError(ex.Message);
            }

            return default(T);
        }

        private void AnotherResponse(HttpResponseMessage response)
        {
            string message = response.Content.ReadAsStringAsync().Result;
            switch (response.StatusCode)
            {

                //case HttpStatusCode.Continue:
                //    break;
                //case HttpStatusCode.SwitchingProtocols:
                //    break;
                //case HttpStatusCode.OK:
                //    break;
                //case HttpStatusCode.Created:
                //    break;
                //case HttpStatusCode.Accepted:
                //    break;
                //case HttpStatusCode.NonAuthoritativeInformation:
                //    break;
                //case HttpStatusCode.NoContent:
                //    break;
                //case HttpStatusCode.ResetContent:
                //    break;
                //case HttpStatusCode.PartialContent:
                //    break;
                //case HttpStatusCode.MultipleChoices:
                //    break;
                
                //case HttpStatusCode.MovedPermanently:
                //    break;
                
                case HttpStatusCode.Found:
                    ResourcesBase.ShowMessageError("Data Tidak Ditemukan");
                    break;
              
                case HttpStatusCode.SeeOther:
                    break;
               
                case HttpStatusCode.NotModified:
                    ResourcesBase.ShowMessageError("Data Tidak Tersimpan");
                    break;
                //case HttpStatusCode.UseProxy:
                //    break;
                //case HttpStatusCode.Unused:
                //    break;
                //case HttpStatusCode.TemporaryRedirect:
                //    break;
                case HttpStatusCode.BadRequest:
                  
                    ResourcesBase.ShowMessageError(message);

                    break;
                case HttpStatusCode.Unauthorized:
                        ResourcesBase.ShowMessageError("Anda Tidak Memiliki Access");
                    break;
                //case HttpStatusCode.PaymentRequired:
                //    break;
                //case HttpStatusCode.Forbidden:
                //    break;
                case HttpStatusCode.NotFound:
                    ResourcesBase.ShowMessageError("Alamat Request Tidak Ditemukan");
                    break;
                //case HttpStatusCode.MethodNotAllowed:
                //    break;
                //case HttpStatusCode.NotAcceptable:
                //    break;
                //case HttpStatusCode.ProxyAuthenticationRequired:
                //    break;
                case HttpStatusCode.RequestTimeout:
                    ResourcesBase.ShowMessageError("Waktu Request Terlalu Lama");
                    break;
                //case HttpStatusCode.Conflict:
                //    break;
                //case HttpStatusCode.Gone:
                //    break;
                //case HttpStatusCode.LengthRequired:
                //    break;
                //case HttpStatusCode.PreconditionFailed:
                //    break;
                //case HttpStatusCode.RequestEntityTooLarge:
                //    break;
                //case HttpStatusCode.RequestUriTooLong:
                //    break;
                //case HttpStatusCode.UnsupportedMediaType:
                //    break;
                //case HttpStatusCode.RequestedRangeNotSatisfiable:
                //    break;
                //case HttpStatusCode.ExpectationFailed:
                //    break;
                //case HttpStatusCode.UpgradeRequired:
                //    break;
                //case HttpStatusCode.InternalServerError:
                //    break;
                //case HttpStatusCode.NotImplemented:
                //    break;
                //case HttpStatusCode.BadGateway:
                //    break;
                //case HttpStatusCode.ServiceUnavailable:
                //    break;
                //case HttpStatusCode.GatewayTimeout:
                //    break;
                //case HttpStatusCode.HttpVersionNotSupported:
                //    break;
                default:
                    message = response.Content.ReadAsStringAsync().Result;
                    ResourcesBase.ShowMessageError(message);
                    break;
            }
        }

        internal async Task<T> Delete<T>(string uri, int id)
        {
          try
            {
                var response = await ClientContext.DeleteAsync(uri + "/" + id);
                if (response.IsSuccessStatusCode)
                {
                    return ConvertResponseToObject<T>(response);
                }
                AnotherResponse(response);
            }
            catch (AggregateException ex)
            {
                ResourcesBase.ShowMessageError(ex.Message);
            }
            catch (WebException ex)
            {
                ResourcesBase.ShowMessageError(ex.Message);
            }
            catch (Exception ex)
            {
                ResourcesBase.ShowMessageError(ex.Message);
            }

            return default(T);

        }

        internal async Task<T> PutAsync<T>(string uri, object id,T content)
        {
            try
            {
                var objJson = JsonConvert.SerializeObject(content);
                var response = await ClientContext.PutAsJsonAsync<T>(uri + "/" + id, content);
                if (response.IsSuccessStatusCode)
                {
                    return ConvertResponseToObject<T>(response);
                }
                AnotherResponse(response);
            }
            catch (AggregateException ex)
            {
                ResourcesBase.ShowMessageError(ex.Message);
            }
            catch (WebException ex)
            {
                ResourcesBase.ShowMessageError(ex.Message);
            }
            catch (Exception ex)
            {
                ResourcesBase.ShowMessageError(ex.Message);
            }

            return default(T);
        }

        public T ConvertResponseToObject<T>(HttpResponseMessage response)
        {
            var strvalue = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(strvalue);
        }
    }
}
