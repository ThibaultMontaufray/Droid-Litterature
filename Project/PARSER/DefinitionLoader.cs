using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using Droid_litterature;
using Droid_database;

namespace Droid_litterature
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
        public static Word LoadClassicWord(string[] dbResult)
        {
            Word w = new Word();
            w.Text = string.IsNullOrEmpty(dbResult[WORDVALEUR]) ? string.Empty : dbResult[WORDVALEUR].ToLower();
            w.Definition = string.IsNullOrEmpty(dbResult[WORDDEFINITION]) ? string.Empty : dbResult[WORDDEFINITION].ToLower();

            if (dbResult[WORDGENRE].ToLower().Equals("masculin")) w.Genre = GENRE.MASCULIN;
            else if (dbResult[WORDGENRE].ToLower().Equals("feminin")) w.Genre = GENRE.FEMININ;
            else if (dbResult[WORDGENRE].ToLower().Equals("neutre")) w.Genre = GENRE.NEUTRE;
            else w.Genre = GENRE.UNKNOWN;

            if (dbResult[WORDNOMBRE].ToLower().Equals("singulier")) w.Nombre = NOMBRE.SINGULIER;
            else if (dbResult[WORDNOMBRE].ToLower().Equals("pluriel")) w.Nombre = NOMBRE.PLURIEL;
            else if (dbResult[WORDNOMBRE].ToLower().Equals("neutre")) w.Nombre = NOMBRE.NEUTRE;
            else w.Nombre = NOMBRE.UNKNOWN;

            w.Exception = dbResult[WORDEXCEPTION].ToLower().Equals("non");

            if (dbResult[WORDPERSONNE].ToLower().Equals("1")) w.Pers = 1;
            else if (dbResult[WORDPERSONNE].ToLower().Equals("2")) w.Pers = 2;
            else if (dbResult[WORDPERSONNE].ToLower().Equals("3")) w.Pers = 3;
            else w.Pers = 0;

            w.Prefixe = string.IsNullOrEmpty(dbResult[WORDPREFIXE]) ? string.Empty : dbResult[9].ToLower();
            w.Suffixe = string.IsNullOrEmpty(dbResult[WORDSUFFIXE]) ? string.Empty : dbResult[10].ToLower();

            //string[] s_tab1 = dbResult[WORDCHAMPLEXICAL].Split('|');
            //foreach (string s in s_tab1) w.LexicalField.Add(new Lexique(s));
            
            string[] s_tab2 = dbResult[WORDSYNONYME].Split('|');
            foreach (string s in s_tab2) w.Synonymes.Add(s);
            //dico.ParseSyllabe(pronom);

            return w;
        }
        public static void LoadPronom(Dico dico)
        {
            Pronom pronom = null;
            List<string[]> dbResult = MySqlAdapter.ExecuteReader("select * from t_mot where role like '%[pronom]%';");
            if (dbResult.Count > 0)
            {
                foreach (string[] dbItem in dbResult)
                {
                    pronom  = ParsePronom(LoadClassicWord(dbItem), dbItem, dico);
                    if (pronom != null) dico.ListPronoms.Add(pronom);
                }
            }
        }
        public static void LoadAdjective(Dico dico)
        {
            Adjective adjective = null;
            List<string[]> dbResult = MySqlAdapter.ExecuteReader("select * from t_mot where role like '%[adjectif]%';");
            if (dbResult.Count > 0)
            {
                foreach (string[] dbItem in dbResult)
                {
                    adjective = ParseAdjective(LoadClassicWord(dbItem), dbItem, dico);
                    if (adjective != null) dico.ListAdjectives.Add(adjective);
                }
            }
        }
        public static void LoadAdverb(Dico dico)
        {
            Adverb adverb = null;
            List<string[]> dbResult = MySqlAdapter.ExecuteReader("select * from t_mot where role like '%[adverbe]%';");
            if (dbResult.Count > 0)
            {
                foreach (string[] dbItem in dbResult)
                {
                    adverb = ParseAdverb(LoadClassicWord(dbItem), dbItem, dico);
                    if (adverb != null) dico.ListAdverbs.Add(adverb);
                }
            }
        }
        public static void LoadConjonction(Dico dico)
        {
            Conjonction conjonction = null;
            List<string[]> dbResult = MySqlAdapter.ExecuteReader("select * from t_mot where role like '%[conjonction]%';");
            if (dbResult.Count > 0)
            {
                foreach (string[] dbItem in dbResult)
                {
                    conjonction = ParseConjonction(LoadClassicWord(dbItem), dbItem, dico);
                    if (conjonction != null) dico.ListConjonctions.Add(conjonction);
                }
            }
        }
        public static void LoadVerb(Dico dico)
        {
            Verb verb = null;
            List<string[]> dbResult = MySqlAdapter.ExecuteReader("select * from t_mot where role like '%[verbe]%';");
            if (dbResult.Count > 0)
            {
                foreach (string[] dbItem in dbResult)
                {
                    try
                    {
                        verb = ParseVerb(LoadClassicWord(dbItem), dbItem, dico);
                        if (verb != null) dico.ListVerbs.Add(verb);
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp.Message);
                    }
                }
            }
        }
        public static void LoadNomCommuns(Dico dico)
        {
            NomCommun nomcommun = null;
            List<string[]> dbResult = MySqlAdapter.ExecuteReader("select * from t_mot where role like '%[nom]%';");
            if (dbResult.Count > 0)
            {
                foreach (string[] dbItem in dbResult)
                {
                    nomcommun = ParseNomCommun(LoadClassicWord(dbItem), dbItem, dico);
                    dico.ListNomCommuns.Add(nomcommun);
                }
            }
        }
        public static void LoadNomPropres(Dico dico)
        {
            NomPropre nompropre = null;
            List<string[]> dbResult = MySqlAdapter.ExecuteReader("select nom, prénom, surnom, genre from t_connaissance;");
            if (dbResult.Count > 0)
            {
                foreach (string[] dbItem in dbResult)
                {
                    Word w = new Word();
                    GENRE g;
                    if (Enum.TryParse(dbItem[3], out g)) w.Genre = g;
                    nompropre = ParseNomPropre(w, dbItem, dico);
                    nompropre.Definition = "connaissance";
                    dico.ListNomPropres.Add(nompropre);
                }
            }
            dbResult = MySqlAdapter.ExecuteReader("select valeur, genre from t_prenom;");
            if (dbResult.Count > 0)
            {
                foreach (string[] dbItem in dbResult)
                {
                    nompropre = new NomPropre();
                    nompropre.Text = dbItem[0];
                    nompropre.Definition = "prénom";
                    switch (dbItem[1])
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
        }
        public static void LoadDeterminants(Dico dico)
        {
            Determinant determinant = null;
            List<string[]> dbResult = MySqlAdapter.ExecuteReader("select * from t_mot where role like '%[determinant]%';");
            if (dbResult.Count > 0)
            {
                foreach (string[] dbItem in dbResult)
                {
                    determinant = ParseDeterminant(LoadClassicWord(dbItem), dbItem, dico);
                    dico.ListDeterminant.Add(determinant);
                }
            }
        }        
        public static void LoadPreposition(Dico dico)
        {
            Preposition preposition = null;
            List<string[]> dbResult = MySqlAdapter.ExecuteReader("select * from t_mot where role like '%[preposition]%';");
            if (dbResult.Count > 0)
            {
                foreach (string[] dbItem in dbResult)
                {
                    preposition = ParsePreposition(LoadClassicWord(dbItem), dbItem, dico);
                    dico.ListPreposition.Add(preposition);
                }
            }
        }
        public static void LoadSyllabe(Dico dico)
        {
            Syllabe syllabe = null;
            List<string[]> dbResult = MySqlAdapter.ExecuteReader("select * from t_syllabe;");
            if (dbResult.Count > 0)
            {
                foreach (string[] dbItem in dbResult)
                {
                    if (dbItem.Length > 1)
                    {
                        syllabe = new Syllabe(dbItem[0], dbItem[1], false);
                        dico.ListSyllabe.Add(syllabe);
                    }
                }
            }
        }
        public static void LoadChiffre(Dico dico)
        {
            Chiffre chf = null;
            List<string[]> dbResult = MySqlAdapter.ExecuteReader("select * from t_mot where role like '%chiffre%';");
            if (dbResult.Count > 0)
            {
                foreach (string[] dbItem in dbResult)
                {
                    if (dbItem.Length > 1)
                    {
                        chf = new Chiffre(LoadClassicWord(dbItem));
                        dico.ListChiffre.Add(chf);
                    }
                }
            }
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
                            List<string[]> dbDetails = MySqlAdapter.ExecuteReader("select définition from t_mot where valeur = '" + t.Infinitif + "';");
                            if (dbDetails.Count > 0 && dbDetails[0].Length > 0 && !string.IsNullOrEmpty(dbDetails[0][0]))
                            {
                                verb.Definition = dbDetails[0][0];
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

        public static void LoadConjugaison(Dico dico)
        {
            Time time = null;
            List<string[]> dbResult = MySqlAdapter.ExecuteReader("select * from t_conjugaison;");
            List<string[]> dbColum = MySqlAdapter.ExecuteReader("desc t_conjugaison;");
            if (dbResult.Count > 0)
            {
                foreach (string[] dbItem in dbResult)
                {
                    time = new Time();
                    for(int i=0 ; i<dbColum.Count ; i++)
                    {
                        switch(dbColum[i][0].ToLower())
                        {
                            case "exception": time.Exception = i <= dbItem.Length ? (dbItem[i].Equals("1")) : false;
                                break;
                            case "groupe": time.Group = i <= dbItem.Length ? dbItem[i] : string.Empty;
                                break;
                            case "type": time.Type = i <= dbItem.Length ? dbItem[i] : string.Empty;
                                break;
                            case "temps": 
                                if (i <= dbItem.Length)
                                    try { time.Form = (FORM)Enum.Parse(typeof(FORM), dbItem[i].ToUpper()); }
                                    catch { }
                                else time.Form = FORM.UNKNOWN;
                                break;
                            case "nombre": 
                                if (i <= dbItem.Length)
                                    try { time.Nombre = (NOMBRE)Enum.Parse(typeof(NOMBRE), dbItem[i].ToUpper()); }
                                    catch { }
                                else time.Nombre = NOMBRE.UNKNOWN;
                                break;
                            case "personne": time.Person = i <= dbItem.Length ? int.Parse(dbItem[i]) : 0;
                                break;
                            case "valeur": time.Val = i <= dbItem.Length ? dbItem[i] : string.Empty;
                                break;
                            case "suffixe": time.Suffixe = dbItem[i];
                                break;
                            case "infinitif": if(i <= dbItem.Length && !string.IsNullOrEmpty(dbItem[i])) time.Infinitif = dbItem[i];
                                break;
                        }
                    }
                    dico.Conjugaison.Times.Add(time);
                }
            }
        }
        public static void LoadAccords(Accord accord)
        {
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
