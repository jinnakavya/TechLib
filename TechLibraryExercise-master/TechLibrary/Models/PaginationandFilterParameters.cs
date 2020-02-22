﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechLibrary.Models
{
    public class PaginationandFilterParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; }
        public string SearchText { get; set; } = "";
    }
}
