using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Droid.litterature
{
    public abstract class SentenseGroup
    {
        #region Attribute
        private string _text;
        private List<string> _structure;
        private List<Word> _words;
        #endregion

        #region Properties
        public List<Word> Words
        {
            get
            {
                if (_words == null) { _words = new List<Word>(); }
                return _words;
            }
            set
            {
                if (_words == null) { _words = new List<Word>(); }
                _words = value;
            }
        }
        public List<string> Structure
        {
            get
            {
                if (_structure == null) { _structure = new List<string>(); }
                return _structure;
            }
            set
            {
                if (_structure == null) { _structure = new List<string>(); }
                _structure = value;
            }
        }
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        #endregion

        #region Constructor
        public SentenseGroup()
        {

        }
        #endregion

        #region Methods
        #endregion
    }
}
