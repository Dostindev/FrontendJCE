using FrontendApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace FrontendApi.Controllers
{
    public class ProfesorController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProfesorController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ActionResult> Index()
        {
            string apiUrl = "https://localhost:7075/api/profesor"; // URL de la API

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    List<Profesor> profesores = JsonConvert.DeserializeObject<List<Profesor>>(responseBody);

                    return View(profesores);
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



        public async Task<IActionResult> CreateUpdateProfesorView(int id)
        {

            if (id == 0)
            {
                return View();

            }
            else
            {

                string apiUrl = "https://localhost:7075/api/profesor/" + id; // URL de la API

                try
                {
                    HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Profesor profesor = JsonConvert.DeserializeObject<Profesor>(responseBody);

                        return View(profesor);
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

        public async Task<IActionResult> CreateAndUpdate(Profesor profesor)
        {

            string apiUrl = "https://localhost:7075/api/profesor"; // URL de la API

            string jsonProfesor = JsonConvert.SerializeObject(profesor);

            var content = new StringContent(jsonProfesor, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);

            return RedirectToAction("Index");

        }


        public async Task<IActionResult> AsignarCursos(int id)
        {
            string apiUrl = "https://localhost:7075/api/Curso/cursosdisponibles"; // URL de la API

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    List<Curso> cursosDisponibles = JsonConvert.DeserializeObject<List<Curso>>(responseBody);

                    var asignarCursos = new CursosDisponibles
                    {
                        idProfesor = id,
                        Cursos = cursosDisponibles
                    };

                    return View(asignarCursos);
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

        public async Task<IActionResult> ProcesarCursosSeleccionados(int idProfesor, List<int> CursosSeleccionados)
        {
            AsignarCursosProfesor asignarCursosProfesor = new AsignarCursosProfesor
            {
                idProfesor = idProfesor,
                idsCursos = CursosSeleccionados
            };

            string apiUrl = "https://localhost:7075/api/profesor/asignar-cursos"; // URL de la API


            string jsonAsignarCursos = JsonConvert.SerializeObject(asignarCursosProfesor);

            var content = new StringContent(jsonAsignarCursos, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ProfesorCursos(int id)
        {

            if (id == 0)
            {
                return View();

            }
            else
            {

                string apiUrl = "https://localhost:7075/api/profesor/" + id; // URL de la API

                try
                {
                    HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Profesor profesor = JsonConvert.DeserializeObject<Profesor>(responseBody);

                        return View(profesor);
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
