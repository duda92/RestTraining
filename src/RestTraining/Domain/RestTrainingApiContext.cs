using System.Data.Entity;
using System.Data.SqlClient;
using RestTraining.Api.Domain.Entities;
using RestTraining.Api.Domain.Repositories;
using RestTraining.Api.Domain.Services;
using System.Collections.Generic;
using RestTraining.Domain;
using System;

namespace RestTraining.Api.Domain
{
    public class RestTrainingApiContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        public void UpdateBoundedBooking(BoundedBooking boundedBooking)
        {
            UpdateBoundedBookingStoredProcedure.Execute(this, boundedBooking);
        }

        public void InsertBoundedBooking(BoundedBooking boundedBooking)
        {
            InsertBoundedBookingStoredProcedure.Execute(this, boundedBooking);
        }

        public void SetHotelType(Hotel hotel)
        {
            SetHotelTypeStoredProcedure.Execute(this, hotel); 
        }

        public static List<string> OnSeedSqlCommands
        {
            get
            {
                return new List<string> { SetHotelTypeStoredProcedure.CreateSqlCommand, InsertBoundedBookingStoredProcedure.CreateSqlCommand, UpdateBoundedBookingStoredProcedure.CreateSqlCommand };
            }
        }

        public static readonly SetHotelTypeStoredProcedure SetHotelTypeStoredProcedure = new SetHotelTypeStoredProcedure();
        public static readonly UpdateBoundedBookingStoredProcedure UpdateBoundedBookingStoredProcedure = new UpdateBoundedBookingStoredProcedure();
        public static readonly InsertBoundedBookingStoredProcedure InsertBoundedBookingStoredProcedure = new InsertBoundedBookingStoredProcedure();
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>().HasMany(j => j.HotelNumbers).WithRequired().HasForeignKey(x => x.HotelId);
            modelBuilder.Entity<HotelNumber>().HasMany(j => j.IncludeItems).WithRequired();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<BoundedReservationsHotel> BoundedReservationsHotels { get; set; }

        public DbSet<FreeReservationsHotel> FreeReservationsHotels { get; set; }

        public DbSet<HotelNumber> HotelNumbers { get; set; }

        public DbSet<WindowView> WindowViews { get; set; }

        public DbSet<IncludedItem> IncludeItems { get; set; }

        public DbSet<HotelsAttraction> HotelsAttractions { get; set; }

        public DbSet<BoundedPeriod> BoundedPeriods { get; set; }

        public DbSet<BoundedBooking> BoundedBookings { get; set; }

        public DbSet<FreeBooking> FreeBookings { get; set; }

    }

    public class DbInitializer : DropCreateDatabaseAlways<RestTrainingApiContext>
    {
        protected override void Seed(RestTrainingApiContext context)
        {
            InitializingByTestValues(context);
            foreach (var sqlCommand in RestTrainingApiContext.OnSeedSqlCommands)
            {
                context.Database.ExecuteSqlCommand(sqlCommand);
            }
        }

        private void InitializingByTestValues(RestTrainingApiContext context)
        {
            context.WindowViews.Add(new WindowView { Type = WindowViewType.Pool });
            context.WindowViews.Add(new WindowView { Type = WindowViewType.Sea });
            context.WindowViews.Add(new WindowView { Type = WindowViewType.Trash });
            context.SaveChanges();

            IClientRepository repository = new ClientRepository();
            repository.InsertOrUpdate(new Client { Name = "Test", PhoneNumber = "test" });
            repository.Save();

            var boundedReservationsHotel = new BoundedReservationsHotel
            {
                Title = "Test",
                Address = "Test",
                Description = "This is description of hotel: Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.",

                HotelsAttractions = new List<HotelsAttraction>
                {
                    new HotelsAttraction
                    {
                        Count = 1, HotelsAttractionType = HotelsAttractionType.TennisCourt
                    },
                    new HotelsAttraction
                    {
                        Count = 2, HotelsAttractionType = HotelsAttractionType.WaterSlides
                    }
                },
                
                HotelNumbers = new List<HotelNumber>
                    {
                        new HotelNumber
                            {
                                HotelNumberType = HotelNumberType.Double,
                                IncludeItems = new List<IncludedItem>
                                    {
                                        new IncludedItem { Count = 1, IncludeItemType = IncludeItemType.AirConditioner },
                                        new IncludedItem { Count = 1, IncludeItemType = IncludeItemType.TvSet }
                                    },
                                    WindowViews = new List<WindowView>
                                        {
                                            new WindowView  { Type = WindowViewType.Pool },
                                            new WindowView  { Type = WindowViewType.Trash },
                                        }
                            },
                            new HotelNumber
                            {
                                HotelNumberType = HotelNumberType.Double,
                                IncludeItems = new List<IncludedItem>
                                    {
                                        new IncludedItem { Count = 1, IncludeItemType = IncludeItemType.Balcony },
                                        new IncludedItem { Count = 1, IncludeItemType = IncludeItemType.TvSet }
                                    },
                                    WindowViews = new List<WindowView>
                                        {
                                            new WindowView  { Type = WindowViewType.Pool },
                                            new WindowView  { Type = WindowViewType.Trash },
                                        }
                            }
                    }
            };

            var freeReservationsHotel = new FreeReservationsHotel
            {
                Title = "Test",
                Address = "Test",
                Description = "This is description of hotel: Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.",
                
                HotelsAttractions = new List<HotelsAttraction>
                {
                    new HotelsAttraction
                    {
                        Count = 1, HotelsAttractionType = HotelsAttractionType.SwimmingPool
                    },
                    new HotelsAttraction
                    {
                        Count = 2, HotelsAttractionType = HotelsAttractionType.TennisCourt
                    }
                },
                
                HotelNumbers = new List<HotelNumber>
                    {
                        new HotelNumber
                            {
                                HotelNumberType = HotelNumberType.Double,
                                IncludeItems = new List<IncludedItem>
                                    {
                                        new IncludedItem { Count = 1, IncludeItemType = IncludeItemType.Balcony },
                                        new IncludedItem { Count = 1, IncludeItemType = IncludeItemType.AirConditioner }
                                    },
                                    WindowViews = new List<WindowView>
                                        {
                                            new WindowView  { Type = WindowViewType.Pool },
                                            new WindowView  { Type = WindowViewType.Trash },
                                        }
                            },
                            new HotelNumber
                            {
                                HotelNumberType = HotelNumberType.Double,
                                IncludeItems = new List<IncludedItem>
                                    {
                                        new IncludedItem { Count = 1, IncludeItemType = IncludeItemType.Balcony },
                                        new IncludedItem { Count = 1, IncludeItemType = IncludeItemType.TvSet }
                                    },
                                    WindowViews = new List<WindowView>
                                        {
                                            new WindowView  { Type = WindowViewType.Pool },
                                            new WindowView  { Type = WindowViewType.Trash },
                                        }
                            }
                    }
            };
            var boundedReservationsHotelRepository = new BoundedReservationsHotelRepository(new HotelNumbersUpdateService());
            boundedReservationsHotelRepository.InsertOrUpdate(boundedReservationsHotel);
            boundedReservationsHotelRepository.Save();

            var freeReservationsHotelRepository = new FreeReservationsHotelRepository(new HotelNumbersUpdateService());
            freeReservationsHotelRepository.InsertOrUpdate(freeReservationsHotel);
            freeReservationsHotelRepository.Save();

            var boundedPeriod1 = new BoundedPeriod
            {
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7),
                BoundedReservationsHotelId = boundedReservationsHotel.Id
            };
            var boundedPeriod2 = new BoundedPeriod
            {
                BeginDate = DateTime.Now.AddDays(8),
                EndDate = DateTime.Now.AddDays(15),
                BoundedReservationsHotelId = boundedReservationsHotel.Id
            };
            var boundedPeriod3 = new BoundedPeriod
            {
                BeginDate = DateTime.Now.AddDays(16),
                EndDate = DateTime.Now.AddDays(23),
                BoundedReservationsHotelId = boundedReservationsHotel.Id
            };

            var boundedPeriodsRepository = new BoundedPeriodRepository(new BookingDatesService());
            boundedPeriodsRepository.InsertOrUpdate(boundedPeriod1);
            boundedPeriodsRepository.InsertOrUpdate(boundedPeriod2);
            boundedPeriodsRepository.InsertOrUpdate(boundedPeriod3);
            boundedPeriodsRepository.Save();

            context.SaveChanges();
        }
    }

    public abstract class StoredProcedure
    {
        public abstract string CreateSqlCommand { get; }
    }
  
    public class SetHotelTypeStoredProcedure : StoredProcedure
    {
        public override string CreateSqlCommand 
        { 
            get
            {
                return @"Create PROCEDURE ChangeHotelType 
                                                  @Id int, 
                                                  @Discriminator nvarchar(128) 
                                                  AS BEGIN 
                                                       update Hotels  
                                                       set Discriminator = @Discriminator  
                                                       where Id = @Id 
                                                  END";
            } 
        }

        public void Execute(RestTrainingApiContext context, Hotel hotel)
        {
            var discriminator = GetDiscriminator(hotel);
            context.Database.ExecuteSqlCommand("exec ChangeHotelType @Id, @Discriminator", new SqlParameter("@Id", hotel.Id), new SqlParameter("@Discriminator", discriminator));    
        }

        private string GetDiscriminator(Hotel hotel)
        {
            return (hotel is FreeReservationsHotel) ? "FreeReservationsHotel" : "BoundedReservationsHotel";
        }
    }

    public class UpdateBoundedBookingStoredProcedure : StoredProcedure
    {
        public override string CreateSqlCommand
        {
            get
            {
                return @"Create PROCEDURE UpdateBoundedBooking
	                @Id int,
	                @HotelId int,
	                @HotelNumberId int,
	                @ClientId int, 
	                @Name nvarchar(max),
	                @PhoneNumber nvarchar(max),
	                @BoundedPeriodId int
                AS
                BEGIN
	                update [BoundedBookings]
	                set HotelId = @HotelId, HotelNumberId = @HotelNumberId, ClientId = @ClientId, BoundedPeriod_Id = @BoundedPeriodId
	                where Id = @Id 

	                update [Clients]
	                set PhoneNumber = @PhoneNumber, Name = @Name
	                where Id = @ClientId
                END";
            }
        }

        public void Execute(RestTrainingApiContext context, BoundedBooking boundedBooking)
        {
            context.Database.ExecuteSqlCommand("exec UpdateBoundedBooking @Id, @HotelId, @HotelNumberId, @ClientId,  @Name, @PhoneNumber, @BoundedPeriodId",
                new SqlParameter("@Id", boundedBooking.Id), new SqlParameter("@HotelId", boundedBooking.HotelId),
                new SqlParameter("@HotelNumberId", boundedBooking.HotelNumberId),
                new SqlParameter("@ClientId", boundedBooking.ClientId),
                new SqlParameter("@Name", boundedBooking.Client.Name),
                new SqlParameter("@PhoneNumber", boundedBooking.Client.PhoneNumber),
                new SqlParameter("@BoundedPeriodId", boundedBooking.BoundedPeriod.Id)
                );
        }
    }

    public class InsertBoundedBookingStoredProcedure : StoredProcedure
    {
        public override string CreateSqlCommand
        {
            get
            {
                return @"Create PROCEDURE [dbo].[InsertBoundedBooking]
	                @HotelId int,
	                @HotelNumberId int,
	                @Name nvarchar(max),
	                @PhoneNumber nvarchar(max),
	                @BoundedPeriodId int
                AS
                BEGIN
	                insert [Clients]
	                values (@Name, @PhoneNumber)
                    
                    insert [BoundedBookings]
                    values (@HotelId, @HotelNumberId, @@IDENTITY, @BoundedPeriodId)
	                
                END";
            }
        }

        public void Execute(RestTrainingApiContext context, BoundedBooking boundedBooking)
        {
            context.Database.ExecuteSqlCommand("exec InsertBoundedBooking @HotelId, @HotelNumberId, @Name, @PhoneNumber, @BoundedPeriodId",
                new SqlParameter("@HotelId", boundedBooking.HotelId),
                new SqlParameter("@HotelNumberId", boundedBooking.HotelNumberId),
                new SqlParameter("@Name", boundedBooking.Client.Name),
                new SqlParameter("@PhoneNumber", boundedBooking.Client.PhoneNumber),
                new SqlParameter("@BoundedPeriodId", boundedBooking.BoundedPeriod.Id)
                );
        }
    }

}