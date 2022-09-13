using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Requests
{
    public class PageRequest
    {
        public int PageNumber { get; set; } = 1;

        const int maxPageSize = 100;

        private int pageSize = 20;
        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
