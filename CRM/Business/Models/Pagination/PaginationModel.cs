using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    public class PaginationModel
    {
        public int FirstVisiblePage { get; set; }
        public int LastVisiblePage { get; set; }

        public int CountOfPages { get; set; }
    }
}
