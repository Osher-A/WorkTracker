using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkTracker.Data.Model;

namespace WorkTracker.Data.DAL
{
    public class ClientRepository(WorkContext db) : IClientRepository
    {
        private readonly WorkContext _db = db;
        public async Task AddClient(Client client)
        {
            await _db.AddAsync(client);
            await _db.SaveChangesAsync();

            _db.Entry(client).State = EntityState.Detached;
        }

        public async Task DeleteClient(int id)
        {
            var clientToDelete = _db.Clients.SingleOrDefault(x => x.Id == id);
            if (clientToDelete is not null)
            {
                _db.Remove(clientToDelete);
                await _db.SaveChangesAsync();
            }

        }

        public async Task<Client?> GetClient(int id)
        {
            return await _db.Clients
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<Client>> GetClients()
        {
            return await _db.Clients
                .ToListAsync();
        }

        public async Task UpdateClient(Client client)
        {
           _db.Update(client);
            await _db.SaveChangesAsync();
        }
    }
}
