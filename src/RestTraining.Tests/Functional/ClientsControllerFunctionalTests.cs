using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTraining.Api.Domain.Entities;
using RestTraining.Api.Tests.Utils;
using System.Linq;

namespace RestTraining.Api.Tests.Functional
{
    [TestClass]
    public class ClientsControllerTests
    {
        [TestMethod]
        public void Get()
        {
            var clientObj = TestHelpers.ClientsApiHelper.CreateRandomClientDTO();
            TestHelpers.ClientsApiHelper.TestPost(clientObj);
            var all = TestHelpers.ClientsApiHelper.TestGet();
            Assert.IsTrue(all.Any(x => x.Name == clientObj.Name && x.PhoneNumber == clientObj.PhoneNumber));
        }

        [TestMethod]
        public void Get_Id()
        {
            var clientObj = TestHelpers.ClientsApiHelper.CreateRandomClientDTO();
            TestHelpers.ClientsApiHelper.TestPost(clientObj);

            var all = TestHelpers.ClientsApiHelper.TestGet();
            foreach (var client1 in all)
            {
                var result = TestHelpers.ClientsApiHelper.TestGet(client1.Id);
                Assert.AreEqual(client1.Name, result.Name);
                Assert.AreEqual(client1.Id, result.Id);
                Assert.AreEqual(client1.PhoneNumber, result.PhoneNumber);
            }
        }

        [TestMethod]
        public void Post()
        {
            var clientObj = TestHelpers.ClientsApiHelper.CreateRandomClientDTO();
            TestHelpers.ClientsApiHelper.TestPost(clientObj);
            var all = TestHelpers.ClientsApiHelper.TestGet();
            Assert.IsTrue(all.Any(x => x.Name == clientObj.Name && x.PhoneNumber == clientObj.PhoneNumber));
        }

        [TestMethod]
        public void Put()
        {
            TestHelpers.ClientsApiHelper.TestPost(TestHelpers.ClientsApiHelper.CreateRandomClientDTO());
            var all = TestHelpers.ClientsApiHelper.TestGet();
            
            var clientObj = all.FirstOrDefault();
            clientObj.Name = RandomUtils.RandomString(10);
            var id = clientObj.Id;

            TestHelpers.ClientsApiHelper.TestPut(clientObj);
            var test = TestHelpers.ClientsApiHelper.TestGet(clientObj.Id);
            Assert.AreEqual(clientObj.Name, test.Name);
            Assert.AreEqual(clientObj.PhoneNumber, test.PhoneNumber);
            Assert.AreEqual(clientObj.Id, test.Id);
        }


    }
}
