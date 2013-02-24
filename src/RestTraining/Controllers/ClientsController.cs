using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
            var client = _clientRepository.Find(id);
            if (client == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            } 
            return _clientRepository.Find(id);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            var client = _clientRepository.Find(id);
            if (client == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            } 
            _clientRepository.Delete(id);
            _clientRepository.Save();
        }
        
        // POST api/values
        //public HttpResponseMessage Post([ModelBinder(typeof(CustomPersonModelBinderProvider))]Client client)
        public HttpResponseMessage Post(Client client)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            _clientRepository.InsertOrUpdate(client);
            _clientRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.Created, client);
            string uri = Url.Route(null, new { id = client.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        // PUT api/Clients/5
        public HttpResponseMessage Put(Client client)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            _clientRepository.InsertOrUpdate(client);
            _clientRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.OK, client);
            string uri = Url.Route(null, new { id = client.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
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

