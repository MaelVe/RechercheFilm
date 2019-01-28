using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TpWPF.Film.Model;
using TpWPF.MVVM;

namespace TpWPF.Film.ViewModels
{
    public class DetailFilmViewModel : ObservableObject
    {
        #region Fields

        private string poster;
        private string description;
        private string title;
        private string year;
        private string rated;
        private string released;
        private string runtime;
        private string genre;
        private string director;
        private string writer;
        private string actors;
        private string language;
        private string country;
        private string award;
        private RelayCommand addCommand;
        //private MaCollectionViewModel maCollectionViewModel;

        #endregion

        public DetailFilmViewModel()
        {
            //this.MaCollectionViewModel = new MaCollectionViewModel();
            AddCommand = new RelayCommand(AddCommandExecute);
        }

        #region Properties

        public string Poster
        {
            get
            {
                try
                {
                    //SetProperty(nameof(Poster), ref poster, value);
                    if(poster == null)
                    {
                        poster = "";
                    }
                    return poster;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return "";
                }
            }
            set
            {
                try
                {
                    SetProperty(nameof(Poster), ref poster, value);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
        public string Description { get => description; set => SetProperty(nameof(Description), ref description, value); }
        public string Title { get => title; set => SetProperty(nameof(Title), ref title, value); }
        public string Year { get => year; set => SetProperty(nameof(Year), ref year, value); }
        public string Rated { get => rated; set => SetProperty(nameof(Rated), ref rated, value); }
        public string Released { get => released; set => SetProperty(nameof(Released), ref released, value); }
        public string Runtime { get => runtime; set => SetProperty(nameof(Runtime), ref runtime, value); }
        public string Genre { get => genre; set => SetProperty(nameof(Genre), ref genre, value); }
        public string Director { get => director; set => SetProperty(nameof(Director), ref director, value); }
        public string Writer { get => writer; set => SetProperty(nameof(Writer), ref writer, value); }
        public string Actors { get => actors; set => SetProperty(nameof(Actors), ref actors, value); }
        public string Language { get => language; set => SetProperty(nameof(Language), ref language, value); }
        public string Country { get => country; set => SetProperty(nameof(Country), ref country, value); }
        public string Awards { get => award; set => SetProperty(nameof(Awards), ref award, value); }
        public RelayCommand AddCommand { get => addCommand; set => addCommand = value; }
        //public MaCollectionViewModel MaCollectionViewModel { get => maCollectionViewModel; set => SetProperty(nameof(maCollectionViewModel), ref maCollectionViewModel, value); }

        #endregion


        public void Appel(string imdbId)
        {
            // On reconstruit la requête avec l'id imdb pour avoir plus d'infos sur le film
            RequeteAPI requeteAPI = new RequeteAPI();
            var result = requeteAPI.ConstructionRequete(imdbId) as FilmCompletModel;
            Attribution(result);
        }

        private void AddCommandExecute(object obj)
        {
            
           MaCollectionViewModel.Instance.UpdateMyCollection();
        }

        public void Attribution(FilmCompletModel filmCompletModel)
        {
            Poster = filmCompletModel.Poster;
            Description = filmCompletModel.Plot;
            Title = filmCompletModel.Title;
            Year = filmCompletModel.Year;
            Rated = filmCompletModel.Rated;
            Released = filmCompletModel.Released;
            Runtime = filmCompletModel.Runtime;
            Genre = filmCompletModel.Genre;
            Director = filmCompletModel.Director;
            Writer = filmCompletModel.Writer;
            Actors = filmCompletModel.Actors;
            Language = filmCompletModel.Language;
            Country = filmCompletModel.Country;
            Awards = filmCompletModel.Awards;
        }
    }
}
