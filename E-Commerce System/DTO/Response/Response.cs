namespace E_Commerce_System.DTO.Response
{
    public class Response<T>
    {
        public Response() { }
        public Response(T data)
        {
            this.data = data;   
        }
        public T data { get; set; }
    }
}
