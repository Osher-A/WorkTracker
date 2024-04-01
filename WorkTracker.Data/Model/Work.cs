using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTracker.Data.Model
{
    public sealed class Work
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public ICollection<WorkDetails> WorkDetails { get; set; }
    }
}
