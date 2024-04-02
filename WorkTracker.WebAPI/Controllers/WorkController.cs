using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkTracker.BusinessLogic;
using WorkTracker.DTO;

namespace WorkTracker.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        private readonly IWorkBusinessLogic _workerBusinessLogic;
        public WorkController(IWorkBusinessLogic workerService)
        {
            _workerBusinessLogic = workerService;
        }

        [HttpGet(Name = "GetWorks")]
        public async Task<ActionResult<IEnumerable<WorkDTO>>> Get([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var workers = await _workerBusinessLogic.GetWorks(fromDate, toDate);

            if (workers == null || workers.Count < 1)
            {
                return NotFound();
            }

            return Ok(workers);
        }


        [HttpPost(Name = "CreateWork")]
        public async Task<ActionResult<WorkDTO>> Post([FromBody] WorkDTO dto)
        {
            await _workerBusinessLogic.AddWork(dto);
            return Created();
        }

        [HttpPut("{id}", Name = "UpdateWork")]
        public async Task<ActionResult<WorkDTO>> Put(int id, [FromBody] WorkDTO worker)
        {
            if (id != worker.Id)
            {
                return BadRequest();
            }

            var work = await _workerBusinessLogic.GetWork(id);
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
