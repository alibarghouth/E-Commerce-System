namespace E_Commerce_System.DTO.Response.Queries
{
    public class PaginationQueries
    {
        public PaginationQueries()
        {
            PageNumber= 1;
            PageSize= 100;
        }
        public PaginationQueries(int PageNumber,int PageSize)
        {
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
