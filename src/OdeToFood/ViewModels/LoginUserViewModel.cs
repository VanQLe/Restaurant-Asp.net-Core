using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.ViewModels
{
    public class LoginUserViewModel
    {
        [Required]
        public string  UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password{ get; set; }
        
        [Display(Name ="Remeber Me")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
