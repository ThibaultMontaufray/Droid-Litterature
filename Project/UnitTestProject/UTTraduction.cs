using System;
using Droid.Litterature;
using System.Linq;
using NUnit.Framework;

namespace UnitTestProject
{
    [TestFixture]
    public class UTTraduction
    {
        [Test]
        public void TraductionEmpty()
        {
            try
            {
                string result = Traduction.This(string.Empty, LANGAGE.EN, LANGAGE.FR);

                Assert.IsTrue(result.Equals(string.Empty));
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void TraductionHelloENFR()
        {
            string result = Traduction.This("hello", LANGAGE.EN, LANGAGE.FR);

            Assert.IsTrue(result.Equals("bonjour"));
        }
        [Test]
        public void TraductionHelloFREN()
        {
            string result = Traduction.This("bonjour", LANGAGE.FR, LANGAGE.EN);

            Assert.IsTrue(result.Equals("hello"));
        }
        [Test]
        public void TraductionHelloTNFR()
        {
            string result = Traduction.This("aslema", LANGAGE.TN, LANGAGE.FR);

            Assert.IsTrue(result.Equals("bonjour"));
        }
        [Test]
        public void TraductionHelloFRTN()
        {
            string result = Traduction.This("bonjour", LANGAGE.FR, LANGAGE.TN);

            Assert.IsTrue(result.Equals("aslema"));
        }
        [Test]
        public void TraductionHelloENTN()
        {
            string result = Traduction.This("hello", LANGAGE.EN, LANGAGE.TN);

            Assert.IsTrue(result.Equals("aslema"));
        }
        [Test]
        public void TraductionHelloTNEN()
        {
            string result = Traduction.This("aslema", LANGAGE.TN, LANGAGE.EN);

            Assert.IsTrue(result.Equals("hello"));
        }
    }
}
