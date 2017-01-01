using Droid_database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace Droid_litterature
{
    /// <summary>
    /// All words / grammar / syllabe parsing for a langage
    /// </summary>
    public class Dico
    {
        #region Attribute        
        private LANGAGE _language;
        private int _maxSyllabeLength;
        private List<string> _country;

        [XmlElement(ElementName = "preposition")]
        private List<Preposition> _listPreposition;
        [XmlElement(ElementName = "verbs")]
        private List<Verb> _listVerbs;
        [XmlElement(ElementName = "pronoms")]
        private List<Pronom> _listPronoms;
        [XmlElement(ElementName = "nomcommuns")]
        private List<NomCommun> _listNomCommuns;
        [XmlElement(ElementName = "nompropres")]
        private List<NomPropre> _listNomPropres;
        [XmlElement(ElementName = "adjectives")]
        private List<Adjective> _listAdjectives;
        [XmlElement(ElementName = "adverbs")]
        private List<Adverb> _listAdverbs;
        [XmlElement(ElementName = "conjonctions")]
        private List<Conjonction> _listConjonctions;
        [XmlElement(ElementName = "determinants")]
        private List<Determinant> _listDeterminants;
        [XmlElement(ElementName = "lexiques")]
        private List<Lexique> _listLexiques;
        [XmlElement(ElementName = "syllabes")]
        private List<Syllabe> _listSyllabe;
        [XmlElement(ElementName = "numbers")]
        private List<Chiffre> _listChiffre;
        //private List<string> _listStructure;

        private List<Syllabe> _listUnSorted;
        private List<Syllabe> _listException;
        private Word _syllabeWord;

        private LANGAGE lang;
        private Accord accord;
        private Conjugaison conjugaison;
        private bool _dbConnectionAvailable;
        //private string _dump;
        #endregion

        #region Properties
        public List<Preposition> ListPreposition
        {
            get
            {
                if (_listPreposition == null) _listPreposition = new List<Preposition>();
                return _listPreposition;
            }
            set
            {
                if (_listPreposition == null) _listPreposition = new List<Preposition>();
                _listPreposition = value;
                Console.WriteLine("Add preposition - " + value[value.Count - 1]);
            }
        }
        /// <summary>
        /// All list of syllabe for this langage
        /// </summary>
        public List<Syllabe> ListSyllabe
        {
            get
            {
                if (_listSyllabe == null) _listSyllabe = new List<Syllabe>();
                return _listSyllabe;
            }
            set
            {
                if (_listSyllabe == null) _listSyllabe = new List<Syllabe>();
                _listSyllabe = value;
                Console.WriteLine("Add syllabe - " + value[value.Count - 1]);
            }
        }
        public List<Chiffre> ListChiffre
        {
            get
            {
                if (_listChiffre == null) _listChiffre = new List<Chiffre>();
                return _listChiffre;
            }
            set
            {
                if (_listChiffre == null) _listChiffre = new List<Chiffre>();
                _listChiffre = value;
            }
        }
        public List<Lexique> ListLexiques
        {
            get
            {
                if (_listLexiques == null) _listLexiques = new List<Lexique>();
                return _listLexiques;
            }
            set
            {
                if (_listLexiques == null) _listLexiques = new List<Lexique>();
                _listLexiques = value;
                Console.WriteLine("Add lexique - " + value[value.Count - 1]);
            }
        }
        public List<Determinant> ListDeterminant
        {
            get
            {
                if (_listDeterminants == null) _listDeterminants = new List<Determinant>();
                return _listDeterminants;
            }
            set
            {
                if (_listDeterminants == null) _listDeterminants = new List<Determinant>();
                _listDeterminants = value;
                Console.WriteLine("Add determinant - " + value[value.Count - 1]);
            }
        }
        public List<Verb> ListVerbs
        {
            get
            {
                if (_listVerbs == null) _listVerbs = new List<Verb>();
                return _listVerbs;
            }
            set
            {
                if (_listVerbs == null) _listVerbs = new List<Verb>();
                _listVerbs = value;
                Console.WriteLine("Add verbe - " + value[value.Count - 1]);
            }
        }
        public List<Pronom> ListPronoms
        {
            get
            {
                if (_listPronoms == null) _listPronoms = new List<Pronom>();
                return _listPronoms;
            }
            set
            {
                if (_listPronoms == null) _listPronoms = new List<Pronom>();
                ListPronoms = value;
                Console.WriteLine("Add pronom - " + value[value.Count - 1]);
            }
        }
        public List<NomPropre> ListNomPropres
        {
            get
            {
                if (_listNomPropres == null) _listNomPropres = new List<NomPropre>();
                return _listNomPropres;
            }
            set
            {
                if (_listNomPropres == null) _listNomPropres = new List<NomPropre>();
                _listNomPropres = value;
                Console.WriteLine("Add nom propre - " + value[value.Count - 1]);
            }
        }
        public List<NomCommun> ListNomCommuns
        {
            get
            {
                if (_listNomCommuns == null) _listNomCommuns = new List<NomCommun>();
                return _listNomCommuns;
            }
            set
            {
                if (_listNomCommuns == null) _listNomCommuns = new List<NomCommun>();
                _listNomCommuns = value;
                Console.WriteLine("Add nom commun - " + value[value.Count - 1]);
            }
        }
        public List<Adjective> ListAdjectives
        {
            get
            {
                if (_listAdjectives == null) _listAdjectives = new List<Adjective>();
                return _listAdjectives;
            }
            set
            {
                if (_listAdjectives == null) _listAdjectives = new List<Adjective>();
                _listAdjectives = value;
                Console.WriteLine("Add adjectif - " + value[value.Count - 1]);
            }
        }
        public List<Adverb> ListAdverbs
        {
            get
            {
                if (_listAdverbs == null) _listAdverbs = new List<Adverb>();
                return _listAdverbs;
            }
            set
            {
                if (_listAdverbs == null) _listAdverbs = new List<Adverb>();
                _listAdverbs = value;
                Console.WriteLine("Add adverb - " + value[value.Count - 1]);
            }
        }
        public List<Conjonction> ListConjonctions
        {
            get
            {
                if (_listConjonctions == null) _listConjonctions = new List<Conjonction>();
                return _listConjonctions;
            }
            set
            {
                if (_listConjonctions == null) _listConjonctions = new List<Conjonction>();
                _listConjonctions = value;
                Console.WriteLine("Add conjonction - " + value[value.Count - 1]);
            }
        }
        public int MaxSyllabeLength
        {
            get { return _maxSyllabeLength; }
            set { _maxSyllabeLength = value; }
        }
        public LANGAGE Lang
        {
            get { return lang; }
            set { lang = value; }
        }
        public Conjugaison Conjugaison
        {
            get { return conjugaison; }
            set { conjugaison = value; }
        }
        public Accord Accord
        {
            get { return accord; }
            set { accord = value; }
        }
        public bool DbConnectionAvailable
        {
            get { return _dbConnectionAvailable; }
            set { _dbConnectionAvailable = value; }
        }
        #endregion

        #region Constructor
        public Dico()
        {
            _listSyllabe = new List<Syllabe>();
            _country = new List<string>();
            _language = LANGAGE.FR;
            _country.Add("France");
            _dbConnectionAvailable = true;
        }
        public Dico(LANGAGE lang, string contr)
        {
            _listSyllabe = new List<Syllabe>();
            _country = new List<string>();
            _language = lang;
            _country.Add(contr);
            _dbConnectionAvailable = true;

            LoadDico();
        }
        public Dico(LANGAGE lang, List<string> lcontr)
        {
            _listSyllabe = new List<Syllabe>();
            _country = lcontr;
            _language = lang;
            _dbConnectionAvailable = true;

            LoadDico();
        }
        public Dico(Dico modelDico)
        {
            _listSyllabe = new List<Syllabe>();
            _country = modelDico._country;
            _language = modelDico._language;
            _dbConnectionAvailable = true;

            LoadDico();
        }
        #endregion

        #region Methods public
        /// <summary>
        /// Load the dictionnary for the current langage
        /// </summary>
        /// <returns>error code after dico lading : 0 -> ok</returns>
        private void LoadDico()
        {
            _dbConnectionAvailable = MySqlAdapter.IsConnectionPossible();
            if (_dbConnectionAvailable)
            { 
                conjugaison = new Conjugaison(this);
                accord = new Accord();

                DefinitionLoader.LoadSyllabe(this);
                DefinitionLoader.LoadConjugaison(this);
                DefinitionLoader.LoadAccords(accord);
                DefinitionLoader.LoadVerb(this);
                DefinitionLoader.LoadNomCommuns(this);
                DefinitionLoader.LoadNomPropres(this);
                DefinitionLoader.LoadDeterminants(this);
                DefinitionLoader.LoadPronom(this);
                DefinitionLoader.LoadAdjective(this);
                DefinitionLoader.LoadAdverb(this);
                DefinitionLoader.LoadConjonction(this);
                DefinitionLoader.LoadPreposition(this);
                DefinitionLoader.LoadChiffre(this);
                DefinitionLoader.RefineVerb(this);

                Dumper.DumpDico(this);
            }
        }
        /// <summary>
        /// Parse a specific ord and decompose syllabes
        /// </summary>
        /// <param name="word">the word to parse</param>
        public void ParseSyllabe(Word word)
        {
            _syllabeWord = word;

            Regex rex = new Regex("^([a-zA-Z\'-]+)$");
            if (rex.IsMatch(_syllabeWord.GetWithoutAccents))
            {
                List<Syllabe> ls = BuildSyllabeCompleteList();
                SortSyllabeList(ls);
            }
        }
        #endregion

        #region Methods private
        private List<Syllabe> BuildSyllabeCompleteList()
        {
            string trashString = string.Empty;
            _listUnSorted = new List<Syllabe>();
            _listException = new List<Syllabe>();

            _listUnSorted = FindSyllbabe(out trashString, _listException);
            //string rest = trashString.Replace("-", string.Empty).Replace("'", string.Empty).Replace(" ", string.Empty);
            string rest = trashString.Replace(" ", string.Empty);

            if (!string.IsNullOrEmpty(rest))
            {
                if (FindBadSyllabe(rest))
                {
                    _listUnSorted = FindSyllbabe(out trashString, _listException);
                    trashString = trashString.Replace("-", string.Empty);

                    if (!string.IsNullOrEmpty(trashString))
                    {
                        return DEBUG_InsertNewSyllabe(_listUnSorted);
                        // holly shit, we have to develope recursive method !
                    }
                }
                else
                {
                    return DEBUG_InsertNewSyllabe(_listUnSorted);
                    // we don't have all syllabe in this language !
                }
            }
            return _listUnSorted;
        }
        private bool FindBadSyllabe(string rest)
        {
            bool badSyllabeFound = false;
            Syllabe BadSyllabeBefore = new Syllabe();
            Syllabe BadSyllabeAfter = new Syllabe();
            bool afterError = false;
            //string trashString = _syllabeWord.GetWithoutAccents.Replace("-", string.Empty).Replace("'", string.Empty).Replace(" ", string.Empty);
            string trashString = _syllabeWord.GetWithoutAccents.Replace(" ", string.Empty);
            int watchDog = 1000;
            bool completed = false;

            if (trashString.StartsWith(rest))
            {
                trashString = trashString.Remove(0, rest.Length);
                afterError = true;
            }

            while (watchDog > 0 && !completed && _listUnSorted.Count != 0)
            {
                foreach (Syllabe s1 in _listUnSorted)
                {
                    string[] splitter = { s1.Text };
                    //string[] tmp = _syllabeWord.GetWithoutAccents.Replace("-", string.Empty).Replace("'", string.Empty).Replace(" ", string.Empty).Split(splitter, StringSplitOptions.None);
                    string[] tmp = _syllabeWord.GetWithoutAccents.Replace(" ", string.Empty).Split(splitter, StringSplitOptions.None);
                    if (trashString.StartsWith(s1.Text) || trashString.Equals(s1.Text))
                    {
                        if (!afterError) BadSyllabeBefore = s1;
                        else
                        {
                            BadSyllabeAfter = s1;
                            completed = true;
                            break;
                        }

                        trashString = trashString.Remove(0, s1.Text.Length);

                        if (trashString.StartsWith(rest))
                        {
                            trashString = trashString.Remove(0, rest.Length);
                            afterError = true;
                        }

                        if (string.IsNullOrEmpty(trashString)) completed = true;
                    }
                    watchDog--;
                }
            }
            //badSyllabeFound = TryFindWithout(BadSyllabeBefore);
            //if (!badSyllabeFound) badSyllabeFound = TryFindWithout(BadSyllabeAfter);
            //if (!badSyllabeFound && !string.IsNullOrEmpty(BadSyllabeBefore.Text))
            //{
            //    rest = BadSyllabeBefore.Text + rest;
            //    badSyllabeFound = FindBadSyllabe(rest);
            //} 
            //if (!badSyllabeFound && !string.IsNullOrEmpty(BadSyllabeAfter.Text))
            //{
            //    rest += BadSyllabeAfter.Text;
            //    badSyllabeFound = FindBadSyllabe(rest);
            //}

            return badSyllabeFound;
        }
        private bool TryFindWithout(Syllabe exceptionSyllabe)
        {
            _listException.Add(exceptionSyllabe);
            if (!string.IsNullOrEmpty(TryFind()))
            {
                _listException.Remove(exceptionSyllabe);
                return false;
            }
            else
            {
                return true;
            }
        }
        private string TryFind()
        {
            string trashString;
            _listUnSorted = FindSyllbabe(out trashString, _listException);
            //return trashString.Replace("-", string.Empty).Replace("'", string.Empty).Replace(" ", string.Empty);
            return trashString.Replace(" ", string.Empty);
        }
        private List<Syllabe> FindSyllbabe(out string trashString, List<Syllabe> lsException)
        {
            //string wordText = _syllabeWord.GetWithoutAccents.Replace("-", string.Empty).Replace("'", string.Empty).Replace(" ", string.Empty);
            bool found = false;
            string wordText = _syllabeWord.GetWithoutAccents.Replace(" ", string.Empty);
            _maxSyllabeLength = wordText.Length;
            trashString = wordText;
            List<Syllabe> listUnSorted = new List<Syllabe>();

            for (int i = _maxSyllabeLength; i > 0; i--)
            {
                if (found) break;
                foreach (Syllabe s in _listSyllabe)
                {
                    if (s.NbLetter == i && !lsException.Contains(s))
                    {
                        if (trashString.Contains(s.Text) && wordText.Contains(s.Text))
                        {
                            int count = 0;
                            string[] sep = {s.Text};
                            string[] tmp = trashString.Split(sep, StringSplitOptions.None);
                            count = tmp.Length - 1;
                            trashString = trashString.Replace(s.Text, " ");
                            while (count > 0)
                            {
                                listUnSorted.Add(s);
                                count--;
                            }
                            found = true;
                            break;
                        }
                    }
                }
            }

            return listUnSorted;
        }
        private void SortSyllabeList(List<Syllabe> listUnSorted)
        {
            if (listUnSorted == null) return;
            //string wordText = _syllabeWord.GetWithoutAccents.Replace("-", string.Empty).Replace("'", string.Empty).Replace(" ", string.Empty);
            string wordText = _syllabeWord.GetWithoutAccents.Replace(" ", string.Empty);
            string trashString = wordText;
            int watchDog = 1000;
            while (!string.IsNullOrEmpty(trashString) && watchDog > 0)
            {
                for (int i = _maxSyllabeLength; i > 0; i--)
                {
                    foreach (Syllabe s in listUnSorted)
                    {
                        if (s.NbLetter == i)
                        {
                            if (trashString.StartsWith(s.Text))
                            {
                                if (!s.ExceptionToExclude) _syllabeWord.ListSyllabe.Add(s);
                                trashString = trashString.Remove(0, s.Text.Length);
                                listUnSorted.Remove(s);
                                break;
                            }
                        }
                    }
                }
                watchDog--;
            }
            if (watchDog <= 0)
            {
            }
        }

        private List<Syllabe> DEBUG_InsertNewSyllabe(List<Syllabe> ls)
        {
            //DEBUG_SYLLABE ds = new DEBUG_SYLLABE(_syllabeWord, this, ls);
            //ds.ShowDialog();
            //return BuildSyllabeCompleteList();
            foreach (Syllabe item in ls)
            {
                MySqlAdapter.ExecuteQuery("insert into s_unknownsyllabe (valeur) values ('" + item.Text + "')");   
            }
            return null;
        }
        #endregion
    }
}
