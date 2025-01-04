using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BL.Convertions
{
    class RequestsConvertion
    {


        public static requests DtoToRequest(RequestsDto t1)
        {
            requests request = new requests();
            request.id = t1.id;
            request.id_person = t1.id_person;
            request.resourse = t1.resourse;
            request.destination = t1.destination;
            request.resourse_city = t1.resourse_city;
            request.destination_city = t1.destination_city;
            request.seats = t1.seats;
            request.date_time = t1.date_time;
            request.active = t1.active;
            request.remarks = t1.remarks;
            request.email_offers = t1.email_offers;

            return request;

        }
        public static RequestsDto RequestToDto(requests t1)
        {
            RequestsDto request = new RequestsDto();
            request.id = t1.id;
            request.id_person = t1.id_person;
            request.resourse = t1.resourse;
            request.destination = t1.destination;
            request.seats = t1.seats;
            request.resourse_city = t1.resourse_city;
            request.destination_city = t1.destination_city;
            request.remarks = t1.remarks;
            request.email_offers = t1.email_offers;



            request.date_time = t1.date_time;
            request.active = t1.active;

            if (t1.persons != null)
            {
                request.driver = PersonConvertion.PersonToDto(t1.persons);
            }
            return request;
        }
    }
}
