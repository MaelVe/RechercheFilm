using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpWPF.Film.Model
{
    public class FilmModel
    {
        #region Fields

        private string title;
        private string year;
        private string imdbID;
        private string type;
        private string poster;
        private string error;

       

        #endregion

        #region Properties

        public string Title { get => title; set => title = value; }
        public string Year { get => year; set => year = value; }     
        public string imdbId { get => imdbID; set => imdbID = value; }
        public string Type { get => type; set => type = value; }
        public string Poster { get => poster; set => poster = value; }
        public string Error { get => error; set => error = value; }

        #endregion

        public FilmModel()
        {

        }
    }
}
