using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using demo;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string passwd = "891487126187117422521931298924620411618024517185152621889179202245169193115202207197";
            string passwd_enter = "12345";

            passwd_enter = FormReg.GetHash(passwd_enter);

            Assert.AreEqual(passwd, passwd_enter);
        }
    }
}
