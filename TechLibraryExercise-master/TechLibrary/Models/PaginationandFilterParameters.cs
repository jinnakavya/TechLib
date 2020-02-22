﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechLibrary.Models
{
    public class PaginationandFilterParameters
    {
		const int maxPageSize = 50;
		public int PageNumber { get; set; } = 1;

		private int _pageSize = 10;
		public string Term { get; set; } = "";
		public int PageSize
		{
			get
			{
				return _pageSize;
			}
			set
			{
				_pageSize = (value > maxPageSize) ? maxPageSize : value;
			}
		}
	}
}
