using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reservering_Reparatie;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
namespace unittestRESERVERING
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Hoofdboeker hb = new Hoofdboeker("henk", "", "Janssen", "straat", "9", "plaats", "nummer");
            string actual = hb.ToString();
            string expected = "henk";
            Assert.AreEqual(expected, actual);
        }
        public void TestMethod2()
        {
            Account ac = new Account("henk","email","hash");
            string actual = ac.ToString();
            string expected = "henk";
            Assert.AreEqual(expected, actual);
        }
        
    }
}
