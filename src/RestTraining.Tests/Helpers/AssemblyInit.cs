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
            //Database.DefaultConnectionFactory.CreateConnection("RestTrainingApiContext");
            
            Database.SetInitializer(new DbInitializer());
        }
    }

}
