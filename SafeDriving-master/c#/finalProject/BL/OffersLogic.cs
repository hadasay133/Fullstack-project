using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using System.Net.Mail;
using System.Net;

namespace BL
{
    public class OffersLogic
    {
        //הכנסת נסיעה
        public static int add(OffersDto offer)
        {
            offers p1 = new offers();
            p1 = Convertions.offersConvertion.DtoToOffer(offer);
            SafeDrivingEntities sd = new SafeDrivingEntities();
            sd.offers.Add(p1);

            sd.SaveChanges();
            //List<RequestsDto> list_requests = search(offer);
            return p1.id;
        }

        public static int UpdateOffer(OffersDto offer)
        {
            using (var sd = new SafeDrivingEntities())
            {
                var off = sd.offers.FirstOrDefault(x => x.id == offer.id);
                off.price = offer.price;
                off.remarks = offer.remarks;
                off.resourse_city = offer.resourse_city;
                off.resourse = offer.resourse;
                off.destination = offer.destination;
                off.destination_city = offer.destination_city;
                off.seats = offer.seats;
                off.date_time = offer.date_time;
                sd.SaveChanges();
            }


            //List<RequestsDto> list_requests = search(offer);
            return offer.id;
        }


        public static List<RequestsDto> search(OffersDto offer)
        {
            //להוסיף לנסיעות משתנה בוליאני כד לדעת מה פעיל ולבדוק גם על פי זה
            SafeDrivingEntities sd = new SafeDrivingEntities();
            //בדיקה לפי מוצא ,יעד ותאריך
            List<requests> it = sd.requests.Where(x => x.resourse == offer.resourse && x.destination == offer.destination
            && x.date_time.Date == offer.date_time.Date).ToList();
            //המרה לdto
            List<RequestsDto> requests = new List<RequestsDto>();
            for (int i = 0; i < it.Count; i++)
            {
                requests.Add(Convertions.RequestsConvertion.RequestToDto(it[i]));
            }
            return requests;
        }

        public static int CheckSetOfferWithRequests(int id, int reqId)
        {
            using (var sd = new SafeDrivingEntities())
            {
                //בדיקה לפי מוצא ,יעד ותאריך
                var offers = sd.offers.Include("persons").FirstOrDefault(x => x.id == id);
                if (offers == null)
                {
                    return 5;
                }
                var requests = sd.requests.Include("persons").FirstOrDefault(x => x.id == reqId);
                if (requests == null)
                {
                    return 6;
                }


                var travles = sd.travels.Include("requests").Where(x => x.id_offer == id);


                var travlesSeats = 0;
                if (travles.Any())
                {
                    travlesSeats = travles.Sum(x => x.requests.seats);


                }

                var offersSeats = offers.seats;
                if (travlesSeats + requests.seats <= offersSeats)
                {
                    var travelReq = sd.travels.FirstOrDefault(x => x.id_request == reqId
                    && x.id_offer == id);
                    if (travelReq != null)
                        return 3;

                    var travelReq2 = sd.travels.Where(x => x.id_request == reqId).ToList();
                    if (travelReq2.Count > 0)
                    {
                        var reqTr = travelReq2.FirstOrDefault(x => x.id_offer != id);
                        if (reqTr != null)
                        {
                            return 7;
                        }

                        return 4;

                    }
                    var newTravel = new travels() { id_offer = id, id_request = reqId };
                    sd.travels.Add(newTravel);
                    requests.active = false;
                    if (!string.IsNullOrEmpty(requests.ignore_offers)
                    && requests.ignore_offers.Split(',').ToList().Contains(id.ToString()))
                    {

                        var ig = requests.ignore_offers.Split(',').ToList();
                        ig.Remove(offers.id.ToString());
                        requests.ignore_offers = String.Join(",", ig);
                    }
                    sd.SaveChanges();
                    return 1;
                }
                return 2;
            }
        }
        public static int GetNumSeatsByOfferId(int id)
        {
            using (var sd = new SafeDrivingEntities())
            {
                var travles = sd.travels.Include("requests").Where(x => x.id_offer == id);
                var travlesSeats = 0;
                if (travles.Any())
                {
                    travlesSeats = travles.Sum(x => x.requests.seats);


                }
                return travlesSeats;
            }
        }

        public static int IgnoreOfferWithRequests(int id, int reqId)
        {
            var msg = 1;
            using (var sd = new SafeDrivingEntities())
            {
                //בדיקה לפי מוצא ,יעד ותאריך
                var offers = sd.offers.Include("persons").FirstOrDefault(x => x.id == id);
                if (offers == null)
                {
                    return 4;
                }
                var requests = sd.requests.Include("persons").FirstOrDefault(x => x.id == reqId);
                if (requests == null)
                {
                    return 5;
                }
                var trs = sd.travels.Where(x => x.id_request == reqId && x.id_offer != id).ToList();
                foreach (var tt in trs)
                {
                    sd.travels.Remove(tt);
                    sd.SaveChanges();
                }

                var travelReq = sd.travels.FirstOrDefault(x => x.id_request == reqId
                   && x.id_offer == id);
                if (travelReq != null)
                {
                    sd.travels.Remove(travelReq);
                    requests.active = true;
                    sd.SaveChanges();
                    msg = 2;
                }
                else if (!string.IsNullOrEmpty(requests.ignore_offers)
                    && requests.ignore_offers.Split(',').ToList().Contains(offers.id.ToString()))
                {
                    return 3;
                }


                if (!string.IsNullOrEmpty(requests.ignore_offers))
                {
                    var ig = requests.ignore_offers.Split(',').ToList();
                    ig.Add(offers.id.ToString());
                    requests.ignore_offers = String.Join(",", ig);
                    sd.SaveChanges();
                }
                else
                {
                    requests.ignore_offers = id.ToString();
                    sd.SaveChanges();
                }
                return msg;
            }
        }

        // החזרת רשימת נסיעות לפי תעודת זהות
        public static List<OffersDto> GetByPersonId(int id)
        {//לבדוק אם הנסיעות פעילות
            using (var sd = new SafeDrivingEntities())
            {
                var offer = sd.offers.Where(x => x.id_person == id
                     && x.date_time >= DateTime.Now).ToList();
                List<OffersDto> offers = new List<OffersDto>();
                for (int i = 0; i < offer.Count; i++)
                {
                    offers.Add(Convertions.offersConvertion.OfferToDto(offer[i]));
                }

                // sendEmail("m058320294@gmail.com", "m058320294@gmail.com");
                return offers;
            }
        }

        // החזרת רשימת נסיעות לפי תעודת זהות
        public static List<OffersDto> GetHistoryByPersonId(int id)
        {//לבדוק אם הנסיעות פעילות
            using (var sd = new SafeDrivingEntities())
            {
                var offer = sd.offers.Where(x => x.id_person == id
                     && x.date_time < DateTime.Now).ToList();
                List<OffersDto> offers = new List<OffersDto>();
                for (int i = 0; i < offer.Count; i++)
                {
                    var off = Convertions.offersConvertion.OfferToDto(offer[i]);
                    off.requests = new List<RequestsDto>();

                    var travelReq = sd.travels.Where(x => x.id_offer == off.id).Select(x => x.id_request).ToList();
                    if (travelReq.Any())
                    {
                        var requestsVal = sd.requests.Where(x => travelReq.Contains(x.id)).ToList();
                        foreach (var req in requestsVal)
                        {
                            var reqDto = Convertions.RequestsConvertion.RequestToDto(req);
                            var person = sd.persons.FirstOrDefault(x => x.id == req.id_person);
                            reqDto.driver = person == null ? null : Convertions.PersonConvertion.PersonToDto(person);
                            off.requests.Add(reqDto);

                        }
                    }
                    offers.Add(off);
                }

                // sendEmail("m058320294@gmail.com", "m058320294@gmail.com");
                return offers;
            }
        }


        public static List<OffersDto> GetAllActiveOffers()
        {//לבדוק אם הנסיעות פעילות
            using (var sd = new SafeDrivingEntities())
            {
                var offerData = sd.offers.Include("persons").Where(x => x.active == true
                     && x.date_time >= DateTime.Now && x.persons.ok == true).ToList();

                var offers = new List<OffersDto>();
                for (int i = 0; i < offerData.Count; i++)
                {
                    var off = Convertions.offersConvertion.OfferToDto(offerData[i]);
                    off.requests = new List<RequestsDto>();

                    //var travelReq = sd.travels.Where(x => x.id_offer == off.id).Select(x => x.id_request).ToList();
                    //if (!travelReq.Any())
                    //{
                    //    var reqsList = 

                    //}
                    off.numSeats = GetNumSeatsByOfferId(offerData[i].id);
                    off.driver = Convertions.PersonConvertion.PersonToDto(offerData[i].persons);
                    offers.Add(off);


                }


                return offers;
            }
        }
        public static List<OffersDto> GetAllActiveOffersByReqId(int id)
        {

            using (var sd = new SafeDrivingEntities())
            {
       
                var req = sd.requests.FirstOrDefault(x => x.id == id);

                var beginHour = req.date_time.AddHours(-3);
                var endHour = req.date_time.AddHours(+3);

                var offerData = sd.offers.Include("persons").Where(x => x.resourse_city == req.resourse_city
                && x.destination_city == req.destination_city
                  && x.date_time >= beginHour && x.id_person != req.id_person
                 && x.date_time <= endHour && x.active == true && x.persons.ok == true).ToList();



                var offers = new List<OffersDto>();
                for (int i = 0; i < offerData.Count; i++)
                {
                    var off = Convertions.offersConvertion.OfferToDto(offerData[i]);
                    off.requests = new List<RequestsDto>();
                    off.numSeats = GetNumSeatsByOfferId(offerData[i].id);
                    off.driver = Convertions.PersonConvertion.PersonToDto(offerData[i].persons);
                    if (!string.IsNullOrEmpty(off.email_requests)
                               && off.email_requests.Split(',').ToList().Contains(id.ToString()))
                    {
                        off.is_emailed = true;
                    }
                    offers.Add(off);


                }


                return offers;
            }
        }


        public static List<OffersDto> GetWithNoRequestsByPersonId(int id)
        {//לבדוק אם הנסיעות פעילות
            using (var sd = new SafeDrivingEntities())
            {
                var offer = sd.offers.Where(x => x.id_person == id
                     && x.date_time >= DateTime.Now).ToList();
                List<OffersDto> offers = new List<OffersDto>();
                for (int i = 0; i < offer.Count; i++)
                {
                    var off = Convertions.offersConvertion.OfferToDto(offer[i]);
                    off.requests = new List<RequestsDto>();

                    var travelReq = sd.travels.Where(x => x.id_offer == off.id).Select(x => x.id_request).ToList();
                    if (!travelReq.Any())
                    {
                        offers.Add(off);
                    }

                }


                return offers;
            }
        }

        public static List<OffersDto> GetWithRequestsByPersonId(int id)
        {//לבדוק אם הנסיעות פעילות
            using (var sd = new SafeDrivingEntities())
            {
                var offer = sd.offers.Where(x => x.id_person == id
                     && x.date_time >= DateTime.Now).ToList();
                List<OffersDto> offers = new List<OffersDto>();
                for (int i = 0; i < offer.Count; i++)
                {
                    var off = Convertions.offersConvertion.OfferToDto(offer[i]);
                    off.requests = new List<RequestsDto>();

                    var travelReq = sd.travels.Where(x => x.id_offer == off.id).Select(x => x.id_request).ToList();
                    if (travelReq.Any())
                    {
                        var requestsVal = sd.requests.Where(x => travelReq.Contains(x.id)).ToList();

                        foreach (var req in requestsVal)
                        {
                            var reqDto = Convertions.RequestsConvertion.RequestToDto(req);
                            var person = sd.persons.FirstOrDefault(x => x.id == req.id_person);
                            reqDto.driver = person == null ? null : Convertions.PersonConvertion.PersonToDto(person);
                            off.requests.Add(reqDto);

                        }
                        offers.Add(off);
                    }

                }


                return offers;
            }
        }

        //החזרת נסיעה לפי מספר נסיעה
        public static OffersDto GetById(int id)
        {
            SafeDrivingEntities sd = new SafeDrivingEntities();

            OffersDto offer = Convertions.offersConvertion.OfferToDto(sd.offers.FirstOrDefault(x => x.id == id));


            return offer;
        }
        public static bool deleteOffer(int id)
        {
            using (var sd = new SafeDrivingEntities())
            {
                var trs = sd.travels.Where(r => r.id_offer == id).ToList();
                foreach (var tr in trs)
                {
                    sd.travels.Remove(tr);
                    sd.SaveChanges();
                }
                var off = sd.offers.FirstOrDefault(r => r.id == id);
                sd.offers.Remove(off);
                sd.SaveChanges();
            }
            return true;//לבדוק שזה נמחק
        }

        public static async Task<bool> RemoveOfferWithTravels(int id)
        {
            using (var sd = new SafeDrivingEntities())
            {
                var off = sd.offers.Include("persons").FirstOrDefault(r => r.id == id);

                var trs = sd.travels.Where(r => r.id_offer == id).ToList();
                foreach (var tr in trs)
                {
                    var reqs = sd.requests.Include("persons").FirstOrDefault(x => x.id == tr.id_request);
                    reqs.active = true;

                    if (!string.IsNullOrEmpty(reqs.ignore_offers))
                    {
                        var igOffersList = reqs.ignore_offers.Split(',').ToList();
                        if (igOffersList.Contains(id.ToString()))
                        {
                            igOffersList.Remove(id.ToString());
                            reqs.ignore_offers = string.Join(",", igOffersList);
                        }
                    }
                    if (!string.IsNullOrEmpty(reqs.email_offers))
                    {
                        var igOffersList = reqs.email_offers.Split(',').ToList();
                        if (igOffersList.Contains(id.ToString()))
                        {
                            igOffersList.Remove(id.ToString());
                            reqs.email_offers = string.Join(",", igOffersList);
                        }
                    }
                    sd.SaveChanges();
                    StringBuilder htmlBody = new StringBuilder("<div style='text-align: center; color: #2c366e; width: 500px; border: 3px solid #2c366e;font-size:18px;'>" +
"<p>הי " + reqs.persons.username + "</p>" +
"<p>הנסיעה אליה הצטרפת בוטלה על ידי הנהג</p> " +
"<p>נסיעה מ" + reqs.resourse + " " + reqs.resourse_city + "</p> " +
"<p>ליעד " + reqs.destination + " " + reqs.destination_city + "  </p> " +
"<p>תאריך ושעת יציאה " + reqs.date_time.ToString("dd/MM/yyyy HH:mm") + " </p> " +
"<p>פרטי הנהג " + off.persons.mail + " | " + off.persons.phone + " | " + off.persons.username + "   </p> " +
"<p>תוכל/י להכנס למערכת ולמצוא נסיעה מתאימה אחרת</p> " +
"<p><a href='http://localhost:4200/privateArea/ActiveOffers' style=' min-width:100px; " +
"    padding: 6px 30px;text-decoration:none;    font-size: 18px;    line-height: 28px;    margin: 10px auto; " +
"    border: none;    cursor: pointer;  background: #2c366e;    border-radius: 0px; " +
"    color: #ffdf00;    font-weight: 500;    border: 1px solid #2c366e; " +
"    height: auto;    box-shadow: none;    transition: 0.2s linear all; " +
"    border-radius: 20px;'>  כניסה לאתר</a> </p> " +
"</div>");




                    await GeneralLogic.SendEmailMessage(reqs.persons.mail, htmlBody.ToString(), "ביטול הנסיעה");

                    sd.travels.Remove(tr);
                    sd.SaveChanges();
                }
                sd.offers.Remove(off);
                sd.SaveChanges();
            }
            return true;//לבדוק שזה נמחק
        }




        public static void sendEmail(string from, string to)
        {
            //MailMessage mail = new MailMessage();
            //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            // Smtp יצירת אוביקט 
            //     SmtpClient smtp = new SmtpClient();

            //הגדרת השרת של גוגל
            //      smtp.Host = "smtp.gmail.com";
            //      mail.From = new MailAddress(from);
            //     mail.To.Add(to);
            //      mail.Subject = "Test Mail";
            //     mail.Body = "This is for testing SMTP mail from GMAIL";

            // SmtpServer.Port = 587;
            //    smtp.Credentials = new System.Net.NetworkCredential("m0583202944@gmail.com", "matti2944");
            //    smtp.EnableSsl = true;

            //   smtp.Send(mail);



            string fromaddr = "m0583202944@gmail.com";
            string toaddr = "m0583202944@gmail.com";
            string password = "matti2944";

            MailMessage msg = new MailMessage();
            msg.Subject = "Username &password";
            msg.From = new MailAddress(fromaddr);
            msg.Body = "Message BODY";
            msg.To.Add(new MailAddress(toaddr));
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            NetworkCredential nc = new NetworkCredential(fromaddr, password);
            smtp.Credentials = nc;
            smtp.Send(msg);
        }

        //החזרת נסיעה לפי מספר נסיעה
        public static bool selectOfferByRequestId(int offerId, int reqId)
        {
            using (SafeDrivingEntities sd = new SafeDrivingEntities())
            {

                //            var offer = sd.offers.FirstOrDefault(x => x.id == offerId);
                //            var personDtriver = sd.persons.FirstOrDefault(x => x.id == offer.id_person);

                //            var req = sd.requests.FirstOrDefault(x => x.id == reqId);
                //            var personReq = sd.persons.FirstOrDefault(x => x.id == req.id_person);

                //            var subject = "בקשת הצטרפות לנסיעה";
                //            var body = "<h1 style='color: #5e9ca0; text-align: right;'>&nbsp; !שלום נהג " + personDtriver.username + "</h1>" +
                //"<h1 style='color: #5e9ca0; text-align: right;'>?" + personReq.username + " האם תרצה לצרף לנסיעה את נוסע</h1> " +
                //"<p style='text-align: right; ;display: inline-block;'><strong><a style='background-color: #5e9ca0; padding: 10px; color: white;' href='http://localhost:4200/'>אשר</a></strong></p> " +
                //"<p style='display: inline-block; text-align: left;'><strong><a style='background-color: #5e9ca0; padding: 10px; color: white;' href='http://localhost:4200/ignore-request?reqid=" + reqId + "&offerid="
                //+ offerId + "'>סרב</a></strong></p>";

                //            await GeneralLogic.sendEmailAsync(personDtriver.mail, subject, body);
                return true;
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
