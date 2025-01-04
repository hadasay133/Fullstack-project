using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using System.Data.Entity.Core.Objects;

namespace BL
{
    public class RequestsLogic
    {
        // החזרת רשימת נסיעות לפי תעודת זהות
        public static List<RequestsDto> GetByPersonId(int id)
        {//לבדוק אם הנסיעות פעילות
            var now = DateTime.Now;
            SafeDrivingEntities sd = new SafeDrivingEntities();
            List<requests> request = sd.requests.Where(x => x.id_person == id
            && x.active == true && x.date_time > now).ToList();
            List<RequestsDto> requests = new List<RequestsDto>();
            for (int i = 0; i < request.Count; i++)
            {
                requests.Add(Convertions.RequestsConvertion.RequestToDto(request[i]));
            }
            return requests;
        }
        // החזרת רשימת נסיעות סגורות לפי תעודת זהות
        public static List<RequestsDto> GetWithOffersByPersonId(int id)
        {//לבדוק אם הנסיעות פעילות
            var now = DateTime.Now;
            using (var sd = new SafeDrivingEntities())
            {
                List<requests> request = sd.requests.Where(x => x.id_person == id
                && x.active == false && x.date_time > now).ToList();



                List<RequestsDto> requests = new List<RequestsDto>();
                for (int i = 0; i < request.Count; i++)
                {
                    var reqId = request[i].id;
                    var travel = sd.travels.FirstOrDefault(x => x.id_request == reqId);
                    offers offer = null;
                    persons person = null;
                    if (travel != null)
                    {
                        offer = sd.offers.FirstOrDefault(x => x.id == travel.id_offer);
                        person = sd.persons.FirstOrDefault(x => x.id == offer.id_person);
                    }

                    var req = Convertions.RequestsConvertion.RequestToDto(request[i]);
                    req.driver = person == null ? null : Convertions.PersonConvertion.PersonToDto(person);
                    req.travel = travel == null ? null : Convertions.travelsConvertion.travelToDto(travel);
                    req.offer = offer == null ? null : Convertions.offersConvertion.OfferToDto(offer);
                    requests.Add(req);
                }

                return requests;
            }
        }
        // החזרת רשימת נסיעות לפי תעודת זהות
        public static List<RequestsDto> GetHistoryByPersonId(int id)
        {//לבדוק אם הנסיעות פעילות
            var now = DateTime.Now;
            SafeDrivingEntities sd = new SafeDrivingEntities();
            List<requests> request = sd.requests.Where(x => x.id_person == id
            && x.active == true && x.date_time < now).ToList();
            List<RequestsDto> requests = new List<RequestsDto>();
            for (int i = 0; i < request.Count; i++)
            {
                requests.Add(Convertions.RequestsConvertion.RequestToDto(request[i]));
            }

            return requests;
        }
        // החזרת רשימת נסיעות סגורות לפי תעודת זהות
        public static List<RequestsDto> GetHistoryWithOffersByPersonId(int id)
        {//לבדוק אם הנסיעות פעילות
            var now = DateTime.Now;
            using (var sd = new SafeDrivingEntities())
            {
                List<requests> request = sd.requests.Where(x => x.id_person == id
                && x.active == false && x.date_time < now).ToList();

                var travel = sd.travels.FirstOrDefault(x => x.id_request == id);
                offers offer = null;
                persons person = null;
                if (travel != null)
                {
                    offer = sd.offers.FirstOrDefault(x => x.id == travel.id_offer);
                    person = sd.persons.FirstOrDefault(x => x.id == offer.id_person);
                }

                List<RequestsDto> requests = new List<RequestsDto>();
                for (int i = 0; i < request.Count; i++)
                {
                    var req = Convertions.RequestsConvertion.RequestToDto(request[i]);
                    req.driver = person == null ? null : Convertions.PersonConvertion.PersonToDto(person);
                    req.travel = travel == null ? null : Convertions.travelsConvertion.travelToDto(travel);
                    req.offer = offer == null ? null : Convertions.offersConvertion.OfferToDto(offer);
                    requests.Add(req);
                }

                return requests;
            }
        }
        //החזרת נסיעה לפי מספר נסיעה
        public static RequestsDto GetById(int id)
        {
            using (var sd = new SafeDrivingEntities())
            {
                RequestsDto request = Convertions.RequestsConvertion.RequestToDto(sd.requests.FirstOrDefault(x => x.id == id));


                return request;
            }
        }
        //מחיקת נסיעה
        public static bool deleteRequest(int id)
        {
            using (var sd = new SafeDrivingEntities())
            {
                var tr = sd.travels.FirstOrDefault(r => r.id_request == id);
                if (tr != null)
                {
                    sd.travels.Remove(tr);
                    sd.SaveChanges();
                }
                var item = sd.requests.FirstOrDefault(r => r.id == id);
                sd.requests.Remove(item);
                sd.SaveChanges();
            }
            return true;
        }
        //עדכון נסיעה
        public static RequestsDto updateRequest(RequestsDto p1)
        {

            using (SafeDrivingEntities sd = new SafeDrivingEntities())
            {
                var fromDB = sd.requests.FirstOrDefault(x => x.id == p1.id);
                fromDB.seats = p1.seats;
                fromDB.destination = p1.destination;
                fromDB.resourse = p1.resourse;
                fromDB.date_time = p1.date_time;
                sd.SaveChanges();

                return Convertions.RequestsConvertion.RequestToDto(fromDB);
            }
        }
        //הכנסת נסיעה
        public static RequestsDto add(RequestsDto req)
        {
            requests p1 = new requests();
            p1 = Convertions.RequestsConvertion.DtoToRequest(req);
            p1.active = true;
            using (SafeDrivingEntities sd = new SafeDrivingEntities())
            {
                sd.requests.Add(p1);
                sd.SaveChanges();

                return Convertions.RequestsConvertion.RequestToDto(p1);
            }
        }

        public static List<RequestsDto> getRelevantWithOffersByOfferId(int id)
        {
            //להוסיף לנסיעות משתנה בוליאני כד לדעת מה פעיל ולבדוק גם על פי זה
            using (var sd = new SafeDrivingEntities())
            {
                //בדיקה לפי מוצא ,יעד ותאריך
                var offers = sd.offers.FirstOrDefault(x => x.id == id);
                var travelsReq = sd.travels.Where(x => x.id_offer == offers.id).Select(x => x.id_request);


                var requests = sd.requests.Include("persons").Where(x => travelsReq.Contains(x.id));

                var lRequests = new List<RequestsDto>();
                foreach (var req in requests)
                {
                    lRequests.Add(Convertions.RequestsConvertion.RequestToDto(req));
                }

                return lRequests;
            }
        }

        public static async Task<bool> sendEmailWithOffer(int id, int reqId)
        {
            //להוסיף לנסיעות משתנה בוליאני כד לדעת מה פעיל ולבדוק גם על פי זה
            using (var sd = new SafeDrivingEntities())
            {
                //בדיקה לפי מוצא ,יעד ותאריך
                var offers = sd.offers.Include("persons").FirstOrDefault(x => x.id == id);
                var requestsVal = sd.requests.Include("persons").FirstOrDefault(x => x.id == reqId);

                if (!string.IsNullOrEmpty(requestsVal.email_offers))
                {
                    var ig = requestsVal.email_offers.Split(',').ToList();
                    ig.Add(offers.id.ToString());
                    requestsVal.email_offers = String.Join(",", ig);

                }
                else
                {
                    requestsVal.email_offers = id.ToString();
                }
                sd.SaveChanges();

                StringBuilder htmlBody = new StringBuilder("<div style='text-align: center; color: #2c366e; width: 550px; border: 3px solid #2c366e;font-size:18px;'>" +
"<p>הי " + requestsVal.persons.username + "</p>" +
"<p>נשלחה עבורך הצעת נסיעה בתאריך עיר מקור ועיר יעד זהים לבקשתך!</p> " +
"<p>נסיעה מ" + offers.resourse + " " + offers.resourse_city + "</p> " +
"<p>ליעד " + offers.destination + " " + offers.destination_city + "  </p> " +
"<p>תאריך ושעת יציאה <strong>" + offers.date_time.ToString("dd/MM/yyyy HH:mm") + "</strong>   </p> " +
"<p>מחיר הנסיעה  <strong>" + offers.price + "</strong> שקלים  </p> " +
"<p>" + offers.remarks + " </p> " +
"<p>פרטי הנהג " + offers.persons.mail + " | " + offers.persons.phone + " | " + offers.persons.username + "   </p> " +
"<p>נא עדכן/י את תשובתך בהקדם על מנת להבטיח את מקומך</p> " +
"<p><a href='http://localhost:4200/accept-request/" + offers.id + "/" + requestsVal.id + "' style=' min-width:100px; " +
"    padding: 6px 30px;text-decoration:none;    font-size: 18px;    line-height: 28px;    margin: 10px auto; " +
"    border: none;    cursor: pointer;  background: #2c366e;    border-radius: 0px; " +
"    color: #ffdf00;    font-weight: 500;    border: 1px solid #2c366e; " +
"    height: auto;    box-shadow: none;    transition: 0.2s linear all; " +
"    border-radius: 20px;'>  כן אני רוצה</a> " +
"<a href='http://localhost:4200/ignore-request/" + offers.id + "/" + requestsVal.id + "' style=' min-width:100px;    padding: 6px 30px;text-decoration:none; " +
"    font-size: 18px;    line-height: 28px; " +
"    margin:  10px auto;    border: none; " +
"    cursor: pointer;    background: #ffdf00 ; " +
"    border-radius: 0px;    color: #2c366e; " +
"    font-weight: 500;    border: 1px solid #2c366e; " +
"    height: auto;    box-shadow: none;    transition: 0.2s linear all; " +
"    border-radius: 20px;'> לא מתאים לי</a> </p> " +
"</div>");




                await GeneralLogic.SendEmailMessage(requestsVal.persons.mail, htmlBody.ToString(), "הצעת נסיעה עבורך");


                return true;
            }
        }
        public static async Task<bool> SendEmailToOffer(int id, int reqId)
        {
            //להוסיף לנסיעות משתנה בוליאני כד לדעת מה פעיל ולבדוק גם על פי זה
            using (var sd = new SafeDrivingEntities())
            {
                //בדיקה לפי מוצא ,יעד ותאריך
                var offers = sd.offers.Include("persons").FirstOrDefault(x => x.id == id);
                var requestsVal = sd.requests.Include("persons").FirstOrDefault(x => x.id == reqId);

                if (!string.IsNullOrEmpty(offers.email_requests))
                {
                    var ig = offers.email_requests.Split(',').ToList();
                    ig.Add(reqId.ToString());
                    offers.email_requests = String.Join(",", ig);

                }
                else
                {
                    offers.email_requests = id.ToString();
                }
                sd.SaveChanges();

                StringBuilder htmlBody = new StringBuilder("<div style='text-align: center; color: #2c366e; width: 550px; border: 3px solid #2c366e;font-size:18px;'>" +
"<p>הי " + offers.persons.username + "</p>" +
"<p>נשלחה עבורך בקשת נסיעה בתאריך עיר מקור ועיר יעד זהים להצעתך!</p> " +
"<p>נסיעה מ" + requestsVal.resourse + " " + requestsVal.resourse_city + "</p> " +
"<p>ליעד " + requestsVal.destination + " " + requestsVal.destination_city + "  </p> " +
"<p>תאריך ושעת יציאה <strong>" + requestsVal.date_time.ToString("dd/MM/yyyy HH:mm") + "</strong>   </p> " +
"<p>" + requestsVal.remarks + " </p> " +
"<p>פרטי הנוסע " + requestsVal.persons.mail + " | " + requestsVal.persons.phone + " | " + requestsVal.persons.username + "   </p> " +
"<p>נא עדכן/י את תשובתך בהקדם על מנת ליעל את המערכת</p> " +
"<p><a href='http://localhost:4200/privateArea/ActiveRequests/" + offers.id  + "' style=' min-width:100px; " +
"    padding: 6px 30px;text-decoration:none;    font-size: 18px;    line-height: 28px;    margin: 10px auto; " +
"    border: none;    cursor: pointer;  background: #2c366e;    border-radius: 0px; " +
"    color: #ffdf00;    font-weight: 500;    border: 1px solid #2c366e; " +
"    height: auto;    box-shadow: none;    transition: 0.2s linear all; " +
"    border-radius: 20px;'>  כניסה למערכת</a></p> " +
"</div>");




                await GeneralLogic.SendEmailMessage(offers.persons.mail, htmlBody.ToString(), "בקשה להצטרפות לנסיעה שלך");


                return true;
            }
        }

        

        public static List<RequestsDto> getDisabledWithOffersByOfferId(int id)
        {
            //להוסיף לנסיעות משתנה בוליאני כד לדעת מה פעיל ולבדוק גם על פי זה
            using (var sd = new SafeDrivingEntities())
            {
                //בדיקה לפי מוצא ,יעד ותאריך
                var offers = sd.offers.FirstOrDefault(x => x.id == id);
                var requests = sd.requests.Include("persons").Where(x => !string.IsNullOrEmpty(x.ignore_offers)
                && x.ignore_offers.IndexOf(id.ToString()) > -1

                && x.active == true);

                var lRequests = new List<RequestsDto>();
                foreach (var req in requests)
                {

                    var reqIgnores = req.ignore_offers.Split(',');
                    if (reqIgnores.Contains(offers.id.ToString()))
                    {
                        lRequests.Add(Convertions.RequestsConvertion.RequestToDto(req));
                    }
                }

                return lRequests;
            }
        }

        public static List<RequestsDto> GetAllActiveRequests()
        {
            //להוסיף לנסיעות משתנה בוליאני כד לדעת מה פעיל ולבדוק גם על פי זה
            using (var sd = new SafeDrivingEntities())
            {
                //בדיקה לפי מוצא ,יעד ותאריך


                var requests = sd.requests.Include("persons").Where(x =>
                   x.date_time >= DateTime.Today
                && x.active == true && x.persons.ok == true);

                var lRequests = new List<RequestsDto>();
                foreach (var req in requests)
                {

                    var reqDto = Convertions.RequestsConvertion.RequestToDto(req);
                    reqDto.driver = Convertions.PersonConvertion.PersonToDto(req.persons);
                    lRequests.Add(reqDto);

                }

                return lRequests;
            }
        }

        public static List<RequestsDto> GetActiveRelevantByOfferId(int id)
        {
            //להוסיף לנסיעות משתנה בוליאני כד לדעת מה פעיל ולבדוק גם על פי זה
            using (var sd = new SafeDrivingEntities())
            {
                //בדיקה לפי מוצא ,יעד ותאריך
                var offers = sd.offers.FirstOrDefault(x => x.id == id);

                var beginHour = offers.date_time.AddHours(-3);
                var endHour = offers.date_time.AddHours(+3);

                var requests = sd.requests.Include("persons").Where(x => x.resourse_city == offers.resourse_city
                && x.destination_city == offers.destination_city
                  && x.date_time >= beginHour && x.id_person != offers.id_person
                 && x.date_time <= endHour && x.active == true);

                var lRequests = new List<RequestsDto>();
                foreach (var req in requests)
                {
                    if (!string.IsNullOrEmpty(req.ignore_offers))
                    {
                        var reqIgnores = req.ignore_offers.Split(',');

                        if (!reqIgnores.Contains(offers.id.ToString()))
                        {
                            var reqDto = Convertions.RequestsConvertion.RequestToDto(req);
                            if (!string.IsNullOrEmpty(req.email_offers)
                                && req.email_offers.Split(',').ToList().Contains(offers.id.ToString()))
                            {
                                reqDto.is_emailed = true;
                            }

                            lRequests.Add(reqDto);
                        }

                    }
                    else
                    {
                        var reqDto = Convertions.RequestsConvertion.RequestToDto(req);
                        if (!string.IsNullOrEmpty(req.email_offers)
                            && req.email_offers.Split(',').ToList().Contains(offers.id.ToString()))
                        {
                            reqDto.is_emailed = true;
                        }
                        lRequests.Add(reqDto);
                    }
                }

                return lRequests;
            }
        }

        public static List<OffersDto> search(RequestsDto req)
        {
            //להוסיף לנסיעות משתנה בוליאני כד לדעת מה פעיל ולבדוק גם על פי זה
            SafeDrivingEntities sd = new SafeDrivingEntities();
            //בדיקה לפי מוצא ,יעד ותאריך
            var it = sd.offers.Where(x => x.resourse == req.resourse && x.destination == req.destination
             && x.date_time.Date == req.date_time.Date).ToList();

            if (!string.IsNullOrEmpty(req.ignore_offers))
            {

                var reqIgnores = req.ignore_offers.Split(',');
                it = it.Where(x => !reqIgnores.Contains(x.id.ToString())).ToList();

            }

            //המרה לdto
            var offers = new List<OffersDto>();
            for (int i = 0; i < it.Count; i++)
            {
                offers.Add(Convertions.offersConvertion.OfferToDto(it[i]));
            }
            return offers;
        }

        public static void selectOfferByRequestId(int offerId, int reqId)
        {
            using (SafeDrivingEntities sd = new SafeDrivingEntities())
            {

                var offer = sd.offers.FirstOrDefault(x => x.id == offerId);
                var personDtriver = sd.persons.FirstOrDefault(x => x.id == offer.id_person);

                var req = sd.requests.FirstOrDefault(x => x.id == reqId);
                var personReq = sd.persons.FirstOrDefault(x => x.id == req.id_person);

                var subject = "בקשת הצטרפות לנסיעה";
                var body = "<h1 style='color: #5e9ca0; text-align: right;'>&nbsp; !שלום נוסע " + personReq.username + "</h1>" +
        "<h1 style='color: #5e9ca0; text-align: right;'>?" + personDtriver.username + " האם תרצה להצטרף לנסיעה עם נהג </h1> " +
        "<p style='text-align: right; ;display: inline-block;'><strong><a style='background-color: #5e9ca0; padding: 10px; color: white;' href='http://localhost:4200/'>אשר</a></strong></p> " +
        "<p style='display: inline-block; text-align: left;'><strong><a style='background-color: #5e9ca0; padding: 10px; color: white;' href='http://localhost:4200/ignore-request?reqid=" + offerId + "&offerid="
        + reqId + "'>סרב</a></strong></p>";


                //GeneralLogic.sendEmailAsync(personDtriver.mail, subject, body);
                //return true;
            }

        }

        public static void UpdateRequestIgnoreOffers(int reqId, int offerId)
        {
            using (SafeDrivingEntities sd = new SafeDrivingEntities())
            {
                var req = sd.requests.FirstOrDefault(x => x.id == reqId);
                if (!string.IsNullOrEmpty(req.ignore_offers))
                {
                    var igOffersList = req.ignore_offers.Split(',');
                    if (!igOffersList.Contains(offerId.ToString()))
                    {
                        req.ignore_offers = req.ignore_offers + "," + offerId;
                    }
                }
                else
                {
                    req.ignore_offers = offerId.ToString();
                }

                sd.SaveChanges();

                //return true;
            }
        }

    }
}
