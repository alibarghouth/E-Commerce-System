namespace E_Commerce_System.Model
{
    public class OrderItem
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public int Count { get; set; }

        public Product Product { get; set; }

        public Order Order { get; set; }
    }
}
