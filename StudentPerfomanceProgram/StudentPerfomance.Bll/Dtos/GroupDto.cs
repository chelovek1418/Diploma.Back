using System.Collections.Generic;

namespace StudentPerfomance.Bll.Dtos
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Faculty { get; set; }
        public string Speciality { get; set; }
        public string Specialization { get; set; }
        public int Year { get; set; }
        public TeacherDto Curator { get; set; }
        public StudentDto Headmen { get; set; }
        public IEnumerable<StudentDto> Students { get; set; }

        public GroupDto()
        {
            Students = new List<StudentDto>();
        }
    }
}
