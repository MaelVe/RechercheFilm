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
        private string searchTitle;
        private string searchId;
        private string searchYear;

        public RechercheFilmsViewModel(IDialogCoordinator dialog)
        {
            dialogCoordinator = dialog;
            RechercherCommand = new RelayCommand(RechercherCommandExecute);
            requeteAPI = new RequeteAPI();
            ListFilm = new ObservableCollection<FilmModel>();
        }

        public RelayCommand RechercherCommand { get => rechercherCommand; set => rechercherCommand = value; }
        public ObservableCollection<FilmModel> ListFilm { get => listFilm; set => SetProperty(nameof(ListFilm), ref listFilm, value); }
        public string SearchTitle { get => searchTitle; set => SetProperty(nameof(SearchTitle), ref searchTitle, value); }
        public string SearchId { get => searchId; set => SetProperty(nameof(SearchId), ref searchId, value); }
        public string SearchYear { get => searchYear; set => SetProperty(nameof(SearchYear), ref searchYear, value); }

        public void RechercherCommandExecute(object commandParameter)
        {
            object resultTemp = requeteAPI.ConstructionRequete(SearchTitle);
            if (resultTemp is ListFilmModel)
            {
                var result = resultTemp as ListFilmModel;
                if (result != null && result.Search != null)
                {
                    foreach (var a in result.Search)
                    {
                        ListFilm.Add(a);
                    }
                }
            }
            else
            {
                var result = resultTemp as ErrorModel;  
                var x = dialogCoordinator.ShowMessageAsync(this, "Erreur", result.Error);
            }
        }
    }
}
