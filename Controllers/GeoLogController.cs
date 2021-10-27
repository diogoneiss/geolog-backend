using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoLogBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GeoLogController : ControllerBase
    {
       
    }
}
