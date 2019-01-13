using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Droid.Litterature
{
    public class Sentense
    {
        #region Structure
        public class ActionTab
        {
            public int Id;
            public string Structure;
            public string Forme;
            public bool Available;
            public string Type;
            public List<int?> Qui;
            public List<int?> Que;
            public List<int?> Quoi;
            public List<int?> Dont;
            public List<int?> Ou;
            public List<int?> Comment;
            public List<int?> Quel;
            public string Action;
            public string KeyWord;
        }
        #endregion

        #region Constants
        public const int ACTID = 0;
        public const int ACTSTRUCUTRE = 1;
        public const int ACTFORME = 2;
        public const int ACTDISPONIBLE = 3;
        public const int ACTTYPE = 4;
        public const int ACTQUI = 5;
        public const int ACTQUE = 6;
        public const int ACTQUOI = 7;
        public const int ACTDONT = 8;
        public const int ACTOU = 9;
        public const int ACTCOMMENT = 10;
        public const int ACTQUEL = 11;
        public const int ACTACTION = 12;
        public const int ACTKEYWORD = 13;
        public const int ACTKEYREGISTER = 14;
        public const int ACTABSTRACTION = 15;
        public const int ACTPERSON = 16;
        #endregion

        #region Attribute
        private string _text;
        private List<Verb> _verbs;
        private List<Pronom> _pronoms;
        private List<Adjective> _adjectives;
        private List<Adverb> _adverb;
        private List<NomCommun> _nomcommuns;
        private List<NomPropre> _nomPropres;
        private List<Conjonction> _conjonctions;
        private List<Determinant> _determinants;
        private List<Preposition> _prepositions;
        private List<Lexique> _lexiques;
        private List<Chiffre> _chiffres;

        private List<Word> _words;
        private PONCTUATION _ponctuation;
        private List<string> _structure;
        private List<GroupNominal> _groupNominaux;
        private List<GroupVerbal> _groupVerbaux;
        private List<COD> _groupCOD;
        private List<COI> _groupCOI;
        private string _structureStr;
        private string _htmlWords;
        private string _htmlDetailled;
        private string _contexte;
        private ActionTab _tableAction;
        #endregion

        #region Properties
        public bool Understood
        {
            get 
            {
                foreach (Word w in _words)
                {
                    if (w.Role == ROLE.UNKNOWN) return false;
                }
                return true; 
            }
        }
        public string Text
        {
            get { return ApplyLanguageCorrections(_text); }
            set { _text = value; }
        }
        public List<string> Structure
        {
            get { return _structure; }
            set { _structure = value; }
        }
        public string StructureStr
        {
            get { return _structureStr; }
            set { _structureStr = value; }
        }
        //public List<SentenceStructure> StructureAnalytic
        //{
        //    get  { return _structureAnalytic; }
        //    set { _structureAnalytic = value; }
        //}
        public List<object> StructureAnalytic
        {
            get
            {
                List<object> lss = new List<object>();
                SentenceStructure lastSs = null;
                foreach (Word w in Words)
                {
                    if (w.AttachedSentenseStructure == null)
                    {
                        lss.Add(w);
                    }
                    else if (!w.AttachedSentenseStructure.Equals(lastSs))
                    {
                        lss.Add(w.AttachedSentenseStructure);
                    }
                    lastSs = w.AttachedSentenseStructure;
                }
                return lss;
            }
        }
        public string StructureAnalyticStr
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                SentenceStructure lastSs = null;
                foreach (Word w in Words)
                {
                    if (w.AttachedSentenseStructure == null)
                    {
                        sb.Append(w.Role + "&");
                    }
                    else if (!w.AttachedSentenseStructure.Equals(lastSs))
                    {
                        sb.Append(w.AttachedSentenseStructure.Name + "&");
                    }
                    lastSs = w.AttachedSentenseStructure;
                }
                return sb.ToString().TrimEnd('&');
            }
        }
        public List<Lexique> Lexiques
        {
            get { return _lexiques; }
            set { _lexiques = value; }
        }
        public List<Verb> Verbs
        {
            get { return _verbs; }
            set { _verbs = value; }
        }
        public List<Adjective> Adjectives
        {
            get { return _adjectives; }
            set { _adjectives = value; }
        }
        public List<Adverb> Adverb
        {
            get { return _adverb; }
            set { _adverb = value; }
        }
        public List<Pronom> Pronoms
        {
            get { return _pronoms; }
            set { _pronoms = value; }
        }
        public List<NomCommun> NomCommuns
        {
            get { return _nomcommuns; }
            set { _nomcommuns = value; }
        }
        public List<NomPropre> NomPropres
        {
            get { return _nomPropres; }
            set { _nomPropres = value; }
        }
        public List<Conjonction> Conjonctions
        {
            get { return _conjonctions; }
            set { _conjonctions = value; }
        }        
        public List<Determinant> Determinants
        {
            get { return _determinants; }
            set { _determinants = value; }
        }
        public List<Preposition> Prepositions
        {
            get { return _prepositions; }
            set { _prepositions = value; }
        }
        public List<Chiffre> Chiffres
        {
            get { return _chiffres; }
            set { _chiffres = value; }
        }
        public List<GroupNominal> GroupNominaux
        {
            get { return _groupNominaux; }
            set { _groupNominaux = value; }
        }
        public List<GroupVerbal> GroupVerbaux
        {
            get { return _groupVerbaux; }
            set { _groupVerbaux = value; }
        }
        public List<COD> GroupCOD
        {
            get { return _groupCOD; }
            set { _groupCOD = value; }
        }
        public List<COI> GroupCOI
        {
            get { return _groupCOI; }
            set { _groupCOI = value; }
        }
        public List<Word> Subject
        {
            get
            {
                try
                {
                    SentenceStructure ss = (SentenceStructure)StructureAnalytic.Where(s => s is SentenceStructure && (s as SentenceStructure).IsSubject).ToList()[0];
                    return ss.Words;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public List<Word> Words
        {
            get { return _words; }
            set { _words = value; }
        }
        public PONCTUATION Ponctuation
        {
            get { return _ponctuation; }
            set { _ponctuation = value; }
        }
        public string HTMLWords
        {
            get { return _htmlWords; }
            set { _htmlWords = value; }
        }
        public string HtmlSentense
        {
            get
            {
                string html = "<table class='TobiAnswerTable'>";
                html += "<tr class='TobiAnswerTableHeader'><th colspan=10>Informations sur la phrase</th>";
                html += "<tr><td>Forme</td><td>" + _ponctuation.ToString() + "</td></tr>";
                html += "</table>";
                return html;
            }
        }
        public string HTMLDetailled
        {
            get { return _htmlDetailled; }
            set { _htmlDetailled = value; }
        }
        public string HTMLGroups
        {
            get
            {
                string html = "<table class='TobiAnswerTable'>";
                html += "<tr class='TobiAnswerTableHeader'><th>Role</th><th>Type</th><th>Texte</th>";
                foreach (var item in StructureAnalytic)
                {
                    switch (item.GetType().Name.ToLower())
                    {
                        case "sentencestructure":
                            html += "<tr><td>" + (item as SentenceStructure).Name + "</td><td>" + ApplyLanguageCorrections((item as SentenceStructure).Type) + "</td><td>" + (item as SentenceStructure).Text + "</td></tr>";
                            break;
                        case "unknown":
                        case "verbe":
                        case "adjectif":
                        case "adverbe":
                        case "pronom":
                        case "nom":
                        case "conjonction":
                        case "determinant":
                        case "preposition":
                        case "chiffre":
                        case "mail":
                        case "word":
                            html += "<tr><td>" + (item as Word).Role.ToString().ToLower() + "</td><td>" + (item as Word).Definition + "</td><td>" + (item as Word).Text + "</td></tr>";
                            break;
                        default:
                            html += "<tr><td>" + item.GetType().Name + "</td><td>" + item.ToString() + "</td></tr>";
                            break;
                    }
                }
                foreach (var item in GroupVerbaux)
                {
                    html += "<tr><td>groupe verbal</td><td>" + item.Text + "</td></tr>";
                }
                html += "</table>";
                return html;
            }
        }
        public string Contexte
        {
            get { return _contexte; }
            set { _contexte = value; }
        }
        public ActionTab TableAction
        {
            get { return _tableAction; }
            set { _tableAction = value; }
        }
        #endregion

        #region Constructor
        public Sentense(string textBrut)
        {
            _text = textBrut;
            _ponctuation = PONCTUATION.UNKNOWN;
            _verbs = new List<Verb>();
            _adjectives = new List<Adjective>();
            _adverb = new List<Adverb>();
            _pronoms = new List<Pronom>();
            _conjonctions = new List<Conjonction>();
            _nomcommuns = new List<NomCommun>();
            _nomPropres = new List<NomPropre>();
            _determinants = new List<Determinant>();
            _prepositions = new List<Preposition>();
            _chiffres = new List<Chiffre>();
            _lexiques = new List<Lexique>();
            _groupNominaux = new List<GroupNominal>();
            _groupVerbaux = new List<GroupVerbal>();
            _groupCOD = new List<COD>();
            _groupCOI = new List<COI>();

            SplitText();
        }
        #endregion

        #region Methods public
        public void DeterminePonctuation(string inputText)
        {
            if (inputText.Length == this._text.Length) 
            {
                this._ponctuation = PONCTUATION.AFFIRMATION;
            }
            else
            {
                int cursor = 0;
                bool flagFound = false;

                string[] pct = { ". ", "! ", "? " };
                string[] lst = string.Format("{0} ",inputText).Split(pct, StringSplitOptions.RemoveEmptyEntries);
                for (int i=0 ; i< lst.Length ; i++)
                {
                    if (!flagFound) cursor += lst[i].Length;
                    if (lst[i].Equals(this._text)) flagFound = true; ;
                    if (!flagFound) cursor += 1;
                }
                if (!flagFound)
                {
                    this._ponctuation = PONCTUATION.UNKNOWN;
                }
                else if ((cursor+1) <= inputText.Length)
                {
                    string ponc = inputText.Substring(cursor, 1);
                    switch (ponc)
                    {
                        case "." :
                            this._ponctuation = PONCTUATION.AFFIRMATION;
                            break;
                        case "?":
                            this._ponctuation = PONCTUATION.QUESTION;
                            break;
                        case "!":
                            this._ponctuation = PONCTUATION.EXCLAMATION;
                            break;
                    }
                }
            }
        }
        public List<Word> splitTextGuillemet(string text_to_split)
        {
            List<Word> returnList = new List<Word>();
            bool flag = true;
            string[] accoSplit = text_to_split.Split('\'');
            if (accoSplit.Length > 1)
            {
                foreach (string sacco in accoSplit)
                {
                    Word w = new Word();
                    if (flag)
                    {
                        w.Text = sacco + '\'';
                        flag = false;
                    }
                    else
                    {
                        w.Text = sacco;
                    }
                    returnList.Add(w);
                }
            }
            return returnList;
        }
        public static string ApplyLanguageCorrections(string text)
        {
            string retVal = string.Empty;
            if (!string.IsNullOrEmpty(text))
            { 
                retVal = text.Replace("|", " ou ");
                retVal = retVal.Replace("&", " et ");
                retVal = retVal.Replace("_", " ");
                retVal = retVal.Replace(" de a", " d'a");
                retVal = retVal.Replace(" de e", " d'e");
                retVal = retVal.Replace(" de i", " d'i");
                retVal = retVal.Replace(" de o", " d'o");
                retVal = retVal.Replace(" de u", " d'u");
                retVal = retVal.Replace(" de y", " d'y");
                retVal = retVal.Replace(" de ha", " d'ha");
                retVal = retVal.Replace(" de he", " d'he");
                retVal = retVal.Replace(" de hi", " d'hi");
                retVal = retVal.Replace(" de ho", " d'ho");
                retVal = retVal.Replace(" de hu", " d'hu");
                retVal = retVal.Replace(" de hy", " d'hy");
                retVal = retVal.Replace("que a", "qu'a");
                retVal = retVal.Replace("que e", "qu'e");
                retVal = retVal.Replace("que i", "qu'i");
                retVal = retVal.Replace("que o", "qu'o");
                retVal = retVal.Replace("que u", "qu'u");
                retVal = retVal.Replace("que y", "qu'y");
                retVal = retVal.Replace(" ce e", " c'e");
                retVal = retVal.Replace(" se a", " s'a");
                retVal = retVal.Replace(" se e", " s'e");
                retVal = retVal.Replace(" se i", " s'i");
                retVal = retVal.Replace(" se o", " s'o");
                retVal = retVal.Replace(" se u", " s'u");
                retVal = retVal.Replace(" se y", " s'y");
                retVal = retVal.Replace(" me a", " m'a");
                retVal = retVal.Replace(" me e", " m'e");
                retVal = retVal.Replace(" me i", " m'i");
                retVal = retVal.Replace(" me o", " m'o");
                retVal = retVal.Replace(" me u", " m'u");
                retVal = retVal.Replace(" me y", " m'y");
                retVal = retVal.Replace(" me ha", " m'ha");
                retVal = retVal.Replace(" me he", " m'he");
                retVal = retVal.Replace(" me hi", " m'hi");
                retVal = retVal.Replace(" me ho", " m'ho");
                retVal = retVal.Replace(" me hu", " m'hu");
                retVal = retVal.Replace(" me hy", " m'hy");
            }
            return retVal;
        }
        public static string SimplifySentense(string text)
        {
            string retVal = string.Empty;
            if (!string.IsNullOrEmpty(text))
            {
                retVal = text.Replace(" d'a", " de a");
                retVal = retVal.Replace(" d'e", " de e");
                retVal = retVal.Replace(" d'i", " de i");
                retVal = retVal.Replace(" d'o", " de o");
                retVal = retVal.Replace(" d'u", " de u");
                retVal = retVal.Replace(" d'y", " de y");
                retVal = retVal.Replace(" d'ha", " de ha");
                retVal = retVal.Replace(" d'he", " de he");
                retVal = retVal.Replace(" d'hi", " de hi");
                retVal = retVal.Replace(" d'ho", " de ho");
                retVal = retVal.Replace(" d'hu", " de hu");
                retVal = retVal.Replace(" d'hy", " de hy");
                retVal = retVal.Replace("qu'a", "que a");
                retVal = retVal.Replace("qu'e", "que e");
                retVal = retVal.Replace("qu'i", "que i");
                retVal = retVal.Replace("qu'o", "que o");
                retVal = retVal.Replace("qu'u", "que u");
                retVal = retVal.Replace("qu'y", "que y");
                retVal = retVal.Replace(" c'e", " ce e");
                retVal = retVal.Replace(" s'a", " se a");
                retVal = retVal.Replace(" s'e", " se e");
                retVal = retVal.Replace(" s'i", " se i");
                retVal = retVal.Replace(" s'o", " se o");
                retVal = retVal.Replace(" s'u", " se u");
                retVal = retVal.Replace(" s'y", " se y");
                retVal = retVal.Replace(" m'a", " me a");
                retVal = retVal.Replace(" m'e", " me e");
                retVal = retVal.Replace(" m'i", " me i");
                retVal = retVal.Replace(" m'o", " me o");
                retVal = retVal.Replace(" m'u", " me u");
                retVal = retVal.Replace(" m'y", " me y");
                retVal = retVal.Replace(" m'ha", " me ha");
                retVal = retVal.Replace(" m'he", " me he");
                retVal = retVal.Replace(" m'hi", " me hi");
                retVal = retVal.Replace(" m'ho", " me ho");
                retVal = retVal.Replace(" m'hu", " me hu");
                retVal = retVal.Replace(" m'hy", " me hy");
            }
            return retVal;
        }
        public void EnrichWordsWithDico()
        {
            for (int i = 0; i < Words.Count; i++)
            {
                switch (Words[i].Role)
                {
                    case ROLE.NOMCOMMUN:
                        if (Words[i].DicoNomCommun != null) Words[i].Definition = Words[i].DicoNomCommun.Definition;
                        break;
                    case ROLE.ADJECTIF:
                        if (Words[i].DicoAdjective != null) Words[i].Definition = Words[i].DicoAdjective.Definition;
                        break;
                    case ROLE.ADVERBE:
                        if (Words[i].DicoAdverb != null) Words[i].Definition = Words[i].DicoAdverb.Definition;
                        break;
                    case ROLE.CONJONCTION:
                        if (Words[i].DicoConjonction != null) Words[i].Definition = Words[i].DicoConjonction.Definition;
                        break;
                    case ROLE.DETERMINANT:
                        if (Words[i].DicoDeterminant != null) Words[i].Definition = Words[i].DicoDeterminant.Definition;
                        break;
                    case ROLE.PREPOSITION:
                        if (Words[i].DicoPreposition != null) Words[i].Definition = Words[i].DicoPreposition.Definition;
                        break;
                    case ROLE.PRONOM:
                        if (Words[i].DicoPronom != null) Words[i].Definition = Words[i].DicoPronom.Definition;
                        break;
                    case ROLE.VERBE:
                        if (Words[i].DicoVerb != null) Words[i].Definition = Words[i].DicoVerb.Definition;
                        break;
                }
            }
        }
        #endregion

        #region Methods private
        private void SplitText()
        {
            CleanAppostrophe();
            _words = new List<Word>();
            string[] spaceSplit = SimplifySentense(_text.Replace(",", string.Empty)).Split(' ');
            foreach (string s in spaceSplit)
            {
                if (!string.IsNullOrEmpty(s) && !s.Equals(' '))
                {
                    Word w = new Word();
                    w.Text = s;
                    _words.Add(w);
                }
            }
        }
        private void CleanAppostrophe()
        {
            _text = _text.Replace("c'", "cela ");
            _text = _text.Replace("C'", "Cela ");
            _text = _text.Replace("d'un", "de un");
            _text = _text.Replace("D'un", "De un");
            _text = _text.Replace("d'une", "de une");
            _text = _text.Replace("D'une", "De une");
            _text = _text.Replace("l'", "le ");
            _text = _text.Replace("L'", "Le ");
            _text = _text.Replace("est-ce", "est ce ");
            _text = _text.Replace("Est-ce", "Est ce ");
            _text = _text.Replace("qu'", "que ");
            _text = _text.Replace("Qu'", "Que ");
        }
        #endregion
    }
}
