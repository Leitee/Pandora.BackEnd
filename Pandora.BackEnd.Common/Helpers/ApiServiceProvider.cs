using Newtonsoft.Json;
using Pandora.BackEnd.Common;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Web;

namespace Pandora.BackEnd.Common.Helpers
{
    /// <summary>
    /// Clase utilizada para las llamadas a las operaciones de las apis.-
    /// </summary>
    /// <typeparam name="Request"></typeparam>
    /// <typeparam name="Response"></typeparam>
    public static class ApiServiceProvider<Request, Response>
    {
        /// <summary>
        ///   Metodo que llama a la api.
        /// </summary>
        /// <example>
        ///   A continuacion se muestra un ejemplo de como llamar al metodo de este provider.
        ///   var _token = "AQIC5wM2LY4SfcwMCRICEAKPdWld7LCdUmYTQ69q1DHVIYE";
        ///   var req = new EmpleadoRequestDTO();
        ///   var response = ApiServiceProvider<EmpleadoRequestDTO, EmpleadoResponseDTO>.callApiHttp("http://server/api/resource", req, _token);
        /// </example>
        /// <param name="uriApi"></param>
        /// <param name="requestBody"></param>
        /// <param name="token"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Response CallApiHttp(string uriApi, Request requestBody, string token, CallAPIHttpMethod method = CallAPIHttpMethod.Unspecified)
        {
            HttpResponseMessage response;
            var handler = new HttpClientHandler()
            {
                PreAuthenticate = true,
                UseDefaultCredentials = true,
                MaxAutomaticRedirections = 100,
                CookieContainer = new CookieContainer()
            };

            using (var client = new HttpClient(handler))
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                //NOTE: Agrego la posibilidad de Serializar objetos con Jil
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Inyecto token en Header
                client.DefaultRequestHeaders.Add(RequestHeadersKeys.AUTHORIZATION_TOKEN, token);
                client.DefaultRequestHeaders.Add(RequestHeadersKeys.CORRELATION_ID, Trace.CorrelationManager.ActivityId.ToString());
                //client.DefaultRequestHeaders.Add(RequestHeadersKeys.USER_HOSTNAME, HttpContext.Current.Request.UserHostAddress);
                //NOTE: Esto lo utiliza el Access Filter en la API
                //client.DefaultRequestHeaders.Add(RequestHeadersKeys.USER_IDENTITY_NAME, HttpContext.Current.User.Identity.Name);

                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(System.Threading.Thread.CurrentThread.CurrentCulture.Name));
                client.Timeout = new System.TimeSpan(0, 30, 0);

                switch (method)
                {
                    case CallAPIHttpMethod.Get:
                        response = client.GetAsync(uriApi).Result;
                        break;
                    case CallAPIHttpMethod.Post:
                        response = client.PostAsJsonAsync(uriApi, requestBody).Result;
                        break;
                    case CallAPIHttpMethod.Put:
                        response = client.PutAsJsonAsync(uriApi, requestBody).Result;
                        break;
                    case CallAPIHttpMethod.Delete:
                        response = client.DeleteAsync(uriApi).Result;
                        break;
                    case CallAPIHttpMethod.Unspecified:
                    default:
                        response = requestBody == null ? client.GetAsync(uriApi).Result : client.PostAsJsonAsync(uriApi, requestBody).Result;
                        break;
                }

            }

            string message = string.Empty;
            string reasonPhrase = response.ReasonPhrase;
            List<ValidationResourceMessage> validationMessages = null;

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Accepted:
                case HttpStatusCode.Created:
                    //return response.Content.ReadAsAsync<Response>().Result;
                case HttpStatusCode.BadRequest:
                    if (response.ReasonPhrase == ExceptionNames.BL_VALIDATION_EXCEPTION)
                    {
                        message = response.Content.ReadAsStringAsync().Result;
                        validationMessages = JsonConvert.DeserializeObject<List<ValidationResourceMessage>>(message);
                    }
                    else
                    {
                        message = response.Content.ReadAsStringAsync().Result;
                    }
                    break;
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                    if (response.ReasonPhrase == ExceptionNames.UNAUTHORIZED_EXCEPTION)
                    {
                        message = response.Content.ReadAsStringAsync().Result;
                        validationMessages = JsonConvert.DeserializeObject<List<ValidationResourceMessage>>(message);
                    }
                    else
                    {
                        message = response.Content.ReadAsStringAsync().Result;
                    }
                    break;
                case HttpStatusCode.InternalServerError:
                default:
                    message = response.Content.ReadAsStringAsync().Result;
                    //throw new Exception(response.ReasonPhrase + ". " + response.Content.ReadAsStringAsync().Result);
                    break;
            }

            var apiException = new APIException(message, reasonPhrase, response.StatusCode);
            if (validationMessages != null) apiException.Data.Add(KeyAndNameConst.VALIDATION_MESSAGES_KEY, validationMessages);

            throw apiException;
        }

        /// <summary>
        /// Devuelve la IP del cliente.
        /// </summary>
        /// <remarks>
        /// Método creado porque se calculaba mal para IPs provenientes de proxies (MEU-3296).
        /// </remarks>
        /// <returns>IP del cliente.</returns>
        public static string GetClientIPAddress()
        {
            string wClientIPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(wClientIPAddress)) return wClientIPAddress;

            wClientIPAddress = HttpContext.Current.Request.UserHostAddress;

            if (!string.IsNullOrEmpty(wClientIPAddress) && wClientIPAddress != "::1") return wClientIPAddress;

            foreach (IPAddress wIPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (wIPAddress.AddressFamily == AddressFamily.InterNetwork)
                    return wIPAddress.ToString();
            }

            return wClientIPAddress;
        }

    }
}
