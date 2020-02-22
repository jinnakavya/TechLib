using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLibrary.Models;

namespace TechLibrary.Contracts.Responses
{
    public class PagedResponse<T> : PagedResultBase where T : class
    {
        public IList<T> Results { get; set; }

        public PagedResponse()
        {
            Results = new List<T>();
        }
    }
}
