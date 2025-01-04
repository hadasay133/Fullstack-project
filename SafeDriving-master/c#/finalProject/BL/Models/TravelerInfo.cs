using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
  public  class TravelerInfo
    {
        public ErrorTypes ErrorType { get; set; }
        public bool Status { get; set; }




        public enum ErrorTypes
        {
            this_drive_not_extist

        }
    }
}
