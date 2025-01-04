using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
//using DAL;
using BL;

using DTO;

namespace UI.Controllers
{
    [RoutePrefix("api/offers")]
    public class offersController : ApiController
    {
        //  GET api/values
        //   public List<offers> Get()
        //     {
        //   return BL.Class1.
        //     }

        // GET api/values/5
        //[Route("/GetById")]
        //public List<OffersDto> Get(int id)
        //{
        //    return BL.OffersLogic.GetById(id);

        //}
        // GET api/values/5
        [Route("GetById")]
        public OffersDto GetId(int id)//מה לעשות?
        {
            return BL.OffersLogic.GetById(id);

        }
        [HttpGet]
        [Route("GetByPersonId")]
        public IHttpActionResult GetByPersonId(int id)//מה לעשות?
        {
            return Ok(BL.OffersLogic.GetByPersonId(id));

        }
        [HttpGet]
        [Route("GetHistoryByPersonId")]
        public IHttpActionResult GetHistoryByPersonId(int id)//מה לעשות?
        {
            return Ok(BL.OffersLogic.GetHistoryByPersonId(id));

        }
        [HttpGet]
        [Route("GetWithRequestsByPersonId")]
        public IHttpActionResult GetWithRequestsByPersonId(int id)//מה לעשות?
        {
            return Ok(BL.OffersLogic.GetWithRequestsByPersonId(id));

        }
        [HttpGet]
        [Route("GetWithNoRequestsByPersonId")]
        public IHttpActionResult GetWithNoRequestsByPersonId(int id)//מה לעשות?
        {
            return Ok(BL.OffersLogic.GetWithNoRequestsByPersonId(id));

        }
        [HttpGet]
        [Route("GetAllActiveOffers")]
        public IHttpActionResult GetAllActiveOffers()//מה לעשות?
        {
            return Ok(BL.OffersLogic.GetAllActiveOffers());

        }
        [HttpGet]
        [Route("GetAllActiveOffersByReqId")]
        public IHttpActionResult GetAllActiveOffersByReqId(int id)//מה לעשות?
        {
            return Ok(BL.OffersLogic.GetAllActiveOffersByReqId(id));

        }

        
        [HttpGet]
        [Route("CheckSetOfferWithRequests")]
        public IHttpActionResult CheckSetOfferWithRequests(int id, int reqId)//מה לעשות?
        {
            return Ok(BL.OffersLogic.CheckSetOfferWithRequests(id, reqId));

        }
        [HttpGet]
        [Route("IgnoreOfferWithRequests")]
        public IHttpActionResult IgnoreOfferWithRequests(int id, int reqId)//מה לעשות?
        {
            return Ok(BL.OffersLogic.IgnoreOfferWithRequests(id, reqId));

        }
        [HttpGet]
        [Route("GetNumSeatsByOfferId")]
        public IHttpActionResult GetNumSeatsByOfferId(int id)//מה לעשות?
        {
            return Ok(BL.OffersLogic.GetNumSeatsByOfferId(id));

        }

        
        // GET api/values/5httpget
        [HttpGet]
        [Route("selectOfferByRequestId/{reqId}/{offerId}")]
        public bool selectOfferByRequestId(int reqId, int offerId)//מה לעשות?
        {
            return BL.OffersLogic.selectOfferByRequestId(offerId, reqId);

        }

        [HttpPost]
        [Route("AddOffer")]
        public int AddOffer(OffersDto offer)
        {
            return BL.OffersLogic.add(offer);
        }//להיכן מוחזרת רשימת ההתאמות?
        [HttpPost]
        [Route("UpdateOffer")]
        public int UpdateOffer(OffersDto offer)
        {
            return BL.OffersLogic.UpdateOffer(offer);
        }//להיכן מוחזרת רשימת ההתאמות?

        [HttpDelete]
        [Route("RemoveOfferWithTravels")]
        public async Task<bool> RemoveOfferWithTravels(int id)
        {
            return await BL.OffersLogic.RemoveOfferWithTravels(id);
        }//להיכן מוחזרת רשימת ההתאמות?

        
        public Boolean Post(int id)
        {
            return BL.OffersLogic.deleteOffer(id);
        }

        // PUT api/values/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        /// <summary>
        [HttpDelete]
        [Route("DeleteOffer")]
        /// </summary>
        /// <param name="id"></param>
        public bool Delete(int id)
        {
            return BL.OffersLogic.deleteOffer(id);
        }
    }
}
