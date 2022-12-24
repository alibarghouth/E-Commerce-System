namespace E_Commerce_System.DTO.OrderItemDto
{
    public class UpdatedOrderItem
    {
        public string? Name { get; set; }


        public int? ProductId { get; set; }

        public int? OrderId { get; set; }

        public int? Count { get; set; }
    }
}
