using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication_Layer.Auth_Models
{
    public class User : IdentityUser<Guid>
    {

        public string Name { get; set; }
        public string AccoutType { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
        public bool IsDeleted { get; set; }
    }
}
