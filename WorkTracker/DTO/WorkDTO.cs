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
        public float Hours { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
