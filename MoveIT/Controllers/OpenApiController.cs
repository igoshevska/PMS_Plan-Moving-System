using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReference1;
using System;
using Microsoft.AspNetCore.Http;

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Extensions.Localization;

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
