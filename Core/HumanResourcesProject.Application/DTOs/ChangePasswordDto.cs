﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Application.DTOs
{
    public class ChangePasswordDto
    {
        public string UserId { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }

    }
}
