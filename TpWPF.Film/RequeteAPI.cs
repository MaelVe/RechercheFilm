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

        /// <summary>
        /// Fonction de création de la requête pour l'api omdb
        /// </summary>
        /// <param name="titre"></param>
        /// <param name="year"></param>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object ConstructionRequete(string titre, string year, string id, string type)
        {
            var requete = debutRequete;

            // Si id est rempli on ne fait une recherche que par id et pas par les autres champs
            if (!string.IsNullOrEmpty(id))
            {
                requete += "i=" + id;
                var resultId = Appel(requete);
                return TransformJsonToObject(resultId, true);
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
            return TransformJsonToObject(result, true);
        }

        /// <summary>
        /// Fonction de création de la requête pour l'api omdb avec juste l'id omdb, permet de récupérer plus d'info sur un film
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object ConstructionRequete(string id)
        {
            var requete = debutRequete;
            requete += "i=" + id;
            var resultId = Appel(requete);
            return TransformJsonToObject(resultId, false);
        }

        /// <summary>
        /// Fait un appel htpp
        /// </summary>
        /// <param name="requete">La requête omdb</param>
        /// <returns></returns>
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

        /// <summary>
        /// Transformation du json reçu de omdb en objet
        /// </summary>
        /// <param name="json"></param>
        /// <param name="fromClassic"></param>
        /// <returns></returns>
        public object TransformJsonToObject(string json, bool fromClassic)
        {
            if (json.Contains("Error"))
            {
                return JsonConvert.DeserializeObject<ErrorModel>(json);
            }

            if (fromClassic)
            {
                return JsonConvert.DeserializeObject<ListFilmModel>(json);
            }
            else
            {
                return JsonConvert.DeserializeObject<FilmCompletModel>(json);
            }
        }       
    }
}
