using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TransportDisplay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController
    {
        // [HttpGet]
        // public ActionResult<PingResponse> Get()
        // {
        //     return new HttpResponse(new PingResponse { Time = DateTime.Now });
        // }

        public class PingResponse
        {
            public DateTime Time { get; set; }
        }
    }
}
