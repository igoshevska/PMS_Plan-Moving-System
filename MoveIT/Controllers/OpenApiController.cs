using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoveIT.Controllers
{
    [AllowAnonymous]
    public class OpenApiController : Controller
    {

        public IActionResult VerifyConnection()
        {
            return View();
        }
    }
}
