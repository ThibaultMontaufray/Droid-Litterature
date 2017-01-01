using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Droid_litterature;

namespace Droid_litterature
{
    public class Conjugaison
    {
        #region Attributte
        private List<Time> _times;
        private Dico _dico;
        #endregion

        #region Properties
        public List<Time> Times
        {
            get { return _times; }
            set { _times = value; }
        }
        #endregion

        #region Constructor
        public Conjugaison()
        {
            _times = new List<Time>();
            _dico = null;
        }
        public Conjugaison(Dico dic)
        {
            _times = new List<Time>();
            _dico = dic;
        }
        #endregion

        #region Methods public
        /// <summary>
        /// Control that infinitif exist and find the _form
        /// </summary>
        /// <param name="infinitif">infinitif to anayse with model : important to compare</param>
        public bool analyseRegular(Verb verb)
        {
            if (verb.GetWithoutAccents.ToLower().Equals(verb.Infinitive))
            {
                verb.Form = FORM.INFINITIF;
                verb.Genre = GENRE.NEUTRE;
                verb.Nombre = NOMBRE.NEUTRE;
                return true;
            }
            else
            {
                foreach (Time t in Times.Where(i => i.Type.Equals(verb.Suffixe)))
                {
                    if (check(verb, t))
                    {
                        verb.Form = t.Form;
                        if (verb.Nombre.Equals(NOMBRE.UNKNOWN) || verb.Nombre.Equals(NOMBRE.NEUTRE)) verb.Nombre = t.Nombre;
                        if (verb.Genre.Equals(GENRE.UNKNOWN) || verb.Genre.Equals(GENRE.NEUTRE)) verb.Genre = t.Genre;
                        verb.Suffixe = t.Val;
                        return true;
                        // TODO : modify because it could be others _form how match with the word
                        // TODO : 3ème groupe *ore
                    }
                }
                return false;
            }
        }
        public bool copyInfo(Verb v, Time t)
        {
            v.Form = t.Form;
            v.Group = t.Group;
            v.Genre = t.Genre;
            v.Nombre = t.Nombre;
            v.Pers = t.Person;
            v.Exception = t.Exception;
            v.Suffixe= t.Type;
            v.LexicalField = t.Lexiques;
            v.Infinitive = t.Infinitif;
            return true;
        }
        #endregion

        #region Methods private
        private bool check(Verb v, Time t)
        {
            if (v.Suffixe != null && v.Group.Equals(t.Group) && v.Suffixe.ToLower().Equals(t.Type.ToLower()) && t.Exception)
            {
                string tmp = t.Val;
                tmp = tmp.Replace('è', 'e');
                tmp = tmp.Replace('é', 'e');
                tmp = tmp.Replace('ê', 'e');
                tmp = tmp.Replace('ô', 'o');
                tmp = tmp.Replace('à', 'a');
                if (tmp.Equals(v.GetWithoutAccents))
                {
                    if (!copyInfo(v, t)) return false;
                    v.Exception = true;
                    v.Infinitive = t.Infinitif;
                    return true;
                }
                else return false;
            }
            else if (v.Suffixe != null && v.Group.Equals(t.Group) && v.Suffixe.ToLower().Equals(t.Type.ToLower()) && !string.IsNullOrEmpty(t.Suffixe) && ! t.Exception)
            {
                string tmp = v.Root + t.Suffixe;
                tmp = tmp.Replace('è', 'e');
                tmp = tmp.Replace('é', 'e');
                tmp = tmp.Replace('ê', 'e');
                tmp = tmp.Replace('ô', 'o');
                tmp = tmp.Replace('à', 'a');
                if (tmp.Equals(v.GetWithoutAccents))
                {
                    if (!copyInfo(v, t)) return false;
                    v.Exception = false;
                    v.Infinitive = v.Root + t.Type;
                    return true;
                }
                else return false;
            }
            else return false;
        }
        #endregion
    }
}
