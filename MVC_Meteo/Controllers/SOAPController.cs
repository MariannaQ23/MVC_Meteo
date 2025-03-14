using Microsoft.AspNetCore.Mvc;
using ModelliMeteo;
using ServiziMeteo;
using SOAP.BusinessLogic;

namespace MVC_Meteo.Controllers
{
    public class SOAPController: Controller
    {
        [HttpPost]
        public async Task<IActionResult> RicercaPerGiorno(string localita, int gCercato)
        {
            var service = new ServiziMeteo.ServiziMeteo();

            Giorni giorno = service.SOAPRicercaPerGiorno(localita, gCercato);

            return View(giorno);
      
        }  
    }
    
}
