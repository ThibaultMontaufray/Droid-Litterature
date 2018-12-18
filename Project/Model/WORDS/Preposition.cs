using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Droid.litterature
{
    public class Preposition : Word
    {
        #region Attribute
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public Preposition()
        {
            this.Synonymes = new List<string>();
            this.Role = ROLE.PREPOSITION;
        }
        public Preposition(Preposition p)
        {
            this.Synonymes = p.Synonymes;
            this.Role = p.Role;
            this.Text = p.Text;
        }
        public Preposition(Word w)
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

            this.DicoPreposition = w.DicoPreposition;
            this.Role = ROLE.PREPOSITION;
        }
        #endregion

        #region Methods
        #endregion
    }
}
