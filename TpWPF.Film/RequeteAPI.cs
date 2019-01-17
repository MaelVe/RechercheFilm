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

        public object ConstructionRequete(string titre, string year, string id, string type)
        {
            var requete = debutRequete;

            // Si id est rempli on ne fait une recherche que par id et pas par les autres champs
            if (!string.IsNullOrEmpty(id))
            {
                requete += "i=" + id;
                var resultId = Appel(requete);
                return TransformJsonToObject(resultId);
            }

            bool titrePresent = false;
            if (!string.IsNullOrEmpty(titre))
            {
                titrePresent = true;
                requete += "s=" + titre;
            }

            bool yearPresent = false;
            if (!string.IsNullOrEmpty(year))
            {
                if (titrePresent)
                {
                    requete += "&";
                }

                requete += "y=" + year;
                yearPresent = true;
            }

            if (!string.IsNullOrEmpty(type))
            {
                if(titrePresent || yearPresent)
                {
                    requete += "&";
                }

                requete += "type=" + type;
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
