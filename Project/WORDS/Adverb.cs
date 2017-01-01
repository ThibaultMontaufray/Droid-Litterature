using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Droid_litterature
{
    public class Adverb : Word
    {
        #region Attribute
        private ADVERB cathegory;
        #endregion

        #region Properties
        public ADVERB Cathegory
        {
            get { return cathegory; }
            set { cathegory = value; }
        }
        #endregion

        #region Constructeur
        public Adverb()
        {
            this.Synonymes = new List<string>();
        }
        public Adverb(Adverb adv)
        {
            this.Synonymes = adv.Synonymes;
            this.Role = ROLE.ADVERBE;
            this.Genre = GENRE.NEUTRE;
            this.Nombre = NOMBRE.NEUTRE;
            this.Cathegory = adv.cathegory;
            this.DicoAdverb = adv.DicoAdverb;
            this.Text = adv.Text;
            this.Definition = adv.Definition;
        }
        public Adverb(Word w)
        {
            this.Synonymes = w.Synonymes;
            this.Text = w.Text;
            this.Suffixe = w.Suffixe;
            this.Prefixe = w.Prefixe;
            this.Genre = w.Genre;
            this.Nombre = w.Nombre;
            this.Exception = w.Exception;
            this.Pers = w.Pers;
            this.LexicalField = w.LexicalField;
            this.Definition = w.Definition;

            this.DicoAdverb = w.DicoAdverb;
            this.Role = ROLE.ADVERBE;
        }
        #endregion

        #region Methods
        #endregion
    }
}
