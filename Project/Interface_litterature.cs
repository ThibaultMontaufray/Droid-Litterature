namespace Droid_litterature
{
    using Droid_database;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Interface for Tobi Assistant application : take care, some french word here to allow Tobi to speak with natural langage.
    /// </summary>            
    public class Interface_litterature
    {
        #region Attribute
        private static Dico _dico;
        #endregion

        #region Properties
        /// <summary>
        /// Current dictionnary to speak with soft
        /// </summary>
        public static Dico Dictionnary
        {
            get { return _dico; }
            set { _dico = value; }
        }
        #endregion

        #region Init
        public static void Init(LANGAGE language)
        {
            if (!MySqlAdapter.IsConnectionPossible())
            {
                _dico = Dumper.LoadDump();
            }
            else
            {
                _dico = new Dico(language, "France");
            }
        }
        public static void Init(string Language)
        {
            if (!MySqlAdapter.IsConnectionPossible())
            {
                _dico = Dumper.LoadDump();
            }
            else
            {
                LANGAGE lang = (LANGAGE)Enum.Parse(typeof(LANGAGE), Language);
                _dico = new Dico(lang, "France");
            }
        }
        #endregion

        #region Methods public
        public static void SetSynonyme(string word1, string word2)
        {
            if (_dico != null && !string.IsNullOrEmpty(word1) && !string.IsNullOrEmpty(word2))
            {
                try
                {
                    if (_dico.ListAdjectives.Where(w => word1.Equals(w.Text)).ToList().Count > 0) { _dico.ListAdjectives.Where(w => word1.Equals(w.Text)).ToList()[0].Synonymes.Add(word2); }
                    else if (_dico.ListAdverbs.Where(w => word1.Equals(w.Text)).ToList().Count > 0) { _dico.ListAdverbs.Where(w => word1.Equals(w.Text)).ToList()[0].Synonymes.Add(word2); }
                    else if (_dico.ListConjonctions.Where(w => word1.Equals(w.Text)).ToList().Count > 0) { _dico.ListConjonctions.Where(w => word1.Equals(w.Text)).ToList()[0].Synonymes.Add(word2); }
                    else if (_dico.ListDeterminant.Where(w => word1.Equals(w.Text)).ToList().Count > 0) { _dico.ListDeterminant.Where(w => word1.Equals(w.Text)).ToList()[0].Synonymes.Add(word2); }
                    else if (_dico.ListNomCommuns.Where(w => word1.Equals(w.Text)).ToList().Count > 0) { _dico.ListNomCommuns.Where(w => word1.Equals(w.Text)).ToList()[0].Synonymes.Add(word2); }
                    else if (_dico.ListPreposition.Where(w => word1.Equals(w.Text)).ToList().Count > 0) { _dico.ListPreposition.Where(w => word1.Equals(w.Text)).ToList()[0].Synonymes.Add(word2); }
                    else if (_dico.ListPronoms.Where(w => word1.Equals(w.Text)).ToList().Count > 0) { _dico.ListPronoms.Where(w => word1.Equals(w.Text)).ToList()[0].Synonymes.Add(word2); }
                    else if (_dico.ListVerbs.Where(w => word1.Equals(w.Text)).ToList().Count > 0) { _dico.ListVerbs.Where(w => word1.Equals(w.Text)).ToList()[0].Synonymes.Add(word2); }

                    if (_dico.ListAdjectives.Where(w => word2.Equals(w.Text)).ToList().Count > 0) { _dico.ListAdjectives.Where(w => word2.Equals(w.Text)).ToList()[0].Synonymes.Add(word1); }
                    else if (_dico.ListAdverbs.Where(w => word2.Equals(w.Text)).ToList().Count > 0) { _dico.ListAdverbs.Where(w => word2.Equals(w.Text)).ToList()[0].Synonymes.Add(word1); }
                    else if (_dico.ListConjonctions.Where(w => word2.Equals(w.Text)).ToList().Count > 0) { _dico.ListConjonctions.Where(w => word2.Equals(w.Text)).ToList()[0].Synonymes.Add(word1); }
                    else if (_dico.ListDeterminant.Where(w => word2.Equals(w.Text)).ToList().Count > 0) { _dico.ListDeterminant.Where(w => word2.Equals(w.Text)).ToList()[0].Synonymes.Add(word1); }
                    else if (_dico.ListNomCommuns.Where(w => word2.Equals(w.Text)).ToList().Count > 0) { _dico.ListNomCommuns.Where(w => word2.Equals(w.Text)).ToList()[0].Synonymes.Add(word1); }
                    else if (_dico.ListPreposition.Where(w => word2.Equals(w.Text)).ToList().Count > 0) { _dico.ListPreposition.Where(w => word2.Equals(w.Text)).ToList()[0].Synonymes.Add(word1); }
                    else if (_dico.ListPronoms.Where(w => word2.Equals(w.Text)).ToList().Count > 0) { _dico.ListPronoms.Where(w => word2.Equals(w.Text)).ToList()[0].Synonymes.Add(word1); }
                    else if (_dico.ListVerbs.Where(w => word2.Equals(w.Text)).ToList().Count > 0) { _dico.ListVerbs.Where(w => word2.Equals(w.Text)).ToList()[0].Synonymes.Add(word1); }
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
        }
        public static void AddWord(string text, List<string> roles)
        {
            try
            {
                List<string[]> dbResult = MySqlAdapter.ExecuteReader(string.Format("select * from t_mot where valeur = '{0}'", text));
                Word word = DefinitionLoader.LoadClassicWord(dbResult[0]);

                foreach (string role in roles)
                {
                    switch (role.ToLower())
                    {
                        case "adjectif":
                            _dico.ListAdjectives.Add(DefinitionLoader.ParseAdjective(word, dbResult[0], _dico));
                            break;
                        case "adverbe":
                            _dico.ListAdverbs.Add(DefinitionLoader.ParseAdverb(word, dbResult[0], _dico));
                            break;
                        case "conjonction":
                            _dico.ListConjonctions.Add(DefinitionLoader.ParseConjonction(word, dbResult[0], _dico));
                            break;
                        case "determinant":
                            _dico.ListDeterminant.Add(DefinitionLoader.ParseDeterminant(word, dbResult[0], _dico));
                            break;
                        case "nom":
                            _dico.ListNomCommuns.Add(DefinitionLoader.ParseNomCommun(word, dbResult[0], _dico));
                            break;
                        case "preposition":
                            _dico.ListPreposition.Add(DefinitionLoader.ParsePreposition(word, dbResult[0], _dico));
                            break;
                        case "pronom":
                            _dico.ListPronoms.Add(DefinitionLoader.ParsePronom(word, dbResult[0], _dico));
                            break;
                        case "verbe":
                            _dico.ListVerbs.Add(DefinitionLoader.ParseVerb(word, dbResult[0], _dico));
                            break;
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("An error occured while parsing word " + text + " ERROR : " + exp.Message);
            }
        }

        public static string ACTION_130_apprendre_mot(string texte, string genre = "", string nombre = "", string langue = "")
        {
            return string.Empty;
        }
        public static string ACTION_131_definir_mot(string texte)
        {
            return string.Empty;
        }
        public static string ACTION_132_comparer_mot(string mot_1, string mot_2, string qualificatif)
        {
            return mot_1;
        }
        public static string ACTION_133_donner_oppose(string mot)
        {
            return string.Empty;
        }
        public static string ACTION_134_donner_contraire(string mot)
        {
            return string.Empty;
        }
        public static string ACTION_135_saluer()
        {
            return "Bonjour";
        }
        public static string ACTION_136_prendre_conge()
        {
            return "Au revoir";
        }
        #endregion

        #region Methods private
        #endregion
    }
}
