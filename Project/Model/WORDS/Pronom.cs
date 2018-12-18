using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Droid.litterature
{
    public enum PRONOM
    {
        PERSONNEL,
        DEMONSTRATIF,
        POSSESSIF,
        RELATIF,
        INTERROGATIF,
        INDEFINI
    }
    public class Pronom : Word
    {
        #region Attribute
        private PRONOM cathegory;
        #endregion

        #region Properties
        public PRONOM Cathegory
        {
            get { return cathegory; }
            set { cathegory = value; }
        }
        #endregion

        #region Constructor
        public Pronom()
        {
            this.Synonymes = new List<string>();
            this.Role = ROLE.PRONOM;
        }
        public Pronom(Pronom ref_nom)
        {
            this.Synonymes = ref_nom.Synonymes;
            this.Role = ROLE.PRONOM;
            this.Genre = ref_nom.Genre;
            this.DicoPronom = ref_nom.DicoPronom;
            this.Nombre = ref_nom.Nombre;
            this.Text = ref_nom.Text;
            this.Definition = ref_nom.Definition;
        }
        public Pronom(Word w)
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

            this.DicoPronom = w.DicoPronom;
            this.Role = ROLE.PRONOM;
        }
        #endregion

        #region Methods public
        public void Copy(Pronom pn)
        {
            this.Cathegory = pn.Cathegory;
            this.DicoPronom = pn;
            this.Exception = pn.Exception;
            this.Genre = pn.Genre;
            this.Nombre = pn.Nombre;
            this.Pers = pn.Pers;
            this.Role = pn.Role;
            this.Roles = pn.Roles;
            this.Text = pn.Text;
            this.Definition = pn.Definition;
        }
        #endregion

        #region Methods private
        #endregion
    }
}
