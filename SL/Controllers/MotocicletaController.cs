using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class MotocicletaController : ControllerBase
    {
        [HttpGet]
        [Route("api/Motocicleta/GetAll")]

        public IActionResult GetAll()
        {
            ML.Motocicleta motocicleta = new ML.Motocicleta();  
            ML.Result result = BL.Motocicleta.GetAll(motocicleta);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost]
        [Route("api/Motocicleta/Add")]
        public IActionResult Post([FromBody] ML.Motocicleta motocicleta)
        {
            ML.Result result = BL.Motocicleta.Add(motocicleta);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
        [HttpPut]
        [Route("api/Motocicleta/update/{IdMotocicleta}")]
        public IActionResult Put(int IdMotocicleta, [FromBody] ML.Motocicleta motocicleta)
        {
            motocicleta.IdMotocicleta = IdMotocicleta;
            ML.Result result = BL.Motocicleta.Update(motocicleta);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpDelete]
        [Route("api/Motocicleta/delete/{IdMotocicleta}")]

        public IActionResult Delete(int IdMotocicleta)
        {
            ML.Motocicleta motocicleta = new ML.Motocicleta();
            motocicleta.IdMotocicleta = IdMotocicleta;
            var result = BL.Motocicleta.Delete(motocicleta);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("api/Motocicleta/getById/{IdMotocicleta}")]
        public IActionResult GetById(int IdMotocicleta)
        {
            var result = BL.Motocicleta.GetId(IdMotocicleta);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
