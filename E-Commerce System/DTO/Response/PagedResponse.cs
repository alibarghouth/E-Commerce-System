namespace E_Commerce_System.DTO.Response
{
    public class PagedResponse<T>
    {
        public PagedResponse(){}

        public PagedResponse(IEnumerable<T> data)
        {
            this.data = data;
        }
        public IEnumerable<T> data { get; set; }

        public int? PageNumber { get; set; } 
        public int? PageSize { get; set; }
        public string NextPage { get; set; } 
        public string PreviousPage { get; set; }
    }
}
