﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ammeg.Blog.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
    }
}