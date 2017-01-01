using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Droid_litterature;
using System.Xml.Serialization;

namespace Droid_litterature
{
    public class Word
    {
        #region Attribute
        private string _text; // dico reference value
        private bool _exception;
        private int _pers;
        private string _suffixe;
        private string _prefixe;
        private string _definition;
        private List<string> _synonymes;
        
        private GENRE _genre;
        private NOMBRE _nombre;
        private ROLE _role;
        private List<ROLE> _possibleRoles;
        private List<Lexique> _lexicalField;
        private List<Syllabe> _listSyllabe;

        private Verb _dicoVerb;
        private Adjective _dicoAdjective;
        private Adverb _dicoAdverb;
        private Conjonction _dicoConjonction;
        private Determinant _dicoDeterminant;
        private Pronom _dicoPronom;
        private Preposition _dicoPreposition;
        private NomCommun _dicoNomCommun;
        private NomPropre _dicoNomPropre;
        private Chiffre _dicoChiffre;

        private SentenceStructure _attachedSentenseStruct;
        #endregion

        #region Properties
        [XmlElement(ElementName = "definition")]
        public string Definition
        {
            get { return _definition; }
            set { _definition = value; }
        }
        [XmlElement(ElementName = "synonymes")]
        public List<string> Synonymes
        {
            get { return _synonymes; }
            set { _synonymes = value; }
        }
        [XmlElement(ElementName = "templaceNomcommun")]
        public NomCommun DicoNomCommun
        {
            get { return _dicoNomCommun; }
            set { _dicoNomCommun = value; }
        }
        [XmlElement(ElementName = "templateNompropre")]
        public NomPropre DicoNomPropre
        {
            get { return _dicoNomPropre; }
            set { _dicoNomPropre = value; }
        }
        [XmlElement(ElementName = "templatePreposition")]
        public Preposition DicoPreposition
        {
            get { return _dicoPreposition; }
            set { _dicoPreposition = value; }
        }
        [XmlElement(ElementName = "templatePronom")]
        public Pronom DicoPronom
        {
            get { return _dicoPronom; }
            set { _dicoPronom = value; }
        }
        [XmlElement(ElementName = "templateDeterminant")]
        public Determinant DicoDeterminant
        {
            get { return _dicoDeterminant; }
            set { _dicoDeterminant = value; }
        }
        [XmlElement(ElementName = "templateConjonction")]
        public Conjonction DicoConjonction
        {
            get { return _dicoConjonction; }
            set { _dicoConjonction = value; }
        }
        [XmlElement(ElementName = "templateAdverb")]
        public Adverb DicoAdverb
        {
            get { return _dicoAdverb; }
            set { _dicoAdverb = value; }
        }
        [XmlElement(ElementName = "templateAdjective")]
        public Adjective DicoAdjective
        {
            get { return _dicoAdjective; }
            set { _dicoAdjective = value; }
        }
        [XmlElement(ElementName = "templateVerb")]
        public Verb DicoVerb
        {
            get { return _dicoVerb; }
            set { _dicoVerb = value; }
        }
        [XmlElement(ElementName = "templateNumbers")]
        public Chiffre DicoChiffre
        {
            get { return _dicoChiffre; }
            set { _dicoChiffre = value; }
        }
        [XmlElement(ElementName = "prefix")]
        public string Prefixe
        {
            get { return _prefixe; }
            set { _prefixe = value; }
        }
        [XmlElement(ElementName = "suffix")]
        public string Suffixe
        {
            get { return _suffixe; }
            set { _suffixe = value; }
        }
        [XmlElement(ElementName = "listSyllabes")]
        public List<Syllabe> ListSyllabe
        {
            get { return _listSyllabe; }
            set { _listSyllabe = value; }
        }
        [XmlElement(ElementName = "lexicalFields")]
        public List<Lexique> LexicalField
        {
            get { return _lexicalField; }
            set { _lexicalField = value; }
        }
        [XmlElement(ElementName = "isException")]
        public bool Exception
        {
            get { return _exception; }
            set { _exception = value; }
        }
        [XmlElement(ElementName = "role")]
        public ROLE Role
        {
            get { return _role; }
            set { _role = value; }
        }
        [XmlElement(ElementName = "gender")]
        public GENRE Genre
        {
            get { return _genre; }
            set { _genre = value; }
        }
        [XmlElement(ElementName = "number")]
        public NOMBRE Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        /// <summary>
        /// Text of the word
        /// </summary>
        [XmlElement(ElementName = "text")]
        public string Text
        {
            get { return string.IsNullOrEmpty(_text) ? string.Empty : _text; }
            set 
            { 
                _text = value; 
            }
        }
        /// <summary>
        /// List of possible role before grammatical analyse
        /// </summary>
        [XmlElement(ElementName = "roles")]
        public List<ROLE> Roles
        {
            get { return _possibleRoles; }
            set { _possibleRoles = value; }
        }
        /// <summary>
        /// the personn to accord / conjugate the word
        /// </summary>
        [XmlElement(ElementName = "person")]
        public int Pers
        {
            get { return _pers; }
            set { _pers = value; }
        }
        /// <summary>
        /// Return the word without accent or other character that could block recognition
        /// </summary>
        [XmlElement(ElementName = "textWithoutAccent")]
        public string GetWithoutAccents
        {
            get
            {
                if (!string.IsNullOrEmpty(Text))
                {
                    string str = Text.ToLower();
                    str = str.Replace("è", "e");
                    str = str.Replace("é", "e");
                    str = str.Replace("ê", "e");
                    str = str.Replace("ë", "e");
                    str = str.Replace("ô", "o");
                    str = str.Replace("à", "a");
                    str = str.Replace("â", "a");
                    str = str.Replace("ï", "i");
                    str = str.Replace("î", "i");
                    str = str.Replace("û", "u");
                    str = str.Replace("ù", "u");
                    str = str.Replace("ç", "c");
                    return str;
                }
                else return string.Empty;
            }
        }
        /// <summary>
        /// Group of the sentense where the word is linked (like verbal group, nominal group, COD, COI, ...
        /// </summary>
        [XmlElement(ElementName = "Structur")]
        public SentenceStructure AttachedSentenseStructure
        {
            get { return _attachedSentenseStruct; }
            set { _attachedSentenseStruct = value; }
        }
        #endregion

        #region Constructor
        public Word()
        {
            _genre = GENRE.UNKNOWN;
            _nombre = NOMBRE.UNKNOWN;
            _role = ROLE.UNKNOWN;
            _listSyllabe = new List<Syllabe>();
            _possibleRoles = new List<ROLE>();
            _exception = false;
            _pers = 0;
            _lexicalField = new List<Lexique>();
            _synonymes = new List<string>();
        }
        public Word(GENRE my_genre, NOMBRE my_nombre, ROLE my_role, List<ROLE> my_list_role, int my_pers, List<Lexique> lexical_field, List<Syllabe> lstSyllabe, List<string> synonymes, string definition, SentenceStructure attachedStructure)
        {
            _definition = definition;
            _genre = my_genre;
            _nombre = my_nombre;
            _role = my_role;
            _possibleRoles = my_list_role;
            _exception = false;
            _pers = my_pers;
            _lexicalField = lexical_field;
            _listSyllabe = lstSyllabe;
            _synonymes = synonymes;
            _attachedSentenseStruct = attachedStructure;
        }
        public Word(Word ref_word)
        {
            this.Definition = ref_word.Definition;
            this.Genre = ref_word.Genre;
            this.Nombre = ref_word.Nombre;
            this.Roles = ref_word.Roles;
            this._exception = ref_word.Exception;
            this._pers = ref_word.Pers;
            this._lexicalField = ref_word.LexicalField;
            this._dicoAdjective = ref_word.DicoAdjective;
            this._dicoAdverb = ref_word._dicoAdverb;
            this._dicoConjonction = ref_word._dicoConjonction;
            this._dicoDeterminant = ref_word._dicoDeterminant;
            this._dicoNomCommun = ref_word._dicoNomCommun;
            this._dicoPreposition = ref_word._dicoPreposition;
            this._dicoPronom = ref_word._dicoPronom;
            this._dicoVerb = ref_word._dicoVerb;
            this._synonymes = ref_word._synonymes;
            this._attachedSentenseStruct = ref_word._attachedSentenseStruct;

            _listSyllabe = new List<Syllabe>();
            if (ref_word.ListSyllabe != null)
            {
                foreach (Syllabe s in ref_word.ListSyllabe)
                {
                    _listSyllabe.Add(new Syllabe(s));
                }
            }

            _possibleRoles = new List<ROLE>();
            if (ref_word.Roles != null)
            {
                foreach (ROLE r in ref_word.Roles)
                {
                    _possibleRoles.Add(r);
                }
            }
        }
        #endregion

        #region Methods public
        public bool IsEqualsTo(Word w)
        {
            if (!w.Text.Equals(this.Text)) return false;
            if (!w.Role.Equals(this.Role)) return false;
            if (!w.Genre.Equals(this.Genre)) return false;
            if (!w.Pers.Equals(this.Pers)) return false;
            if (w.LexicalField != null && !w.LexicalField.Equals(this.LexicalField)) return false;
            return true;
        }
        #endregion

        #region Methods private
        #endregion
    }
}
