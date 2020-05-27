using Autofac.Extras.Moq;
using FlightControlWeb.Controllers;
using FlightControlWeb.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text.Json;

namespace FlightControlWeb.Testing
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string jsonData = @"{
""method"": ""POST"",
        ""headers"": {
            'Content-Type': ""application/json;charset=utf-8""
        },
        ""body"":
		{
        ""passengers"": 2,
        ""company_name"": ""DragAndDropAirWaves"",
        ""initial_location"": {
            ""longitude"": 34.847036,
            ""latitude"": 32.130232,
            ""date_time"": ""2021-12-26T20:00:00Z""
        },
        ""segments"": [
            {
                ""longitude"": 34.847037,
                ""latitude"": 32.130233,
                ""timespan_seconds"": 600
            },
            {
                ""longitude"": 34.847038,
                ""latitude"": 32.130234,
                ""timespan_seconds"": 300
            }
        ]
}
		      
}";
            JsonElement j = new JsonElement();
            

            using (var mock = AutoMock.GetLoose())
            {
                //Server y = new Server() { ServerId = "test", ServerURL = "test" };
                mock.Mock<FlightPlanController>()
                    .Setup(x => x.Post(j));


            }
        }
    }
}
