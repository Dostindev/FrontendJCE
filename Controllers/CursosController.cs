using FrontendApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace FrontendApi.Controllers
{
    public class CursosController : Controller
    {
        private readonly HttpClient _httpClient;

        public CursosController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ActionResult> Index()
        {
            string apiUrl = "https://localhost:7075/api/curso"; // URL de la API

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    List<Curso> cursos = JsonConvert.DeserializeObject<List<Curso>>(responseBody);

                    return View(cursos);
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


        public async Task<IActionResult> CreateUpdateCursosView(int id)
        {

            if (id == 0)
            {
                return View();

            }
            else
            {

                string apiUrl = "https://localhost:7075/api/curso/" + id; // URL de la API

                try
                {
                    HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Curso curso = JsonConvert.DeserializeObject<Curso>(responseBody);

                        return View(curso);
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




        public async Task<IActionResult> CreateAndUpdate(Curso curso)
        {

            string apiUrl = "https://localhost:7075/api/curso"; // URL de la API

            string jsonEstudiante = JsonConvert.SerializeObject(curso);

            var content = new StringContent(jsonEstudiante, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> DetalleCurso(int id)
        {

                string apiUrl = "https://localhost:7075/api/curso/estudiantesporcurso/" + id; // URL de la API

                try
                {
                    HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        List<Estudiante> estudiantes = JsonConvert.DeserializeObject<List<Estudiante>>(responseBody);

                        return View(estudiantes);
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
