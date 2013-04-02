using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTraining.Api.Domain;

namespace RestTraining.Api.Tests.Helpers
{
    [TestClass]
    public class AssemblyInit
    {
        [AssemblyInitialize]
        public static void Init(TestContext testContext)
        {
            Database.DefaultConnectionFactory = new SqlConnectionFactory("System.Data.SqlClient");
            Database.SetInitializer(new TestDbInitializer());
        }
    }

    public class TestDbInitializer : DropCreateDatabaseIfModelChanges<RestTrainingApiContext>
    {
        protected override void Seed(RestTrainingApiContext context)
        {
            context.Database.ExecuteSqlCommand(@"Create PROCEDURE ChangeHotelType 
                                                  @Id int, 
                                                  @Discriminator nvarchar(128) 
                                                  AS BEGIN 
                                                       update Hotels  
                                                       set Discriminator = @Discriminator  
                                                       where Id = @Id 
                                                  END");
        }
    }

}
