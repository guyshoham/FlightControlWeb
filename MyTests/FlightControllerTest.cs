using FlightControlWeb.Controllers;
using FlightControlWeb.Models;
using FlightControlWeb.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace FlightControlWeb.Test
{

    [TestClass]
    public class FlightControllerTest
    {
        static Mock<FlightService> flightServiceMock;
        static FlightController flightController;
        static FlightPlanController flightPlanController;
        static List<Flight> flightsList;
        static DateTime postDateTime;


        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            flightServiceMock = new Mock<FlightService>();
            flightController = new FlightController(flightServiceMock.Object);
            flightPlanController = new FlightPlanController(flightServiceMock.Object);

            //Take current Time
            postDateTime = DateTime.Now;
            postDateTime = postDateTime.ToUniversalTime();
            //Create Json with current time
            string dateAsString = postDateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");

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
            ""timespan_seconds"": 600
        }
    ]}";
            //Post Flight
            flightPlanController.Post(jsonFlight);
        }

        [TestMethod]
        public void FlightIsActive()
        {
            //Flight still Active
            DateTime oneMinute = postDateTime.AddMinutes(1);
            //Get Active Flights
            flightsList = flightServiceMock.Object.GetAllFlightsRelativeToDate(oneMinute);
            Assert.AreEqual(flightsList.Count, 1);
        }

        [TestMethod]
        public void FlightIsNotActiveAnymore()
        {
            //Flight Not active anymore
            DateTime dateTime = postDateTime.AddMinutes(11);

            flightsList = flightServiceMock.Object.GetAllFlightsRelativeToDate(dateTime);
            Assert.AreEqual(flightsList.Count, 0);
        }


    }
}