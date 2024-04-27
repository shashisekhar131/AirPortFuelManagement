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
        public async void GetAllAircrafts_ShouldReturnListOfAircrafts()
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

        [Fact]
        public async Task GetAllTransactions_WithTransactions_ReturnsListOfTransactions()
        {
            // Arrange
            var mockBusiness = new Mock<IBusiness>();
            var controller = new TransactionController(mockBusiness.Object);
            var testData = new List<FuelTransactionModel>
            {
                new FuelTransactionModel { AirportId = 1, AirportName = "Airport 1", Quantity=200 ,TransactionType =1 },
                new FuelTransactionModel { AirportId = 1, AirportName = "Airport 1", Quantity=200 ,TransactionType =1 },
            };
            mockBusiness.Setup(ub => ub.GetAllTransactions()).ReturnsAsync(testData);

            // Act
            var result = await controller.GetAllTransactions();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task InsertUser_WithValidData_ReturnsTrue()
        {
            // Arrange
            var mockBusiness = new Mock<IBusiness>();

            var controller = new UserController(mockBusiness.Object);
            var user = new UserModel
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = "password"
            };

            // Act
            var result = await controller.InsertUser(user);

            // Assert
            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

        }

        [Fact]
        public async Task FuelConsumptionReport_WithTransactions_ReturnsReport()
        {
            // Arrange
            var mockBusiness = new Mock<IBusiness>();
            var mockLogger = new Mock<Utils.ILogger>();
            var controller = new TransactionController(mockBusiness.Object);

            // Act
            var result = await controller.FuelConsumptionReport();

            // Assert 
            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
        }


        [Fact]
        public async Task GetAirportSummary_WithAirports_ReturnsSummaryList()
        {
            // Arrange
            var mockBusiness = new Mock<IBusiness>();
            var mockLogger = new Mock<Utils.ILogger>();
            var controller = new AirPortController(mockBusiness.Object);

            // Act
            var result = await controller.GetAirportSummary();


            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}