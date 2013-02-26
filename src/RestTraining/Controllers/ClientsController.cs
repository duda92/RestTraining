using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestTraining.Api.DTO;
using RestTraining.Api.Domain.Entities;
using RestTraining.Api.Domain.Repositories;

namespace RestTraining.Api.Controllers
{   
    public class ClientsController : ApiController
    {
		private readonly IClientRepository _clientRepository;

        public ClientsController(IClientRepository clientRepository)
        {
			_clientRepository = clientRepository;
        }

        public List<ClientDTO> Get()
        {
            var clients = _clientRepository.All.ToList();
            return clients.Select(x => x.ToDTO()).ToList();
        }

        public ClientDTO Get(int id)
        {
            var client = _clientRepository.Find(id);
            if (client == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return client.ToDTO();
        }

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
        
        public HttpResponseMessage Post(ClientDTO clientDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            var client = clientDTO.ToEntity();
            _clientRepository.InsertOrUpdate(client);
            _clientRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.Created, client.ToDTO());
            string uri = Url.Route(null, new { id = client.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        public HttpResponseMessage Put(ClientDTO clientDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            var client = clientDTO.ToEntity();
            _clientRepository.InsertOrUpdate(client);
            _clientRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.OK, client.ToDTO());
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

