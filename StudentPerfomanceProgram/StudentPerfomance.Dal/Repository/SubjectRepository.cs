using Microsoft.EntityFrameworkCore;
using StudentPerfomance.Dal.Entities;
using StudentPerfomance.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPerfomance.Dal.Repository
{
    public class SubjectRepository : AbstractRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(StudentPerfomanceDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Subject>> GetLessonsWithMarksForTimeByStudentId(int studentId, DateTime startDate, DateTime endDate)
        {
            var lessons = await dbContext.Subjects
            .Include(l => l.Marks)
            .Where(l => l.GroupSubjects.Any(g => g.Group.Students.Any(s => s.Id == studentId))).ToListAsync();
            lessons.ForEach(x => x.Marks = x.Marks.Where(m => m.StudentId == studentId && m.Date >= startDate && m.Date <= endDate).ToList());
            return lessons;
        }
    }
}
