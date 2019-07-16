using System;
using Droid.Litterature;
using System.Linq;
using NUnit.Framework;

namespace UnitTestProject
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestUTRuns()
        {
            Assert.IsTrue(true);
        }
        [Test]
        public void BuildSentense()
        {
            string text = "je suis une mouette";
            Sentense sent = new Sentense(text);
            Assert.IsTrue(sent.Text.Equals(text));
        }
        [Test]
        public void BuildDetectWords()
        {
            string text = "je suis une mouette";
            Sentense sent = new Sentense(text);
            Assert.IsTrue(sent.Words.Count == 4);
        }
        [Test]
        public void BuildDetectUnderstood()
        {
            string text = "je suis une mouette";
            Sentense sent = new Sentense(text);
            Assert.IsTrue(sent.Understood);
        }
        [Test]
        public void BuildDetectSubject()
        {
            string text = "je suis une mouette";
            Sentense sent = new Sentense(text);
            Assert.IsTrue(sent.Subject.Count(s => s.Text.Equals("je")) > 0);
        }
        [Test]
        public void BuildDetectVerb()
        {
            string text = "je suis une mouette";
            Sentense sent = new Sentense(text);
            Assert.IsTrue(sent.Verbs.Count(s => s.Text.Equals("etre")) > 0);
        }
        [Test]
        public void BuildDetectCOD()
        {
            string text = "je suis une mouette";
            Sentense sent = new Sentense(text);
            Assert.IsTrue(sent.GroupCOD.Count(g => g.Text.Equals("une mouette")) > 0);
        }
        [Test]
        public void BuildDetectCOI()
        {
            string text = "je suis une mouette à bec noir";
            Sentense sent = new Sentense(text);
            Assert.IsTrue(sent.GroupCOI.Count(g => g.Text.Equals("à bec noir")) > 0);
        }
    }
}
