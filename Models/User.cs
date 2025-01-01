namespace British_Kingdom_back.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } // Nouvelle propriété

    }
}
