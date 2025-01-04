using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace UI.Controllers
{
    
    public class HomeController : ApiController
    {
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> IndexAsync()
        {
            var x = await BL.GeneralLogic.SendSimpleMessage();
            return Ok(x);
        }

    }
}
