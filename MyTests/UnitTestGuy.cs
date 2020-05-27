using FlightControlWeb.Services;
using FlightControlWeb.Controllers;
using FlightControlWeb.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Text.Json;
using Newtonsoft.Json;

namespace MyTests
{
    [TestClass]
    public class UnitTestGuy
    {
        [TestMethod]
        public void TestMethod1()
        {
            var valueServiceMock = new Mock<ServerService>();
            var controller = new ServerController(valueServiceMock.Object);

            string jsonServer = @"{       
       ""ServerURL"": ""helloworld.com""             
        }";

            Server json = JsonConvert.DeserializeObject<Server>(jsonServer);


            controller.Post(jsonServer);
            var values = controller.Get();
            System.Console.WriteLine("values count: " + values.Value.Count);

            
            Assert.AreEqual(values.Value.Count, 1);
        }
    }
}
