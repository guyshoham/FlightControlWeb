using FlightControlWeb.Controllers;
using FlightControlWeb.Models;
using FlightControlWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlightControlWeb.Test
{

    [TestClass]
    public class FlightControllerTest
    {
        static Mock<FlightService> flightServiceMock;
        static FlightController flightController;
        static FlightPlanController flightPlanController;
        static List<Flight> flightsList;
        

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            flightServiceMock = new Mock<FlightService>();
            flightController = new FlightController(flightServiceMock.Object);
            flightPlanController = new FlightPlanController(flightServiceMock.Object);

            //Take current Time
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.ToUniversalTime();
            //Create Json with current time
            string dateAsString = dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");

            string jsonFlight = @"{""passengers"": 216,
    ""company_name"": ""SwissAir"",
    ""initial_location"": {
        ""longitude"": 33.000,
        ""latitude"": 31.000,
        ""date_time"": """ + dateAsString + @"""
    },
    ""segments"": [
        {
            ""longitude"": 33.500,
            ""latitude"": 31.500,
            ""timespan_seconds"": 300
        }
    ]}";
            //Post Flight
            flightPlanController.Post(jsonFlight);
        }

        [TestMethod]
        public void FlightIsActive()
        {
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.ToUniversalTime();
            //Flight still Active
            dateTime = dateTime.AddMinutes(4);
            //Get Active Flights
            flightsList = flightServiceMock.Object.GetAllFlightsRelativeToDate(dateTime);
            Assert.AreEqual(flightsList.Count, 1);
        }

        [TestMethod]
        public void FlightIsNotActiveAnymore()
        {
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.ToUniversalTime();
            //Flight Not active anymore
            dateTime = dateTime.AddMinutes(5);

            flightsList = flightServiceMock.Object.GetAllFlightsRelativeToDate(dateTime);
            Assert.AreEqual(flightsList.Count, 0);
        }


    }
}
