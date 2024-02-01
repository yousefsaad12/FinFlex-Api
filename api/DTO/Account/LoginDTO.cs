using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Account
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Must have UserName")]
        public string ? UserName { get; set; }

         [Required (ErrorMessage = "Must have PassWord")]
        public string ? Password { get; set; }
    }
}