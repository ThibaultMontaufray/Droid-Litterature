using System.Collections.Generic;

namespace Droid.litterature
{
    public enum FORM
    {
        UNKNOWN,
        INFINITIF,
        PRESENT,
        FUTUR,
        IMPARFAIT,
        PASSESIMPLE,
        PARTICIPEPASSE,
        PASSECOMPOSE,
        PARFAIT,
        CONDITIONNEL
    }

    public class Time
    {
        #region Attribute
        private string _group;
        private string _type;
        private GENRE _genre;
        private NOMBRE _nombre;
        private FORM _form;
        private int _pers;
        private string _val;
        private bool _exception;
        private string _infinitif;
        private List<Lexique> _lexiques;
        private string _suffixe;
        #endregion

        #region Properties
        public string Suffixe
        {
            get { return _suffixe; }
            set { _suffixe = value; }
        }
        public bool Exception
        {
            get { return _exception; }
            set { _exception = value; }
        }
        public string Infinitif
        {
            get { return _infinitif; }
            set { _infinitif = value; }
        }
        public string Group
        {
            get { return _group; }
            set { _group = value; }
        }
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public string Val
        {
            get { return _val; }
            set { _val = value; }
        }
        public int Person
        {
            get { return _pers; }
            set { _pers = value; }
        }
        public GENRE Genre
        {
            get { return _genre; }
            set { _genre = value; }
        }
        public NOMBRE Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public FORM Form
        {
            get { return _form; }
            set { _form = value; }
        }
        public List<Lexique> Lexiques
        {
            get { return _lexiques; }
            set { _lexiques = value; }
        }
        #endregion

        #region Constructor
        public Time()
        {
            this._genre = GENRE.NEUTRE;
            this._exception = false;
        }
        public Time(string my_group, string my_type, string my_val, GENRE my_genre, NOMBRE my_nombre)
        {
            this._group = my_group;
            this._type = my_type;
            this._val = my_val;
            this._genre = my_genre;
            this._nombre = my_nombre;
            this._exception = false;
        }
        #endregion

        #region Methods
        #endregion
    }
}
