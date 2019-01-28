using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TpWPF.MVVM;

namespace TpWPF.Film.Model
{
    public class FilmCompletModel : ObservableObject
    {
        #region Fields

        private string maNote;
        private string monStatut;
        private string statut;
        private string imdbId;
        private string title;
        private string year;
        private string rated;
        private string released;
        private string runtime;
        private string genre;
        private string director;
        private string writer;
        private string actors;
        private string plot;
        private string language;
        private string country;
        private string awards;
        private string poster;
        private string boxOffice;
        private string production;
        private string type;

        #endregion

        #region Properties

        public string Title
        {
            get => title; set
            {
                try
                {
                    SetProperty(nameof(Title), ref title, value);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
        public string Year { get => year; set => year = value; }
        public string Rated { get => rated; set => rated = value; }
        public string Released { get => released; set => released = value; }
        public string Runtime { get => runtime; set => runtime = value; }
        public string Genre { get => genre; set => genre = value; }
        public string Director { get => director; set => director = value; }
        public string Writer { get => writer; set => writer = value; }
        public string Actors { get => actors; set => actors = value; }
        public string Plot { get => plot; set => plot = value; }
        public string Language { get => language; set => language = value; }
        public string Country { get => country; set => country = value; }
        public string Awards { get => awards; set => awards = value; }
        public string Poster { get => awards; set => awards =value; }
        public string BoxOffice { get => boxOffice; set => boxOffice = value; }
        public string Production { get => production; set => production = value; }
        public string Type { get => type; set => type = value; }
        public string ImdbId { get => imdbId; set => imdbId = value; }
        public string MaNote { get => maNote; set => maNote = value; }
        public string Statut { get => statut; set => statut = value; }
        public string MonStatut { get => monStatut; set => monStatut = value; }

        #endregion
    }
}