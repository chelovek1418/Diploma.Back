using System.Collections.Generic;

namespace StudentPerfomance.Bll.Dtos
{
    public class LessonDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<MarkDto> Marks {get;set;}
    }
}
