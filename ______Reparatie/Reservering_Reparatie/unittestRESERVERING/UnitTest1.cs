using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reservering_Reparatie;
namespace unittestRESERVERING
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Boeking boeking = new Boeking();
            boeking.LaatsteHoofdbezoeker("jan");
        }
    }
}
