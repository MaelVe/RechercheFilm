using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TpWPF.Film.Model;
using TpWPF.MVVM;

namespace TpWPF.Film.ViewModels
{
    public class DetailFilmViewModel : ObservableObject
    {
        private string poster;
        private string description;

        public DetailFilmViewModel()
        {

        }

        public string Poster { get => poster; set => SetProperty(nameof(Poster), ref poster, value); }
        public string Description { get => description; set => SetProperty(nameof(Description), ref description, value); }

        public void Appel(string imdbId)
        {
            // On reconstruit la requête avec l'id imdb pour avoir plus d'infos sur le film
            RequeteAPI requeteAPI = new RequeteAPI();
            var result = requeteAPI.ConstructionRequete(imdbId) as FilmCompletModel;
        }

        public void Attribution(FilmCompletModel filmCompletModel)
        {
            Poster = filmCompletModel.Poster;
            Description = filmCompletModel.Plot;
        }
    }
}
