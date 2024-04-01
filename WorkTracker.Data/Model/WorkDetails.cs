using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTracker.Data.Model
{
    public class WorkDetails
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public float Hours { get; set; }
        public string? ClientName { get; set; }
    }
}
