namespace British_Kingdom_back.Models
{
    using System;
    using System.Collections.Generic;

    public class Ticket
    {
        public int Id { get; set; }
        public int ProfilId { get; set; }
        public string Subject { get; set; }
        // public DateTime CreationDate { get; set; }
        public DateTimeOffset CreationDate { get; set; }

        public string Message { get; set; }
        public List<Conversation> Conversations { get; set; } = new List<Conversation>();
        public Profil Profil { get; set; }  

        public string Status { get; set; }

        public string Image { get; set; }

    }
}
