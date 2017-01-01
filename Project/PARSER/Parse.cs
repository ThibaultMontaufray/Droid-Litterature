// LOG CODE 36 01

using Tools4Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Droid_database;

namespace Droid_litterature
{
    public class Parse
    {
        #region Attribute
        private bool _flagFind;
        private List<Verb> _lv;
        private Regex _rexChiffre = new Regex("[0-9]");
        private Regex _rexMail = new Regex("[0-9a-zA-Z]@[a-z].[a-z]");
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public Parse()
        {
        }
        #endregion

        #region Methods Public
        public Sentense Process(Sentense sent)
        {
            FindWords(sent);
            FindRole(sent);
            FindStructures(sent);

            if (sent.StructureStr == null) return null;

            FindGroupNominal(sent);
            FindCOD_COI(sent); // need GN to find COD / COI
            FindGroupVerbal(sent); // need COD / COI / GN to find GV
            FindSubject(sent); // need verb to find subject
            FindSuperlatif(sent);
            FindLexicalFields(sent);
            return sent;
        }
        #endregion

        #region Methods Private
        private void FindWords(Sentense sent)
        {
            if (sent.Words.Count > 0)
            {
                List<Word> lw = sent.Words;
                Word wRef;
                for (int i = 0; i < sent.Words.Count; i++)
                {
                    try
                    {
                        wRef = lw[i];
                        ProcessWord(sent, ref wRef);
                    }
                    catch (IndexOutOfRangeException exp3601)
                    {
                        Log.write("[ DEB : 3601 ] Pff out of the range, problem while parsing a simple word sorry :/\n" + exp3601.Message);
                    }
                }
            }
        }
        private void FindRole(Sentense sent)
        {
            sent.HTMLWords = "";
            sent.HTMLDetailled = "<table class='TobiAnswerTable'>";
            sent.HTMLDetailled += "<tr class='TobiAnswerTableHeader'><th>Texte</th><th>Role</th><th>Nombre</th><th>Genre</th><th>Personne</th><th>Definition</th><th>Synonymes</th></tr>";
            List<Word> NewList = new List<Word>();

            Word wRef;
            for (int i = 0; i < sent.Words.Count; i++)
            {
                wRef = sent.Words[i];
                //ProcessWord(sent, ref wRef);
                sent.Words[i] = wRef;
                ChooseRole(sent.Words[i]);
                Attribute(sent.Words[i], 0, sent);
                NewList.Add(sent.Words[i]);
            }
            sent.Words = NewList;
            sent.HTMLDetailled += "</table>";
        }
        private void FindStructures(Sentense sent)
        {
            sent.Structure = new List<string>();
            //sent.StructureAnalytic = new List<SentenceStructure>();
            
            if (!string.IsNullOrEmpty(sent.StructureStr))
            {
                string[] tabStruct = sent.StructureStr.Split('&');
                List<string> tempTab;
                sent.Structure.Add(string.Empty);

                foreach (string item in tabStruct)
                {
                    string[] roles = item.Split('|');
                    tempTab = new List<string>();
                    foreach (string s in roles)
                    {
                        for (int i = 0; i < sent.Structure.Count; i++)
                        {
                            tempTab.Add(string.IsNullOrEmpty(sent.Structure[i]) ? s : sent.Structure[i] + "&" + s);
                        }
                    }
                    sent.Structure = tempTab;
                }
            }
        }
        private void FindGroupVerbal(Sentense sent)
        {
            //bool contigue = false;
            GroupVerbal gv = null;
            foreach (Word word in sent.Words)
            {
                if (word.Roles.Contains(ROLE.VERBE))
                {
                    if (word.DicoVerb != null &&  (word.DicoVerb.Form == FORM.INFINITIF)) { continue; }
                    if (gv == null) { gv = new GroupVerbal(); }
                    gv.Words.Add(word);
                    //contigue = true;
                }
                //else if (word.AttachedSentenseStructure != null && ((word.AttachedSentenseStructure.Name.Equals(STRUCTURE_COMPLEXE.COD.ToString())) || (word.AttachedSentenseStructure.Name.Equals(STRUCTURE_COMPLEXE.COI.ToString()))))
                //{
                //    if (contigue)
                //    {
                //        gv.Words.AddRange(word.AttachedSentenseStructure.Words);
                //        sent.GroupVerbaux.Add(gv);
                //        sent.StructureAnalytic.Add(gv);
                //        gv = null;
                //        contigue = false;
                //    }
                //}
                else
                {
                    if (gv != null)
                    {
                        sent.GroupVerbaux.Add(gv);
                        sent.StructureAnalytic.Add(SetWordsSS(gv.Words.ToArray(), STRUCTURE_COMPLEXE.GROUPE_VERBAL));
                        gv = null;
                    }
                    //contigue = false;
                }
            }
            if (gv != null)
            {
                sent.GroupVerbaux.Add(gv);
                sent.StructureAnalytic.Add(SetWordsSS(gv.Words.ToArray(), STRUCTURE_COMPLEXE.GROUPE_VERBAL));
                gv = null;
            }

            // ---------------

            //string[] tabStruct = sent.StructureStr.Split('&');
            //List<Word> listVerbs = new List<Word>();

            //for (int i = 0; i < tabStruct.Length - 1; i++)
            //{
            //    if (tabStruct[i].Contains("VERBE") && sent.Words.Count >= i && sent.Words[i].AttachedSentenseStructure == null && sent.Words[i].Roles.Contains(ROLE.VERBE))
            //    {
            //        listVerbs.Add(sent.Words[i]);
            //    }
            //}
            //for (int i = 0; i < listVerbs.Count; i++)
            //{
            //    string definition = string.Empty;
            //    if (listVerbs[i].DicoVerb != null) definition = listVerbs[i].DicoVerb.Definition;
            //    listVerbs[i].AttachedSentenseStructure = new SentenceStructure() { Name = STRUCTURE_COMPLEXE.GROUPE_VERBAL, Text = listVerbs[i].Text, Type = definition, Words = new List<Word>() { listVerbs[i] } };
            //}
        }
        private void FindGroupNominal(Sentense sent)
        {
            string[] tabStruct = sent.StructureStr.Split('&');
            List<SentenceStructure> retVals = new List<SentenceStructure>();

            for (int i = 0; i < tabStruct.Length; i++)
            {
                if (i + 3 < tabStruct.Length && tabStruct[i].Contains("DETERMINANT") && tabStruct[i + 1].Contains("ADJECTIF") && tabStruct[i + 2].Contains("ADJECTIF") && tabStruct[i + 3].Contains("NOM"))
                {
                    retVals.Add(SetWordsSS(new Word[] { sent.Words[i], sent.Words[i + 1], sent.Words[i + 2], sent.Words[i + 3] }, STRUCTURE_COMPLEXE.GROUPE_NOMINAL));
                    i += 3;
                }
                else if (i + 2 < tabStruct.Length && tabStruct[i].Contains("PRONOM") && tabStruct[i + 1].Contains("PRONOM") && tabStruct[i + 2].Contains("PREPOSITION"))
                {
                    retVals.Add(SetWordsSS(new Word[] { sent.Words[i], sent.Words[i + 1], sent.Words[i + 2] }, STRUCTURE_COMPLEXE.GROUPE_NOMINAL));
                    i += 2;
                }
                else if (i + 2 < tabStruct.Length && tabStruct[i].Contains("DETERMINANT") && tabStruct[i + 1].Contains("ADJECTIF") && tabStruct[i + 2].Contains("NOM"))
                {
                    retVals.Add(SetWordsSS(new Word[] { sent.Words[i], sent.Words[i + 1], sent.Words[i + 2] }, STRUCTURE_COMPLEXE.GROUPE_NOMINAL));
                    i += 2;
                }
                else if (i + 2 < tabStruct.Length && tabStruct[i].Contains("DETERMINANT") && tabStruct[i + 1].Contains("NOM") && tabStruct[i + 2].Contains("ADJECTIF"))
                {
                    retVals.Add(SetWordsSS(new Word[] { sent.Words[i], sent.Words[i + 1], sent.Words[i + 2] }, STRUCTURE_COMPLEXE.GROUPE_NOMINAL));
                    i += 2;
                }
                else if (i + 1 < tabStruct.Length && tabStruct[i].Contains("DETERMINANT") && tabStruct[i + 1].Contains("NOM"))
                {
                    retVals.Add(SetWordsSS(new Word[] { sent.Words[i], sent.Words[i + 1] }, STRUCTURE_COMPLEXE.GROUPE_NOMINAL));
                    i++;
                }
                else if (tabStruct[i].Contains("PRENOM") || tabStruct[i].Contains("PRONOM"))
                {
                    retVals.Add(SetWordsSS(new Word[] { sent.Words[i] }, STRUCTURE_COMPLEXE.GROUPE_NOMINAL));
                }
            }
            sent.StructureAnalytic.AddRange(retVals);
        }
        private void FindSubject(Sentense sent)
        {
            bool flagFound = false;
            bool flagVerbFound = false;
            SentenceStructure last = new SentenceStructure() { Name = STRUCTURE_COMPLEXE.UNKNOW };

            foreach (object item in sent.StructureAnalytic)
            {
                if (item is SentenceStructure)
                {
                    if ((last.Name == STRUCTURE_COMPLEXE.GROUPE_NOMINAL || last.Name == STRUCTURE_COMPLEXE.COD) && ((SentenceStructure)item).Name == STRUCTURE_COMPLEXE.GROUPE_VERBAL)
                    {
                        last.IsSubject = true;
                        last.Name = STRUCTURE_COMPLEXE.SUJET;
                        flagFound = true;
                    }
                    if (!flagFound && (((SentenceStructure)item).Name == STRUCTURE_COMPLEXE.GROUPE_NOMINAL || ((SentenceStructure)item).Name == STRUCTURE_COMPLEXE.COD) && last.Name == STRUCTURE_COMPLEXE.GROUPE_VERBAL)
                    {
                        ((SentenceStructure)item).IsSubject = true;
                        ((SentenceStructure)item).Name = STRUCTURE_COMPLEXE.SUJET;
                        flagFound = true;
                    }
                    last = item as SentenceStructure;
                }
                else
                {
                    last = new SentenceStructure() { Name = STRUCTURE_COMPLEXE.UNKNOW };
                }
            }

            // search inverse for questions
            if (!flagFound)
            {
                foreach (object item in sent.StructureAnalytic)
                {
                    if (item is SentenceStructure)
                    {
                        if (last.Name == STRUCTURE_COMPLEXE.GROUPE_VERBAL && ((SentenceStructure)item).Name == STRUCTURE_COMPLEXE.GROUPE_NOMINAL)
                        {
                            ((SentenceStructure)item).IsSubject = true;
                            ((SentenceStructure)item).Name = STRUCTURE_COMPLEXE.SUJET;
                            flagFound = true;
                        }
                        last = item as SentenceStructure;
                    }
                    else
                    {
                        last = new SentenceStructure() { Name = STRUCTURE_COMPLEXE.UNKNOW };
                    }
                }
            }

            // search inverse for questions
            //if (!flagFound)
            //{
            //    lastWord = null;
            //    foreach (Word wLoop in sent.Words)
            //    {
            //        currentWord = wLoop;
            //        if (currentWord != null && currentWord.AttachedSentenseStructure != null)
            //        {
            //            if (lastWord != null && lastWord.Roles.Contains(ROLE.VERBE) && currentWord.AttachedSentenseStructure.Name == STRUCTURE_COMPLEXE.GROUPE_NOMINAL)
            //            {
            //                if (currentWord.AttachedSentenseStructure == null
            //                    || lastWord.AttachedSentenseStructure == null
            //                    || (currentWord.AttachedSentenseStructure != null && lastWord.AttachedSentenseStructure != null && !currentWord.Equals(lastWord)))
            //                {
            //                    lastWord.AttachedSentenseStructure.IsSubject = true;
            //                    lastWord.AttachedSentenseStructure.Name = STRUCTURE_COMPLEXE.SUJET;
            //                    flagFound = true;
            //                }
            //            }
            //        }
            //        lastWord = currentWord;
            //    }
            //}

            // found no subject, then it's imperative form
            if (!flagFound && flagVerbFound)
            {
                sent.Ponctuation = PONCTUATION.EXCLAMATION;
            }
        }
        private void FindCOD_COI(Sentense sent)
        {
            Word wordPreposition;
            SentenceStructure ss;
            int index = 0;
            List<int> objectToRemove = new List<int>();
            foreach (object item in sent.StructureAnalytic)
            {
                if (item is SentenceStructure)
                {
                    ss = item as SentenceStructure;
                    wordPreposition = null;
                    if (!ss.IsSubject && ss.Name == STRUCTURE_COMPLEXE.GROUPE_NOMINAL)
                    {
                        foreach (Word w in sent.Words)
                        {
                            if (w.AttachedSentenseStructure == null && w.Roles.Contains(ROLE.PREPOSITION)) wordPreposition = w;
                            else if (ss.Equals(w.AttachedSentenseStructure) && wordPreposition != null)
                            {
                                ss.Name = STRUCTURE_COMPLEXE.COI;
                                List<Word> lw = new List<Word>();
                                lw.AddRange(ss.Words);
                                ss.Words = new List<Word>();
                                ss.Words.Add(wordPreposition);
                                ss.Words.AddRange(lw);
                                ss.Text = string.Format("{0} {1}", wordPreposition.Text, ss.Text);
                                objectToRemove.Add(index-1);
                                break;
                            }
                            else if (ss.Equals(w.AttachedSentenseStructure) && wordPreposition == null)
                            {
                                ss.Name = STRUCTURE_COMPLEXE.COD;
                                break;
                            }
                            else wordPreposition = null;
                        }
                    }
                }
                index++;
            }
            foreach (var item in objectToRemove)
            {
                sent.StructureAnalytic.RemoveAt(item);
            }
        }
        private void FindSuperlatif(Sentense sent)
        {
            string[] tabStruct = sent.StructureStr.Split('&');
            List<SentenceStructure> retVals = new List<SentenceStructure>();
            
            for (int i = 0; i < tabStruct.Length - 1; i++)
            {
                if (i + 2 < tabStruct.Length && tabStruct[i].Contains("DETERMINANT") && tabStruct[i + 1].Contains("ADVERBE") && tabStruct[i + 2].Contains("ADJECTIF"))
                {
                    retVals.Add(SetWordsSS(new Word[] { sent.Words[i], sent.Words[i + 1], sent.Words[i + 2] }, STRUCTURE_COMPLEXE.SUPERLATIF));
                }
            }

            //sent.StructureAnalytic.AddRange(retVals);
        }
        private void FindLexicalFields(Sentense sent)
        {
            foreach (Word w in sent.Words)
            {
                if (w.LexicalField != null)
                {
                    foreach (Lexique l in w.LexicalField)
                    {
                        sent.Lexiques.Add(l);
                    }
                }
            }
        }

        private void ChooseRole(Word w)
        {
            if (w.DicoPronom != null) w.Role = ROLE.PRONOM;
            else if (w.DicoVerb != null) w.Role = ROLE.VERBE;
            else if (w.DicoAdjective != null) w.Role = ROLE.ADJECTIF;
            else if (w.DicoAdverb != null) w.Role = ROLE.ADVERBE;
            else if (w.DicoConjonction != null) w.Role = ROLE.CONJONCTION;
            else if (w.DicoDeterminant != null) w.Role = ROLE.DETERMINANT;
            else if (w.DicoNomCommun != null) w.Role = ROLE.NOMCOMMUN;
            else if (w.DicoNomPropre != null) w.Role = ROLE.NOMPROPRE;
            else if (w.DicoPreposition != null) w.Role = ROLE.PREPOSITION;
            else if (w.DicoChiffre != null || w.Roles.Contains(ROLE.CHIFFRE)) w.Role = ROLE.CHIFFRE;
            else if (w.Roles.Contains(ROLE.MAIL)) w.Role = ROLE.MAIL;
            else w.Role = ROLE.UNKNOWN;
        }
        private void Attribute(Word w, int indexDico, Sentense sent)
        {
            switch (w.Role)
            {
                case ROLE.ADJECTIF:
                    w.Genre = w.DicoAdjective.Genre;
                    w.Nombre = w.DicoAdjective.Nombre;
                    w.Pers = w.DicoAdjective.Pers;
                    break;
                case ROLE.ADVERBE:
                    w.Genre = w.DicoAdverb.Genre;
                    w.Nombre = w.DicoAdverb.Nombre;
                    w.Pers = w.DicoAdverb.Pers;
                    break;
                case ROLE.CONJONCTION:
                    w.Genre = w.DicoConjonction.Genre;
                    w.Nombre = w.DicoConjonction.Nombre;
                    w.Pers = w.DicoConjonction.Pers;
                    break;
                case ROLE.DETERMINANT:
                    w.Genre = w.DicoDeterminant.Genre;
                    w.Nombre = w.DicoDeterminant.Nombre;
                    w.Pers = w.DicoDeterminant.Pers;
                    break;
                case ROLE.NOMCOMMUN:
                    w.Genre = w.DicoNomCommun.Genre;
                    w.Nombre = w.DicoNomCommun.Nombre;
                    w.Pers = w.DicoNomCommun.Pers;
                    break;
                case ROLE.NOMPROPRE:
                    w.Genre = w.DicoNomPropre.Genre;
                    w.Nombre = w.DicoNomPropre.Nombre;
                    w.Pers = w.DicoNomPropre.Pers;
                    break;
                case ROLE.PREPOSITION:
                    w.Genre = w.DicoPreposition.Genre;
                    w.Nombre = w.DicoPreposition.Nombre;
                    w.Pers = w.DicoPreposition.Pers;
                    break;
                case ROLE.PRONOM:
                    w.Genre = w.DicoPronom.Genre;
                    w.Nombre = w.DicoPronom.Nombre;
                    w.Pers = w.DicoPronom.Pers;
                    break;
                case ROLE.VERBE:
                    w.Genre = w.DicoVerb.Genre;
                    w.Nombre = w.DicoVerb.Nombre;
                    w.Pers = w.DicoVerb.Pers;
                    break;
            }
            if (!string.IsNullOrEmpty(sent.StructureStr)) sent.StructureStr += "&";
            string synonymeList = string.Empty;
            string finalRole = string.Empty;
            foreach (ROLE r in w.Roles)
            {
                if (!string.IsNullOrEmpty(finalRole))
                {
                    if (finalRole.Contains(r.ToString().ToUpper())) continue;
                    finalRole += "|";
                }
                finalRole += r.ToString().ToUpper();
            }
            sent.StructureStr += finalRole;
            sent.EnrichWordsWithDico();
            sent.HTMLWords += "<span id=\"" + finalRole + "\"> " + w.Text + "</span>";
            foreach (var item in w.Synonymes) 
            {
                if (!string.IsNullOrEmpty(synonymeList)) synonymeList += ", ";
                synonymeList += item;
            }
            sent.HTMLDetailled += string.Format("<tr class='TobiAnswerTableTD{0}'><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td></tr>", sent.Words.IndexOf(w) % 2, w.Text, w.Role, w.Nombre, w.Genre, w.Pers, Sentense.ApplyLanguageCorrections(w.Definition), synonymeList);
        }
        private void MatchDicoWord(Sentense sent, ref Word w)
        {
            sent.Verbs.AddRange(SearchVerbs(w));
            sent.Prepositions.AddRange(SearchPreposition(w));            
            sent.Pronoms.AddRange(SearchPronoms(w));
            sent.NomCommuns.AddRange(SearchNomsCommuns(w));
            sent.NomPropres.AddRange(SearchNomsPropres(w));
            sent.Conjonctions.AddRange(SearchConjonctions(w));
            sent.Adverb.AddRange(SearchAdverbs(w));
            sent.Adjectives.AddRange(SearchAdjectives(w));
            sent.Determinants.AddRange(SearchDeterminants(w));
        }
        private SentenceStructure SetWordsSS(Word[] words, STRUCTURE_COMPLEXE strCplx)
        {
            StringBuilder sb = new StringBuilder();
            SentenceStructure ss = new SentenceStructure();
            ss.Name = strCplx;
            ss.Words = new List<Word>();

            foreach (Word w in words)
            {
                sb.Append(string.Format("{0} ", w.Text));
                ss.Words.Add(w);
                w.AttachedSentenseStructure = ss;
            }
            ss.Text = sb.ToString().TrimEnd();

            return ss;
        }

        private void ProcessWord(Sentense sent, ref Word w)
        {
            ProcessAll(sent, ref w);
            Interface_litterature.Dictionnary.ParseSyllabe(w);
        }
        private void ProcessAll(Sentense sent, ref Word w)
        {
            MatchDicoWord(sent, ref w);
            ProcessVerb(sent, ref w);
            ProcessPronom(sent, ref w);
            ProcessConjonction(sent, ref w);
            ProcessAdjective(sent, ref w);
            ProcessAdverb(sent, ref w);
            ProcessNomCommun(sent, ref w);
            ProcessNomPropre(sent, ref w);
            ProcessDeterminant(sent, ref w);
            ProcessPreposition(sent, ref w);
            ProcessChiffre(sent, ref w);
            ProcessMail(sent, ref w);
            ProcessUnknow(sent, ref w);
        }
        private void ProcessVerb(Sentense sent, ref Word w)
        {
            if (sent.Verbs.Count != 0)
            {
                foreach (Verb v in sent.Verbs)
                {
                    if (w.GetWithoutAccents.Equals(v.GetWithoutAccents))
                    {
                        w.DicoVerb = v;
                        w.Roles.Add(v.Role);
                        w.Exception = v.Exception;
                        w.Pers = v.Pers;
                        w.Nombre = v.Nombre;
                        w.Genre = v.Genre;
                        w.LexicalField = v.LexicalField;
                        w.Roles.Add(ROLE.VERBE);
                        w.Synonymes = v.Synonymes;
                        w.Definition = v.Definition;
                    }
                }
            }
        }
        private void ProcessPronom(Sentense sent, ref Word w)
        {
            if (sent.Pronoms.Count != 0)
            {
                foreach (Pronom p in sent.Pronoms)
                {
                    if (w.GetWithoutAccents.Equals(p.GetWithoutAccents))
                    {
                        w.Roles.Add(p.Role);
                        w.DicoPronom = p;
                        w.Exception = p.Exception;
                        w.Pers = p.Pers;
                        w.Nombre = p.Nombre;
                        w.Genre = p.Genre;
                        w.Synonymes = p.Synonymes;
                        w.Definition = p.Definition;
                        sent.Lexiques.Add(new Lexique(p.Cathegory.ToString().ToLower()));
                    }
                }
            }
        }
        private void ProcessDeterminant(Sentense sent, ref Word w)
        {
            if (sent.Determinants.Count != 0)
            {
                foreach (Determinant d in sent.Determinants)
                {
                    if (w.GetWithoutAccents.Equals(d.GetWithoutAccents))
                    {
                        w.Roles.Add(d.Role);
                        w.DicoDeterminant = d;
                        w.Exception = d.Exception;
                        w.Pers = d.Pers;
                        w.Nombre = d.Nombre;
                        w.Genre = d.Genre;
                        w.Synonymes = d.Synonymes;
                        w.Definition = d.Definition;
                    }
                }
            }
        }
        private void ProcessNomCommun(Sentense sent, ref Word w)
        {
            if (sent.NomCommuns.Count != 0)
            {
                foreach (NomCommun nc in sent.NomCommuns)
                {
                    if (w.Text.Equals(nc.Text))
                    {
                        w.DicoNomCommun = nc;
                        w.Roles.Add(nc.Role);
                        w.Exception = nc.Exception;
                        w.Pers = nc.Pers;
                        w.Nombre = nc.Nombre;
                        w.Genre = nc.Genre;
                        w.LexicalField = nc.LexicalField;
                        w.Synonymes = nc.Synonymes;
                        w.Definition = nc.Definition;
                    }
                }
            }
        }
        private void ProcessNomPropre(Sentense sent, ref Word w)
        {
            if (sent.NomPropres.Count != 0)
            {
                foreach (NomPropre np in sent.NomPropres)
                {
                    if (w.Text.Equals(np.Text))
                    {
                        w.DicoNomPropre = np;
                        w.Roles.Add(np.Role);
                        w.Exception = np.Exception;
                        w.Pers = np.Pers;
                        w.Nombre = np.Nombre;
                        w.Genre = np.Genre;
                        w.LexicalField = np.LexicalField;
                        w.Synonymes = np.Synonymes;
                        w.Definition = np.Definition;
                    }
                }
            }
        }
        private void ProcessAdjective(Sentense sent, ref Word w)
        {
            if (sent.Adjectives.Count != 0)
            {
                foreach (Adjective a in sent.Adjectives)
                {
                    if (w.GetWithoutAccents.Equals(a.GetWithoutAccents))
                    {
                        w.Roles.Add(a.Role);
                        w.DicoAdjective = a;
                        w.Exception = a.Exception;
                        w.Pers = a.Pers;
                        w.Nombre = a.Nombre;
                        w.Genre = a.Genre;
                        w.LexicalField = a.LexicalField;
                        w.Synonymes = a.Synonymes;
                        w.Definition = a.Definition;
                    }
                }
            }
        }
        private void ProcessAdverb(Sentense sent, ref Word w)
        {
            if (sent.Adverb.Count != 0)
            {
                foreach (Adverb a in sent.Adverb)
                {
                    if (w.GetWithoutAccents.Equals(a.GetWithoutAccents))
                    {
                        w.Roles.Add(a.Role);
                        w.DicoAdverb = a;
                        w.Exception = a.Exception;
                        w.Pers = a.Pers;
                        w.Nombre = a.Nombre;
                        w.Genre = a.Genre;
                        w.LexicalField = a.LexicalField;
                        w.Synonymes = a.Synonymes;
                        w.Definition = a.Definition;
                    }
                }
            }
        }
        private void ProcessConjonction(Sentense sent, ref Word w)
        {
            if (sent.Conjonctions.Count != 0)
            {
                foreach (Conjonction c in sent.Conjonctions)
                {
                    if (w.GetWithoutAccents.Equals(c.GetWithoutAccents))
                    {
                        w.Roles.Add(c.Role);
                        w.DicoConjonction = c;
                        w.Exception = c.Exception;
                        w.Pers = c.Pers;
                        w.Nombre = c.Nombre;
                        w.Genre = c.Genre;
                        w.Synonymes = c.Synonymes;
                        w.Definition = c.Definition;
                    }
                }
            }
        }
        private void ProcessPreposition(Sentense sent, ref Word w)
        {
            if (sent.Prepositions.Count != 0)
            {
                foreach (Preposition p in sent.Prepositions)
                {
                    if (w.GetWithoutAccents.Equals(p.GetWithoutAccents))
                    {
                        w.Roles.Add(p.Role);
                        w.DicoPreposition = p;
                        w.Exception = p.Exception;
                        w.Pers = p.Pers;
                        w.Nombre = p.Nombre;
                        w.Genre = p.Genre;
                        w.Synonymes = p.Synonymes;
                        w.Definition = p.Definition;
                    }
                }
            }
        }
        private void ProcessChiffre(Sentense sent, ref Word w)
        {
            if (_rexChiffre.IsMatch(w.Text))
            {
                w.Roles.Add(ROLE.CHIFFRE);
                w.Exception = false;
                w.Pers = 0;
                w.Nombre = NOMBRE.NEUTRE;
                w.Genre = GENRE.NEUTRE;
            }
            else if (sent.Chiffres.Count != 0)
            {
                foreach (Chiffre c in sent.Chiffres)
                {
                    if (w.GetWithoutAccents.Equals(c.GetWithoutAccents))
                    {
                        w.Roles.Add(c.Role);
                        w.DicoChiffre = c;
                        w.Exception = c.Exception;
                        w.Pers = c.Pers;
                        w.Nombre = c.Nombre;
                        w.Genre = c.Genre;
                        w.Synonymes = c.Synonymes;
                        w.Definition = c.Definition;
                    }
                }
            }
        }
        private void ProcessMail(Sentense sent, ref Word w)
        {
            if (_rexMail.IsMatch(w.Text))
            {
                w.Roles.Add(ROLE.MAIL);
                w.Exception = false;
                w.Pers = 0;
                w.Nombre = NOMBRE.NEUTRE;
                w.Genre = GENRE.NEUTRE;
            }
        }
        private void ProcessUnknow(Sentense sent, ref Word w)
        {
            if (w.Roles.Count == 0)
                w.Roles.Add(ROLE.UNKNOWN);
        }

        private List<Verb> SearchVerbs(Word w)
        {
            _flagFind = false;
            _lv = new List<Verb>();
            foreach (Time t in Interface_litterature.Dictionnary.Conjugaison.Times)
            {
                try
                {
                    if (!t.Exception && !string.IsNullOrEmpty(t.Infinitif) && w.GetWithoutAccents.EndsWith(t.Infinitif))
                    {
                        SetVerbInfinitif(w, t);
                        break;
                    }
                    if (w.Text.Length > 3 && (w.GetWithoutAccents.Substring(0, 2).Equals("re")) && (w.GetWithoutAccents.Substring(2, w.GetWithoutAccents.Length - 2).Equals(t.Infinitif)))
                    {
                        // TODO : add the idea of repetition because of the re at the begining of the word, in infinitif object
                        // Never lose a user information
                        SetVerbInfinitif(w, t);
                        break;
                    }
                    if (t.Exception && t.Val != null && t.Val.Equals(w.GetWithoutAccents))
                    {
                        SetVerbException(w, t);
                        break;
                    }
                }
                catch (Exception exp3600)
                {
                    Log.write("[ ERR : 3600 ] error while parsing gramar.\n" + exp3600.Message);
                }
            }
            if (!_flagFind)
            {
                foreach (Verb dico_verb in Interface_litterature.Dictionnary.ListVerbs)
                {
                    if (!_flagFind && !string.IsNullOrEmpty(dico_verb.Root) && !dico_verb.Exception && w.GetWithoutAccents.ToLower().Contains(dico_verb.Root.ToLower()))
                    {
                        Verb v = new Verb(dico_verb);
                        v.Text = w.Text;
                        v.LexicalField = w.LexicalField;
                        v.DicoVerb = dico_verb;
                        v.Suffixe = dico_verb.Suffixe;
                        if (w.Text.EndsWith("é") || w.Text.EndsWith("ée") || w.Text.EndsWith("ées"))
                        {
                            SetVerbParticipePasse(w);
                            break;
                        }
                        if (Interface_litterature.Dictionnary.Conjugaison.analyseRegular(v))
                        {
                            _lv.Add(v);
                            break;
                        }
                    }
                }
            }
            foreach (Verb v in _lv)
            {
                SetVerbDefinition(v);
            }
            return _lv;
        }
        private List<Pronom> SearchPronoms(Word w)
        {
            List<Pronom> ln = new List<Pronom>();
            foreach (Pronom dico_pronoms in Interface_litterature.Dictionnary.ListPronoms)
            {
                if (w.GetWithoutAccents.Equals(dico_pronoms.GetWithoutAccents))
                {
                    Pronom pn = new Pronom(dico_pronoms);
                    pn.Text = w.Text;
                    pn.Copy(dico_pronoms);
                    ln.Add(pn);
                }
            }
            return ln;
        }
        private List<NomCommun> SearchNomsCommuns(Word w)
        {
            List<NomCommun> lnc = new List<NomCommun>();
            foreach (NomCommun dico_nc in Interface_litterature.Dictionnary.ListNomCommuns)
            {
                if (dico_nc.Detect(w.GetWithoutAccents))
                {
                    NomCommun nc = new NomCommun(dico_nc);
                    nc.Copy(dico_nc);
                    nc.Text = w.Text;
                    lnc.Add(nc);
                }
            }
            return lnc;
        }
        private List<NomPropre> SearchNomsPropres(Word w)
        {
            List<NomPropre> lnp = new List<NomPropre>();
            foreach (NomPropre dico_np in Interface_litterature.Dictionnary.ListNomPropres)
            {
                if (dico_np.Detect(w.GetWithoutAccents))
                {
                    NomPropre np = new NomPropre(dico_np);
                    np.Copy(dico_np);
                    np.Text = w.Text;
                    lnp.Add(np);
                }
            }
            return lnp;
        }
        private List<Adjective> SearchAdjectives(Word w)
        {
            List<Adjective> ladj = new List<Adjective>();
            foreach (Adjective dico_adj in Interface_litterature.Dictionnary.ListAdjectives)
            {
                if (w.Text.ToLower().Equals(dico_adj.Text.ToLower()))
                {
                    Adjective adj = new Adjective(dico_adj);
                    adj.Text = w.Text;
                    ladj.Add(adj);
                }

            }
            return ladj;
        }
        private List<Adverb> SearchAdverbs(Word w)
        {
            List<Adverb> ladv = new List<Adverb>();
            foreach (Adverb dico_adv in Interface_litterature.Dictionnary.ListAdverbs)
            {
                if (w.Text.ToLower().Equals(dico_adv.Text.ToLower()))
                {
                    Adverb adv = new Adverb(dico_adv);
                    adv.Text = w.Text;
                    ladv.Add(adv);
                }
            }
            return ladv;
        }
        private List<Conjonction> SearchConjonctions(Word w)
        {
            List<Conjonction> lc = new List<Conjonction>();
            foreach (Conjonction dico_c in Interface_litterature.Dictionnary.ListConjonctions)
            {
                if (w.Text.ToLower().Equals(dico_c.Text.ToLower()))
                {
                    Conjonction conj = new Conjonction(dico_c);
                    conj.Text = w.Text;
                    lc.Add(conj);
                }
            }
            return lc;
        }
        private List<Determinant> SearchDeterminants(Word w)
        {
            List<Determinant> ld = new List<Determinant>();
            foreach (Determinant dico_d in Interface_litterature.Dictionnary.ListDeterminant)
            {
                if (w.Text.ToLower().Equals(dico_d.Text.ToLower()))
                {
                    Determinant det = new Determinant(dico_d);
                    det.Text = w.Text;
                    ld.Add(det);
                }
            }
            return ld;
        }
        private List<Preposition> SearchPreposition(Word w)
        {
            // TODO : here more difficult, we have to check if there is multiple words a kind of .Contain()
            List<Preposition> lp = new List<Preposition>();
            foreach (Preposition dico_p in Interface_litterature.Dictionnary.ListPreposition)
            {
                if (w.GetWithoutAccents.ToLower().Equals(dico_p.GetWithoutAccents.ToLower()))
                {
                    Preposition prepo = new Preposition(dico_p);
                    prepo.Text = w.Text;
                    lp.Add(prepo);
                }
            }
            return lp;
        }
        private List<Chiffre> SearchChiffre(Word w)
        {
            List<Chiffre> lc = new List<Chiffre>();
            foreach (Chiffre dico_c in Interface_litterature.Dictionnary.ListChiffre)
            {
                if (w.GetWithoutAccents.ToLower().Equals(dico_c.GetWithoutAccents.ToLower()))
                {
                    Chiffre chf = new Chiffre(dico_c);
                    chf.Text = w.Text;
                    lc.Add(chf);
                }
            }
            return lc;
        }

        private void SetVerbInfinitif(Word w, Time t)
        {
            Verb v = new Verb();
            v.Text = w.Text;
            v.LexicalField = w.LexicalField;
            if (Interface_litterature.Dictionnary.Conjugaison.copyInfo(v, t))
            {
                _flagFind = true;
                v.Form = FORM.INFINITIF;
                v.Genre = GENRE.NEUTRE;
                v.Nombre = NOMBRE.NEUTRE;
                v.Pers = 0;
                _lv.Add(v);
            }
        }
        private void SetVerbParticipePasse(Word w)
        {
            Verb v = new Verb();
            v.Text = w.Text;
            v.LexicalField = w.LexicalField;
            _flagFind = true;
            v.Form = FORM.PARTICIPEPASSE;
            v.Genre = GENRE.NEUTRE;
            v.Nombre = NOMBRE.NEUTRE;
            v.Pers = 0;
            _lv.Add(v);
        }
        private void SetVerbException(Word w, Time t)
        {
            Verb v = new Verb();
            v.Text = w.Text;
            v.LexicalField = w.LexicalField;
            v.Infinitive = t.Infinitif;
            if (Interface_litterature.Dictionnary.Conjugaison.copyInfo(v, t))
            {
                _lv.Add(v);
                _flagFind = true;
            }
        }
        private void SetVerbDefinition(Word w)
        {
            try
            {
                if (w is Verb && !string.IsNullOrEmpty((w as Verb).Infinitive))
                {
                    List<string[]> dbDetails = MySqlAdapter.ExecuteReader("select définition from t_mot where valeur = '" + (w as Verb).Infinitive + "';");
                    if (dbDetails.Count > 0 && dbDetails[0].Length > 0)
                    {
                        w.Definition = dbDetails[0][0];
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("Cannot find definition for a verb : " + exp.Message);
                throw;
            }
        }
        #endregion
    }
}
