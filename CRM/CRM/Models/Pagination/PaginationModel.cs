using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.Pagination
{
    public class PaginationModel
    {
        public int FirstVisiblePage { get; set; }
        public int LastVisiblePage { get; set; }

        public int LastPage { get; set; }
    }
}
