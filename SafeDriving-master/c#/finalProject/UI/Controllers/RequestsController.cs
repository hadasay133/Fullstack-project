using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using DTO;

namespace UI.Controllers
{
    [RoutePrefix("api/Requests")]
    public class RequestsController : ApiController
    {
        // GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //GET api/values/5
        [Route("GetByPersonId")]
        public List<RequestsDto> GetByPersonId(int id)
        {
            return BL.RequestsLogic.GetByPersonId(id);

        }
        [Route("GetWithOffersByPersonId")]
        public List<RequestsDto> GetWithOffersByPersonId(int id)
        {
            return BL.RequestsLogic.GetWithOffersByPersonId(id);
        }

        [Route("GetHistoryByPersonId")]
        public List<RequestsDto> GetHistoryByPersonId(int id)
        {
            return BL.RequestsLogic.GetHistoryByPersonId(id);

        }
        [Route("GetHistoryWithOffersByPersonId")]
        public List<RequestsDto> GetHistoryWithOffersByPersonId(int id)
        {
            return BL.RequestsLogic.GetHistoryWithOffersByPersonId(id);
        }

        // GET api/values/5
        [Route("GetById")]
        public RequestsDto GetId(int id)//מה לעשות?
        {
            return BL.RequestsLogic.GetById(id);

        }
        [Route("SearchOffersByRequest")]
        [HttpPost]
        public List<OffersDto> SearchOffersByRequest(RequestsDto req)//מה לעשות?
        {
            return BL.RequestsLogic.search(req);
        }
        [Route("GetActiveRelevantByOfferId")]
        [HttpGet]
        public List<RequestsDto> GetActiveRelevantByOfferId(int id)//מה לעשות?
        {
            return BL.RequestsLogic.GetActiveRelevantByOfferId(id);
        }

        [Route("GetAllActiveRequests")]
        [HttpGet]
        public List<RequestsDto> GetAllActiveRequests()//מה לעשות?
        {
            return BL.RequestsLogic.GetAllActiveRequests();
        }

        
        [Route("getRelevantWithOffersByOfferId")]
        [HttpGet]
        public List<RequestsDto> getRelevantWithOffersByOfferId(int id)//מה לעשות?
        {
            return BL.RequestsLogic.getRelevantWithOffersByOfferId(id);
        }
        [Route("getDisabledWithOffersByOfferId")]
        [HttpGet]
        public List<RequestsDto> getDisabledWithOffersByOfferId(int id)//מה לעשות?
        {
            return BL.RequestsLogic.getDisabledWithOffersByOfferId(id);
        }
        [Route("sendEmailWithOffer")]
        [HttpGet]
        public async Task<bool> sendEmailWithOffer(int id, int reqId)//מה לעשות?
        {
            return await BL.RequestsLogic.sendEmailWithOffer(id, reqId);
        }
        [Route("SendEmailToOffer")]
        [HttpGet]
        public async Task<bool> SendEmailToOffer(int id, int reqId)//מה לעשות?
        {
            return await BL.RequestsLogic.SendEmailToOffer(id, reqId);
        }


        
        [HttpPost]
        [Route("AddRequest")]
        public IHttpActionResult AddRequest(RequestsDto req)
        {
            return Ok(BL.RequestsLogic.add(req));
        }
        [HttpPost]
        [Route("UpdateRequest")]
        public IHttpActionResult UpdateRequest(RequestsDto req)
        {
            return Ok(BL.RequestsLogic.updateRequest(req));
        }

        [HttpDelete]
        [Route("DeleteRequest")]
        public IHttpActionResult DeleteRequest(int id)
        {
            return Ok(BL.RequestsLogic.deleteRequest(id));
        }


        [HttpPost]
        [Route("connectDriver/{offerId}/{reqId}")]
        public void ConnectDriver(int offerid, int requestid)
        {
            BL.RequestsLogic.selectOfferByRequestId(offerid, requestid);
        }
        [HttpPost]
        [Route("deletefromlist /{offerId}/{reqId} ")]
        public void deletefromlist(int offerid, int requestid)
        {
            BL.RequestsLogic.UpdateRequestIgnoreOffers(offerid, requestid);
        }
        //// PUT api/values/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}
