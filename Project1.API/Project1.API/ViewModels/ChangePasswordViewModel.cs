using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project1.API.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        [Compare("ConfirmPassword",ErrorMessage="{0} didn't match.")]
        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}