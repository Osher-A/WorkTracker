using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkTracker.BusinessLogic;
using WorkTracker.DTO;

namespace WorkTracker.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController(IWorkBusinessLogic workerService) : ControllerBase
    {
        private readonly IWorkBusinessLogic _workerBusinessLogic = workerService;

        [HttpGet(Name = "GetWorks")]
        public async Task<ActionResult<IEnumerable<WorkDTO>>> Get([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var workers = await _workerBusinessLogic.GetWorks(fromDate, toDate);

            return Ok(workers);
        }

        [HttpGet("{id}", Name = "GetWorkById")]
        public async Task<ActionResult<WorkDTO>> Get(int id)
        {
            var work = await _workerBusinessLogic.GetWork(id);
            if (work == null)
            {
                return NotFound();
            }
            return Ok(work);
        }


        [HttpPost(Name = "CreateWork")]
        public async Task<ActionResult<WorkDTO>> Post([FromBody] WorkDTO dto)
        {
            await _workerBusinessLogic.AddWork(dto);
            return Created();
        }

        [HttpPut(Name = "UpdateWork")]
        public async Task<ActionResult<WorkDTO>> Put([FromBody] WorkDTO worker)
        {
            var work = await _workerBusinessLogic.GetWork(worker.Id);
            if (work == null)
            {
                return NotFound();
            }

            await _workerBusinessLogic.UpdateWork(worker);

            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteWork")]
        public async Task<ActionResult> Delete(int id)
        {
            var work = await _workerBusinessLogic.GetWork(id);
            if (work == null)
            {
                return NotFound();
            }
            await _workerBusinessLogic.DeleteWork(work.Id);
            return NoContent();
        }
    }
}
