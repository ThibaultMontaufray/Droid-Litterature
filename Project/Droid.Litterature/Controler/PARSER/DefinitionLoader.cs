using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using Droid.Database;
using System.Data;
using System.Configuration;

namespace Droid.Litterature
{
    public static class DefinitionLoader
    {
        #region Attribute
        private const int WORDVALEUR = 0;
        private const int WORDLANGUE = 1;
        private const int WORDROLE = 2;
        private const int WORDGENRE = 3;
        private const int WORDNOMBRE = 4;
        private const int WORDGROUPE = 5;
        private const int WORDINVARIABLE = 6;
        private const int WORDEXCEPTION = 7;
        private const int WORDPERSONNE = 8;
        private const int WORDPREFIXE = 9;
        private const int WORDSUFFIXE = 10;
        private const int WORDSYNONYME = 11;
        private const int WORDFREQUENCE = 12;
        private const int WORDPTVANNE = 13;
        private const int WORDPTPOESIE = 14;
        private const int WORDCONTRAIRE = 15;
        private const int WORDDEFINITION = 16;
        private const int WORDCAPACITE = 17;
        private const int WORDINCAPACITE = 18;
        #endregion

        #region Properties
        #endregion

        #region Methods public
        public static Word LoadClassicWord(DataRow dbResult)
        {
            Word w = new Word();
            w.Text = string.IsNullOrEmpty(dbResult.ItemArray[WORDVALEUR].ToString()) ? string.Empty : dbResult.ItemArray[WORDVALEUR].ToString().ToLower();
            w.Definition = string.IsNullOrEmpty(dbResult.ItemArray[WORDDEFINITION].ToString()) ? string.Empty : dbResult.ItemArray[WORDDEFINITION].ToString().ToLower();

            if (dbResult.ItemArray[WORDGENRE].ToString().ToLower().Equals("masculin")) w.Genre = GENRE.MASCULIN;
            else if (dbResult.ItemArray[WORDGENRE].ToString().ToLower().Equals("feminin")) w.Genre = GENRE.FEMININ;
            else if (dbResult.ItemArray[WORDGENRE].ToString().ToLower().Equals("neutre")) w.Genre = GENRE.NEUTRE;
            else w.Genre = GENRE.UNKNOWN;

            if (dbResult.ItemArray[WORDNOMBRE].ToString().ToLower().Equals("singulier")) w.Nombre = NOMBRE.SINGULIER;
            else if (dbResult.ItemArray[WORDNOMBRE].ToString().ToLower().Equals("pluriel")) w.Nombre = NOMBRE.PLURIEL;
            else if (dbResult.ItemArray[WORDNOMBRE].ToString().ToLower().Equals("neutre")) w.Nombre = NOMBRE.NEUTRE;
            else w.Nombre = NOMBRE.UNKNOWN;

            w.Exception = dbResult.ItemArray[WORDEXCEPTION].ToString().ToLower().Equals("non");

            if (dbResult.ItemArray[WORDPERSONNE].ToString().ToLower().Equals("1")) w.Pers = 1;
            else if (dbResult.ItemArray[WORDPERSONNE].ToString().ToLower().Equals("2")) w.Pers = 2;
            else if (dbResult.ItemArray[WORDPERSONNE].ToString().ToLower().Equals("3")) w.Pers = 3;
            else w.Pers = 0;

            w.Prefixe = string.IsNullOrEmpty(dbResult.ItemArray[WORDPREFIXE].ToString()) ? string.Empty : dbResult.ItemArray[9].ToString().ToLower();
            w.Suffixe = string.IsNullOrEmpty(dbResult.ItemArray[WORDSUFFIXE].ToString()) ? string.Empty : dbResult.ItemArray[10].ToString().ToLower();

            //string[] s_tab1 = dbResult[WORDCHAMPLEXICAL].Split('|');
            //foreach (string s in s_tab1) w.LexicalField.Add(new Lexique(s));
            
            string[] s_tab2 = dbResult.ItemArray[WORDSYNONYME].ToString().Split('|');
            foreach (string s in s_tab2) w.Synonymes.Add(s);
            //dico.ParseSyllabe(pronom);

            return w;
        }
        public static void LoadPronom(Dico dico)
        {
            Pronom pronom = null;
            DataTable dbResult = DBAdapter.ExecuteReader(ConfigurationManager.AppSettings["DB_NAME"].ToString(), string.Format("select * from {0}.t_mot where role like '%[pronom]%';", ConfigurationManager.AppSettings["DB_NAME"].ToString()));
            if (dbResult.Rows.Count > 0)
            {
                foreach (DataRow dbItem in dbResult.Rows)
                {
                    pronom  = ParsePronom(LoadClassicWord(dbItem), dbItem.ItemArray.Select(i => i.ToString()).ToArray(), dico);
                    if (pronom != null) dico.ListPronoms.Add(pronom);
                }
            }
            Console.WriteLine("Loading [" + dico.ListPronoms.Count + "] pronouns completed ...");
        }
        public static void LoadAdjective(Dico dico)
        {
            Adjective adjective = null;
            DataTable dbResult = DBAdapter.ExecuteReader(ConfigurationManager.AppSettings["DB_NAME"].ToString(), string.Format("select * from {0}.t_mot where role like '%[adjectif]%';", ConfigurationManager.AppSettings["DB_NAME"].ToString()));
            if (dbResult.Rows.Count > 0)
            {
                foreach (DataRow dbItem in dbResult.Rows)
                {
                    adjective = ParseAdjective(LoadClassicWord(dbItem), dbItem.ItemArray.Select(i => i.ToString()).ToArray(), dico);
                    if (adjective != null) dico.ListAdjectives.Add(adjective);
                }
            }
            Console.WriteLine("Loading [" + dico.ListAdjectives.Count + "] adjectives completed ...");
        }
        public static void LoadAdverb(Dico dico)
        {
            Adverb adverb = null;
            DataTable dbResult = DBAdapter.ExecuteReader(ConfigurationManager.AppSettings["DB_NAME"].ToString(), string.Format("select * from {0}.t_mot where role like '%[adverbe]%';", ConfigurationManager.AppSettings["DB_NAME"].ToString()));
            if (dbResult.Rows.Count > 0)
            {
                foreach (DataRow dbItem in dbResult.Rows)
                {
                    adverb = ParseAdverb(LoadClassicWord(dbItem), dbItem.ItemArray.Select(i => i.ToString()).ToArray(), dico);
                    if (adverb != null) dico.ListAdverbs.Add(adverb);
                }
            }
            Console.WriteLine("Loading [" + dico.ListAdverbs.Count + "] adverbs completed ...");
        }
        public static void LoadConjonction(Dico dico)
        {
            Conjonction conjonction = null;
            DataTable dbResult = DBAdapter.ExecuteReader(ConfigurationManager.AppSettings["DB_NAME"].ToString(), string.Format("select * from {0}.t_mot where role like '%[conjonction]%';", ConfigurationManager.AppSettings["DB_NAME"].ToString()));
            if (dbResult.Rows.Count > 0)
            {
                foreach (DataRow dbItem in dbResult.Rows)
                {
                    conjonction = ParseConjonction(LoadClassicWord(dbItem), dbItem.ItemArray.Select(i => i.ToString()).ToArray(), dico);
                    if (conjonction != null) dico.ListConjonctions.Add(conjonction);
                }
            }
            Console.WriteLine("Loading [" + dico.ListConjonctions.Count + "] conjonctions completed ...");
        }
        public static void LoadVerb(Dico dico)
        {
            Verb verb = null;
            DataTable dbResult = DBAdapter.ExecuteReader(ConfigurationManager.AppSettings["DB_NAME"].ToString(), string.Format("select * from {0}.t_mot where role like '%[verbe]%';", ConfigurationManager.AppSettings["DB_NAME"].ToString()));
            if (dbResult.Rows.Count > 0)
            {
                foreach (DataRow dbItem in dbResult.Rows)
                {
                    try
                    {
                        verb = ParseVerb(LoadClassicWord(dbItem), dbItem.ItemArray.Select(i => i.ToString()).ToArray(), dico);
                        if (verb != null) dico.ListVerbs.Add(verb);
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp.Message);
                    }
                }
            }
            Console.WriteLine("Loading [" + dico.ListVerbs.Count + "] verbs completed ...");
        }
        public static void LoadNomCommuns(Dico dico)
        {
            NomCommun nomcommun = null;
            DataTable dbResult = DBAdapter.ExecuteReader(ConfigurationManager.AppSettings["DB_NAME"].ToString(), string.Format("select * from {0}.t_mot where role like '%[nom]%';", ConfigurationManager.AppSettings["DB_NAME"].ToString()));
            if (dbResult.Rows.Count > 0)
            {
                foreach (DataRow dbItem in dbResult.Rows)
                {
                    nomcommun = ParseNomCommun(LoadClassicWord(dbItem), dbItem.ItemArray.Select(i => i.ToString()).ToArray(), dico);
                    dico.ListNomCommuns.Add(nomcommun);
                }
            }
            Console.WriteLine("Loading [" + dico.ListNomCommuns.Count + "] common nouns completed ...");
        }
        public static void LoadNomPropres(Dico dico)
        {
            NomPropre nompropre = null;
            DataTable dbResult = DBAdapter.ExecuteReader(ConfigurationManager.AppSettings["DB_NAME"].ToString(), string.Format("select nom, prénom, surnom, genre from {0}.t_connaissance;", ConfigurationManager.AppSettings["DB_NAME"].ToString()));
            if (dbResult.Rows.Count > 0)
            {
                foreach (DataRow dbItem in dbResult.Rows)
                {
                    Word w = new Word();
                    GENRE g;
                    if (Enum.TryParse(dbItem.ItemArray[3].ToString(), out g)) w.Genre = g;
                    nompropre = ParseNomPropre(w, dbItem.ItemArray.Select(i => i.ToString()).ToArray(), dico);
                    nompropre.Definition = "connaissance";
                    dico.ListNomPropres.Add(nompropre);
                }
            }
            dbResult = DBAdapter.ExecuteReader(ConfigurationManager.AppSettings["DB_NAME"].ToString(), string.Format("select valeur, genre from {0}.t_prenom;", ConfigurationManager.AppSettings["DB_NAME"].ToString()));
            if (dbResult.Rows.Count > 0)
            {
                foreach (DataRow dbItem in dbResult.Rows)
                {
                    nompropre = new NomPropre();
                    nompropre.Text = dbItem.ItemArray[0].ToString();
                    nompropre.Definition = "prénom";
                    switch (dbItem.ItemArray[1].ToString())
                    {
                        case "m":
                            nompropre.Genre = GENRE.MASCULIN;
                            break;
                        case "f":
                            nompropre.Genre = GENRE.FEMININ;
                            break;
                        case "n":
                            nompropre.Genre = GENRE.NEUTRE;
                            break;
                        default:
                            nompropre.Genre = GENRE.UNKNOWN;
                            break;
                    }
                    dico.ListNomPropres.Add(nompropre);
                }
            }
            Console.WriteLine("Loading [" + dico.ListNomPropres.Count + "] personal nouns completed ...");
        }
        public static void LoadDeterminants(Dico dico)
        {
            Determinant determinant = null;
            DataTable dbResult = DBAdapter.ExecuteReader(ConfigurationManager.AppSettings["DB_NAME"].ToString(), string.Format("select * from {0}.t_mot where role like '%[determinant]%';", ConfigurationManager.AppSettings["DB_NAME"].ToString()));
            if (dbResult.Rows.Count > 0)
            {
                foreach (DataRow dbItem in dbResult.Rows)
                {
                    determinant = ParseDeterminant(LoadClassicWord(dbItem), dbItem.ItemArray.Select(i => i.ToString()).ToArray(), dico);
                    dico.ListDeterminant.Add(determinant);
                }
            }
            Console.WriteLine("Loading [" + dico.ListDeterminant.Count + "] determinants completed ...");
        }        
        public static void LoadPreposition(Dico dico)
        {
            Preposition preposition = null;
            DataTable dbResult = DBAdapter.ExecuteReader(ConfigurationManager.AppSettings["DB_NAME"].ToString(), string.Format("select * from {0}.t_mot where role like '%[preposition]%';", ConfigurationManager.AppSettings["DB_NAME"].ToString()));
            if (dbResult.Rows.Count > 0)
            {
                foreach (DataRow dbItem in dbResult.Rows)
                {
                    preposition = ParsePreposition(LoadClassicWord(dbItem), dbItem.ItemArray.Select(i => i.ToString()).ToArray(), dico);
                    dico.ListPreposition.Add(preposition);
                }
            }
            Console.WriteLine("Loading [" + dico.ListPreposition.Count + "] prepositions completed ...");
        }
        public static void LoadSyllabe(Dico dico)
        {
            Syllabe syllabe = null;
            DataTable dbResult = DBAdapter.ExecuteReader(ConfigurationManager.AppSettings["DB_NAME"].ToString(), string.Format("select * from {0}.t_syllabe;", ConfigurationManager.AppSettings["DB_NAME"].ToString()));
            if (dbResult.Rows.Count > 0)
            {
                foreach (DataRow dbItem in dbResult.Rows)
                {
                    if (dbItem.ItemArray.Count() > 1)
                    {
                        syllabe = new Syllabe(dbItem.ItemArray[0].ToString(), dbItem.ItemArray[1].ToString(), false);
                        dico.ListSyllabe.Add(syllabe);
                    }
                }
            }
            Console.WriteLine("Loading [" + dico.ListSyllabe.Count + "] syllabes completed ...");
        }
        public static void LoadChiffre(Dico dico)
        {
            Chiffre chf = null;
            DataTable dbResult = DBAdapter.ExecuteReader(ConfigurationManager.AppSettings["DB_NAME"].ToString(), string.Format("select * from {0}.t_mot where role like '%chiffre%';", ConfigurationManager.AppSettings["DB_NAME"].ToString()));
            if (dbResult.Rows.Count > 0)
            {
                foreach (DataRow dbItem in dbResult.Rows)
                {
                    if (dbItem.ItemArray.Count() > 1)
                    {
                        chf = new Chiffre(LoadClassicWord(dbItem));
                        dico.ListChiffre.Add(chf);
                    }
                }
            }
            Console.WriteLine("Loading [" + dico.ListChiffre.Count + "] numbers completed ...");
        }
        public static void LoadConjugaison(Dico dico)
        {
            Time time = null;
            DataTable dbResult = DBAdapter.ExecuteReader(ConfigurationManager.AppSettings["DB_NAME"].ToString(), string.Format("select * from {0}.t_conjugaison;", ConfigurationManager.AppSettings["DB_NAME"].ToString()));
            DataTable dbColum = DBAdapter.Desc(ConfigurationManager.AppSettings["DB_NAME"].ToString(), "t_conjugaison;");
            if (dbResult.Rows.Count > 0)
            {
                foreach (DataRow dbItem in dbResult.Rows)
                {
                    time = new Time();
                    for (int i = 0; i < dbColum.Rows.Count; i++)
                    {
                        switch (dbColum.Rows[i].ItemArray[0].ToString().ToLower())
                        {
                            case "exception":
                                time.Exception = i <= dbItem.ItemArray.Length ? (dbItem.ItemArray[i].ToString().Equals("1")) : false;
                                break;
                            case "groupe":
                                time.Group = i <= dbItem.ItemArray.Length ? dbItem.ItemArray[i].ToString() : string.Empty;
                                break;
                            case "type":
                                time.Type = i <= dbItem.ItemArray.Length ? dbItem.ItemArray[i].ToString() : string.Empty;
                                break;
                            case "temps":
                                if (i <= dbItem.ItemArray.Length)
                                    try { time.Form = (FORM)Enum.Parse(typeof(FORM), dbItem.ItemArray[i].ToString().ToUpper()); }
                                    catch { }
                                else time.Form = FORM.UNKNOWN;
                                break;
                            case "nombre":
                                if (i <= dbItem.ItemArray.Length)
                                    try { time.Nombre = (NOMBRE)Enum.Parse(typeof(NOMBRE), dbItem.ItemArray[i].ToString().ToUpper()); }
                                    catch { }
                                else time.Nombre = NOMBRE.UNKNOWN;
                                break;
                            case "personne":
                                time.Person = i <= dbItem.ItemArray.Length ? int.Parse(dbItem.ItemArray[i].ToString()) : 0;
                                break;
                            case "valeur":
                                time.Val = i <= dbItem.ItemArray.Length ? dbItem.ItemArray[i].ToString() : string.Empty;
                                break;
                            case "suffixe":
                                time.Suffixe = dbItem.ItemArray[i].ToString();
                                break;
                            case "infinitif":
                                if (i <= dbItem.ItemArray.Length && !string.IsNullOrEmpty(dbItem.ItemArray[i].ToString())) time.Infinitif = dbItem.ItemArray[i].ToString();
                                break;
                        }
                    }
                    dico.Conjugaison.Times.Add(time);
                }
            }
            Console.WriteLine("Loading [" + dico.Conjugaison.Times.Count + "] conjugaisons completed ...");
        }
        public static void LoadAccords(Accord accord)
        {
            Console.WriteLine("Loading [0] accords completed ...");
        }
        public static void RefineVerb(Dico dico)
        {
            foreach (Verb verb in dico.ListVerbs)
            {
                foreach (Time t in dico.Conjugaison.Times)
                {
                    if (t.Exception && verb.Text.Equals(t.Val))
                    {
                        verb.Infinitive = t.Infinitif;
                        break;
                    }
                }
            }
            Console.WriteLine("Refining [" + dico.ListVerbs.Count + "] verbs completed ...");
        }

        public static Pronom ParsePronom(Word w, string[] dbItem, Dico dico)
        {
            Pronom pronom = new Pronom(w);
            switch (dbItem[WORDGROUPE].ToLower())
            {
                case "demonstratif":
                    if (pronom != null) dico.ListPronoms.Add(pronom);
                    pronom = new Pronom();
                    pronom.Cathegory = PRONOM.DEMONSTRATIF;
                    break;
                case "indefini":
                    if (pronom != null) dico.ListPronoms.Add(pronom);
                    pronom = new Pronom();
                    pronom.Cathegory = PRONOM.INDEFINI;
                    break;
                case "interrogatif":
                    if (pronom != null) dico.ListPronoms.Add(pronom);
                    pronom = new Pronom();
                    pronom.Cathegory = PRONOM.INTERROGATIF;
                    break;
                case "personnel":
                    if (pronom != null) dico.ListPronoms.Add(pronom);
                    pronom = new Pronom();
                    pronom.Cathegory = PRONOM.PERSONNEL;
                    break;
                case "possessif":
                    if (pronom != null) dico.ListPronoms.Add(pronom);
                    pronom = new Pronom();
                    pronom.Cathegory = PRONOM.POSSESSIF;
                    break;
                case "relatif":
                    if (pronom != null) dico.ListPronoms.Add(pronom);
                    pronom = new Pronom();
                    pronom.Cathegory = PRONOM.RELATIF;
                    break;
            }
            return pronom;
        }
        public static Adjective ParseAdjective(Word w, string[] dbItem, Dico dico)
        {
            Adjective adjective = new Adjective(w);
            switch (dbItem[WORDGROUPE].ToLower())
            {
                case "apparence":
                    adjective.Cathegory = ADJECTIVE.APPARENCE;
                    break;
                case "couleur":
                    adjective.Cathegory = ADJECTIVE.COULEUR;
                    break;
                case "etendue":
                    adjective.Cathegory = ADJECTIVE.ETENDUE;
                    break;
                case "forme":
                    adjective.Cathegory = ADJECTIVE.FORME;
                    break;
                case "gout":
                    adjective.Cathegory = ADJECTIVE.GOUT;
                    break;
                case "personnalite":
                    adjective.Cathegory = ADJECTIVE.PERSONNALITE;
                    break;
                case "quantite":
                    adjective.Cathegory = ADJECTIVE.QUANTITE;
                    break;
                case "sentiment":
                    adjective.Cathegory = ADJECTIVE.SENTIMENT;
                    break;
                case "son":
                    adjective.Cathegory = ADJECTIVE.SON;
                    break;
                case "temps":
                    adjective.Cathegory = ADJECTIVE.TEMPS;
                    break;
                case "toucher":
                    adjective.Cathegory = ADJECTIVE.TOUCHER;
                    break;
            }
            return adjective;
        }
        public static Adverb ParseAdverb(Word w, string[] dbItem, Dico dico)
        {
            Adverb adverb = new Adverb(w);
            switch (dbItem[WORDGROUPE].ToLower())
            {
                case "affirmation":
                    if (adverb != null) dico.ListAdverbs.Add(adverb);
                    adverb.Cathegory = ADVERB.AFFIRMATION;
                    break;
                case "liaison":
                    if (adverb != null) dico.ListAdverbs.Add(adverb);
                    adverb.Cathegory = ADVERB.LIAISON;
                    break;
                case "lieu":
                    if (adverb != null) dico.ListAdverbs.Add(adverb);
                    adverb.Cathegory = ADVERB.LIEU;
                    break;
                case "maniere":
                    if (adverb != null) dico.ListAdverbs.Add(adverb);
                    adverb.Cathegory = ADVERB.MANIERE;
                    break;
                case "modal":
                    if (adverb != null) dico.ListAdverbs.Add(adverb);
                    adverb.Cathegory = ADVERB.MODAL;
                    break;
                case "negation":
                    if (adverb != null) dico.ListAdverbs.Add(adverb);
                    adverb.Cathegory = ADVERB.NEGATION;
                    break;
                case "quantite":
                    if (adverb != null) dico.ListAdverbs.Add(adverb);
                    adverb.Cathegory = ADVERB.QUANTITE;
                    break;
                case "relation":
                    if (adverb != null) dico.ListAdverbs.Add(adverb);
                    adverb.Cathegory = ADVERB.RELATION;
                    break;
                case "temps":
                    if (adverb != null) dico.ListAdverbs.Add(adverb);
                    adverb.Cathegory = ADVERB.TEMPS;
                    break;
                case "question":
                    if (adverb != null) dico.ListAdverbs.Add(adverb);
                    adverb.Cathegory = ADVERB.QUESTION;
                    break;
            }
            return adverb;
        }
        public static Conjonction ParseConjonction(Word w, string[] dbItem, Dico dico)
        {
            Conjonction conjonction = new Conjonction(w);

            switch (dbItem[WORDGROUPE].ToLower())
            {
                case "coordination":
                    if (conjonction != null) dico.ListConjonctions.Add(conjonction);
                    conjonction.Cathegory = CONJONCTION.COORDINATION;
                    break;
                case "locution":
                    if (conjonction != null) dico.ListConjonctions.Add(conjonction);
                    conjonction.Cathegory = CONJONCTION.LOCUTION;
                    break;
                case "subordination":
                    if (conjonction != null) dico.ListConjonctions.Add(conjonction);
                    conjonction.Cathegory = CONJONCTION.SUBORDINATION;
                    break;
            }
            return conjonction;
        }
        public static Verb ParseVerb(Word w, string[] dbItem, Dico dico)
        {
            try
            {
                Verb verb = new Verb(w);
                verb.Group = dbItem[WORDGROUPE];
                verb.Root = verb.Text.Substring(0, (verb.Text.Length - verb.Suffixe.Length));
                foreach (Time t in dico.Conjugaison.Times)
                {
                    if (t.Exception && t.Infinitif != null && t.Infinitif.Equals(verb.Text))
                    {
                        verb.Exception = true;
                        verb.Root = null;
                        t.Lexiques = verb.LexicalField;
                        if (!string.IsNullOrEmpty(t.Infinitif))
                        {
                            // Datatable change, check column/row order
                            DataTable dbDetails = DBAdapter.ExecuteReader(ConfigurationManager.AppSettings["DB_NAME"].ToString(), string.Format("select définition from {0}.t_mot where valeur = '" + t.Infinitif + "';", ConfigurationManager.AppSettings["DB_NAME"].ToString()));
                            if (dbDetails.Rows.Count > 0 && dbDetails.Columns.Count > 0 && !string.IsNullOrEmpty(dbDetails.Rows[0].ItemArray[0].ToString()))
                            {
                                verb.Definition = dbDetails.Rows[0].ItemArray[0].ToString();
                            }
                        }
                    }
                }
                return verb;
            }
            catch (Exception exp)
            {
                Console.WriteLine("Cannot find verbs : " + exp.Message);
                return null;
            }
        }
        public static NomCommun ParseNomCommun(Word w, string[] dbItem, Dico dico)
        {
            NomCommun nomCommun = new NomCommun(w);
            nomCommun.Capacities.AddRange(dbItem[WORDCAPACITE].Split(';'));
            nomCommun.Incapacities.AddRange(dbItem[WORDINCAPACITE].Split(';'));
            return nomCommun;
        }
        public static NomPropre ParseNomPropre(Word w, string[] dbItem, Dico dico)
        {
            NomPropre nomPropre = new NomPropre(w);
            nomPropre.Text = dbItem[1] + " " + dbItem[0];
            nomPropre.Nom = dbItem[0];
            nomPropre.Prenom = dbItem[1];
            nomPropre.Surnom = dbItem[2];
            return nomPropre;
        }
        public static Determinant ParseDeterminant(Word w, string[] dbItem, Dico dico)
        {
            Determinant determinant = new Determinant(w);
            
            if (dbItem[5].ToLower().Equals("indetermine")) determinant.Group = Determinant.GROUP.Indetermine;
            else if (dbItem[5].ToLower().Equals("demonstratif")) determinant.Group = Determinant.GROUP.Demonstratif;
            else if (dbItem[5].ToLower().Equals("possesif")) determinant.Group = Determinant.GROUP.Possessif;
            else determinant.Group = Determinant.GROUP.Unknown;
            return determinant;
        }
        public static Preposition ParsePreposition(Word w, string[] dbItem, Dico dico)
        {
            Preposition preposition = new Preposition(w);
            return preposition;
        }
        #endregion

        #region Methods private
        private static Time buildTime(XmlTextReader textReader, FORM f)
        {
            Time time = new Time();
            time.Form = f;
            time.Group = textReader.GetAttribute(0).ToString();
            time.Type = textReader.GetAttribute(1).ToString();
            if (textReader.GetAttribute(2).ToString().Equals("singulier")) time.Nombre = NOMBRE.SINGULIER;
            else if (textReader.GetAttribute(2).ToString().Equals("pluriel")) time.Nombre = NOMBRE.PLURIEL;
            else time.Nombre = NOMBRE.UNKNOWN;
            time.Person = int.Parse(textReader.GetAttribute(3).ToString());
            return time;
        }
        private static Time buildException(XmlTextReader textReader)
        {
            Time time = new Time();
            time.Group = textReader.GetAttribute(0).ToString();
            time.Type = textReader.GetAttribute(1).ToString();

            if (textReader.GetAttribute(2).ToString().Equals("present")) time.Form = FORM.PRESENT;
            else if (textReader.GetAttribute(2).ToString().Equals("passesimple")) time.Form = FORM.PASSESIMPLE;
            else if (textReader.GetAttribute(2).ToString().Equals("imparfait")) time.Form = FORM.IMPARFAIT;
            else if (textReader.GetAttribute(2).ToString().Equals("futur")) time.Form = FORM.FUTUR;
            else if (textReader.GetAttribute(2).ToString().Equals("conditionnel")) time.Form = FORM.CONDITIONNEL;
            else time.Form = FORM.UNKNOWN;

            if (textReader.GetAttribute(3).ToString().Equals("singulier")) time.Nombre = NOMBRE.SINGULIER;
            else if (textReader.GetAttribute(3).ToString().Equals("pluriel")) time.Nombre = NOMBRE.PLURIEL;
            else time.Nombre = NOMBRE.UNKNOWN;

            time.Person = int.Parse(textReader.GetAttribute(4).ToString());

            // pas dans conj
            time.Exception = true;
            time.Infinitif = textReader.GetAttribute(5).ToString();
            return time;
        }
        #endregion
    }
}
