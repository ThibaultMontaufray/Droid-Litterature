using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Droid_litterature
{
    public class Adjective : Word
    {
        #region Attribute
        private ADJECTIVE cathegory;
        #endregion

        #region Properties
        public ADJECTIVE Cathegory
        {
            get { return cathegory; }
            set { cathegory = value; }
        }
        #endregion

        #region Constructor
        public Adjective()
        {
            this.Synonymes = new List<string>();
            this.Role = ROLE.ADJECTIF;
        }
        public Adjective(string exception, string genre)
        {
            this.Synonymes = new List<string>();
            if (exception.Equals("n")) this.Exception = false;
            else if (Exception.Equals("y")) this.Exception = true;

            if (genre.Equals("masculin")) this.Genre = GENRE.MASCULIN;
            else if (genre.Equals("feminin")) this.Genre = GENRE.FEMININ;
            else if (genre.Equals("neutre")) this.Genre = GENRE.NEUTRE;
            else this.Genre = GENRE.UNKNOWN;

            this.Role = ROLE.ADJECTIF;
        }
        public Adjective(Adjective adj)
        {
            this.Synonymes = adj.Synonymes;
            this.Role = ROLE.ADJECTIF;
            this.Cathegory = adj.cathegory;
            this.Genre = adj.Genre;
            this.DicoAdjective = adj.DicoAdjective;
            this.Nombre = adj.Nombre;
            this.Text = adj.Text;
            this.Definition = adj.Definition;
        }
        public Adjective(Word w)
        {
            this.Synonymes = w.Synonymes;
            this.Text = w.Text;
            this.Suffixe = w.Suffixe;
            this.Prefixe = w.Prefixe;
            this.Genre = w.Genre;
            this.Nombre = w.Nombre;
            this.Exception = w.Exception;
            this.Pers = w.Pers;
            this.LexicalField = w.LexicalField;
            this.Definition = w.Definition;

            this.DicoAdjective = w.DicoAdjective;
            this.Role = ROLE.ADJECTIF;
        }
        #endregion

        #region Methods public
        #endregion

        #region Methods private
        #endregion
    }
}
