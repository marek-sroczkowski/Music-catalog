using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Filters
{
    public abstract class QueryStringParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int pageSize = 10;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}
