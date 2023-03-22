using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class RegisterModel
    {
            
/*        public int Id { get; set; }
*/
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        /*[Required]*/
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        /*[Required]*/
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
