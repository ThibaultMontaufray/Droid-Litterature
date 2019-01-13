using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Droid.Litterature
{
    public static class Dumper
    {
        #region Attribute
        public const string DICOXMLPATH = @"./DicoDump.xml";
        #endregion

        #region Methods public
        /// <summary>
        /// Dump to export in nuget format
        /// this allow programs to have no database with specific dictionnary
        /// </summary>
        public static void DumpDico(Dico dico)
        {
            string dump;
            XmlSerializer xsSubmit = new XmlSerializer(typeof(Dico));
            using (StringWriter sww = new StringWriter())
            using (XmlWriter writer = XmlWriter.Create(sww))
            {
                xsSubmit.Serialize(writer, dico);
                dump = sww.ToString();
                using (StreamWriter sw = new StreamWriter(DICOXMLPATH, false, Encoding.UTF8))
                {
                    sw.Write(dump);
                }
            }
        }
        /// <summary>
        /// Return the dico from dump xml file
        /// </summary>
        /// <returns>Your dico or null if file not found</returns>
        public static Dico LoadDump()
        {
            string dump;
            Dico dico = null;
            XmlSerializer xsSubmit = new XmlSerializer(typeof(Dico));
            if (File.Exists(DICOXMLPATH))
            { 
                using (StreamReader sr = new StreamReader(DICOXMLPATH))
                {
                    dump = sr.ReadToEnd();
                }
                using (StringReader srr = new StringReader(dump))
                using (XmlReader reader = XmlReader.Create(srr))
                {
                    var obj = xsSubmit.Deserialize(reader);
                    dico = obj as Dico;
                }
            }
            return dico;
        }
        #endregion

        #region Methods private
        #endregion
    }
}
