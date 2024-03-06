using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tucson.Data.Config;
using Tucson.Data.Interfaces;
using Tucson.Models.Entities;

namespace Tucson.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly TucsonDbContext dbContext;
        public ClientRepository(TucsonDbContext db)
        {
            dbContext = db;
        }
        public Client GetClientById(int clientId)
        {
            var client = dbContext.Set<Client>().Find(clientId);
            return client;
        }

    }
}
