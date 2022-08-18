using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class MotocicletaController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly IHostingEnvironment _hostingEnvironment;

        public MotocicletaController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Motocicleta motocicleta = new ML.Motocicleta();
            ML.Result resultmotocicleta = new ML.Result();
            resultmotocicleta.Objects = new List<object>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebAPI"]);

                var responseTask = client.GetAsync("api/Motocicleta/GetAll");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync <ML.Result> ();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Motocicleta resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Motocicleta>(resultItem.ToString());
                        resultmotocicleta.Objects.Add(resultItemList);
                    }
                }
                motocicleta.Motocicletas = resultmotocicleta.Objects;
            }

            return View(motocicleta);
        }
        [HttpPost]
        public IActionResult GetAll(ML.Motocicleta motocicleta)
        {
            ML.Result result = BL.Motocicleta.GetAll(motocicleta);
            if (result.Correct)
            {
                motocicleta.Motocicletas = result.Objects.ToList();
                return View(motocicleta);
            }
            else
            {
                ViewBag.Message = "Ocurrió un error al obtener la información" + result.ErrorMessage;
                return PartialView("ValidationModal");

            }

        }

        [HttpGet]
        public ActionResult Form(int? IdMotocicleta)

        {
            ML.Motocicleta motocicleta = new ML.Motocicleta();
            ML.Result resultTipo = BL.Tipo.GetAll();
           



            if (IdMotocicleta == null)
            {
                ViewBag.Titulo ="Agregar una nueva moto";
                ViewBag.Accion ="Agregar";

                motocicleta.Tipo = new ML.Tipo();
                motocicleta.Tipo.Tipos = resultTipo.Objects.ToList();

                return View(motocicleta);
            }
            else
            {
                ViewBag.Titulo = "Actualizar una Moto";
                ViewBag.Accion = "Actualizar";

                ML.Result result = new ML.Result();

                motocicleta = (ML.Motocicleta)result.Object;


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["WebAPI"]);
                    var responseTask = client.GetAsync("api/Motocicleta/getById/" + IdMotocicleta);
                    responseTask.Wait();
                    var resultAPI = responseTask.Result;
                    if (resultAPI.IsSuccessStatusCode)
                    {
                        var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        ML.Motocicleta resultItemList = new ML.Motocicleta();
                        resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Motocicleta>(readTask.Result.Object.ToString());
                        result.Object = resultItemList;

                        resultItemList.Tipo.Tipos = resultTipo.Objects.ToList();

                        return View(resultItemList);
                    }
                    else
                    {
                        ViewBag.Message = result.ErrorMessage;
                        return View();
                    }

                }


            }

        }

        [HttpPost]
        public ActionResult Form(ML.Motocicleta motocicleta)
        {
            //Obtener la imagen
            IFormFile file = Request.Form.Files["IFImagen"];

            //Valido si trae la imagen
            if (file != null)
            {
                //LLama al metodo que tranforma a byte la imagen
                byte[] ImagenBytes = ConvertToBytes(file);
                //Convierte a base 64 y guardalo
                motocicleta.Imagen = Convert.ToBase64String(ImagenBytes);

            }

            if (motocicleta.IdMotocicleta == 0)
            {
                

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["WebAPI"]);

                    var postTask = client.PostAsJsonAsync<ML.Motocicleta>("api/Motocicleta/Add", motocicleta);
                    postTask.Wait();

                    var result = postTask.Result;


                    if (result.IsSuccessStatusCode)
                    {

                        ViewBag.Message = "Se ha agregado correctamente";
                        return PartialView("Modal");

                    }

                    else
                    {

                        ViewBag.Message = "No se pudo agregar";
                        return PartialView("Modal");

                    }

                }
            }
            else
            {
                

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["WebAPI"]);

                    //HTTP POST
                    var postTask = client.PutAsJsonAsync<ML.Motocicleta>("api/Motocicleta/update/" + motocicleta.IdMotocicleta, motocicleta);
                    postTask.Wait();

                    var result = postTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = " se actualizo Correctamente";
                        return PartialView("Modal");
                    }

                    else
                    {
                        ViewBag.Message = " No se ha podido actualizar";
                        return PartialView("Modal");


                    }
                }


            }
        }

        [HttpGet]
        public ActionResult Delete(int IdMotocicleta)
        {

            ML.Motocicleta motocicleta = new ML.Motocicleta();
            motocicleta.IdMotocicleta = IdMotocicleta;


           


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebAPI"]);

                //HTTP POST
                var postTask = client.DeleteAsync("api/Motocicleta/delete/" + IdMotocicleta);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "se ha eliminado correctamente";
                }
                else
                {
                    ViewBag.Message = "Error al eliminar ";

                }
                return PartialView("Modal");
            }
        }
        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }

    }
}
