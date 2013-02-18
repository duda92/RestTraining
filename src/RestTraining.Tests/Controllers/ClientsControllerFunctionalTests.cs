using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestTraining.Api.Tests.Utils;
using RestTraining.Domain;
using System.Linq;

namespace RestTraining.Api.Tests.Controllers
{
    [TestClass]
    public class ClientsControllerFunctionalTests
    {
        private const string Resource = "/api/Clients/";
        private const string BaseUrl = "http://localhost:9075";

        [TestMethod]
        public void Get()
        {
            var clientObj = GetRandomClient();
            TestPost(clientObj);
            var all = TestGet();
            Assert.IsTrue(all.Any(x => x.Name == clientObj.Name && x.PhoneNumber == clientObj.PhoneNumber));
        }

        [TestMethod]
        public void Get_Id()
        {
            var clientObj = GetRandomClient();
            TestPost(clientObj);

            List<Client> all = TestGet();
            foreach (var client1 in all)
            {
                var result = TestGet(client1.Id);
                Assert.AreEqual(client1.Name, result.Name);
                Assert.AreEqual(client1.Id, result.Id);
                Assert.AreEqual(client1.PhoneNumber, result.PhoneNumber);
            }
        }

        [TestMethod]
        public void Post()
        {
            var clientObj = GetRandomClient();
            TestPost(clientObj);
            var all = TestGet();
            Assert.IsTrue(all.Any(x => x.Name == clientObj.Name && x.PhoneNumber == clientObj.PhoneNumber));
        }

        [TestMethod]
        public void Put()
        {
            TestPost(GetRandomClient());
            List<Client> all = TestGet();
            
            var clientObj = all.FirstOrDefault();
            clientObj.Name = RandomStringUtils.RandomString(10);
            var id = clientObj.Id;

            TestPut(clientObj);
            var test = TestGet(clientObj.Id);
            Assert.AreEqual(clientObj.Name, test.Name);
            Assert.AreEqual(clientObj.PhoneNumber, test.PhoneNumber);
            Assert.AreEqual(clientObj.Id, test.Id);
        }

        private void TestPost(Client clientObj)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(Resource, Method.POST);

            request.AddObject(clientObj);
            client.Execute<Client>(request);
        }

        private List<Client> TestGet()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(Resource, Method.GET);

            var response = client.Execute<List<Client>>(request);
            return response.Data;
        }

        private Client TestGet(int id)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(Resource + id.ToString(), Method.GET);
            IRestResponse<Client> response = client.Execute<Client>(request);
            return response.Data;
        }

        private void TestPut(Client clientObj)
        {
            var client = new RestClient(BaseUrl);
            var request2 = new RestRequest(Resource, Method.PUT);
            request2.AddObject(clientObj);
            client.Execute<Client>(request2);
        }

        private Client GetRandomClient()
        {
            return new Client { Name = RandomStringUtils.RandomString(10), PhoneNumber = RandomStringUtils.RandomString(10) };
        }
    }
}
