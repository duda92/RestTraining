using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTraining.Api.Domain;
using RestTraining.Api.Domain.Entities;
using RestTraining.Domain;

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

    public class TestDbInitializer : DropCreateDatabaseAlways<RestTrainingApiContext>
    {
        protected override void Seed(RestTrainingApiContext context)
        {
            context.WindowViews.Add(new WindowView { Type = WindowViewType.Pool });
            context.WindowViews.Add(new WindowView { Type = WindowViewType.Sea });
            context.WindowViews.Add(new WindowView { Type = WindowViewType.Trash });
            context.SaveChanges();

            foreach (var sqlCommand in RestTrainingApiContext.OnSeedSqlCommands)
            {
                context.Database.ExecuteSqlCommand(sqlCommand);
            }
        }
    }

}
