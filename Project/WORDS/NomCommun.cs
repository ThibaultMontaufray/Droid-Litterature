using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Droid_litterature
{
    public class NomCommun : Word
    {
        #region Attributes
        private List<string> _capacities;
        private List<string> _incapacities;
        #endregion

        #region Properties
        public List<string> Incapacities
        {
            get { return _incapacities; }
            set { _incapacities = value; }
        }
        public List<string> Capacities
        {
            get { return _capacities; }
            set { _capacities = value; }
        }
        #endregion

        #region Constructor
        public NomCommun()
        {
            this.Capacities = new List<string>();
            this.Incapacities = new List<string>();
            this.Synonymes = new List<string>();
            this.LexicalField = new List<Lexique>();
            this.Role = ROLE.NOMCOMMUN;
        }
        public NomCommun(NomCommun nc)
        {
            this.Capacities = nc.Capacities;
            this.Incapacities = nc.Incapacities;
            this.Synonymes = nc.Synonymes;
            this.Role = ROLE.NOMCOMMUN;
            this.Genre = nc.Genre;
            this.Nombre = nc.Nombre;
            this.Exception = nc.Exception;
            this.Text = nc.Text;
            this.Pers = nc.Pers;
            this.Roles = nc.Roles;
            this.DicoNomCommun = nc;
            this.LexicalField = nc.LexicalField;
        }
        public NomCommun(Word w)
        {
            this.Capacities = new List<string>();
            this.Incapacities = new List<string>();
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

            this.DicoNomCommun = w.DicoNomCommun;
            this.Role = ROLE.NOMCOMMUN;
        }
        #endregion

        #region Methods public
        public bool Detect(string val)
        {
            if (val.ToLower().Equals(this.GetWithoutAccents)) return true;
            else return false;
        }
        public void Copy(NomCommun nc)
        {
            this.Capacities = nc.Capacities;
            this.Incapacities = nc.Incapacities;
            this.Role = ROLE.NOMCOMMUN;
            this.Genre = nc.Genre;
            this.Nombre = nc.Nombre;
            this.Exception = nc.Exception;
            this.Text = nc.Text;
            this.Pers = nc.Pers;
            this.Roles = nc.Roles;
            this.DicoNomCommun = nc;
            this.LexicalField = nc.LexicalField;
            this.Synonymes = nc.Synonymes;
            this.Definition = nc.Definition;
        }
        #endregion
    }
}
