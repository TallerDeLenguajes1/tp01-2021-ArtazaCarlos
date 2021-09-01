using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net;
using System.IO;
using System.Collections.Generic;

namespace Pruebas
{
    class Program
    {
        static void Main(string[] args)
        {
                var url = "https://apis.datos.gob.ar/georef/api/provincias?campos=id,nombre";
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Accept = "application/json";


                try
                {
                    using (WebResponse response = request.GetResponse())
                    {
                        using (Stream strReader = response.GetResponseStream())
                        {
                            if (strReader == null)
                            {
                                Console.WriteLine("Error en la consulta");
                            }
                            using (StreamReader objReader = new StreamReader(strReader))
                            {
                                string responseBody = objReader.ReadToEnd();
                                ProvinciasArg provinciasArg = JsonSerializer.Deserialize<ProvinciasArg>(responseBody);
                                foreach (Provincia prov in provinciasArg.Provincias)
                                {
                                    Console.WriteLine(prov.Id + " " + prov.Nombre);
                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            
        }

        public class ProvinciasArg
        {
            [JsonPropertyName("cantidad")]
            public int Cantidad { get; set; }

            [JsonPropertyName("inicio")]
            public int Inicio { get; set; }

            [JsonPropertyName("parametros")]
            public Parametros Parametros { get; set; }

            [JsonPropertyName("provincias")]
            public List<Provincia> Provincias { get; set; }

            [JsonPropertyName("total")]
            public int Total { get; set; }
        }

        public class Parametros
        {
            [JsonPropertyName("campos")]
            public List<string> Campos { get; set; }
        }

        public class Provincia
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("nombre")]
            public string Nombre { get; set; }
        }
    }  
}
