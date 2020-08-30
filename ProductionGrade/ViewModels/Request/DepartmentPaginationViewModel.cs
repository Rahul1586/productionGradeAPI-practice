using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.Request
{
    public class DepartmentPaginationViewModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 3;
        public PaginationViewModel Pagination
        {
            get { return new PaginationViewModel() { PageNumber = PageNumber, PageSize = PageSize }; }
        }
        public string Code { get; set; }
        public string Name { get; set; }
        public string OrderingField { get; set; }
        public bool AscendingOrder { get; set; } = true;
    }
}
