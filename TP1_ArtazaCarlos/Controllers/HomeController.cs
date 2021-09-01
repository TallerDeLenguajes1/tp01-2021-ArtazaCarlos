using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TP1_ArtazaCarlos.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net;
using System.IO;

namespace TP1_ArtazaCarlos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Problema1()
        {
            return View();
        }
        public int Problema1_cod(int a)
        {
            return a * a;
        }

        public IActionResult Problema2()
        {
            return View();
        }
        public float Problema2_cod(float a, float b)
        {
            return a/b;
        }

        public void Problema3()
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
