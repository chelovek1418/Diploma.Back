using System.Collections.Generic;

namespace StudentPerfomance.Bll.Dtos
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<StudentDto> Students { get; set; }

        public GroupDto()
        {
            Students = new List<StudentDto>();
        }
    }
}
