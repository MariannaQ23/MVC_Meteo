using Newtonsoft.Json;
using ModelliMeteo;
using System.Reflection;
using static SOAP.BusinessLogic.SOAPService;
using ServiceReference1;

namespace ServiziMeteo
{
    public class ServiziMeteo
    {
        public static async Task<RootMeteo> DaiMeteo()
        {
            using (HttpClient client = new HttpClient())
            {
                string Uri = "https://www.meteotrentino.it/protcivtn-meteo/api/front/previsioneOpenDataLocalita?localita";

                HttpResponseMessage response = await client.GetAsync(Uri);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    RootMeteo rootMeteo = JsonConvert.DeserializeObject<RootMeteo>(result);
                    return rootMeteo;
                }
                else
                {
                    throw new Exception("Errore nella richiesta.");
                }

            }

        }
        

        public static async Task<ModelliMeteo.Giorni> DettaglioMeteo(string localita, int idPrevisione)
        {

            var meteo = await DaiMeteo();
            var previsione = meteo.previsione.First(s => s.localita.Equals(localita));

            var giorno = previsione.giorni.FirstOrDefault(s => s.idPrevisioneGiorno == idPrevisione);

            giorno.localita = localita;

            return giorno;
        }



        public ModelliMeteo.Giorni SOAPRicercaPerGiorno(string localita, int gCercato)
        {
            var giorno = new ModelliMeteo.Giorni();
            ServiceReference1.ISoapService soapServiceChannel = new SoapServiceClient(SoapServiceClient.EndpointConfiguration.BasicHttpBinding_ISoapService_soap);

            //richiesta per il servizio SOAP
            var request = new RicercaPerGiornoRequest
            {
                Body = new RicercaPerGiornoRequestBody()
                {
                    localita = localita,
                    giorno = gCercato
                }
            };
            RicercaPerGiornoResponse response = soapServiceChannel.RicercaPerGiornoAsync(request).Result;

            //conversione del tipo del servizio soap nel tipo per l'applicazione
            return Converter(response.Body.RicercaPerGiornoResult);
        }
      
        private ModelliMeteo.Giorni Converter(ServiceReference1.Giorni service)
        {
            ModelliMeteo.Giorni result = new ModelliMeteo.Giorni();

            result.localita = service.localita;
            result.icona = service.icona;
            result.tMaxGiorno = service.tMaxGiorno;
            result.tMinGiorno = service.tMinGiorno;
            result.giorno = service.giorno;
            result.testoGiorno = service.testoGiorno;
            result.idPrevisioneGiorno = service.idPrevisioneGiorno;
            result.descIcona = service.descIcona;
            result.fasce = new List<ModelliMeteo.Fasce>();
            foreach (var s in service.fasce)
            {
                var tmp = new ModelliMeteo.Fasce
                {
                    idPrevisioneFascia = s.idPrevisioneFascia,
                    fascia = s.fascia,
                    fasciaPer = s.fasciaPer,
                    fasciaOre = s.fasciaOre,
                    icona = s.icona,
                    descIcona = s.descIcona,
                    idPrecProb = s.idPrecProb,
                    descPrecProb = s.descPrecProb,
                    idPrecInten = s.idPrecInten,
                    descPrecInten = s.descPrecInten,
                    idTempProb = s.idTempProb,
                    descTempProb = s.descTempProb,
                    idVentoIntQuota = s.idVentoIntQuota,
                    descVentoIntQuota = s.descVentoIntQuota,
                    idVentoDirQuota = s.idVentoDirQuota,
                    descVentoDirQuota = s.descVentoDirQuota,
                    idVentoIntValle = s.idVentoIntValle,
                    descVentoIntValle = s.descVentoIntQuota,
                    idVentoDirValle = s.idVentoDirValle,
                    descVentoDirValle = s.descVentoDirValle,
                    iconaVentoQuota = s.iconaVentoQuota,
                    zeroTermico = s.zeroTermico
                };
                result.fasce.Add(tmp);
            }
            return result;
        }
    }
}
