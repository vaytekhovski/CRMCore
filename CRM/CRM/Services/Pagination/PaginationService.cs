using CRM.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Pagination
{
    public class PaginationService
    {
        public PaginationService()
        {

        }

        public PaginationModel GetPaginationModel(int currentPage, int countOfPages)
        {
            PaginationModel paginationModel = new PaginationModel();

            paginationModel.FirstVisiblePage = currentPage - 5 >= 1 ? currentPage - 3 : 1;
            paginationModel.LastVisiblePage = currentPage + 5 <= countOfPages ? currentPage + 5 : countOfPages;
            paginationModel.LastPage = countOfPages;

            return paginationModel;
        }
    }
}
