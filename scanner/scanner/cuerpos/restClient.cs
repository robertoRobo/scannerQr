using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace scanner.cuerpos
{
    class restClient
    {
         HttpClient client;

        public restClient() {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 2500;
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("Content-type", "application/json");
            //client.BaseAddress = new Uri("http://192.168.1.70:3000/orden");
        }
        public  async Task<orden> getOrden(String codigoElemento) {
            itemCodigo codigo = new itemCodigo {
                    code = codigoElemento
            };
            string json = JsonConvert.SerializeObject(codigo, Formatting.Indented);
            StringContent Content = new StringContent(json, Encoding.UTF8, "application/json");
            Uri RequestUri = new Uri("http://192.168.1.70:3000/orden");
            HttpResponseMessage res =   client.PutAsync(RequestUri, Content).Result;
            string result = JsonConvert.SerializeObject(res.Content.ReadAsStringAsync().Result, Formatting.Indented);
            orden find = JsonConvert.DeserializeObject<orden>(res.Content.ReadAsStringAsync().Result);
            return find;
        }

        public async Task<orden> DeleteOrden(String codigoElemento)
        {
            itemCodigo codigo = new itemCodigo
            {
                code = codigoElemento
            };
            string json = JsonConvert.SerializeObject(codigo, Formatting.Indented);
            StringContent Content = new StringContent(json, Encoding.UTF8, "application/json");
            Uri RequestUri = new Uri("http://192.168.1.70:3000/baja");
            HttpResponseMessage res = client.PutAsync(RequestUri, Content).Result;
            string result = JsonConvert.SerializeObject(res.Content.ReadAsStringAsync().Result, Formatting.Indented);
            orden find = JsonConvert.DeserializeObject<orden>(res.Content.ReadAsStringAsync().Result);
            return find;
        }

    }
}
