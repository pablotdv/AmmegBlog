using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ammeg.Blog.Areas.Admin.Controllers
{
    [Authorize("Administradores")]
    [Area("Admin")]
    public class Controller : Microsoft.AspNetCore.Mvc.Controller
    {
    }
}
