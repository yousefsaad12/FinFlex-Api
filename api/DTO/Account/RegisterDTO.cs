using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Account
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Must have UserName")]
        public string ? UserName { get; set; }

        [Required(ErrorMessage = "Must have Email")]
        [EmailAddress]
        public string ? Email { get; set; }

        [Required]
        public string ? Password { get; set; }
    }
}