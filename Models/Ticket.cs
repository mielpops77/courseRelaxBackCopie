namespace British_Kingdom_back.Models
{
    using System;
    using System.Collections.Generic;

    public class Ticket
    {
        public int Id { get; set; }
        public int IdProfil { get; set; }
        public string Subject { get; set; }
        // public DateTime CreationDate { get; set; }
        public DateTimeOffset CreationDate { get; set; }

        public string Message { get; set; }
        public List<Conversation> Conversations { get; set; } = new List<Conversation>();
        /*  public List<Profil> Profil { get; set; } */
        public Profil Profil { get; set; }  // Une seule propriété Profil au lieu d'une liste

        public string Status { get; set; }

        public string Image { get; set; }

    }
}
