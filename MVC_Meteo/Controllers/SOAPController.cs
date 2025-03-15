using Microsoft.AspNetCore.Mvc;
using ModelliMeteo;
using ServiziMeteo;
using SOAP.BusinessLogic;

namespace MVC_Meteo.Controllers
{
    public class SOAPController: Controller
    {
        [HttpGet]
        public async Task<IActionResult> RicercaPerGiorno(string localita, int gCercato)
        {
            Giorni giorno = await ServiziMeteo.ServiziMeteo.DettaglioMeteo(localita, gCercato);

            return View(giorno);
      
        }  
    }
    
}
