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
    }
}
