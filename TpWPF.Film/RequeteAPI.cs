using System;
using System.Collections.Generic;
using System.Linq;
using TpWPF.Film.Model;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace TpWPF.Film
{
    public class RequeteAPI
    {
        public const string debutRequete = "http://www.omdbapi.com/?apikey=12434fb2&";

        public object ConstructionRequete(string titre, string year, string id)
        {
            var requete = debutRequete;

            // Si id est rempli on ne fait une recherche que par id et pas par les autres champs
            if (string.IsNullOrEmpty(id))
            {
                requete += "i=" + id;
                var resultId = Appel(requete);
                return TransformJsonToObject(resultId);
            }

            if (string.IsNullOrEmpty(titre))
            {
                requete += "s=" + titre;
            }

            if (string.IsNullOrEmpty(titre))
            {
                requete += "y=" + year;
            }

            var result = Appel(requete);
            return TransformJsonToObject(result);
        }

        static string Appel(string requete)
        {
            using (HttpClient client = new HttpClient())
            {
                // la requête
                using (HttpResponseMessage response = client.GetAsync(requete).Result)
                {

                    using (HttpContent content = response.Content)
                    {
                        // récupère la réponse, il ne resterai plus qu'à désérialiser
                        string result = content.ReadAsStringAsync().Result;
                        return result;
                    }
                }
            }
        }

        public object TransformJsonToObject(string json)
        {
            if (json.Contains("Error"))
            {
                return JsonConvert.DeserializeObject<ErrorModel>(json);
            }

            return JsonConvert.DeserializeObject<ListFilmModel>(json);
        }
    }
}
