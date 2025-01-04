using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DTO
{
   public  class RequestsDto
    {
        public int id { get; set; }
        public int id_person { get; set; }
        public string resourse { get; set; }
        public string destination { get; set; }
        public string resourse_city { get; set; }
        public string destination_city { get; set; }
        public int seats { get; set; }
        public System.DateTime date_time { get; set; }
        public bool active { get; set; }
        public string ignore_offers { get; set; }
        public string remarks { get; set; }
        public string email_offers { get; set; }
        //----not from DB -----
        public OffersDto offer { get; set; }
        public TravelsDto travel { get; set; }
        public PersonDto driver { get; set; }
        public bool is_emailed { get; set; }

    }
}
