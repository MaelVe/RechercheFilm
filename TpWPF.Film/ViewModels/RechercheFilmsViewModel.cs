using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;
using TpWPF.Film.Model;
using TpWPF.MVVM;

namespace TpWPF.Film.ViewModels
{
    
    public class RechercheFilmsViewModel : ObservableObject
    {
        private IDialogCoordinator dialogCoordinator;
        private RelayCommand rechercherCommand;
        private RequeteAPI requeteAPI;
        private ObservableCollection<FilmModel> listFilm;
        private ObservableCollection<string> searchType;
        private string searchTitle;
        private string searchId;
        private string searchYear;

        public RechercheFilmsViewModel(IDialogCoordinator dialog)
        {
            dialogCoordinator = dialog;
            RechercherCommand = new RelayCommand(RechercherCommandExecute);
            requeteAPI = new RequeteAPI();
            ListFilm = new ObservableCollection<FilmModel>();
            SearchType = new ObservableCollection<string>
            {
                "movie",
                "series",
                "episode"
            };
        }

        public RelayCommand RechercherCommand { get => rechercherCommand; set => rechercherCommand = value; }
        public ObservableCollection<FilmModel> ListFilm { get => listFilm; set => SetProperty(nameof(ListFilm), ref listFilm, value); }
        public ObservableCollection<string> SearchType { get => searchType; set => SetProperty(nameof(SearchType), ref searchType, value); }
        public string SearchTitle { get => searchTitle; set => SetProperty(nameof(SearchTitle), ref searchTitle, value); }
        public string SearchId { get => searchId; set => SetProperty(nameof(SearchId), ref searchId, value); }
        public string SearchYear { get => searchYear; set => SetProperty(nameof(SearchYear), ref searchYear, value); }

        /// <summary>
        /// Action réaliser lors de l'appui sur le bouton de recherche 
        /// </summary>
        /// <param name="commandParameter"></param>
        public void RechercherCommandExecute(object commandParameter)
        {
            object resultTemp = requeteAPI.ConstructionRequete(SearchTitle, SearchYear, searchId);
            if (resultTemp is ListFilmModel)
            {
                var result = resultTemp as ListFilmModel;
                ListFilm = new ObservableCollection<FilmModel>();
                if (result != null && result.Search != null)
                {
                    foreach (var a in result.Search)
                    {
                        if (string.IsNullOrEmpty(a.Poster))
                        {
                            a.Poster = "../../Ressources/noImage.jpg";
                        }

                        ListFilm.Add(a);
                    }
                }
            }
            else
            {
                var result = resultTemp as ErrorModel;  
                var x = dialogCoordinator.ShowMessageAsync(this, "Erreur", result.Error);
                ListFilm = new ObservableCollection<FilmModel>();
            }
        }
    }
}
