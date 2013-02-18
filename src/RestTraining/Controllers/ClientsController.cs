using System.Collections.Generic;
using System.Web.Http;
using RestTraining.Domain;
using RestTraining.Api.Models;

namespace RestTraining.Api.Controllers
{   
    public class ClientsController : ApiController
    {
		private readonly IClientRepository _clientRepository;

        public ClientsController(IClientRepository clientRepository)
        {
			_clientRepository = clientRepository;
        }

        // GET api/Clients
        public List<Client> Get()
        {
            return _clientRepository.All;
        }

        // GET api/Clients/5
        public Client Get(int id)
        {
            return _clientRepository.Find(id);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            _clientRepository.Delete(id);
            _clientRepository.Save();
        }
        
        // POST api/values
        public void Post(Client client)
        {
            //Add
            _clientRepository.InsertOrUpdate(client);
            _clientRepository.Save();
        }

        // PUT api/Clients/5
        public void Put(Client client)
        {
            //Update
            _clientRepository.InsertOrUpdate(client);
            _clientRepository.Save();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _clientRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

