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
        private RelayCommand cacherDetailCommand;
        private RequeteAPI requeteAPI;
        private ObservableCollection<FilmModel> listFilm;
        private ObservableCollection<string> searchType;
        private DetailFilmViewModel detailFilm;
        private string searchTitle;
        private string searchId;
        private string searchYear;
        private string typeSelected;
        private bool detail;
        private bool cacherDetailVisibility;

        #endregion
    
        #region Constructor

        public RechercheFilmsViewModel(IDialogCoordinator dialog)
        {
            this.Detail = false;
            dialogCoordinator = dialog;
            RechercherCommand = new RelayCommand(RechercherCommandExecute);
            FilmDetailCommand = new RelayCommand(FilmDetailCommandExecute);
            CacherDetailCommand = new RelayCommand(CacherDetailCommandExecute);
            requeteAPI = new RequeteAPI();
            ListFilm = new ObservableCollection<FilmModel>();
            SearchType = new ObservableCollection<string>
            {
                "movie",
                "series",
                "episode"
            };
            this.DetailFilm = DetailFilmViewModel.Instance;
            this.Detail = false;
        }

        #endregion

        #region Properties

        public RelayCommand FilmDetailCommand { get => filmDetailCommand; set => filmDetailCommand = value; }
        public RelayCommand RechercherCommand { get => rechercherCommand; set => rechercherCommand = value; }
        public RelayCommand CacherDetailCommand { get => cacherDetailCommand; set => cacherDetailCommand = value; }
        public ObservableCollection<FilmModel> ListFilm { get => listFilm; set => SetProperty(nameof(ListFilm), ref listFilm, value); }
        public ObservableCollection<string> SearchType { get => searchType; set => SetProperty(nameof(SearchType), ref searchType, value); }
        public string SearchTitle { get => searchTitle; set => SetProperty(nameof(SearchTitle), ref searchTitle, value); }
        public string SearchId { get => searchId; set => SetProperty(nameof(SearchId), ref searchId, value); }
        public string SearchYear { get => searchYear; set => SetProperty(nameof(SearchYear), ref searchYear, value); }
        public string TypeSelected { get => typeSelected; set => SetProperty(nameof(TypeSelected), ref typeSelected, value); }
        public bool Detail { get => detail; set => SetProperty(nameof(Detail), ref detail, value); }
        public DetailFilmViewModel DetailFilm { get => detailFilm; set => SetProperty(nameof(DetailFilm), ref detailFilm, value); }
        public bool CacherDetailVisibility { get => cacherDetailVisibility; set => SetProperty(nameof(CacherDetailVisibility), ref cacherDetailVisibility, value); }

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
                        if (string.IsNullOrEmpty(a.Poster) || a.Poster.Equals("N/A") || !a.Poster.Contains("http"))
                        {
                            a.Poster = "../../Ressources/noImage.jpg";
                        }
                        else
                        {
                            // on enlève le titre ici car il y a une image
                            a.Title = "";
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

        /// <summary>
        /// Fonction éxécutée lors de l'appui sur un film
        /// </summary>
        /// <param name="commandParameter"></param>
        public void FilmDetailCommandExecute(object commandParameter)
        {
            if (!this.Detail)
            {
                this.Detail = true;
                this.CacherDetailVisibility = true;
            }

            this.DetailFilm.FromMaCollection = false;
            this.DetailFilm.FromMaCollectionHide = true;
            this.DetailFilm.Appel(commandParameter as string);
        }

        /// <summary>
        /// Utillisée pour cacher les détails d'un film
        /// </summary>
        /// <param name="commandParameter"></param>
        public void CacherDetailCommandExecute(object commandParameter)
        {
            this.Detail = false;
            this.CacherDetailVisibility = false;
        }

        #endregion
    }
}
