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
            try
            {
                string text = "je suis une mouette";
                Sentense sent = new Sentense(text);

                Assert.IsTrue(sent.Text.Equals(text));
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void BuildDetectWords()
        {
            try
            {
                string text = "je suis une mouette";
                Sentense sent = new Sentense(text);
                Assert.IsTrue(sent.Words.Count == 4);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void BuildDetectUnderstood()
        {
            try
            {
                string text = "je suis une mouette";
                Sentense sent = new Sentense(text);
                Assert.IsTrue(sent.Understood);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void BuildDetectSubject()
        {
            try
            {
                string text = "je suis une mouette";
                Sentense sent = new Sentense(text);
                Assert.IsTrue(sent.Subject.Count(s => s.Text.Equals("je")) > 0);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void BuildDetectVerb()
        {
            try
            {
                string text = "je suis une mouette";
                Sentense sent = new Sentense(text);
                Assert.IsTrue(sent.Verbs.Count(s => s.Text.Equals("etre")) > 0);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void BuildDetectCOD()
        {
            try
            {
                string text = "je suis une mouette";
                Sentense sent = new Sentense(text);
                Assert.IsTrue(sent.GroupCOD.Count(g => g.Text.Equals("une mouette")) > 0);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void BuildDetectCOI()
        {
            try
            {
                string text = "je suis une mouette à bec noir";
                Sentense sent = new Sentense(text);
                Assert.IsTrue(sent.GroupCOI.Count(g => g.Text.Equals("à bec noir")) > 0);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
    }
}
