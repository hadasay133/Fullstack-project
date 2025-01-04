using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BL;
using DTO;


namespace UI.Controllers
{
    
  //  [System.Web.Http.Cors()]
    public class TravelController : ApiController
    {
        // GET api/travel/5
        [Route("api/travel/GetByPersonId")]
        public List<TravelsDto> GetByPersonId(int id)
        {
            return BL.TravelLogic.GetByPersonId(id);
        }

        [Route("api/travel/GetById")]
        public TravelsDto GetId(int id)//
        {
            return BL.TravelLogic.GetById(id);

        }


        // GET api/travel
     //   public string Get()
     //   {
     //       return "value";
     //   }

        // POST api/travel
     //   public void Post([FromBody] string value)
     //   {
     //   }


    }
}
