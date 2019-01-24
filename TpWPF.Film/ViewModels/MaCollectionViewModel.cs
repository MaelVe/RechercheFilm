using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TpWPF.Film.Model;
using TpWPF.MVVM;

namespace TpWPF.Film.ViewModels
{
    public class MaCollectionViewModel : ObservableObject
    {
        private ObservableCollection<FilmModel> mesFilms;
        public ObservableCollection<FilmModel> MesFilms { get => mesFilms; set => SetProperty(nameof(MesFilms), ref mesFilms, value); }

        public MaCollectionViewModel()
        {
            InteractionMaCollection interactionMaCollection = new InteractionMaCollection();
            var resultTemp = interactionMaCollection.GetMyCollection();
            if (resultTemp is ListFilmModel)
            {
                var result = resultTemp as ListFilmModel;
                MesFilms = new ObservableCollection<FilmModel>();
                if (result != null && result.Search != null)
                {
                    foreach (var a in result.Search)
                    {
                        if (string.IsNullOrEmpty(a.Poster) || a.Poster.Equals("N/A") || !a.Poster.Contains("http"))
                        {
                            a.Poster = "../../Ressources/noImage.jpg";
                        }
                        else
                        {
                            // on enlève le titre ici car il y a une image
                            a.Title = "";
                        }

                        MesFilms.Add(a);
                    }
                }
            }
        }
    }
}
