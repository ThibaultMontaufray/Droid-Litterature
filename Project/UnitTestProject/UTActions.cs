using System;
using Droid.Litterature;
using System.Linq;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestFixture]
    public class UTActions
    {
        private IConfiguration _config = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build();
        
        [SetUp]
        public void SetUp()
        {
            Droid.Database.DBAdapter.Schema = _config["DB_SCHEMA"];
            Droid.Database.DBAdapter.DbType = (Droid.Database.DataBasesType)Enum.Parse(typeof(Droid.Database.DataBasesType), _config["DB_TYPE"]);
            Droid.Database.DBAdapter.Database = _config["DB_NAME"];
            Droid.Database.DBAdapter.Server = _config["DB_HOST"];
            Droid.Database.DBAdapter.User = _config["DB_USER"];
            Droid.Database.DBAdapter.Password = _config["DB_PSWD"];
            Droid.Database.DBAdapter.Port = _config["DB_PORT"];
        }

        [Test]
        public void TestACTION_130_definition_word_Empty()
        {
            Dictionary<string, object[]> actual = Interface_litterature.ACTION_130_definition_word(null);
            Assert.IsTrue(actual != null && actual.Count == 0);
        }
        [Test]
        public void TestACTION_130_definition_word_chat()
        {
            Dictionary<string, object[]> actual = Interface_litterature.ACTION_130_definition_word("chat");
            var v = actual.ToList();
            Assert.IsTrue(actual != null && v[0].Key.Equals("animal"));
        }
    }
}
