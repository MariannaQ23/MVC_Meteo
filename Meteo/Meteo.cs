using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelliMeteo
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Fasce
    {
        public int idPrevisioneFascia { get; set; }
        public string fascia { get; set; }
        public string fasciaPer { get; set; }
        public string fasciaOre { get; set; }
        public string icona { get; set; }
        public string descIcona { get; set; }
        public string idPrecProb { get; set; }
        public string descPrecProb { get; set; }
        public string idPrecInten { get; set; }
        public string descPrecInten { get; set; }
        public string idTempProb { get; set; }
        public string descTempProb { get; set; }
        public string idVentoIntQuota { get; set; }
        public string descVentoIntQuota { get; set; }
        public string idVentoDirQuota { get; set; }
        public string descVentoDirQuota { get; set; }
        public string idVentoIntValle { get; set; }
        public string descVentoIntValle { get; set; }
        public string idVentoDirValle { get; set; }
        public string descVentoDirValle { get; set; }
        public string iconaVentoQuota { get; set; }
        public int zeroTermico { get; set; }
    }

    public class Giorni
    {
        public string localita { get; set; }
        public int idPrevisioneGiorno { get; set; }
        public string giorno { get; set; }
        public int idIcona { get; set; }
        public string icona { get; set; }
        public string descIcona { get; set; }
        public string testoGiorno { get; set; }
        public int tMinGiorno { get; set; }
        public int tMaxGiorno { get; set; }
        public List<Fasce> fasce { get; set; }
    }

    public class Previsione
    {
        public string localita { get; set; }
        public int quota { get; set; }
        public List<Giorni> giorni { get; set; }
    }

    public class RootMeteo
    {
        [JsonProperty("fonte-da-citare")]
        public string fontedacitare { get; set; }

        [JsonProperty("codice-ipa-titolare")]
        public string codiceipatitolare { get; set; }

        [JsonProperty("nome-titolare")]
        public string nometitolare { get; set; }

        [JsonProperty("codice-ipa-editore")]
        public string codiceipaeditore { get; set; }

        [JsonProperty("nome-editore")]
        public string nomeeditore { get; set; }
        public string dataPubblicazione { get; set; }
        public int idPrevisione { get; set; }
        public string evoluzione { get; set; }
        public string evoluzioneBreve { get; set; }
        public List<object> AllerteList { get; set; }
        public List<Previsione> previsione { get; set; }
    }

}
