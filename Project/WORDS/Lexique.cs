using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Droid_litterature
{
    public class Lexique
    {
        #region Attribute
        private string _text;
        #endregion

        #region Properties
        [XmlElement(ElementName = "text")]
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        #endregion

        #region Constructor
        public Lexique()
        {
            _text = string.Empty;
        }
        public Lexique(string word)
        {
            _text = word;
        }
        #endregion

        #region Methods public
        #endregion

        #region Methods private
        #endregion
    }
}
