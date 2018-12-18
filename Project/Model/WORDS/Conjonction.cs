using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Droid.litterature
{
    public class Conjonction : Word
    {
        #region Attribute
        private CONJONCTION cathegory;
        #endregion

        #region Properties
        public CONJONCTION Cathegory
        {
            get { return cathegory; }
            set { cathegory = value; }
        }
        #endregion

        #region Constructor
        public Conjonction()
        {
            this.Synonymes = new List<string>();
        }
        public Conjonction(Conjonction c)
        {
            this.Synonymes = c.Synonymes;
            this.Role = ROLE.CONJONCTION;
            this.Genre = GENRE.NEUTRE;
            this.Nombre = NOMBRE.NEUTRE;
            this.Cathegory = c.cathegory;
            this.DicoConjonction = c.DicoConjonction;
            this.Text = c.Text;
            this.Definition = c.Definition;
        }
        public Conjonction(Word w)
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

            this.DicoConjonction = w.DicoConjonction;
            this.Role = ROLE.CONJONCTION;
        }
        #endregion

        #region Methods
        #endregion
    }
}
