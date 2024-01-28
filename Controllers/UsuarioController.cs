using FrontendApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace FrontendApi.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly HttpClient _httpClient;

        public UsuarioController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ActionResult> Index()
        {
            string apiUrlD = "https://localhost:7075/api/usuario"; // URL de la API

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrlD);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    List<Usuario> usuarios = JsonConvert.DeserializeObject<List<Usuario>>(responseBody);

                    return View(usuarios);
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

        public async Task<IActionResult> CreateUpdateUsuarioView(int id)
        {

            if (id == 0)
            {
                return View();

            }
            else
            {

                string apiUrlD = "https://localhost:7075/api/usuario/" + id; // URL de la API

                try
                {
                    HttpResponseMessage response = await _httpClient.GetAsync(apiUrlD);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Usuario usuario = JsonConvert.DeserializeObject<Usuario>(responseBody);

                        return View(usuario);
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

        public async Task<IActionResult> CreateAndUpdate(Usuario usuario)
        {

            string apiUrlD = "https://localhost:7075/api/usuario"; // URL de la API

            string jsonUsuario = JsonConvert.SerializeObject(usuario);

            var content = new StringContent(jsonUsuario, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(apiUrlD, content);

            return RedirectToAction("Index");

        }


        public async Task<IActionResult> AsignarProyectos(int id)
        {
            string apiUrlD = "https://localhost:7075/api/Proyecto/proyectosdisponibles"; // URL de la API

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrlD);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    List<Proyecto> proyectosDisponibles = JsonConvert.DeserializeObject<List<Proyecto>>(responseBody);

                    var asignarProyectos = new ProyectosDisponibles
                    {
                        idUsuario = id,
                        Proyectos = proyectosDisponibles
                    };

                    return View(asignarProyectos);
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

        public async Task<IActionResult> ProcesarProyectosSeleccionados(int idUsuario, List<int> ProyectosSeleccionados)
        {
            AsignarProyectosUsuario asignarProyectosUsuario = new AsignarProyectosUsuario
            {
                idUsuario = idUsuario,
                idsProyectos = ProyectosSeleccionados
            };

            string apiUrlD = "https://localhost:7075/api/usuario/asignar-proyecto"; // URL de la API


            string jsonAsignarProyectos = JsonConvert.SerializeObject(asignarProyectosUsuario);

            var content = new StringContent(jsonAsignarProyectos, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(apiUrlD, content);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UsuarioProyectos(int id)
        {

            if (id == 0)
            {
                return View();

            }
            else
            {

                string apiUrlD = "https://localhost:7075/api/usuario/" + id; // URL de la API

                try
                {
                    HttpResponseMessage response = await _httpClient.GetAsync(apiUrlD);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Usuario usuario = JsonConvert.DeserializeObject<Usuario>(responseBody);

                        return View(usuario);
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
}
