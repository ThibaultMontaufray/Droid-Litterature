namespace Droid.Litterature
{
    using Droid.Database;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration;
    using System.Data;
    using System.Linq;

    /// <summary>
    /// Interface for Tobi Assistant application : take care, some french word here to allow Tobi to speak with natural language.
    /// </summary>            
    public class Interface_litterature
    {
        #region Attribute
        private static Dico _dico;
        private static IConfiguration _config = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build();
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

        #region Constructor
        private Interface_litterature()
        {
            _dico = new Dico();
        }
        #endregion  

        #region Init
        public static void Init(LANGAGE language, DataBasesType dbType)
        {
            if (!DBAdapter.IsConnected)
            {
                _dico = Dumper.LoadDump();
            }
            else
            {
                _dico = new Dico(language, "France");
            }
        }
        public static void Init(string language, DataBasesType dbType)
        {
            if (!DBAdapter.IsConnected)
            {
                _dico = Dumper.LoadDump();
            }
            else
            {
                LANGAGE lang = (LANGAGE)Enum.Parse(typeof(LANGAGE), language);
                _dico = new Dico(lang, "France");
            }
        }
        #endregion

        #region Methods public

        #region Action
        [Description("french[definir.mot(mot)];english[definition.word(word)]")]
        public static Dictionary<string, object[]> ACTION_130_definition_word(string word)
        {
            Dictionary<string, object[]> objDump = new Dictionary<string, object[]>();
            if (string.IsNullOrEmpty(word)) { return objDump; }

            DataTable dtDef = Droid.Database.DBAdapter.ExecuteReader(_config["DB_SCHEMA"], string.Format("select définition from ppartobid01.t_mot where valeur = '{0}';", word.ToLower()));
            try
            {
                string[] definition = dtDef.Rows[0][0].ToString().Split('|');
                foreach (var item in definition)
                {
                    DataTable dt = Droid.Database.DBAdapter.ExecuteReader(_config["DB_SCHEMA"], string.Format("select * from ppartobid01.t_{0} where valeur = '{1}';", item.ToLower(), word.ToLower()));
                    if (dt.Rows.Count > 0)
                    {
                        objDump[item.ToLower()] = dt.Rows[0].ItemArray;
                    }
                }
                return objDump;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                return null;
            }
        }
        #endregion

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
                // datatable check
                DataTable dbResult = DBAdapter.ExecuteReader(Parameters.Config["DB_NAME"].ToString(), string.Format("select * from {0}.t_mot where valeur = '{1}'", Parameters.Config["DB_SCHEMA"].ToString(), text));

                string[] row = dbResult.Rows[0].ItemArray.Select(i => i.ToString()).ToArray();
                Word word = DefinitionLoader.LoadClassicWord(dbResult.Rows[0]);


                foreach (string role in roles)
                {
                    switch (role.ToLower())
                    {
                        case "adjectif":
                            _dico.ListAdjectives.Add(DefinitionLoader.ParseAdjective(word, row, _dico));
                            break;
                        case "adverbe":
                            _dico.ListAdverbs.Add(DefinitionLoader.ParseAdverb(word, row, _dico));
                            break;
                        case "conjonction":
                            _dico.ListConjonctions.Add(DefinitionLoader.ParseConjonction(word, row, _dico));
                            break;
                        case "determinant":
                            _dico.ListDeterminant.Add(DefinitionLoader.ParseDeterminant(word, row, _dico));
                            break;
                        case "nom":
                            _dico.ListNomCommuns.Add(DefinitionLoader.ParseNomCommun(word, row, _dico));
                            break;
                        case "preposition":
                            _dico.ListPreposition.Add(DefinitionLoader.ParsePreposition(word, row, _dico));
                            break;
                        case "pronom":
                            _dico.ListPronoms.Add(DefinitionLoader.ParsePronom(word, row, _dico));
                            break;
                        case "verbe":
                            _dico.ListVerbs.Add(DefinitionLoader.ParseVerb(word, row, _dico));
                            break;
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("An error occured while parsing word " + text + " ERROR : " + exp.Message);
            }
        }
        #endregion

        #region Methods private
        #endregion
    }
}
