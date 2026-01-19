using System;
using System.Collections.Generic;
using System.Text;

namespace SR.HomeServices.Application.DTOs
{
    public class ForgotPasswordRequestDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
