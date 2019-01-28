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
        /// <summary>
        /// Ajout d'un élément dans le json
        /// </summary>
        /// <param name="maCollectionModel"></param>
        public void AddToMyCollection(MaCollectionModel maCollectionModel)
        {
            // On est obligé de d'abord déserialiser pour récuperer ce qui existent déjà
            List<MaCollectionModel> result = new List<MaCollectionModel>();
            using (StreamReader file = File.OpenText(@"../../Ressources/MaCollection.json"))
            {
                result = JsonConvert.DeserializeObject<List<MaCollectionModel>>(file.ReadToEnd());
            }

            result.Add(maCollectionModel);

            using (StreamWriter file = File.CreateText(@"../../Ressources/MaCollection.json"))
            {
                
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, result);
                
            }
        }

        /// <summary>
        /// Ajout d'un élément dans le json
        /// </summary>
        /// <param name="maCollectionModel"></param>
        public void UpdateMyCollection(List<MaCollectionModel> maCollectionModel)
        {           
            using (StreamWriter file = File.CreateText(@"../../Ressources/MaCollection.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, maCollectionModel);
            }
        }

        public MaCollectionModel GetOneFilmById(string id)
        {
            List<MaCollectionModel> result;
            using (StreamReader file = File.OpenText(@"../../Ressources/MaCollection.json"))
            {
                result = JsonConvert.DeserializeObject<List<MaCollectionModel>>(file.ReadToEnd());
            }
            return result.FirstOrDefault(w => w.ImdbId == id);
        }

        /// <summary>
        /// Récupération de tous les éléments présents dans MesFilms
        /// </summary>
        /// <returns></returns>
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

        public void RemoveFilmById(string id)
        {
            List<MaCollectionModel> result;
            using (StreamReader file = File.OpenText(@"../../Ressources/MaCollection.json"))
            {
                result = JsonConvert.DeserializeObject<List<MaCollectionModel>>(file.ReadToEnd());
            }

            result.Remove(result.First(w => w.ImdbId == id));

            using (StreamWriter file = File.CreateText(@"../../Ressources/MaCollection.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, result);
            }
        }
    }
}
