using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using RestTraining.Domain;

namespace RestTraining.Api.Models
{ 
    public class ClientRepository : IClientRepository
    {
        readonly RestTrainingApiContext _context = new RestTrainingApiContext();

        public List<Client> All
        {
            get { return _context.Clients.ToList(); }
        }

        public IQueryable<Client> AllIncluding(params Expression<Func<Client, object>>[] includeProperties)
        {
            IQueryable<Client> query = _context.Clients;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Client Find(int id)
        {
            return _context.Clients.Find(id);
        }

        public void InsertOrUpdate(Client client)
        {
            if (client.Id == default(int)) {
                // New entity
                _context.Clients.Add(client);
            } else {
                // Existing entity
                _context.Entry(client).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var client = _context.Clients.Find(id);
            _context.Clients.Remove(client);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose() 
        {
            _context.Dispose();
        }
    }

    public interface IClientRepository : IDisposable
    {
        List<Client> All { get; }
        IQueryable<Client> AllIncluding(params Expression<Func<Client, object>>[] includeProperties);
        Client Find(int id);
        void InsertOrUpdate(Client client);
        void Delete(int id);
        void Save();
    }

        
}