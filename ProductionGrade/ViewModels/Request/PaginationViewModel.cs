using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.Request
{
    public class PaginationViewModel
    {
        public int PageNumber { get; set; } = 1;

        private int pageSize;
        private readonly int maxRecordsPerPage = 50;

        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = (value > maxRecordsPerPage) ? maxRecordsPerPage : value;
            }
        }
    }
}
