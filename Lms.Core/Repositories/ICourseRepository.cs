using Lms.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Core.Repositories
{
    public interface ICourseRepository
    {
        public Task<IEnumerable<Course>> GetAllCourses(bool includeModules);
        public Task<Course?> GetCourse(int? id, bool includeModules);
        public Task<Course> FindAsync(int? id);
        public Task<bool> AnyAsync(int? id);
        public Task AddAsync(Course course);
        public void Update(Course course);
        public Task RemoveAsync(Course course);
        public bool CheckIfDbIsNull();
    }
}
