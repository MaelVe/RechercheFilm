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

        #region Fields

        private IDialogCoordinator dialogCoordinator;
        private RelayCommand rechercherCommand;
        private RelayCommand filmDetailCommand;
        private RequeteAPI requeteAPI;
        private ObservableCollection<FilmModel> listFilm;
        private ObservableCollection<string> searchType;
        private DetailFilmViewModel detailFilm;
        private string searchTitle;
        private string searchId;
        private string searchYear;
        private string typeSelected;
        private bool detail;

        #endregion

        #region Constructor

        public RechercheFilmsViewModel(IDialogCoordinator dialog)
        {
            this.Detail = false;
            dialogCoordinator = dialog;
            RechercherCommand = new RelayCommand(RechercherCommandExecute);
            FilmDetailCommand = new RelayCommand(FilmDetailCommandExecute);
            requeteAPI = new RequeteAPI();
            ListFilm = new ObservableCollection<FilmModel>();
            SearchType = new ObservableCollection<string>
            {
                "movie",
                "series",
                "episode"
            };
            this.DetailFilm = new DetailFilmViewModel();
            this.Detail = false;
        }

        #endregion

        #region Properties

        public RelayCommand FilmDetailCommand { get => filmDetailCommand; set => filmDetailCommand = value; }
        public RelayCommand RechercherCommand { get => rechercherCommand; set => rechercherCommand = value; }
        public ObservableCollection<FilmModel> ListFilm { get => listFilm; set => SetProperty(nameof(ListFilm), ref listFilm, value); }
        public ObservableCollection<string> SearchType { get => searchType; set => SetProperty(nameof(SearchType), ref searchType, value); }
        public string SearchTitle { get => searchTitle; set => SetProperty(nameof(SearchTitle), ref searchTitle, value); }
        public string SearchId { get => searchId; set => SetProperty(nameof(SearchId), ref searchId, value); }
        public string SearchYear { get => searchYear; set => SetProperty(nameof(SearchYear), ref searchYear, value); }
        public string TypeSelected { get => typeSelected; set => SetProperty(nameof(TypeSelected), ref typeSelected, value); }
        public bool Detail { get => detail; set => SetProperty(nameof(Detail), ref detail, value); }
        public DetailFilmViewModel DetailFilm { get => detailFilm; set => SetProperty(nameof(DetailFilm), ref detailFilm, value); }

        #endregion

        #region Methods

        /// <summary>
        /// Action réaliser lors de l'appui sur le bouton de recherche 
        /// </summary>
        /// <param name="commandParameter"></param>
        public void RechercherCommandExecute(object commandParameter)
        {
            object resultTemp = requeteAPI.ConstructionRequete(SearchTitle, SearchYear, searchId, TypeSelected);
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

        public void FilmDetailCommandExecute(object commandParameter)
        {
            this.Detail = !this.Detail;
        }

        #endregion
    }
}
