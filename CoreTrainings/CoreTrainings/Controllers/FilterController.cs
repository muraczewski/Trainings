using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreTrainings.Controllers
{
    [Produces("application/json")]
    [Route("api/Filter")]
    public class FilterController : Controller
    {
        [HttpGet]
        [Route("Exception")]
        public void Exception()
        {
            throw new NotSupportedException("just test");
        }
    }
}