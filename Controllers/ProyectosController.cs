using FrontendApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace FrontendApi.Controllers
{
    public class ProyectosController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProyectosController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ActionResult> Index()
        {
            string apiUrlD = "https://localhost:7075/api/proyecto"; // URL de la API

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrlD);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    List<Proyecto> proyectos = JsonConvert.DeserializeObject<List<Proyecto>>(responseBody);

                    return View(proyectos);
                }
                else
                {
                    ViewBag.Error = $"La solicitud falló con el código de estado: {response.StatusCode}";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Ocurrió un error al realizar la solicitud: {ex.Message}";
                return View();
            }
        }


        public async Task<IActionResult> CreateUpdateProyectosView(int id)
        {

            if (id == 0)
            {
                return View();

            }
            else
            {

                string apiUrlD = "https://localhost:7075/api/proyectos/" + id; // URL de la API

                try
                {
                    HttpResponseMessage response = await _httpClient.GetAsync(apiUrlD);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Proyecto proyecto = JsonConvert.DeserializeObject<Proyecto>(responseBody);

                        return View(proyecto);
                    }
                    else
                    {
                        ViewBag.Error = $"La solicitud falló con el código de estado: {response.StatusCode}";
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = $"Ocurrió un error al realizar la solicitud: {ex.Message}";
                    return View();
                }
            }

        }




        public async Task<IActionResult> CreateAndUpdate(Proyecto proyecto)
        {

            string apiUrlD = "https://localhost:7075/api/proyecto"; // URL de la API

            string jsonCliente = JsonConvert.SerializeObject(proyecto);

            var content = new StringContent(jsonCliente, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(apiUrlD, content);

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> DetalleProyecto(int id)
        {

                string apiUrlD = "https://localhost:7075/api/proyecto/clienteporproyecto/" + id; // URL de la API

                try
                {
                    HttpResponseMessage response = await _httpClient.GetAsync(apiUrlD);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        List<Cliente> clientes = JsonConvert.DeserializeObject<List<Cliente>>(responseBody);

                        return View(clientes);
                    }
                    else
                    {
                        ViewBag.Error = $"La solicitud falló con el código de estado: {response.StatusCode}";
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = $"Ocurrió un error al realizar la solicitud: {ex.Message}";
                    return View();
                }
        }
    }
}
