using AirportFuelManagementWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportFuelManagementWebAPI.Business
{
    public interface IBusiness
    {
        public Task<List<AirportModel>> GetAllAirports();
        public Task<AirportModel> GetAirportById(int id);
        public Task<bool> InsertAirport(AirportModel airport);
        public Task<bool> UpdateAirport(AirportModel airport);
        public Task<List<AircraftModel>> GetAllAircrafts();
        public Task<AircraftModel> GetAircraftById(int id);
        public Task<bool> InsertAircraft(AircraftModel aircraft);
        public Task<bool> UpdateAircraft(AircraftModel aircraft);
        public Task<List<FuelTransactionModel>> GetAllTransactions();
        public Task<bool> InsertTransaction(FuelTransactionModel transaction);
        public Task<bool> InsertUser(UserModel user);
        public Task<int> CheckIfUserExists(string userEmail, string userPassword);
        public Task<bool> CheckIfEmailAlreadyExists(string userEmail);
        public Task<List<AirportTransactionInfo>> FuelConsumptionReport();
        public Task<FuelTransactionModel> GetTransactionById(int id);
        public Task<bool> RemoveAllTransactions();

    }
}
