using Droid.Database;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Droid.Litterature
{
    public class Syllabe
    {
        #region Attribute
        private string _text;
        private string _sound;
        private int _nbLetter;
        private bool _exception;
        #endregion

        #region Properties
        [XmlElement(ElementName = "exceptions")]
        public bool ExceptionToExclude
        {
            get { return _exception; }
            set { _exception = value; }
        }
        [XmlElement(ElementName = "sound")]
        public string Sound
        {
            get { return _sound; }
            set { _sound = value; }
        }
        [XmlElement(ElementName = "count")]
        public int NbLetter
        {
            get { return _nbLetter; }
            set { _nbLetter = value; }
        }
        [XmlElement(ElementName = "text")]
        public string Text
        {
            get { return _text; }
            set 
            { 
                _text = value;
                _nbLetter = _text.Length;
            }
        }
        #endregion

        #region Constructor
        public Syllabe()
        {

        }
        public Syllabe(Syllabe sRef)
        {
            this.Text = sRef.Text;
            this.Sound = sRef.Sound;
            this.NbLetter = sRef.NbLetter;
            this.ExceptionToExclude = sRef.ExceptionToExclude;
        }
        public Syllabe(string txt, string snd, bool exc)
        {
            this._exception = exc;
            this._text = txt;
            this._nbLetter = txt.Length;
            this._sound = snd;
        }
        #endregion

        #region Methods public
        public static void Inject(string sound, string text)
        {
            DBAdapter.ExecuteQuery(Parameters.Config["DB_NAME"].ToString(), string.Format("update table t_syllabe set valeur = '{0}' sound = '{1}'", text, sound));

        }
        #endregion

        #region Methods private
        #endregion
    }
}
