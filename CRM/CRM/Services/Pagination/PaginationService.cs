using Business;

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
            //paginationModel.CountOfPages = (int)Math.Ceiling((decimal)((double)listCount / pageSize));

            paginationModel.CountOfPages = listCount / pageSize + (listCount % pageSize > 1 ? 1 : 0);
            //paginationModel.FirstVisiblePage = currentPage - 5 >= 1 ? currentPage - 3 : 1; //TODO: [COMPLETE] move to pagination control (partial view)
            //paginationModel.LastVisiblePage = currentPage + 5 <= paginationModel.CountOfPages ? currentPage + 5 : paginationModel.CountOfPages;

            return paginationModel;
        }
    }
}
