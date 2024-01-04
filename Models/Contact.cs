namespace British_Kingdom_back.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public int ProfilId { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Num { get; set; } = string.Empty;
        public string Hour { get; set; } = string.Empty;

        public Boolean Vue { get; set; }
        public DateTime DateofCrea { get; set; }

    }
}