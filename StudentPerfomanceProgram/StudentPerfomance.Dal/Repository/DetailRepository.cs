using Microsoft.EntityFrameworkCore;
using StudentPerfomance.Dal.Entities;
using StudentPerfomance.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPerfomance.Dal.Repository
{
    public class DetailRepository : AbstractRepository<Detail>, IDetailRepository
    {
        public DetailRepository(StudentPerfomanceDbContext context) : base(context)
        {
        }

        public override async Task<int> CreateAsync(Detail model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(Detail));

            var existing = await GetExisting(model);

            if (existing == null)
                return await base.CreateAsync(model);

            existing.IsNumerator = model.IsNumerator;
            await UpdateAsync(existing);

            return existing.Id;
        }

        public override async Task UpdateAsync(Detail detail)
        {
            if (detail == null)
                throw new ArgumentNullException(nameof(Detail));

            var dbDetail = await dbContext.Details.FindAsync(detail.Id);
            if (dbDetail == null)
                throw new NullReferenceException(nameof(Detail));

            dbDetail.IsNumerator = detail.IsNumerator;
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Detail>> GetScheduleForGroup(int groupId, int semestr) => await Task.Run(() => dbContext.Details
                .Include(x => x.Teacher).ThenInclude(t => t.User)
                .Include(x => x.GroupSubject).ThenInclude(g => g.Subject)
                .Where(x => x.GroupSubject.GroupId == groupId && x.Semestr == semestr));

        public async Task<IEnumerable<Detail>> GetScheduleForTeacher(int teacherId, int semestr) => await Task.Run(() => dbContext.Details
                .Include(x => x.GroupSubject).ThenInclude(g => g.Group)
                .Include(x => x.GroupSubject).ThenInclude(g => g.Subject)
                .Where(x => x.TeacherId == teacherId && x.Semestr == semestr));

        public async Task<IEnumerable<Detail>> GetScheduleForTeacheByLessonAndGroup(int teacherId, int lessonId, int groupId, int semestr) => await Task.Run(() => dbContext.Details
                .Include(x => x.GroupSubject).ThenInclude(g => g.Group)
                .Include(x => x.GroupSubject).ThenInclude(g => g.Subject)
                .Where(x => x.TeacherId == teacherId && x.Semestr == semestr && x.GroupSubject.SubjectId == lessonId && x.GroupSubject.GroupId == groupId));

        private async Task<Detail> GetExisting(Detail model) => await dbContext.Details.FirstOrDefaultAsync(d => d.GroupSubjectId == model.GroupSubjectId &&
                    d.Semestr == model.Semestr &&
                    d.DayOfWeek == model.DayOfWeek &&
                    d.Pair == model.Pair &&
                    (d.IsNumerator == null || model.IsNumerator == null || d.IsNumerator == model.IsNumerator));
    }
}
