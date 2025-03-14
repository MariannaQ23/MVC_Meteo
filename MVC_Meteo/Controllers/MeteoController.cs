using Microsoft.AspNetCore.Mvc;
using ServiziMeteo;
using Newtonsoft.Json;

namespace MVC_Meteo.Controllers
{  
    public class MeteoController : Controller
   {
        [HttpGet]
        public async Task<IActionResult> Meteo()
        {
            var meteo = await ServiziMeteo.ServiziMeteo.DaiMeteo();
            
            return View(meteo);
        }
     
        [HttpGet]
        public async Task<IActionResult> DettaglioMeteo(string localita, int idPrevisione)
        {
            var giorno= await ServiziMeteo.ServiziMeteo.DettaglioMeteo(localita, idPrevisione);
            if(giorno == null)
            {
                return NotFound();
            }
            return View(giorno);
        }

    }
}
