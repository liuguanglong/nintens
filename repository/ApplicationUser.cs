using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository
{
    public class ApplicationUser : IdentityUser
    {
        public string? CustomTag { get; set; }
        public string? TimeZoneId { get; set; } = TimeZoneInfo.Local.Id;
    }
}
