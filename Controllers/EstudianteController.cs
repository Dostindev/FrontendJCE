using FrontendApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FrontendApi.Controllers
{
    public class EstudianteController : Controller
    {
        private readonly HttpClient _httpClient;

        public EstudianteController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ActionResult> Index()
        {
            
            string apiUrl = "https://localhost:7075/api/estudiante"; // URL de la API

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


      

        public async Task<IActionResult> CreateUpdateEstudiante(int id)
        {

            if(id == 0)
            {
                return View();

            }else{

                string apiUrl = "https://localhost:7075/api/estudiante/" + id; // URL de la API

                try
                {
                    HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Estudiante estudiante = JsonConvert.DeserializeObject<Estudiante>(responseBody);

                        return View(estudiante);
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

     


        public async Task<IActionResult> Create(Estudiante estudiante)
        {

            string apiUrl = "https://localhost:7075/api/estudiante";


            string jsonEstudiante = JsonConvert.SerializeObject(estudiante);

            var content = new StringContent(jsonEstudiante, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);

            return RedirectToAction("Index");

        }



        public async Task<IActionResult> AsignarCursoEstudianteView(int id)
        {
            string apiUrl = "https://localhost:7075/api/Curso"; // URL de la API

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    List<Curso> cursos = JsonConvert.DeserializeObject<List<Curso>>(responseBody);

                    var asignarCursos = new CursosEstudiante
                    {
                        idEstudiante = id,
                        cursos = cursos
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
        public async Task<IActionResult> ProcesarCursoSeleccionado(int idEstudiante, int CursoSeleccionado)
        {
            AsignarCursoEstudiante asignarCursoEstudiante = new AsignarCursoEstudiante
            {
                idEstudiante = idEstudiante,
                idCurso = CursoSeleccionado
            };

            string apiUrl = "https://localhost:7075/api/estudiante/asignarcurso";

            string jsonAsignarCursos = JsonConvert.SerializeObject(asignarCursoEstudiante);

            var content = new StringContent(jsonAsignarCursos, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);

            return RedirectToAction("Index");
        }

        

    }


   

}
