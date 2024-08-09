using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WorkTracker.Data.DAL;
using WorkTracker.Data.Model;
using WorkTracker.DTO;

namespace WorkTracker.BusinessLogic
{
    public class ClientBusinessLogic(IMapper mapper, IClientRepository clientRepository) : IClientBusinessLogic
    {
        public async Task AddClient(ClientDTO client) => await clientRepository.AddClient(MapToModel(client));

        public async Task<ClientDTO> GetClient(int id)
        {
            var work = await clientRepository.GetClient(id);
            return mapper.Map<ClientDTO>(work);
        }

        public async Task UpdateClient(ClientDTO client) => await clientRepository.UpdateClient(MapToModel(client));

        public async Task DeleteClient(int id) => await clientRepository.DeleteClient(id);

        public async Task<ICollection<ClientDTO>> GetClients()
        {
            var modelClients = await clientRepository.GetClients();
            var dtoClients = mapper.Map<ICollection<ClientDTO>>(modelClients);
            return dtoClients;
        }

        private Client MapToModel(ClientDTO client)
        {
            return mapper.Map<Client>(client);
        }

    }
}
