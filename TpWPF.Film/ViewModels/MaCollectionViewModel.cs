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

        #region Fields

        private ObservableCollection<FilmCompletModel> mesFilms;
        private RelayCommand filmDetailCommand;
        private bool detail;
        private static MaCollectionViewModel instance = null;
        private DetailFilmViewModel detailFilm;
        private bool cacherDetailVisibility;
        private RelayCommand cacherDetailCommand;

        #endregion

        #region Properties

        public RelayCommand FilmDetailCommand { get => filmDetailCommand; set => filmDetailCommand = value; }
        public RelayCommand CacherDetailCommand { get => cacherDetailCommand; set => cacherDetailCommand = value; }
        public ObservableCollection<FilmCompletModel> MesFilms { get => mesFilms; set => SetProperty(nameof(MesFilms), ref mesFilms, value); }
        public bool Detail { get => detail; set => SetProperty(nameof(Detail), ref detail, value); }
        public DetailFilmViewModel DetailFilm { get => detailFilm; set => SetProperty(nameof(DetailFilm), ref detailFilm, value); }
        public bool CacherDetailVisibility { get => cacherDetailVisibility; set => SetProperty(nameof(CacherDetailVisibility), ref cacherDetailVisibility, value); }

        #endregion

        #region Constructor(s)

        private MaCollectionViewModel()
        {
            this.DetailFilm = DetailFilmViewModel.Instance;
            FilmDetailCommand = new RelayCommand(FilmDetailCommandExecute);
            CacherDetailCommand = new RelayCommand(CacherDetailCommandExecute);
            this.UpdateMyCollection();
        }

        public static MaCollectionViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MaCollectionViewModel();
                }
                return instance;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fonction appelé lorsque l'on appuie sur un film, permet d'afficher les détails du film
        /// </summary>
        /// <param name="commandParameter"></param>
        private void FilmDetailCommandExecute(object commandParameter)
        {
            if (!this.Detail)
            {
                this.Detail = true;
                this.CacherDetailVisibility = true;
            }


            DetailFilmViewModel.Instance.Appel(commandParameter as string);
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

        /// <summary>
        /// Permet de mettre à jour la collection de mes films
        /// </summary>
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

        #endregion
    }
}
