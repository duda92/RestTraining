using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RestTraining.Api.Tests.Helpers
{
    [TestClass]
    public class AssemblyInit
    {

        [AssemblyInitialize]
        public static void Init(TestContext testContext)
        {
            Database.SetInitializer(new DbInitializer());
            Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
        }
    }

}
