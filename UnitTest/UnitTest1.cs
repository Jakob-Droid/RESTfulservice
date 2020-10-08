using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTful_service;
using RESTful_service.Controllers;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        public ItemsController _itemController;

        [TestInitialize]
        public void TestInitialize()
        {
            _itemController = new ItemsController();
        }
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(_itemController.Get(5), ItemsController.items[4]);
        }
    }
}
