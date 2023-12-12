using System.ComponentModel.DataAnnotations;

namespace TicketsAPI.Core.DTO
{
    public class CreateTicketDTO
    {
      
        public DateTime Time { get; set; }
        public string PassengerName { get; set; }
        public long PassengerSSN { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int Price { get; set; }
      

    }
}
