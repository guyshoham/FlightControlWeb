using FlightControlWeb.Controllers;
using FlightControlWeb.Models;
using FlightControlWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightControlWeb.Test
{

    [TestClass]
    public class FlightPlanControllerTest
    {
        static Mock<FlightService> flightServiceMock;
        static FlightPlanController flightPlanController;
        static ActionResult<Dictionary<string, FlightPlan>> flightControlResult;
        static FlightPlan returnedPlan;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            flightServiceMock = new Mock<FlightService>();
            flightPlanController = new FlightPlanController(flightServiceMock.Object);
            string jsonFlight = @"{""passengers"": 216,
    ""company_name"": ""SwissAir"",
    ""initial_location"": {
        ""longitude"": 33.000,
        ""latitude"": 31.000,
        ""date_time"": ""2020-12-26T18:00:00Z""
    },
    ""segments"": [
        {
            ""longitude"": 33.500,
            ""latitude"": 31.500,
            ""timespan_seconds"": 600
        }
    ]}";
            ActionResult<FlightPlan> returned = flightPlanController.Post(jsonFlight);
            var result = returned.Result as OkObjectResult;
            var plan = result.Value as FlightPlan;
            flightControlResult = flightPlanController.Get(plan.FlightId);
            var returnedResult = flightControlResult.Result as OkObjectResult;
            returnedPlan = result.Value as FlightPlan;
        }

        [TestMethod]
        public void CompanyName()
        {
            Assert.AreEqual(returnedPlan.CompanyName, "SwissAir");
        }

        [TestMethod]
        public void Passengers()
        {
            Assert.AreEqual(returnedPlan.Passengers, 216);
        }

        [TestMethod]
        public void startPoint()
        {
            Assert.AreEqual(returnedPlan.Segments[0].Latitude, 31.5);
            Assert.AreEqual(returnedPlan.Segments[0].Longitude, 33.5);
        }

        [TestMethod]
        public void FlightIDNotNull()
        {
            Assert.IsNotNull(returnedPlan.FlightId);
        }

        [TestMethod]
        public void DeleteFlight()
        {
            ActionResult<Flight> response = flightPlanController.Delete(returnedPlan.FlightId);
            var result = response.Result as OkObjectResult;
            Assert.AreEqual(result.StatusCode, 200);
        }


    }
}
