using System;
using System.Collections.Generic;

namespace StudentPerfomance.Dal.Entities
{
    public partial class User
    {
        public User()
        {
            UsersLessons = new HashSet<UsersLessons>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Photo { get; set; }

        public virtual Students Students { get; set; }
        public virtual ICollection<UsersLessons> UsersLessons { get; set; }
    }
}
