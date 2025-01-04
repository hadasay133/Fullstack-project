using DAL;
using System.Collections.Generic;
using System.Linq;
using DTO;
using System;

namespace BL
{
    public class PersonLogic
    {



        public static PersonInfo Login(string email, string password)
        {

            PersonInfo result = new PersonInfo();

            using (var sd = new SafeDrivingEntities())
            {
                if (sd.persons.FirstOrDefault(x => x.mail == email) == null)
                {
                    result.Status = false;
                    result.ErrorType = ErrorTypes.errorEmail;
                    return result;
                }
                if (sd.persons.FirstOrDefault(x => x.password == password) == null)
                {
                    result.Status = false;
                    result.ErrorType = ErrorTypes.errorPassword;
                    return result;
                }
                if (sd.persons.FirstOrDefault(x => x.ok == true  && x.mail == email && x.password == password) == null)
                {
                    result.Status = false;
                    result.ErrorType = ErrorTypes.errorDisable;
                    return result;
                }
                result.Person = Convertions.PersonConvertion.PersonToDto(sd.persons.FirstOrDefault(x => x.password == password && x.mail == email));
                result.Status = true;
            }
            return result;
        }

        public static List<PersonDto> GetPersonStatus()
        {

            PersonInfo result = new PersonInfo();

            using (var sd = new SafeDrivingEntities())
            {
                var pr = sd.persons.Where(x => x.is_manager == false).ToList();

                var personDtoList = new List<PersonDto>();
                for (int i = 0; i < pr.Count; i++)
                {
                    personDtoList.Add(Convertions.PersonConvertion.PersonToDto(pr[i]));
                }
                return personDtoList;
            }

        }

        public static PersonDto GetPersonById(int id)
        {

            PersonInfo result = new PersonInfo();

            using (var sd = new SafeDrivingEntities())
            {
                var pr = sd.persons.FirstOrDefault(x => x.id == id);


                return Convertions.PersonConvertion.PersonToDto(pr);

            }

        }


        public static PersonInfo signUp(PersonDto personDto)
        {
            PersonInfo result = new PersonInfo();
            persons person = new persons();
            person = Convertions.PersonConvertion.DtoToPerson(personDto);
            SafeDrivingEntities sd = new SafeDrivingEntities();
            if (sd.persons.FirstOrDefault(x => x.mail == personDto.mail && x.id == personDto.id) != null)
            {
                result.ErrorType = ErrorTypes.personAlreadyExist;

                return result;
            }
            result.Status = true;
            result.Person = personDto;
            sd.persons.Add(person);
            //לדאוג שהסטטוס יהיה כבוי
            sd.SaveChanges();
            return result;
        }



        public static bool UpdatePerson(PersonDto personDto)
        {

            using (var sd = new SafeDrivingEntities())
            {
                var pr = sd.persons.FirstOrDefault(x => x.id == personDto.id);
                pr.adress = personDto.adress;
                pr.tz = personDto.tz;
                pr.inqure = personDto.inqure;
                pr.mail = personDto.mail;
                pr.ok = personDto.ok;
                pr.phone = personDto.phone;
                pr.username = personDto.username;

                sd.SaveChanges();
            }
            return true;
        }


    }
}
