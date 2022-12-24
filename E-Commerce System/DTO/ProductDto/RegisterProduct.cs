namespace E_Commerce_System.DTO.ProductDto
{
    public class RegisterProduct
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public IFormFile ProductImg { get; set; }

        public int CategoryId { get; set; }
    }
}
