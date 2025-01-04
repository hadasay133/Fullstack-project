using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DTO
{
   public  class OffersDto
    {
        public int id { get; set; }
        public int id_person { get; set; }
        public string resourse { get; set; }
        public string destination { get; set; }
        public string resourse_city { get; set; }
        public string destination_city { get; set; }
        public string remarks { get; set; }
        public int seats { get; set; }
        public System.DateTime date_time { get; set; }
        public double price { get; set; }
        public bool active { get; set; }
        public string email_requests { get; set; }

    
        public List<RequestsDto> requests { get; set; }
        public PersonDto driver { get; set; }
        public int numSeats { get; set; }
        public bool is_emailed { get; set; }

    }
}
