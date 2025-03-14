using Newtonsoft.Json;
using System.ServiceModel;
using ModelliMeteo;

namespace SOAP.BusinessLogic
{
    public class SOAPService
    {
        [ServiceContract]
        public interface ISoapService
        {
            [OperationContract]
            Giorni RicercaPerGiorno(string localita, int giorno);
        }


        public class SoapService : ISoapService
        {
            public Giorni RicercaPerGiorno(string localita, int gCercato)
            {
                Giorni giorno = new Giorni();
                string URI = "https://www.meteotrentino.it/protcivtn-meteo/api/front/previsioneOpenDataLocalita?";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = client.GetAsync(URI).Result)
                    {
                        using (HttpContent content = response.Content)
                        {
                            string result = content.ReadAsStringAsync().Result;

                            RootMeteo rootMeteo = JsonConvert.DeserializeObject<RootMeteo>(result);

                            foreach (var local in rootMeteo.previsione)
                            {
                                if (local.localita == localita)
                                {
                                    foreach (var g in local.giorni)
                                    {
                                        if (g.idPrevisioneGiorno == gCercato)
                                        {
                                            giorno = g;
                                            giorno.localita = local.localita;
                                        }
                                    }
                                }
                            }
                            return giorno;
                        }
                    }
                }
            }
        }
    }
}
