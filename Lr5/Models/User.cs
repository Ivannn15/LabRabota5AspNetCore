﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace Lr5.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string Password { get; set; }
        public bool isAuthorized { get; set; }
    }
}
