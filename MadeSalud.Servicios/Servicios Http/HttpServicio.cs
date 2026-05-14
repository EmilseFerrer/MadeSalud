using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MadeSalud.Servicio.ServiciosHttp
{
    public class HttpServicio : IHttpServicio
    {
        private readonly HttpClient http;

        public HttpServicio(HttpClient http)
        {
            this.http = http;
        }
        public async Task<HttpRespuesta<T>> Get<T>(string url)
        {
            var response = await http.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var respuesta = await DesSerializar<T>(response);
                return new HttpRespuesta<T>(respuesta, false, response);
            }
            else
            {
                return new HttpRespuesta<T>(default, true, response);
            }

        }

        public async Task<HttpRespuesta<TResp>> Post<T, TResp>(string url, T entidad)
        {
            var JsonEnviar = JsonSerializer.Serialize(entidad);
            var contenido = new StringContent(JsonEnviar, Encoding.UTF8, "application/json");

            var response = await http.PostAsync(url, contenido);
            if (response.IsSuccessStatusCode)
            {
                var respuesta = await DesSerializar<TResp>(response);
                return new HttpRespuesta<TResp>(respuesta, false, response);
            }
            else
            {
                return new HttpRespuesta<TResp>(default, true, response);
            }
        }

        public async Task<HttpRespuesta<TResp>> Put<T, TResp>(string url, T entidad)
        {
            var jsonAEnviar = JsonSerializer.Serialize(entidad);
            var contenido = new StringContent(jsonAEnviar,
                                              System.Text.Encoding.UTF8,
                                              "application/json");

            var response = await http.PutAsync(url, contenido);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return new HttpRespuesta<TResp>(default, false, response);
                }

                var respuesta = await DesSerializar<TResp>(response);
                return new HttpRespuesta<TResp>(respuesta, false, response);
            }
            else
            {
                return new HttpRespuesta<TResp>(default, true, response);
            }
        }

        public async Task<HttpRespuesta<object>> Delete(string url)
        {
            var respuesta = await http.DeleteAsync(url);
            if (respuesta.IsSuccessStatusCode)
            {
                return new HttpRespuesta<object>(new object(), false, respuesta);
            }
            else
            {
                return new HttpRespuesta<object>(default, true, respuesta);
            }
        }
        private async Task<T?> DesSerializar<T>(HttpResponseMessage response)
        {
            var respStr = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(respStr))
                return default;

            // Si empieza con '{' o '[', asumimos que es JSON
            if (respStr.TrimStart().StartsWith("{") || respStr.TrimStart().StartsWith("["))
            {
                return JsonSerializer.Deserialize<T>(respStr,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            // Si no es JSON, devolvemos default (no error)
            return default;
        }

    }

}
