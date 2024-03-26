using AirportFuelManagementWebAPI.DAL;
using AirportFuelManagementWebAPI.Models;

namespace AirportFuelManagementWebAPI.Business
{
    public class Business : IBusiness
    {
        private IDataAccess dataAccess;
        public Business(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public Task<AirportModel> GetAirportById(int id)
        {
            return dataAccess.GetAirportById(id);
        }

        public Task<List<AirportModel>> GetAllAirports()
        {
            return dataAccess.GetAllAirports();
        }

        public Task<bool> InsertAirport(AirportModel airport)
        {
            return dataAccess.InsertAirport(airport);
        }
        public Task<bool> UpdateAirport(AirportModel airport)
        {
            return dataAccess.UpdateAirport(airport);
        }
        public Task<AircraftModel> GetAircraftById(int id)
        {
            return dataAccess.GetAircraftById(id);
        }

        public Task<List<AircraftModel>> GetAllAircrafts()
        {
            return dataAccess.GetAllAircrafts();
        }

        public Task<bool> InsertAircraft(AircraftModel aircraft)
        {
            return dataAccess.InsertAircraft(aircraft);
        }

        public Task<bool> UpdateAircraft(AircraftModel aircraft)
        {
            return dataAccess.UpdateAircraft(aircraft);
        }

        public Task<List<FuelTransactionModel>> GetAllTransactions()
        {
            return dataAccess.GetAllTransactions();
        }
        public Task<bool> InsertTransaction(FuelTransactionModel transaction)
        {
            return dataAccess.InsertTransaction(transaction);
        }

        public Task<int> CheckIfUserExists(string userEmail, string userPassword)
        {
            return dataAccess.CheckIfUserExists(userEmail, userPassword);
        }
        public Task<bool> CheckIfEmailAlreadyExists(string userEmail)
        {
            return dataAccess.CheckIfEmailAlreadyExists(userEmail);
        }
        public Task<bool> InsertUser(UserModel user)
        {
            return dataAccess.InsertUser(user);
        }
        public Task<List<AirportTransactionInfo>> FuelConsumptionReport()
        {
            return dataAccess.FuelConsumptionReport();
        }
        public Task<FuelTransactionModel> GetTransactionById(int id)
        {
            return dataAccess.GetTransactionById(id);
        }
        public Task<bool> RemoveAllTransactions()
        {
            return dataAccess.RemoveAllTransactions();
        }


    }
}
