﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MotoristaCache
    {
        public Guid Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
