using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTraining.Api.Controllers;
using RestTraining.Api.Domain.Entities;
using RestTraining.Api.Domain.Repositories;
using Rhino.Mocks;

namespace RestTraining.Api.Tests.Controllers
{
    [TestClass]
    public class ClientsControllerTests
    {
        private IClientRepository _mockClientRepositry;
        private Client _mockClient;
        private List<Client> _mockAll;

        [TestInitialize()]
        public void SetUp()
        {
            _mockClientRepositry = MockRepository.GenerateMock<IClientRepository>();
            _mockClient = new Client();
            _mockAll = new List<Client> {_mockClient};
            _mockClientRepositry.Stub(x => x.All).Return(_mockAll);
            
        }
        
        [TestMethod]
        public void Get()
        {
            var controller = new ClientsController(_mockClientRepositry);

            controller.Get();

            _mockClientRepositry.AssertWasCalled(x => x.All);
        }

    }
}
