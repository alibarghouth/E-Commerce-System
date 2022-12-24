namespace E_Commerce_System.Model
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public byte[] ProductImg { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public OrderItem OrderItem { get; set; }
    }
}
