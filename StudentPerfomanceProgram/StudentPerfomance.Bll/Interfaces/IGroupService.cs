using StudentPerfomance.Bll.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Interfaces
{
    public interface IGroupService : ICrudService<GroupDto>
    {
        Task AddLesson(int groupId, int lessonId);
        Task DropLesson(int groupId, int lessonId);
    }
}
