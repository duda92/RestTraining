using System.Data.Entity;
using System.Data.SqlClient;
using RestTraining.Api.Domain.Entities;
using RestTraining.Api.Domain.Repositories;
using RestTraining.Api.Domain.Services;
using System.Collections.Generic;
using RestTraining.Domain;

namespace RestTraining.Api.Domain
{
    public class RestTrainingApiContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        public static List<string> OnSeedSqlCommands = new List<string> 
        { 
            @"Create PROCEDURE ChangeHotelType 
                                                  @Id int, 
                                                  @Discriminator nvarchar(128) 
                                                  AS BEGIN 
                                                       update Hotels  
                                                       set Discriminator = @Discriminator  
                                                       where Id = @Id 
                                                  END"
        };

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

        public DbSet<BoundedPeriod> BoundedPeriods { get; set; }

        public DbSet<BoundedBooking> BoundedBookings { get; set; }

        public DbSet<FreeBooking> FreeBookings { get; set; }

        public void SetHotelType(Hotel hotel)
        {
            var discriminator = GetDiscriminator(hotel);
            this.Database.ExecuteSqlCommand("exec ChangeHotelType @Id, @Discriminator", new SqlParameter("@Id", hotel.Id), new SqlParameter("@Discriminator", discriminator));    
        }

        private string GetDiscriminator(Hotel hotel)
        {
            return (hotel is FreeReservationsHotel) ? "FreeReservationsHotel" : "BoundedReservationsHotel";
        }
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
                HotelNumbers = new List<HotelNumber>
                    {
                        new HotelNumber
                            {
                                HotelNumberType = HotelNumberType.Double,
                                IncludeItems = new List<IncludedItem>
                                    {
                                        new IncludedItem { Count = 1, IncludeItemType = IncludeItemType.AirConditioner },
                                        new IncludedItem { Count = 1, IncludeItemType = IncludeItemType.AirConditioner }
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
                HotelNumbers = new List<HotelNumber>
                    {
                        new HotelNumber
                            {
                                HotelNumberType = HotelNumberType.Double,
                                IncludeItems = new List<IncludedItem>
                                    {
                                        new IncludedItem { Count = 1, IncludeItemType = IncludeItemType.AirConditioner },
                                        new IncludedItem { Count = 1, IncludeItemType = IncludeItemType.AirConditioner }
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

            context.SaveChanges();
        }

    }
}