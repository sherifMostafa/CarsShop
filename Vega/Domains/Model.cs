﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Vega.Domains
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int MakeId { get; set; }
        // [JsonIgnore]
        public Make Make { get; set; }

       
    }
}
