using System;
using System.Collections.Generic;
using System.Text;

namespace StudentPerfomance.Bll.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public UserDto User { get; set; }
    }
}
