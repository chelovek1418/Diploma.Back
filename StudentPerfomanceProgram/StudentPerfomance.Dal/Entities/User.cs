using StudentPerfomance.Dal.Interfaces;

namespace StudentPerfomance.Dal.Entities
{
    public class User : IDbEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Department { get; set; }
        public byte[] Photo { get; set; }

        public Student Student { get; set; }
        public Teacher Teacher { get; set; }
    }
}
