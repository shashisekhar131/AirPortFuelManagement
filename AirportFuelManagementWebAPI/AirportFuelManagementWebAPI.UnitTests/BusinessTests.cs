using AirportFuelManagementWebAPI.DAL;
using AirportFuelManagementWebAPI.Models;
using AirportFuelManagementWebAPI.Business;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportFuelManagementWebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using AirportFuelManagementWebAPI.DAL.Models;

namespace AirportFuelManagementWebAPI.UnitTests
{
    public class BusinessTests
    {
        private readonly Mock<IDataAccess> _mockDataAccess;
        private readonly AirportFuelManagementWebAPI.Business.Business _business;

        public BusinessTests()
        {
            _mockDataAccess = new Mock<IDataAccess>();
            _business = new AirportFuelManagementWebAPI.Business.Business(_mockDataAccess.Object);
        }       

        [Fact]
        public async void GetAllAirports_ShouldReturnListOfAirports()
        {

            var testData = new List<Airport>
            {
                new Airport { AirportId = 1, AirportName = "Airport 1", FuelAvailable = 100, FuelCapacity = 300 },
                new Airport { AirportId = 2, AirportName = "Airport 2", FuelAvailable = 150, FuelCapacity = 250 }
            };

            _mockDataAccess.Setup(ub => ub.GetAllAirports()).ReturnsAsync(testData);

            // Act                                                                       
            var result = await _business.GetAllAirports();
            var airports = result.Select(a => new Airport
            {
                AirportId = a.AirportId,
                AirportName = a.AirportName,
                FuelAvailable = a.FuelAvailable,
                FuelCapacity = a.FuelCapacity
            }).ToList();

            // Assert           
            Assert.IsType<List<Airport>>(airports);

        }

        [Fact]
        public async void GetAllAircrafts_ShouldReturnListOfAircrafts()
        {
           
            var testData = new List<AircraftModel>
            {
                new AircraftModel { AircraftId = 1, AircraftNumber = "A001", AirLine = "Airline 1" },
                new AircraftModel { AircraftId = 2, AircraftNumber = "A002", AirLine = "Airline 2"}
            };

            _mockDataAccess.Setup(ub => ub.GetAllAircrafts()).ReturnsAsync(testData);

            // Act
            var result = await _business.GetAllAircrafts();

            // Assert
            Assert.IsType<List<AircraftModel>>(result);
        }


        [Fact]
        public async Task GetAirportById_ExistingId_ShouldReturnAirport()
        {
            // Arrange
         
            var testData = new Airport { AirportId = 1, AirportName = "Airport 1", FuelAvailable = 100, FuelCapacity = 300 };

            _mockDataAccess.Setup(ub => ub.GetAirportById(1)).ReturnsAsync(testData);

            // Act
            var result = await _business.GetAirportById(1);

            // Assert
            Assert.IsType<AirportModel>(result);
        }

        [Fact]
        public async Task GetTransactionById_ExistingId_ShouldReturnTransaction()
        {
            // Arrange
          
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

            _mockDataAccess.Setup(ub => ub.GetTransactionById(1)).ReturnsAsync(testData);

            // Act
            var result = await _business.GetTransactionById(1);

            // Assert
            Assert.IsType<FuelTransactionModel>(result);

        }

        [Fact]
        public async Task GetAllTransactions_WithTransactions_ReturnsListOfTransactions()
        {
            // Arrange
            var testData = new List<FuelTransactionModel>
            {
                new FuelTransactionModel { AirportId = 1, AirportName = "Airport 1", Quantity=200 ,TransactionType =1 },
                new FuelTransactionModel { AirportId = 1, AirportName = "Airport 1", Quantity=200 ,TransactionType =1 },
            };
           _mockDataAccess.Setup(ub => ub.GetAllTransactions()).ReturnsAsync(testData);

            // Act
            var result = await _business.GetAllTransactions();

            // Assert
            Assert.IsType<List<FuelTransactionModel>>(result);
        }




        [Fact]
        public async Task FuelConsumptionReport_WithTransactions_ReturnsReport()
        {
            // Arrange// Arrange
            var testData = new List<AirportTransactionInfo>{
            new AirportTransactionInfo
            {
                AirportName = "Airport 1",
                AirportFuelAvailable = 1000,
                Transactions = new List<TransactionItem> {
                    new TransactionItem  {   TransactionTime = DateTime.Now.AddDays(-1),  TransactionType = 1,  Quantity = 200,   AircraftName = "Aircraft 1"  },
                    new TransactionItem  { TransactionTime = DateTime.Now.AddDays(-2), TransactionType = 1, Quantity = 150,  AircraftName = "Aircraft 2" }
                }
             },
            new AirportTransactionInfo
             {
                AirportName = "Airport 2",
                AirportFuelAvailable = 1500,
                Transactions = new List<TransactionItem>
                {
                    new TransactionItem { TransactionTime = DateTime.Now.AddDays(-3), TransactionType = 1, Quantity = 300, AircraftName = "Aircraft 3" },
                    new TransactionItem {  TransactionTime = DateTime.Now.AddDays(-4), TransactionType = 1, Quantity = 250, AircraftName = "Aircraft 4" }
                }
               }
             };

            _mockDataAccess.Setup(ub => ub.FuelConsumptionReport()).ReturnsAsync(testData);

            // Act
            var result = await _business.FuelConsumptionReport();

            // Assert
            Assert.IsType<List<AirportTransactionInfo>>(result);
        }


        [Fact]
        public async Task GetAirportSummary_WithAirports_ReturnsSummaryList()
        {
            // Arrange            

            var testData = new List<Airport>
            {
                new Airport { AirportId = 1, AirportName = "Airport 1", FuelAvailable = 100, FuelCapacity = 300 },
                new Airport { AirportId = 2, AirportName = "Airport 2", FuelAvailable = 150, FuelCapacity = 250 }
            };

            _mockDataAccess.Setup(ub => ub.GetAllAirports()).ReturnsAsync(testData);

            // Act                                                                       
            var result = await _business.GetAllAirports();
            var airports = result.Select(a => new Airport
            {
                AirportId = a.AirportId,
                AirportName = a.AirportName,
                FuelAvailable = a.FuelAvailable,
                FuelCapacity = a.FuelCapacity
            }).ToList();

            // Assert           
            Assert.IsType<List<Airport>>(airports);
        }

        [Fact]
        public async Task RemoveAllTransactions_ShouldReturnTrue()
        {
            // Arrange
            _mockDataAccess.Setup(ub => ub.RemoveAllTransactions()).ReturnsAsync(true);

            // Act
            var result = await _business.RemoveAllTransactions();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task InsertTransaction_WithValidTransaction_ReturnsTupleWithSuccessTrue()
        {
            // Arrange
            var transaction = new FuelTransactionModel { AirportId = 1, AirportName = "Airport 1", Quantity = 200, TransactionType = 1 };

            _mockDataAccess.Setup(ub => ub.InsertTransaction(transaction)).ReturnsAsync(Tuple.Create("Success Message", true));

            // Act
            var result = await _business.InsertTransaction(transaction);

            // Assert
            Assert.True(result.Item2);
        }

        [Fact]
        public async Task InsertAirport_WithValidAirport_ReturnsTrue()
        {
            // Arrange
            var airport = new AirportModel { AirportId = 1, AirportName = "Airport 1", FuelAvailable = 100, FuelCapacity = 300 };
            _mockDataAccess.Setup(ub => ub.InsertAirport(It.IsAny<Airport>())).ReturnsAsync(true);

            // Act
            var result = await _business.InsertAirport(airport);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task InsertAircraft_WithValidAircraft_ReturnsTrue()
        {
            // Arrange
            var aircraft = new AircraftModel { AircraftId = 1, AircraftNumber = "A001", AirLine = "Airline 1" };
            _mockDataAccess.Setup(ub => ub.InsertAircraft(It.IsAny<Aircraft>())).ReturnsAsync(true);

            // Act
            var result = await _business.InsertAircraft(aircraft);

            // Assert
            Assert.True(result);
        }


    }
}
