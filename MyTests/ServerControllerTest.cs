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
        static string serverId;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            serverServiceMock = new Mock<ServerService>();
            serverController = new ServerController(serverServiceMock.Object);

            string jsonServer = @"{""ServerURL"": ""test.com""}";
            serverController.Post(jsonServer);
            serverControlResult = serverController.Get();
            serverId = serverControlResult.Value[0].ServerId;
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

        [TestMethod]
        public void DeleteServer()
        {
            ActionResult<Server> response = serverController.Delete(serverId);
            var result = response.Result as OkObjectResult;
            Assert.AreEqual(result.StatusCode, 200);
        }
    }
}
