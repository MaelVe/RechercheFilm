using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TpWPF.Film.Model;

namespace TpWPF.Film
{
    /// <summary>
    /// Classe d'interaction avec le fichier json contenant une collection de films
    /// </summary>
    public class InteractionMaCollection
    {
        public void AddToMyCollection(MaCollectionModel maCollectionModel)
        {
            using (StreamWriter file = File.CreateText(@"../../Ressources/MaCollection.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, maCollectionModel);
            }
        }

        public List<FilmCompletModel> GetMyCollection()
        {
            List<MaCollectionModel> result;
            using (StreamReader file = File.OpenText(@"../../Ressources/MaCollection.json"))
            {
                result = JsonConvert.DeserializeObject<List<MaCollectionModel>>(file.ReadToEnd());
            }
            List<FilmCompletModel> filmCompletModels = new List<FilmCompletModel>();

            RequeteAPI requeteAPI = new RequeteAPI();
            foreach(var col in result)
            {
                filmCompletModels.Add(requeteAPI.ConstructionRequete(col.ImdbId) as FilmCompletModel);
            }


            return filmCompletModels;          
        }
    }
}
