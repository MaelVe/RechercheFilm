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
        private ObservableCollection<FilmCompletModel> mesFilms;
        public ObservableCollection<FilmCompletModel> MesFilms { get => mesFilms; set => SetProperty(nameof(MesFilms), ref mesFilms, value); }

        public MaCollectionViewModel()
        {
            this.UpdateMyCollection();
        }

        public void UpdateMyCollection()
        {
            InteractionMaCollection interactionMaCollection = new InteractionMaCollection();
            var results = interactionMaCollection.GetMyCollection();

            MesFilms = new ObservableCollection<FilmCompletModel>();

            foreach (var result in results)
            {
                if (string.IsNullOrEmpty(result.Poster) || result.Poster.Equals("N/A") || !result.Poster.Contains("http"))
                {
                    result.Poster = "../../Ressources/noImage.jpg";
                }
                else
                {
                    // on enlève le titre ici car il y a une image
                    result.Title = "";
                }

                MesFilms.Add(result);
            }
        }
    }
}
