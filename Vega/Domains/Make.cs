﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vega.Domains
{
    public class Make
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Model> Models { get; set; }

 
    }
}
