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

        private string maNote;
        private string imdbId;
        private string poster;
        private string description;
        private string noteDonner;
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
        private bool fromMaCollection;
        private bool fromMaCollectionHide;
        private RelayCommand addCommand;
        private RelayCommand removeCommand;
        private RelayCommand noteCommand;

        #endregion

        private static DetailFilmViewModel instance = null;

        private DetailFilmViewModel()
        {
            AddCommand = new RelayCommand(AddCommandExecute);
            NoteCommand = new RelayCommand(NoteCommandExecute);
            RemoveCommand = new RelayCommand(RemoveCommandExecute);

        }

        public static DetailFilmViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DetailFilmViewModel();
                }
                return instance;
            }
        }

        #region Properties

        public string Poster
        {
            get
            {
                try
                {
                    if (poster == null)
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
        public string NoteDonner { get => noteDonner; set => SetProperty(nameof(NoteDonner), ref noteDonner, value); }
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
        public RelayCommand NoteCommand { get => noteCommand; set => noteCommand = value; }
        public string ImdbId { get => imdbId; set => SetProperty(nameof(ImdbId), ref imdbId, value); }
        public string MaNote { get => maNote; set => SetProperty(nameof(MaNote), ref maNote, value); }
        public bool FromMaCollection { get => fromMaCollection; set => SetProperty(nameof(FromMaCollection), ref fromMaCollection, value); }
        public bool FromMaCollectionHide { get => fromMaCollectionHide; set => SetProperty(nameof(FromMaCollectionHide), ref fromMaCollectionHide, value); }
        public RelayCommand RemoveCommand { get => removeCommand; set => removeCommand = value; }

        #endregion

        #region Methods

        /// <summary>
        /// Appel avec l'id imdb pour avoir plus d'infos sur le film
        /// </summary>
        /// <param name="imdbId"></param>
        public void Appel(string imdbId)
        {
            // On reconstruit la requête avec l'id imdb pour avoir plus d'infos sur le film
            RequeteAPI requeteAPI = new RequeteAPI();
            var result = requeteAPI.ConstructionRequete(imdbId) as FilmCompletModel;
            result.ImdbId = imdbId;
            InteractionMaCollection interactionMaCollection = new InteractionMaCollection();
            var film = interactionMaCollection.GetOneFilmById(imdbId);
            if(film != null)
            {
                MaNote = film.MaNote;
            }

            Attribution(result);
        }

        /// <summary>
        /// Foncttion d'ajout dans la liste MesFilms lors de l'appui sur le bouton "Ajouter à mes films"
        /// </summary>
        /// <param name="obj"></param>
        private void AddCommandExecute(object obj)
        {
            MaCollectionModel maCollectionModel = new MaCollectionModel();
            maCollectionModel.ImdbId = obj as string;
            InteractionMaCollection interactionMaCollection = new InteractionMaCollection();
            interactionMaCollection.AddToMyCollection(maCollectionModel);
            MaCollectionViewModel.Instance.UpdateMyCollection();
        }

        /// <summary>
        /// Foncttion de suppression dans la liste MesFilms lors de l'appui sur le bouton "Ajouter à mes films"
        /// </summary>
        /// <param name="obj"></param>
        private void RemoveCommandExecute(object obj)
        {
            InteractionMaCollection interactionMaCollection = new InteractionMaCollection();
            interactionMaCollection.RemoveFilmById(obj as string);
            MaCollectionViewModel.Instance.UpdateMyCollection();
            
        }

        /// <summary>
        /// Apuui sur le bouton pour mettre une note
        /// </summary>
        /// <param name="obj"></param>
        private void NoteCommandExecute(object obj)
        {
            InteractionMaCollection interactionMaCollection = new InteractionMaCollection();
            var result = interactionMaCollection.GetMyCollection();
            var filmChange = result.FirstOrDefault(w => w.ImdbId == obj as string);
            filmChange.MaNote = NoteDonner;
            List<MaCollectionModel> maCollectionModels = new List<MaCollectionModel>();
            foreach(var film in result)
            {

                MaCollectionModel maCollectionModel = new MaCollectionModel();
                maCollectionModel.ImdbId = film.ImdbId;                

                if (film.ImdbId == obj as string)
                {
                    maCollectionModel.MaNote = film.MaNote;
                }
                else
                {
                    var res = interactionMaCollection.GetOneFilmById(film.ImdbId);
                    maCollectionModel.MaNote = res.MaNote;
                }

                maCollectionModels.Add(maCollectionModel);

            }

            interactionMaCollection.UpdateMyCollection(maCollectionModels);
            MaCollectionViewModel.Instance.UpdateMyCollection();
        }

        /// <summary>
        /// Fonction qui va afficher tous les détails d'un film
        /// </summary>
        /// <param name="filmCompletModel"></param>
        public void Attribution(FilmCompletModel filmCompletModel)
        {
            ImdbId = filmCompletModel.ImdbId;
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

        #endregion
    }
}
