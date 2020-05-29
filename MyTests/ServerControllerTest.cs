using FlightControlWeb.Services;
using FlightControlWeb.Controllers;
using FlightControlWeb.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace FlightControlWeb.Test
{
    [TestClass]
    public class ServerControllerTest
    {
        static Mock<ServerService> serverServiceMock;
        static ServerController serverController;
        static ActionResult<List<Server>> serverControlResult;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            serverServiceMock = new Mock<ServerService>();
            serverController = new ServerController(serverServiceMock.Object);

            string jsonServer = @"{""ServerURL"": ""test.com""}";
            serverController.Post(jsonServer);
            serverControlResult = serverController.Get();
            var a = 5;
        }

        [TestMethod]
        public void ServersCount()
        {
            Assert.AreEqual(serverControlResult.Value.Count, 1);
        }

        [TestMethod]
        public void ServerIDNotNull()
        {
            Assert.IsNotNull(serverControlResult.Value[0].ServerId);
        }

        [TestMethod]
        public void ServersURLIsValid()
        {
            Assert.AreEqual(serverControlResult.Value[0].ServerURL, "test.com");
        }
    }
}
