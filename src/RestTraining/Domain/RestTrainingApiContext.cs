using System.Data.Entity;
using RestTraining.Api.Domain.Entities;
using RestTraining.Domain;

namespace RestTraining.Api.Domain
{
    public class RestTrainingApiContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<RestTraining.Api.Models.RestTrainingApiContext>());

        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>().HasMany(j => j.HotelNumbers).WithRequired().HasForeignKey(x => x.HotelId);
            modelBuilder.Entity<HotelNumber>().HasMany(j => j.IncludeItems).WithRequired();
            //modelBuilder.Entity<BoundedBooking>().Has(j => j.BoundedPeriodId).;
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

    }

}