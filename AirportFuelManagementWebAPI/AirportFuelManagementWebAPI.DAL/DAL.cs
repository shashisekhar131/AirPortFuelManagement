using AirportFuelManagementWebAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;


using AirportFuelManagementWebAPI.Models;
using System.Linq.Expressions;

namespace AirportFuelManagementWebAPI.DAL
{
    public class DataAccess:IDataAccess
    {
        private readonly AirportFuelManagementContext context;
        private readonly Utils.ILogger logger;


        public DataAccess(AirportFuelManagementContext context, Utils.ILogger logger)
        {
            // this is used as parameters and properties are with same name
            this.context = context;
            this.logger = logger;
        }
       
        public async Task<List<Airport>> GetAllAirports()
        {
            List<Airport> airports = new List<Airport>();            
            airports = await context.Airports.ToListAsync();
            return airports;
        }

        public async Task<Airport> GetAirportById(int id)
        {
            Airport airport = new Airport();
            airport = await context.Airports.FirstOrDefaultAsync(a => a.AirportId == id);
            if (airport != null)
            {
                return airport;
            }      

            return airport;
        }

        public async Task<bool> InsertAirport(Airport airport)
        {
            bool flag = false;
            
            await context.Airports.AddAsync(airport);
            await context.SaveChangesAsync();
            flag = true;
          
            return flag;
        }
        public async Task<bool> UpdateAirport(AirportModel airport)
        {
            bool flag = false;
            
            var existingAirport = await context.Airports.FirstOrDefaultAsync(a => a.AirportId == airport.AirportId);

            if (existingAirport != null)
            {
                existingAirport.AirportName = airport.AirportName;
                existingAirport.FuelAvailable = airport.FuelAvailable;
                existingAirport.FuelCapacity = airport.FuelCapacity;

                context.Airports.Update(existingAirport);
                await context.SaveChangesAsync();
                flag = true;
            }
            
            return flag;
        }

      public async Task<List<AircraftModel>> GetAllAircrafts()
        {
             List<AircraftModel> aircrafts = new List<AircraftModel>();

           
            var airports = await context.Airports.ToListAsync();

            aircrafts = await context.Aircraft
                .Select(s => new AircraftModel
                {
                    AircraftId = s.AircraftId,
                    AircraftNumber = s.AircraftNumber,
                    AirLine = s.AirLine,
                    SourceId = s.SourceId,
                    DestinationId = s.DestinationId
                })
                .ToListAsync();

                
            foreach (var aircraft in aircrafts)
            {
                aircraft.SourceName = airports.FirstOrDefault(a => a.AirportId == aircraft.SourceId)?.AirportName;
                aircraft.DestinationName = airports.FirstOrDefault(a => a.AirportId == aircraft.DestinationId)?.AirportName;
            }
           

            return aircrafts;
        }


        public async Task<Aircraft> GetAircraftById(int id)
        {
            Aircraft aircraft = new Aircraft();
  
            aircraft = await context.Aircraft.FirstOrDefaultAsync(a => a.AircraftId == id);
                
            if (aircraft != null)
            {
                return aircraft;                   
            }
            
            return aircraft;
        }

        public async Task<bool> InsertAircraft(Aircraft aircraft)
        {
            bool flag = false;
                      
            await context.Aircraft.AddAsync(aircraft);
            await context.SaveChangesAsync();
            flag = true;
          
            return flag;
        }

        public async Task<bool> UpdateAircraft(AircraftModel aircraft)
        {
            bool flag = false;
            var existingAircraft = await context.Aircraft.FirstOrDefaultAsync(a => a.AircraftId == aircraft.AircraftId);

            if (existingAircraft != null)
            {
                existingAircraft.AircraftNumber = aircraft.AircraftNumber;
                existingAircraft.AirLine = aircraft.AirLine;
                existingAircraft.SourceId = aircraft.SourceId;
                existingAircraft.DestinationId = aircraft.DestinationId;

                context.Aircraft.Update(existingAircraft);
                await context.SaveChangesAsync();
                flag = true;
            }
           
            return flag;
        }
        public async Task<List<FuelTransactionModel>> GetAllTransactions()
        {
            List<FuelTransactionModel> transactions = new List<FuelTransactionModel>();
            // Eager loading 
                transactions = await context.FuelTransactions
            .Include(t => t.Aircraft) 
            .Include(t => t.Airport)  
            .Select(t => new FuelTransactionModel
            {
                TransactionId = t.TransactionId,
                AircraftId = t.AircraftId,
                AirportId = t.AirportId,
                Quantity = t.Quantity,
                TransactionType = t.TransactionType,
                TransactionIdparent = t.TransactionIdparent,
                TransactionTime = t.TransactionTime,
                AircraftName = t.Aircraft != null ? t.Aircraft.AircraftNumber : "",
                AirportName = t.Airport != null ? t.Airport.AirportName : ""
            })
            .ToListAsync();           

            return transactions;
        }


        public async Task<Tuple<string, bool>> InsertTransaction(FuelTransactionModel transaction)
        {
                var airport = await context.Airports.FirstOrDefaultAsync(a => a.AirportId == transaction.AirportId);

                // before transaction total fuel for airport is  total IN(=1) - total OUT(=2)
                decimal totalFuelIN = (await context.FuelTransactions
                    .Where(ft => ft.AirportId == transaction.AirportId && ft.TransactionType == 1)
                    .SumAsync(ft => (decimal?)ft.Quantity) ?? 0);

                decimal totalFuelOUT = (await context.FuelTransactions
                        .Where(ft => ft.AirportId == transaction.AirportId && ft.TransactionType == 2)
                        .SumAsync(ft => (decimal?)ft.Quantity) ?? 0);

                decimal totalFuelAvailable = totalFuelIN - totalFuelOUT;

                decimal fuelAvailableAfterTransaction = totalFuelAvailable;

                if (transaction.TransactionType == 1)
                {
                    fuelAvailableAfterTransaction += transaction.Quantity;
                }
                else if (transaction.TransactionType == 2)
                {
                    fuelAvailableAfterTransaction -= transaction.Quantity;
                }

                if (fuelAvailableAfterTransaction < 0 || fuelAvailableAfterTransaction > airport.FuelCapacity)
                {
                    if (fuelAvailableAfterTransaction < 0)
                    {
                        return Tuple.Create("Insufficient fuel(fuel available is " + totalFuelAvailable + ")", false);
                    }

                    if (fuelAvailableAfterTransaction > airport.FuelCapacity)
                    {
                        return Tuple.Create("Exceeded airport capacity(airport can hold only " + (airport.FuelCapacity - totalFuelAvailable) + " fuel)", false);
                    }
                }

                // If validation passes, add the transaction to the database
                FuelTransaction dbTransaction = new FuelTransaction()
                {
                    TransactionTime = DateTime.Now,
                    TransactionType = transaction.TransactionType,
                    AirportId = transaction.AirportId,
                    Quantity = transaction.Quantity,
                    TransactionIdparent = transaction.TransactionIdparent,
                    AircraftId = transaction.AircraftId,
                };
               

                context.FuelTransactions.Add(dbTransaction);
                airport.FuelAvailable = fuelAvailableAfterTransaction;

                await context.SaveChangesAsync();

                return Tuple.Create("Transaction successful", true);
           
        }


        public async Task<FuelTransactionModel> GetTransactionById(int id)
        {
            FuelTransactionModel fuelTransaction = new FuelTransactionModel();

            var  tempFuelTransaction = await context.FuelTransactions
            .Include(ft => ft.Aircraft) 
            .Include(ft => ft.Airport)  
            .FirstOrDefaultAsync(ft => ft.TransactionId == id);

            if (tempFuelTransaction != null)
            {
                    fuelTransaction = new FuelTransactionModel
                {
                    TransactionId = tempFuelTransaction.TransactionId,
                    TransactionTime = tempFuelTransaction.TransactionTime,
                    TransactionType = tempFuelTransaction.TransactionType,
                    Quantity = tempFuelTransaction.Quantity,
                    TransactionIdparent = tempFuelTransaction.TransactionIdparent,
                    AirportId = tempFuelTransaction.AirportId,
                    AircraftId = tempFuelTransaction.AircraftId,
                    AirportName = tempFuelTransaction.Airport?.AirportName,
                    AircraftName = tempFuelTransaction.Aircraft?.AircraftNumber
                };

            }            

            return fuelTransaction;
        }

        public async Task<bool> RemoveAllTransactions()
        {
            bool flag = false;
            
            var allTransactions = await context.FuelTransactions.ToListAsync();
                
            context.RemoveRange(allTransactions);

            await context.SaveChangesAsync();

            var airports = await context.Airports.ToListAsync();

            foreach (var airport in airports)
            {
                airport.FuelAvailable = 0;
            }
            await context.SaveChangesAsync();
            flag = true;
           
            return flag;
        }
        public async Task<bool> InsertUser(UserModel user)
        {
            bool flag = false;
            
            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };

            context.Users.Add(newUser);
            await context.SaveChangesAsync();
            flag  = true; 
            
            return flag;
        }
        public async Task<int> CheckIfUserExists(string userEmail, string userPassword)
        {
            int userId = -1;
            
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            // Check if user exists and password matches (case-sensitive)
            if (user != null && user.Password == userPassword)
            {
                userId = user.UserId;
            }
            
            return userId;
        }

        public async Task<bool> CheckIfEmailAlreadyExists(string userEmail)
        {
            bool flag = false;
            
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user != null)
            {
                flag = true;
            }
            
            return flag;
        }

        public async Task<List<AirportTransactionInfo>> FuelConsumptionReport()
        {
            List<AirportTransactionInfo> airportTransactionsInfo = new List<AirportTransactionInfo>();
            
            var aircrafts = await context.Aircraft.ToListAsync(); // Fetch aircraft data

            airportTransactionsInfo = await context.Airports
                .Select(airport => new AirportTransactionInfo
                {
                    AirportName = airport.AirportName,
                    AirportFuelAvailable =airport.FuelAvailable??0, // Handle nullable FuelAvailable property
                    Transactions = airport.FuelTransactions
                        .Select(ft => new TransactionItem
                        {
                            TransactionTime = ft.TransactionTime,
                            TransactionType = ft.TransactionType,
                            Quantity = ft.Quantity,
                            AircraftName = ft.Aircraft.AircraftNumber
                        }).ToList()
                })
                .ToListAsync();
           
            return airportTransactionsInfo;
        }

    }
}
