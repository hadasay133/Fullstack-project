using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BL.Convertions
{
    class offersConvertion
    {
       

        public static offers DtoToOffer(OffersDto t1)
        {
            offers offer = new offers();
            offer.id = t1.id;
            offer.id_person = t1.id_person;
            offer.resourse = t1.resourse;
            offer.destination = t1.destination;
            offer.resourse_city = t1.resourse_city;
            offer.destination_city = t1.destination_city;
            offer.seats = t1.seats;
            offer.date_time = t1.date_time;
            offer.price = t1.price;
            offer.remarks = t1.remarks;
            offer.email_requests = t1.email_requests;

            offer.active = t1.active;
            return offer;

        }
        public static OffersDto OfferToDto(offers t1)
        {
            OffersDto offer = new OffersDto();
            offer.id = t1.id;
            offer.id_person = t1.id_person;
            offer.resourse = t1.resourse;
            offer.destination = t1.destination;
            offer.resourse_city = t1.resourse_city;
            offer.destination_city = t1.destination_city;
            offer.seats = t1.seats;
            offer.date_time = t1.date_time;
            offer.price = t1.price;
            offer.remarks = t1.remarks;
            offer.email_requests = t1.email_requests;
            
            offer.active = t1.active;
            return offer;
        }
    }
}
