using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using RestTraining.Api.Domain.Entities;

namespace RestTraining.Api.Domain.Repositories
{
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