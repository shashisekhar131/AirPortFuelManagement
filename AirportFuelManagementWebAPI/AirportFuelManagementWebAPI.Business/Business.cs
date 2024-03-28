using AirportFuelManagementWebAPI.DAL;
using AirportFuelManagementWebAPI.DAL.Models;
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

        public async Task<AirportModel> GetAirportById(int id)
        {
            Airport tempairport = await dataAccess.GetAirportById(id);

            AirportModel airport = new AirportModel()
            {
                AirportId = tempairport.AirportId,
                AirportName = tempairport.AirportName,
                FuelAvailable = tempairport.FuelAvailable,
                FuelCapacity = tempairport.FuelCapacity
            };
            
            return airport;
        }

        public async Task<List<AirportModel>> GetAllAirports()
        {

            List<AirportModel> airports = (await dataAccess.GetAllAirports())
        .Select(s => new AirportModel
        {
            AirportId = s.AirportId,
            AirportName = s.AirportName,
            FuelAvailable = s.FuelAvailable,
            FuelCapacity = s.FuelCapacity
        })
        .ToList();

            return airports;

        }

        public Task<bool> InsertAirport(AirportModel airport)
        {
            Airport tempAirport = new Airport
            {
                AirportName = airport.AirportName,
                FuelAvailable = airport.FuelAvailable,
                FuelCapacity = airport.FuelCapacity
            };
            return dataAccess.InsertAirport(tempAirport);
        }
        public Task<bool> UpdateAirport(AirportModel airport)
        {
            return dataAccess.UpdateAirport(airport);
        }
        public async Task<AircraftModel> GetAircraftById(int id)
        {
            Aircraft tempAircraft = await dataAccess.GetAircraftById(id);

            AircraftModel aircraft = new AircraftModel ()
            {
                AircraftId = tempAircraft.AircraftId,
                AircraftNumber = tempAircraft.AircraftNumber,
                AirLine = tempAircraft.AirLine,
                SourceId = tempAircraft.SourceId,
                DestinationId = tempAircraft.DestinationId
            };

            return aircraft;
        }

        public Task<List<AircraftModel>> GetAllAircrafts()
        {
            return dataAccess.GetAllAircrafts();
        }

        public Task<bool> InsertAircraft(AircraftModel aircraft)
        {
            Aircraft tempAircraft = new Aircraft
            {
                AircraftNumber = aircraft.AircraftNumber,
                AirLine = aircraft.AirLine,
                SourceId = aircraft.SourceId,
                DestinationId = aircraft.DestinationId
            };
            return dataAccess.InsertAircraft(tempAircraft);
        }

        public Task<bool> UpdateAircraft(AircraftModel aircraft)
        {
            return dataAccess.UpdateAircraft(aircraft);
        }

        public Task<List<FuelTransactionModel>> GetAllTransactions()
        {
            return dataAccess.GetAllTransactions();
        }
        public Task<Tuple<string, bool>> InsertTransaction(FuelTransactionModel transaction)
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
