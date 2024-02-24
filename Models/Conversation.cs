namespace British_Kingdom_back.Models
{
    using System;

    public class Conversation
    {
        public int Id { get; set; }
        public int IdTicket { get; set; }
        public int UniqueProfilId { get; set; }
        public DateTime DateCrea { get; set; }
        public string Message { get; set; }
        public bool Admin { get; set; }
        public bool VueUser { get; set; }

        public bool VueAdmin { get; set; }

        public string Image { get; set; }


    }
}
