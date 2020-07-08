using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Thinktecture.Docker.WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("health")]
    public class HealthController : Controller
    {

        [HttpGet]
        [Route("ready")]
        public IActionResult IsReady()
        {
            return Ok();
        }

        [HttpGet]
        [Route("alive")]
        public IActionResult IsAlive()
        {
            return Ok();
        }
    }
}
