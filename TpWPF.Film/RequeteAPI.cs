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

        public void GetByTitre(string titre)
        {
            var requete = debutRequete + "t=" + titre;
            var result = Appel(requete);
            TransformJsonToObject(result);
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

        public void TransformJsonToObject(string json)
        {
            var deserializedProduct = JsonConvert.DeserializeObject<FilmModel>(json);
        }
    }
}
