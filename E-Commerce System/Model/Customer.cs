namespace E_Commerce_System.Model
{
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }

        public DateTime? TokenCreate { get; set; }

        public DateTime? TokenExpier { get; set; }

        public string? Token { get; set; }

        public string? RefreshToken { get; set; }

        public string Role { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSlot { get; set; }

        public byte[] Picture { get; set; }

        public string Email { get; set; }

        public Order Order { get; set; }

    }
}
