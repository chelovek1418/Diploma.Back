using System.Collections.Generic;

namespace StudentPerfomance.Bll.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        public GroupDto Group { get; set; }
        public UserDto User { get; set; }
        public IEnumerable<MarkDto> Marks { get; set; }
    }
}
