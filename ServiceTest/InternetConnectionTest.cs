using System;
using App.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ServiceTest
{
    [TestClass]
    public class InternetConnectionTest
    {
        [TestMethod]
        public void CheckInternetConnectionWithValideURL()
        {
            InternetConnection internetConnection = new InternetConnection("https://www.lotto24.de/webshop/product/lottonormal/result");
            Assert.IsTrue(internetConnection.CheckInternetConnection());
        }

        [TestMethod]
        public void CheckIfHTMLConntentIsAvailable()
        {
            InternetConnection internetConnection = new InternetConnection("https://www.lotto24.de/webshop/product/lottonormal/result");
            Assert.IsTrue(internetConnection.HtmlQuellcode != null);
            Assert.IsTrue(internetConnection.HtmlQuellcode.Length > 0);
        }
        [TestMethod]
        public void CheckIfHTMLConntentIsEmptyAtWrongURL()
        {
            InternetConnection internetConnection = new InternetConnection("www.gooogle.de");
            Assert.IsTrue(internetConnection.HtmlQuellcode == null);
        }

    }
}
