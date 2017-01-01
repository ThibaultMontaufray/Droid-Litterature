using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Droid_litterature
{
    public class SentenceStructure
    {
        #region Attribute
        private string _text;
        private List<Word> _words;
        private STRUCTURE_COMPLEXE _name;
        private bool _isSubject;
        private string _type;
        #endregion

        #region Properties
        public List<Word> Words
        {
            get { return _words; }
            set { _words = value; }
        }
        public STRUCTURE_COMPLEXE Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        public bool IsSubject
        {
            get { return _isSubject; }
            set { _isSubject = value; }
        }
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        #endregion

        #region Constructor
        public SentenceStructure()
        {
            _name = STRUCTURE_COMPLEXE.UNKNOW;
            _words = new List<Word>();
        }
        #endregion

        #region Methods
        #endregion
    }
}
