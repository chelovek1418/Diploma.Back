using StudentPerfomance.Dal.Entities;
using System;
using System.Collections.Generic;

namespace StudentPerfomance.Dal.Interfaces
{
    public interface ILessonRepository : IRepository<Lessons>
    {
        IAsyncEnumerable<Lessons> GetLessonsByGroup(int groupId);
    }
}
