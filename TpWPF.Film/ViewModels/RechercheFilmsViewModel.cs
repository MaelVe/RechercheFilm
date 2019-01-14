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
    public class RechercheFilmsViewModel : ObservableObject
    {
        private RelayCommand rechercherCommand;
        private RequeteAPI requeteAPI;
        private ObservableCollection<FilmModel> listFilm;

        public RechercheFilmsViewModel()
        {
            RechercherCommand = new RelayCommand(RechercherCommandExecute);
            requeteAPI = new RequeteAPI();
            ListFilm = new ObservableCollection<FilmModel>();
        }

        public RelayCommand RechercherCommand { get => rechercherCommand; set => rechercherCommand = value; }
        public ObservableCollection<FilmModel> ListFilm { get => listFilm; set => SetProperty(nameof(ListFilm), ref listFilm, value); }

        public void RechercherCommandExecute(object commandParameter)
        {
            ListFilmModel result = requeteAPI.GetByTitre("Pirate");

            foreach (var a in result.Search)
            {
                ListFilm.Add(a);
            }
        }
    }
}
