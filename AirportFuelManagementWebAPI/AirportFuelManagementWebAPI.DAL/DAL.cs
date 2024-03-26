using AirportFuelManagementWebAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;


using AirportFuelManagementWebAPI.Models;

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
       
        public async Task<List<AirportModel>> GetAllAirports()
        {
            List<AirportModel> airports = new List<AirportModel>();

            try
            {
                airports = await context.Airports
                                         .Select(s => new AirportModel
                                         {
                                             AirportId = s.AirportId,
                                             AirportName = s.AirportName,
                                             FuelAvailable = s.FuelAvailable,
                                             FuelCapacity = s.FuelCapacity
                                         })
                                         .ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogException(ex);                
            }

            return airports;
        }

        public async Task<AirportModel> GetAirportById(int id)
        {
            AirportModel airport = new AirportModel();

            try
            {
                var tempAirport = await context.Airports.FirstOrDefaultAsync(a => a.AirportId == id);

                if (tempAirport != null)
                {
                    airport = new AirportModel
                    {
                        AirportId = tempAirport.AirportId,
                        AirportName = tempAirport.AirportName,
                        FuelAvailable = tempAirport.FuelAvailable,
                        FuelCapacity = tempAirport.FuelCapacity
                    };
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);           
            }

            return airport;
        }

        public async Task<bool> InsertAirport(AirportModel airport)
        {
            bool flag = false;
            try
            {
                var tempAirport = new Airport
                {
                    AirportName = airport.AirportName,
                    FuelAvailable = airport.FuelAvailable,
                    FuelCapacity = airport.FuelCapacity
                };
                await context.Airports.AddAsync(tempAirport);
                await context.SaveChangesAsync();
                flag = true;
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }
            return flag;
        }
        public async Task<bool> UpdateAirport(AirportModel airport)
        {
            bool flag = false;
            try
            {
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
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }
            return flag;
        }

      public async Task<List<AircraftModel>> GetAllAircrafts()
        {
             List<AircraftModel> aircrafts = new List<AircraftModel>();

            try
            {
                var airports = await context.Airports.ToListAsync();

                aircrafts = await context.Aircraft
                    .Select(s => new AircraftModel
                    {
                        AircraftId = s.AircraftId,
                        AircraftNumber = s.AircraftNumber,
                        AirLine = s.AirLine,
                        Source = s.Source,
                        Destination = s.Destination
                    })
                    .ToListAsync();

                
                foreach (var aircraft in aircrafts)
                {
                    aircraft.SourceName = airports.FirstOrDefault(a => a.AirportId == Convert.ToInt32(aircraft.Source))?.AirportName;
                    aircraft.DestinationName = airports.FirstOrDefault(a => a.AirportId == Convert.ToInt32(aircraft.Destination))?.AirportName;
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }

            return aircrafts;
        }


        public async Task<AircraftModel> GetAircraftById(int id)
        {
            AircraftModel aircraft = new AircraftModel();

            try
            {
                var tempAircraft = await context.Aircraft.FirstOrDefaultAsync(a => a.AircraftId == id);

                if (tempAircraft != null)
                {
                    aircraft = new AircraftModel
                    {
                        AircraftId = tempAircraft.AircraftId,
                        AircraftNumber = tempAircraft.AircraftNumber,
                        AirLine = tempAircraft.AirLine,
                        Source = tempAircraft.Source,
                        Destination = tempAircraft.Destination
                    };
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }

            return aircraft;
        }

        public async Task<bool> InsertAircraft(AircraftModel aircraft)
        {
            bool flag = false;
            try
            {
                var tempAircraft = new Aircraft
                {
                    AircraftNumber = aircraft.AircraftNumber,
                    AirLine = aircraft.AirLine,
                    Source = aircraft.Source,
                    Destination = aircraft.Destination
                };
                await context.Aircraft.AddAsync(tempAircraft);
                await context.SaveChangesAsync();
                flag = true;
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }
            return flag;
        }

        public async Task<bool> UpdateAircraft(AircraftModel aircraft)
        {
            bool flag = false;
            try
            {
                var existingAircraft = await context.Aircraft.FirstOrDefaultAsync(a => a.AircraftId == aircraft.AircraftId);

                if (existingAircraft != null)
                {
                    existingAircraft.AircraftNumber = aircraft.AircraftNumber;
                    existingAircraft.AirLine = aircraft.AirLine;
                    existingAircraft.Source = aircraft.Source;
                    existingAircraft.Destination = aircraft.Destination;

                    context.Aircraft.Update(existingAircraft);
                    await context.SaveChangesAsync();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }
            return flag;
        }
        public async Task<List<FuelTransactionModel>> GetAllTransactions()
        {
            List<FuelTransactionModel> transactions = new List<FuelTransactionModel>();

            try
            {
                List<FuelTransaction> fuelTransactions = await context.FuelTransactions.ToListAsync();

                List<Aircraft> aircrafts = await context.Aircraft.ToListAsync();

                List<Airport> airports = await context.Airports.ToListAsync();

                transactions = fuelTransactions
                                .Select(t => new FuelTransactionModel
                                {
                                    TransactionId = t.TransactionId,
                                    AircraftId = t.AircraftId,
                                    AirportId = t.AirportId,
                                    Quantity = t.Quantity,
                                    TransactionType = t.TransactionType,
                                    TransactionIdparent = t.TransactionIdparent,
                                    TransactionTime = t.TransactionTime,
                                    AircraftName = aircrafts.FirstOrDefault(a => a.AircraftId == t.AircraftId).AircraftNumber,
                                    AirportName = airports.FirstOrDefault(ap => ap.AirportId == t.AirportId).AirportName
                                })
                                .ToList();
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }

            return transactions;
        }


        public async Task<bool> InsertTransaction(FuelTransactionModel transaction)
        {
            try
            {
                var airport = await context.Airports.FirstOrDefaultAsync(a => a.AirportId == transaction.AirportId);
                if (airport == null)
                {
                    return false;
                }

                decimal totalFuelAvailable = (await context.FuelTransactions
                    .Where(ft => ft.AirportId == transaction.AirportId && ft.TransactionType == 1)
                    .SumAsync(ft => (decimal?)ft.Quantity) ?? 0)
                    - (await context.FuelTransactions
                        .Where(ft => ft.AirportId == transaction.AirportId && ft.TransactionType == 2)
                        .SumAsync(ft => (decimal?)ft.Quantity) ?? 0)
                    + transaction.Quantity;
                FuelTransaction dbTransaction = new FuelTransaction()
                {
                    TransactionTime = DateTime.Now,
                    TransactionType = transaction.TransactionType,
                    AirportId = transaction.AirportId,
                    Quantity = transaction.Quantity,
                    TransactionIdparent = transaction.TransactionIdparent
                };
                if (transaction.AircraftId != 0)
                {
                    dbTransaction.AircraftId = transaction.AircraftId;

                }
                else
                {   // for IN transaction default aircraft 
                    dbTransaction.AircraftId = 1;
                }



                context.FuelTransactions.Add(dbTransaction);
                await context.SaveChangesAsync();

                var fuelAvailable = (await context.FuelTransactions
                    .Where(ft => ft.AirportId == transaction.AirportId && ft.TransactionType == 1)
                    .SumAsync(ft => (decimal?)ft.Quantity) ?? 0)
                    - (await context.FuelTransactions
                        .Where(ft => ft.AirportId == transaction.AirportId && ft.TransactionType == 2)
                        .SumAsync(ft => (decimal?)ft.Quantity) ?? 0);

                if (fuelAvailable < 0 || fuelAvailable > airport.FuelCapacity)
                {
                    context.FuelTransactions.Remove(dbTransaction);
                    await context.SaveChangesAsync();
                    return false;
                }

                airport.FuelAvailable = fuelAvailable;
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return false;
            }
        }

        public async Task<FuelTransactionModel> GetTransactionById(int id)
        {
            FuelTransactionModel fuelTransaction = new FuelTransactionModel();

            try
            {
                var tempFuelTransaction = await context.FuelTransactions.FirstOrDefaultAsync(ft => ft.TransactionId == id);
               
                if (tempFuelTransaction != null)
                {
                    // Map the properties from the database entity to the model
                    fuelTransaction = new FuelTransactionModel
                    {
                        TransactionId = tempFuelTransaction.TransactionId,
                        TransactionTime = tempFuelTransaction.TransactionTime,
                        TransactionType = tempFuelTransaction.TransactionType,
                        Quantity = tempFuelTransaction.Quantity,
                        TransactionIdparent = tempFuelTransaction.TransactionIdparent,
                        AirportId = tempFuelTransaction.AirportId,
                        AircraftId = tempFuelTransaction.AircraftId,
                        AirportName = (await context.Airports.FirstOrDefaultAsync(a => a.AirportId == tempFuelTransaction.AirportId))?.AirportName,
                        AircraftName = (await context.Aircraft.FirstOrDefaultAsync(ac => ac.AircraftId == tempFuelTransaction.AircraftId))?.AircraftNumber

                    };
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }

            return fuelTransaction;
        }

        public async Task<bool> RemoveAllTransactions()
        {
            bool flag = false;
            try
            {
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
            }
            catch (Exception ex)
            {
                logger.LogException(ex);

            }
            return flag;
        }
        public async Task<bool> InsertUser(UserModel user)
        {
            bool flag = false;
            try
            {
                var newUser = new User
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password
                };

                context.Users.Add(newUser);
                await context.SaveChangesAsync();
                flag  = true; 
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
               
            }
            return flag;
        }
        public async Task<int> CheckIfUserExists(string userEmail, string userPassword)
        {
            int userId = -1;
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

                // Check if user exists and password matches (case-sensitive)
                if (user != null && user.Password == userPassword)
                {
                    userId = user.UserId;
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }
            return userId;
        }

        public async Task<bool> CheckIfEmailAlreadyExists(string userEmail)
        {
            bool flag = false;
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

                if (user != null)
                {
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }
            return flag;
        }

        public async Task<List<AirportTransactionInfo>> FuelConsumptionReport()
        {
            List<AirportTransactionInfo> airportTransactionsInfo = new List<AirportTransactionInfo>();
            try
            {
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
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }
            return airportTransactionsInfo;
        }







    }
}
