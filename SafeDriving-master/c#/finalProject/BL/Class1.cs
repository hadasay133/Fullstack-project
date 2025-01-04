using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BL
{
    public class Class1
    {
        SafeDrivingEntities sd = new SafeDrivingEntities();
        public List<offers> drivers()//רשימת כל הנהגים 
        {
            return sd.offers.ToList();
        }
        public Boolean get_driver(int id)
        {
         //   sd.drivers.First(tz => drivers.tz);
            return false;
        }
        public List<persons> names()//רשימת כל האנשים 
        {
            return sd.persons.ToList();
        }
        public List<requests> travelers()//רשימת כל הנוסעים 
        {
            return sd.requests.ToList();
        }
    }


}
