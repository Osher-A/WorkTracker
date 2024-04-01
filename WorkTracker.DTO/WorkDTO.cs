using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTracker.DTO
{
    public sealed class WorkDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<WorkDetailsDTO> WorkDetails { get; set; }
    }
}
