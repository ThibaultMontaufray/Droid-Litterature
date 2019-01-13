using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Droid.Litterature
{
    public class Determinant: Word
    {
        #region Enum
        public enum GROUP
        {
            Unknown,
            Indetermine,
            Demonstratif,
            Possessif
        }
        #endregion

        #region Attribute
        private GROUP _group;        
        #endregion

        #region Properties
        public GROUP Group
        {
            get { return _group; }
            set { _group = value; }
        }
        #endregion

        #region Constructor
        public Determinant()
        {
            this.Synonymes = new List<string>();
            this.Role = ROLE.DETERMINANT;
        }
        public Determinant(Determinant d)
        {
            this.Synonymes = d.Synonymes;
            this.Role = ROLE.DETERMINANT;
            this.Roles.Add(d.Role);
            this.DicoDeterminant = d;
            this.Exception = d.Exception;
            this.Pers = d.Pers;
            this.Nombre = d.Nombre;
            this.Genre = d.Genre;
            this.Group = d.Group;
            this.Definition = d.Definition;
        }
        public Determinant(Word w)
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

            this.DicoDeterminant = w.DicoDeterminant;
            this.Role = ROLE.DETERMINANT;
        }
        #endregion

        #region Methods private
        #endregion
    }
}
