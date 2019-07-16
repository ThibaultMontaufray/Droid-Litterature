using Droid.Database;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Droid.Litterature
{
    public static class Traduction
    {
        private static IConfiguration config;

        static Traduction()
        {
            config = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build();
            DBAdapter.DbType = (DataBasesType)Enum.Parse(typeof(DataBasesType), config["DB_TYPE"].ToString());
            DBAdapter.User = config["DB_USER"].ToString();
            DBAdapter.Password = config["DB_PSWD"].ToString();
            DBAdapter.Port = config["DB_PORT"].ToString();
            DBAdapter.Server = config["DB_HOST"].ToString();
        }

        public static string This(string input, LANGAGE langIn, LANGAGE langOut)
        {
            string bikey = langIn.ToString() + "_" + langOut.ToString();

            switch(bikey)
            {
                case "EN_FR": return EN_FR(input);
                case "FR_EN": return FR_EN(input);
                case "TN_FR": return TN_FR(input);
                case "FR_TN": return FR_TN(input);
                case "TN_EN": return TN_EN(input);
                case "EN_TN": return EN_TN(input);
                default: return string.Empty;
            }
        }

        private static string EN_FR(string input)
        {
            string[] tab = input.Split(' ');
            string output = string.Empty;            
            foreach (var word in tab) { output += Query("anglais", "francais", word) + " "; }
            return output.Trim();
        }
        private static string FR_EN(string input)
        {
            string[] tab = input.Split(' ');
            string output = string.Empty;
            foreach (var word in tab) { output += Query("francais", "anglais", word) + " "; }
            return output.Trim();
        }
        private static string TN_FR(string input)
        {
            string[] tab = input.Split(' ');
            string output = string.Empty;
            foreach (var word in tab) { output += Query("tunisien", "francais", word) + " "; }
            return output.Trim();
        }
        private static string FR_TN(string input)
        {
            string[] tab = input.Split(' ');
            string output = string.Empty;
            foreach (var word in tab) { output += Query("francais", "tunisien", word) + " "; }
            return output.Trim();
        }
        private static string TN_EN(string input)
        {
            string[] tab = input.Split(' ');
            string output = string.Empty;
            foreach (var word in tab) { output += Query("tunisien", "anglais", word) + " "; }
            return output.Trim();
        }
        private static string EN_TN(string input)
        {
            string[] tab = input.Split(' ');
            string output = string.Empty;
            foreach (var word in tab) { output += Query("anglais", "tunisien", word) + " "; }
            return output.Trim();
        }
        private static string Query(string langIn, string langOut, string word)
        {
            string output = string.Empty;
            try
            {
                DataTable dbRes;

                dbRes = DBAdapter.ExecuteReader(config["DB_NAME"].ToString(), string.Format("select " + langOut + " from {0}.t_traduction where " + langIn + " = '" + word + "';", config["DB_SCHEMA"].ToString()));
                if (dbRes != null && dbRes.Rows != null && dbRes.Rows.Count > 0)
                {
                    output = dbRes.Rows[0].ItemArray[0].ToString();
                }
                else
                {
                    output = string.Format("\"{0}\"", word);
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            return output;
        }
    }
}
