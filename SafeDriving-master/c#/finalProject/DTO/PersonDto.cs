using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DTO
{
   public  class PersonDto
    {
        public int id { get; set; }
        public string username { get; set; }
        public int tz { get; set; }
        public string adress { get; set; }
        public string mail { get; set; }
        public string phone { get; set; }
        public string inqure { get; set; }
        public bool ok { get; set; }
        public string password { get; set; }
        public bool is_manager { get; set; }
    }
}
