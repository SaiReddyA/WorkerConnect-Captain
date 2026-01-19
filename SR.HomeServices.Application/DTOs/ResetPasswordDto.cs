using System;
using System.Collections.Generic;
using System.Text;

namespace SR.HomeServices.Application.DTOs
{
    public class ResetPasswordDto
    {
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string NewPassword { get; set; } = string.Empty;
    }
}
