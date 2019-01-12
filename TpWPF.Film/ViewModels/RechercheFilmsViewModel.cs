using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TpWPF.MVVM;

namespace TpWPF.Film.ViewModels
{
    public class RechercheFilmsViewModel : ObservableObject
    {
        private RelayCommand rechercherCommand;
        private RequeteAPI requeteAPI;

        public RechercheFilmsViewModel()
        {
            RechercherCommand = new RelayCommand(RechercherCommandExecute);
            requeteAPI = new RequeteAPI();
        }

        public RelayCommand RechercherCommand { get => rechercherCommand; set => rechercherCommand = value; }

        public void RechercherCommandExecute(object commandParameter)
        {
           requeteAPI.GetByTitre("Pirate");
        }       
    }
}
