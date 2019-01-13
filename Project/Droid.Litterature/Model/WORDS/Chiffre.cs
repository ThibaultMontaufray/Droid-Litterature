using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Droid.Litterature
{
    public class Chiffre : Word
    {
        #region Attribute
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public Chiffre()
        {
            this.Role = ROLE.CHIFFRE;
            this.Genre = GENRE.NEUTRE;
        }
        public Chiffre(Preposition p)
        {
            this.Role = p.Role;
            this.Text = p.Text;
        }
        public Chiffre(Word w)
        {
            this.Text = w.Text;
            this.Suffixe = w.Suffixe;
            this.Prefixe = w.Prefixe;
            this.Genre = w.Genre;
            this.Nombre = w.Nombre;
            this.Exception = w.Exception;
            this.Pers = w.Pers;
            this.LexicalField = w.LexicalField;
            this.Definition = w.Definition;

            this.DicoPreposition = w.DicoPreposition;
            this.Role = ROLE.CHIFFRE;
        }
        #endregion

        #region Methods
        #endregion
    }
}
