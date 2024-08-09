using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkTracker.BusinessLogic;
using WorkTracker.DTO;

namespace WorkTracker.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController(IClientBusinessLogic clientService) : ControllerBase
    {
        private readonly IClientBusinessLogic _clientBusinessLogic = clientService;

        [HttpGet(Name = "GetClients")]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> Get()
        {
            var clients = await _clientBusinessLogic.GetClients();

            return Ok(clients);
        }

        [HttpGet("{id}", Name = "GetClientById")]
        public async Task<ActionResult<ClientDTO>> Get(int id)
        {
            var client = await _clientBusinessLogic.GetClient(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        [HttpPost(Name = "CreateClient")]
        public async Task<ActionResult<ClientDTO>> Post([FromBody] ClientDTO dto)
        {
            await _clientBusinessLogic.AddClient(dto);
            return Created();
        }

        [HttpPut(Name = "UpdateClient")]
        public async Task<ActionResult<ClientDTO>> Put([FromBody] ClientDTO dto)
        {
            var client = await _clientBusinessLogic.GetClient(dto.Id);
            if (client == null)
            {
                return NotFound();
            }

            await _clientBusinessLogic.UpdateClient(dto);

            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteClient")]
        public async Task<ActionResult> Delete(int id)
        {
            await _clientBusinessLogic.DeleteClient(id);
            return Ok();
        }
    }
}
