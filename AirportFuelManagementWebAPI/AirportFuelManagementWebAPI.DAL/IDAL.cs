using AirportFuelManagementWebAPI.DAL.Models;
using AirportFuelManagementWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportFuelManagementWebAPI.DAL
{
    public interface IDataAccess
    {
        public Task<List<Airport>> GetAllAirports();
        public Task<Airport> GetAirportById(int id);
        public  Task<bool> InsertAirport(Airport airport);
        public Task<bool> UpdateAirport(AirportModel airport);
        public Task<List<AircraftModel>> GetAllAircrafts();
        public Task<Aircraft> GetAircraftById(int id);
        public Task<bool> InsertAircraft(Aircraft aircraft);
        public Task<bool> UpdateAircraft(AircraftModel aircraft);
        public Task<List<FuelTransactionModel>> GetAllTransactions();
        public Task<Tuple<string, bool>> InsertTransaction(FuelTransactionModel transaction);
        public  Task<bool> InsertUser(UserModel user);
        public Task<int> CheckIfUserExists(string userEmail, string userPassword);
        public Task<bool> CheckIfEmailAlreadyExists(string userEmail);
        public Task<List<AirportTransactionInfo>> FuelConsumptionReport();
        public Task<FuelTransactionModel> GetTransactionById(int id);
        public Task<bool> RemoveAllTransactions();

    }
}
