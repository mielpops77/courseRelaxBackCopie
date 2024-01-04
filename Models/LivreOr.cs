namespace British_Kingdom_back.Models
{
    public class LivreOr
    {
        public int Id { get; set; }
        public int ProfilId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public Boolean Validation { get; set; }

        public DateTime DateofCrea { get; set; }
    }
}

