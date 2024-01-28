using FrontendApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FrontendApi.Controllers
{
    public class ClienteController : Controller
    {
        private readonly HttpClient _httpClient;

        public ClienteController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ActionResult> Index()
        {
            
            string apiUrlD = "https://localhost:7075/api/cliente"; // URL de la API

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


      

        public async Task<IActionResult> CreateUpdateCliente(int id)
        {

            if(id == 0)
            {
                return View();

            }else{

                string apiUrlD = "https://localhost:7075/api/cliente/" + id; // URL de la API

                try
                {
                    HttpResponseMessage response = await _httpClient.GetAsync(apiUrlD);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Cliente cliente = JsonConvert.DeserializeObject<Cliente>(responseBody);

                        return View(cliente);
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

     


        public async Task<IActionResult> Create(Cliente cliente)
        {

            string apiUrlD = "https://localhost:7075/api/estudiante";


            string jsonCliente = JsonConvert.SerializeObject(cliente);

            var content = new StringContent(jsonCliente, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(apiUrlD, content);

            return RedirectToAction("Index");

        }



        public async Task<IActionResult> AsignarProyectoClienteView(int id)
        {
            string apiUrlD = "https://localhost:7075/api/Proyecto"; // URL de la API

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrlD);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    List<Proyecto> proyectos = JsonConvert.DeserializeObject<List<Proyecto>>(responseBody);

                    var asignarProyectos = new ProyectosCliente
                    {
                        idCliente = id,
                        proyectos = proyectos
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
        public async Task<IActionResult> ProcesarCursoSeleccionado(int idCliente, int ProyectoSeleccionado)
        {
            AsignarProyectoCliente asignarProyectoCliente = new AsignarProyectoCliente
            {
                idCliente = idCliente,
                idProyecto = ProyectoSeleccionado
            };

            string apiUrlD = "https://localhost:7075/api/cliente/asignarproyecto";

            string jsonAsignarProyectos = JsonConvert.SerializeObject(asignarProyectoCliente);

            var content = new StringContent(jsonAsignarProyectos, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(apiUrlD, content);

            return RedirectToAction("Index");
        }
    }
}
