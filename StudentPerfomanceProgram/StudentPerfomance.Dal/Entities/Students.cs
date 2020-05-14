using System.Collections.Generic;

namespace StudentPerfomance.Dal.Entities
{
    public partial class Students
    {
        public Students()
        {
            Marks = new HashSet<Marks>();
        }

        public int Id { get; set; }
        public int GroupId { get; set; }

        public virtual Groups Group { get; set; }
        public virtual User IdNavigation { get; set; }
        public virtual ICollection<Marks> Marks { get; set; }
    }
}
