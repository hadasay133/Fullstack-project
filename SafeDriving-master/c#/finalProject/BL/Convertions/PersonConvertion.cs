using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BL.Convertions
{
    class PersonConvertion
    {
      
        public static persons DtoToPerson(PersonDto personDto)
        {
            persons person = new persons();
            person.id = personDto.id;
            person.adress = personDto.adress;
            person.tz = personDto.tz;
            person.mail = personDto.mail;
            person.inqure = personDto.inqure;
            person.ok = personDto.ok;
            person.phone = personDto.phone;
            person.username = personDto.username;
            person.password = personDto.password;
            person.is_manager = personDto.is_manager;

            return person;
        }
        public static PersonDto PersonToDto(persons person)
        {
            PersonDto personDto = new PersonDto();
            personDto.id = person.id;
            personDto.adress = person.adress; 
            personDto.tz = person.tz;
            personDto.mail = person.mail;
            personDto.inqure = person.inqure;
            personDto.ok = person.ok;
            personDto.phone = person.phone;
            personDto.username = person.username;
            personDto.is_manager = person.is_manager;

            return personDto;
        }


    }
}
