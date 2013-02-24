using System.Data.Entity;
using System.Linq;
using RestTraining.Domain;

namespace RestTraining.Api.Models
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

        public DbSet<RestTraining.Domain.Client> Clients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<HotelNumber>().HasMany(x => x.WindowViews).WithOptional().WillCascadeOnDelete();
            //modelBuilder.Entity<HotelNumber>().HasMany(x => x.IncludeItems).WithOptional().WillCascadeOnDelete();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<BoundedReservationsHotel> BoundedReservationsHotels { get; set; }

        public DbSet<FreeReservationsHotel> FreeReservationsHotels { get; set; }

        public DbSet<HotelNumber> HotelNumbers { get; set; }

        public DbSet<WindowView> WindowViews { get; set; }

        public DbSet<IncludeItem> IncludeItems { get; set; }

        public DbSet<BoundedPeriod> BoundedPeriods { get; set; }

        public DbSet<Department> Departments { get; set; }

        public void PreInsertOrUpdateHotel(Hotel hotel)
        {
            foreach (var hotelNumber in hotel.HotelNumbers)
            {
                PreInsertOrUpdateHotelNumber(hotelNumber);
            }
        }

        public void PreInsertOrUpdateHotelNumber(HotelNumber hotelNumber)
        {
            for (var i = 0; i < hotelNumber.WindowViews.Count; i++)
            {
                var type = hotelNumber.WindowViews[i].Type;
                var existedWindowView = WindowViews.SingleOrDefault(x => x.Type == type);
                hotelNumber.WindowViews[i] = existedWindowView;
            }
        }


    }

}