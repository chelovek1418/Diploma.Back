﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace StudentPerfomance.Dal.Entities
{
    public partial class Users : IdentityUser<int>
    {
        public Users()
        {
            UsersLessons = new HashSet<UsersLessons>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Photo { get; set; }

        public virtual Students Students { get; set; }
        public virtual ICollection<UsersLessons> UsersLessons { get; set; }
    }
}
