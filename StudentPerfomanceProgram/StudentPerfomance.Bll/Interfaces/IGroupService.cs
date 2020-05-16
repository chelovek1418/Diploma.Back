using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Interfaces
{
    public interface IGroupService : ICrudService<Group, GroupDto>
    {
        Task<GroupDto> GetWithMarksByLesson(int groupId, int lessonId, DateTime date);
        Task AddLesson(int groupId, int lessonId);
        Task DropLesson(int groupId, int lessonId);
        Task<bool> CheckTitleAsync(string title);
        Task<IEnumerable<GroupDto>> GetByLessonAsync(int id);
        Task<IEnumerable<GroupDto>> SearchGroupsAsync(string term);
    }
}
