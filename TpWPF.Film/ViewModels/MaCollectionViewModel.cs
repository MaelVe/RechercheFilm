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
        private RelayCommand filmDetailCommand;
        public RelayCommand FilmDetailCommand { get => filmDetailCommand; set => filmDetailCommand = value; }
        private bool detail;
        public ObservableCollection<FilmCompletModel> MesFilms { get => mesFilms; set => SetProperty(nameof(MesFilms), ref mesFilms, value); }

        //public MaCollectionViewModel()
        //{
        //}

        private static MaCollectionViewModel instance = null;

        private MaCollectionViewModel()
        {
            FilmDetailCommand = new RelayCommand(FilmDetailCommandExecute);
            this.UpdateMyCollection();
        }

        public bool Detail { get => detail; set => SetProperty(nameof(Detail), ref detail, value); }

        private void FilmDetailCommandExecute(object commandParameter)
        {
            if (!this.Detail)
            {
                this.Detail = true;
            }

            DetailFilmViewModel.Instance.Appel(commandParameter as string);
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
