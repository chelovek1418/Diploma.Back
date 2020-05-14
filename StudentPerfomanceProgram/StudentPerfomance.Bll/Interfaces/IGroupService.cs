using StudentPerfomance.Bll.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Interfaces
{
    public interface IGroupService : ICrudService<GroupDto>
    {
        Task<GroupDto> GetWithMarksByLesson(int groupId, int lessonId, DateTime date);
        Task AddLesson(int groupId, int lessonId);
        Task DropLesson(int groupId, int lessonId);
        Task<bool> CheckTitleAsync(string title);
        IAsyncEnumerable<GroupDto> GetByLessonAsync(int id);
        IAsyncEnumerable<GroupDto> SearchGroupsAsync(string term);
    }
}
