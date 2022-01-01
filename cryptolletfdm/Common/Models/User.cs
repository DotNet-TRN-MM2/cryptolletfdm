﻿using System;
using cryptolletfdm.Common.Database;

namespace cryptolletfdm.Common.Models
{
    public class User : BaseDatabaseItem
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
    }
}
