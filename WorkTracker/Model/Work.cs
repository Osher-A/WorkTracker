using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTracker.Model
{
    public sealed class Work
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float Hours { get; set; }
        public string Description { get; set; }
    }
}
