﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTraining.Api.Controllers;
using RestTraining.Api.Models;
using RestTraining.Domain;
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

            var result = controller.Get();

            _mockClientRepositry.AssertWasCalled(x => x.All);
            CollectionAssert.AreEquivalent(_mockAll.ToList(), result.ToList());
        }

    }
}
