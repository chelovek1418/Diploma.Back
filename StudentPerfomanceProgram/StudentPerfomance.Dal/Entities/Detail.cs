using StudentPerfomance.Dal.Interfaces;

namespace StudentPerfomance.Dal.Entities
{
    public class Detail : IDbEntity
    {
        public int Id { get; set; }
        public int DayOfWeek { get; set; }
        public int Pair { get; set; }
        public int GroupSubjecttId { get; set; }
        public GroupSubject GroupSubject { get; set; }
        public bool? IsNumerator {get;set;}
    }
}
