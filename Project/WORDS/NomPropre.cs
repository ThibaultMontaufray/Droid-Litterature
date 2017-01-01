namespace Droid_litterature
{
    using System.Collections.Generic;

    public class NomPropre : Word
    {
        #region Attributes
        private List<string> _capacities;
        private List<string> _incapacities;
        private string _nom;
        private string _prenom;
        private string _surnom;
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
        public string Surnom
        {
            get { return _surnom; }
            set { _surnom = value; }
        }
        public string Prenom
        {
            get { return _prenom; }
            set { _prenom = value; }
        }
        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }
        #endregion

        #region Constructor
        public NomPropre()
        {
            this.Capacities = new List<string>();
            this.Incapacities = new List<string>();
            this.Synonymes = new List<string>();
            this.LexicalField = new List<Lexique>();
            this.Role = ROLE.NOMPROPRE;
        }
        public NomPropre(NomPropre pc)
        {
            this.Capacities = pc.Capacities;
            this.Incapacities = pc.Incapacities;
            this.Synonymes = pc.Synonymes;
            this.Role = ROLE.NOMPROPRE;
            this.Genre = pc.Genre;
            this.Nombre = pc.Nombre;
            this.Exception = pc.Exception;
            this.Text = pc.Text;
            this.Pers = pc.Pers;
            this.Roles = pc.Roles;
            this.DicoNomPropre = pc;
            this.LexicalField = pc.LexicalField;
        }
        public NomPropre(Word w)
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
            this.Role = ROLE.NOMPROPRE;
        }
        #endregion

        #region Methods public
        public bool Detect(string val)
        {
            if (string.IsNullOrEmpty(val)) return false;
            else if (!string.IsNullOrEmpty(this.Nom) && val.ToLower().Equals(this.Nom.ToLower())) return true;
            else if (!string.IsNullOrEmpty(this.Prenom) && val.ToLower().Equals(this.Prenom.ToLower())) return true;
            else if (!string.IsNullOrEmpty(this.Surnom) && val.ToLower().Equals(this.Surnom.ToLower())) return true;
            else return false;
        }
        public void Copy(NomPropre np)
        {
            this.Capacities = np.Capacities;
            this.Incapacities = np.Incapacities;
            this.Role = ROLE.NOMPROPRE;
            this.Genre = np.Genre;
            this.Nombre = np.Nombre;
            this.Exception = np.Exception;
            this.Text = np.Text;
            this.Pers = np.Pers;
            this.Roles = np.Roles;
            this.DicoNomPropre = np;
            this.LexicalField = np.LexicalField;
            this.Synonymes = np.Synonymes;
            this.Definition = np.Definition;

            this.Nom = np.Nom;
            this.Prenom = np.Prenom;
            this.Surnom = np.Surnom;
        }
        #endregion
    }
}
