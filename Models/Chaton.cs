namespace British_Kingdom_back.Models
{
    public class Chaton
    {
        public int Id { get; set; }
        public int IdPortee { get; set; }
        public int ProfilId { get; set; }
        public string  DateOfBirth { get; set; } = string.Empty;
        public string Name { get; set; }
        public string PorteeName { get; set; }
        public string UrlProfil { get; set; } = string.Empty;

        // public string Robe { get; set; }
        // public string Breed { get; set; }
        public string Sex { get; set; }
        public string Status { get; set; }
        public string[]? Photos { get; set; }


        public string Robe { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public Boolean Loof { get; set; }


    }
}
