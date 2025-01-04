using DAL;
using System.Collections.Generic;
using System.Linq;
using DTO;
using System.ComponentModel;
using System.Reflection;


namespace BL
{
    public class PersonInfo
    {
        public PersonDto Person { get; set; }
        public ErrorTypes ErrorType { get; set; }
        public bool Status
        {
            get; set;
        }
    }
    public enum ErrorTypes
    {
        errorEmail,
        errorPassword,
        errorDisable,
        personAlreadyExist
    }
}
