﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTracker.Data.Model
{
    public sealed class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<WorkDetails> WorkDetails { get; set; }
    }
}
