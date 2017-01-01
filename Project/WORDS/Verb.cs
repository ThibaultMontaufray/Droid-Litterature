using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Droid_litterature;

namespace Droid_litterature
{
    public class Verb : Word
    {
        #region Attribute
        private string _group;
        private string _infinitive;
        private FORM _form;
        private string _root;
        #endregion

        #region Properties
        public string Group
        {
            get { return _group; }
            set { _group = value; }
        }
        public string Root
        {
            get 
            {
                if (_root == null)
                    return "";
                else
                    return _root;
            }
            set { _root = value; }
        }
        public FORM Form
        {
            get { return _form; }
            set { _form = value; }
        }
        public string Infinitive
        {
            get 
            {
                if (!string.IsNullOrEmpty(_infinitive)) return _infinitive;
                else return _root + Suffixe;
            }
            set { _infinitive = value; }
        }
        #endregion

        #region Constructor
        public Verb()
        {
            this.Synonymes = new List<string>();
            this.Role = ROLE.VERBE;
            this.Form = FORM.UNKNOWN;
            this.LexicalField = new List<Lexique>();
            if (!string.IsNullOrEmpty(this.Text) && this.Text.EndsWith("r")) Infinitive = this.Text;
        }
        public Verb(Verb verbOriginal)
        {
            this.Synonymes = verbOriginal.Synonymes;
            this.Definition = verbOriginal.Definition;
            this.Role = ROLE.VERBE;
            this.Form = FORM.UNKNOWN;
            this.LexicalField = new List<Lexique>();
            if (!string.IsNullOrEmpty(this.Text) && this.Text.EndsWith("r")) Infinitive = this.Text;

            this.Group = verbOriginal._group;
            this.Text = verbOriginal.Text;
            this.Role = verbOriginal.Role;
            this.Root = verbOriginal.Root;
            this.DicoVerb = verbOriginal.DicoVerb;
            this.Exception = verbOriginal.Exception;
            this.Pers = verbOriginal.Pers;
            this.LexicalField = verbOriginal.LexicalField;
            this.Infinitive = verbOriginal.Infinitive;
        }
        public Verb(Word w)
        {
            this.Synonymes = w.Synonymes;
            this.Role = ROLE.VERBE;
            this.Form = FORM.UNKNOWN;
            this.LexicalField = new List<Lexique>();
            
            this.Text = w.Text;
            this.Suffixe = w.Suffixe;
            this.Prefixe = w.Prefixe;
            this.Genre = w.Genre;
            this.Nombre = w.Nombre;
            this.Exception = w.Exception;
            this.Pers = w.Pers;
            this.LexicalField = w.LexicalField;
            this.Definition = w.Definition;

            if (!string.IsNullOrEmpty(this.Text) && this.Text.EndsWith("r")) Infinitive = this.Text;

            if (w.DicoVerb != null)
            {
                this.DicoVerb = w.DicoVerb;
                this.Infinitive = w.DicoVerb.Infinitive;
            }
        }
        #endregion

        #region Methods public
        public Verb find(List<Verb> verbs, string search_verb)
        {
            string s;
            foreach (Verb v in verbs)
            {
                s = v._root + v.Suffixe;
                if (s.Equals(search_verb))
                {
                    Verb v_ret = new Verb(v);
                    return v_ret;
                }
            }
            return null;
        }
        #endregion

        #region Methods private
        #endregion
    }
}
