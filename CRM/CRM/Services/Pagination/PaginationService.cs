﻿using CRM.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Pagination
{
    public class PaginationService
    {
        private PaginationModel paginationModel;

        public PaginationService()
        {
            paginationModel = new PaginationModel();
        }

        public PaginationModel GetPaginationModel(int currentPage, int listCount, int pageSize = 100)
        {
            paginationModel.CountOfPages = (int)Math.Ceiling((decimal)((double)listCount / pageSize));
            paginationModel.FirstVisiblePage = currentPage - 5 >= 1 ? currentPage - 3 : 1; //TODO: move to pagination control (partial view)
            paginationModel.LastVisiblePage = currentPage + 5 <= paginationModel.CountOfPages ? currentPage + 5 : paginationModel.CountOfPages;

            return paginationModel;
        }
    }
}
