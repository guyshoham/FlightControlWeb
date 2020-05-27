using FlightControlWeb.Services;
using FlightControlWeb.Controllers;
using FlightControlWeb.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MyTests
{
    [TestClass]
    public class ServerControllerTest
    {
        static Mock<ServerService> serverServiceMock;
        static ServerController serverController;
        static ActionResult<List<Server>> result;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            serverServiceMock = new Mock<ServerService>();
            serverController = new ServerController(serverServiceMock.Object);

            string jsonServer = @"{""ServerURL"": ""helloworld.com""}";

            serverController.Post(jsonServer);
            result = serverController.Get();
        }

        [TestMethod]
        public void ServersCount()
        {
            Assert.AreEqual(result.Value.Count, 1);
        }

        [TestMethod]
        public void ServerIDNotNull()
        {
            Assert.IsNotNull(result.Value[0].ServerId);
        }

        [TestMethod]
        public void ServersURLIsValid()
        {
            Assert.AreEqual(result.Value[0].ServerURL, "helloworld.com");
        }
    }
}
