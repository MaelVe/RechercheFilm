﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpWPF.Film.Model
{
    public class FilmCompletModel
    {
        #region Fields

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

        public string Title { get => title; set => title = value; }
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
        public string Poster { get => poster; set => poster = value; }
        public string BoxOffice { get => boxOffice; set => boxOffice = value; }
        public string Production { get => production; set => production = value; }
        public string Type { get => type; set => type = value; }

        #endregion
    }
}