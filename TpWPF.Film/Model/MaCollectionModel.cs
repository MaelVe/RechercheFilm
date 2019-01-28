using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpWPF.Film.Model
{
    public class MaCollectionModel
    {
        [JsonProperty("ImdbId")]
        public string ImdbId { get; set; }
        [JsonProperty("MaNote")]
        public string MaNote { get; set; }
        [JsonProperty("Statut")]
        public string Statut { get; set; }
    }
}
