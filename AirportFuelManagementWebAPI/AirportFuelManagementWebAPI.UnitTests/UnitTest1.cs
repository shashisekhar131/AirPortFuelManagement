using AirportFuelManagementWebAPI.Business;
using AirportFuelManagementWebAPI.Controllers;
using AirportFuelManagementWebAPI.DAL;
using AirportFuelManagementWebAPI.DAL.Models;
using AirportFuelManagementWebAPI.Models;
using AirportFuelManagementWebAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace AirportFuelManagementWebAPI.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public async void GetAllAirports_ShouldReturnListOfAirports()
        {
            var mockBusiness = new Mock<IBusiness>();

            var controller = new AirPortController(mockBusiness.Object);

            var testData = new List<AirportModel>
            {
                new AirportModel { AirportId = 1, AirportName = "Airport 1", FuelAvailable = 100, FuelCapacity = 300 },
                new AirportModel { AirportId = 2, AirportName = "Airport 2", FuelAvailable = 150, FuelCapacity = 250 }
            };

            mockBusiness.Setup(ub => ub.GetAllAirports()).ReturnsAsync(testData);
         
            // Act                                                                       
            var result = await controller.GetAllAirports();

            // Assert
            Assert.IsType<OkObjectResult>(result);
          
        }

        [Fact]
        public async Task GetAllAircrafts_ShouldReturnListOfAircrafts()
        {
            // Arrange
            var mockBusiness = new Mock<IBusiness>();
            var controller = new AircraftController(mockBusiness.Object);

            var testData = new List<AircraftModel>
            {
                new AircraftModel { AircraftId = 1, AircraftNumber = "A001", AirLine = "Airline 1" },
                new AircraftModel { AircraftId = 2, AircraftNumber = "A002", AirLine = "Airline 2"}
            };

            mockBusiness.Setup(ub => ub.GetAllAircrafts()).ReturnsAsync(testData);

            // Act
            var result = await controller.GetAllAircrafts();

            // Assert
            Assert.IsType<OkObjectResult>(result);

         
        }


        [Fact]
        public async Task GetAirportById_ExistingId_ShouldReturnAirport()
        {
            // Arrange
            var mockBusiness = new Mock<IBusiness>();
            var controller = new AirPortController(mockBusiness.Object);

            var testData = new AirportModel { AirportId = 1, AirportName = "Airport 1", FuelAvailable = 100, FuelCapacity = 300 };

            mockBusiness.Setup(ub => ub.GetAirportById(1)).ReturnsAsync(testData);

            // Act
            var result = await controller.GetAirportById(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            var airport = okResult.Value as AirportModel;
            Assert.NotNull(airport);

            // Compare with test data
            Assert.Equal(testData.AirportId, airport.AirportId);
            Assert.Equal(testData.AirportName, airport.AirportName);
            Assert.Equal(testData.FuelAvailable, airport.FuelAvailable);
            Assert.Equal(testData.FuelCapacity, airport.FuelCapacity);
        }

        [Fact]
        public async Task GetTransactionById_ExistingId_ShouldReturnTransaction()
        {
            // Arrange
            var mockBusiness = new Mock<IBusiness>();
            var controller = new TransactionController(mockBusiness.Object);

            var testData = new FuelTransactionModel
            {
                TransactionId = 1,
                TransactionTime = DateTime.Now,
                TransactionType = 1,
                Quantity = 100,
                AirportId = 1,
                AircraftId = 1,
                AirportName = "Airport 1",
                AircraftName = "Aircraft 1"
            };

            mockBusiness.Setup(ub => ub.GetTransactionById(1)).ReturnsAsync(testData);

            // Act
            var result = await controller.GetTransactionById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
           
        }


    }
}