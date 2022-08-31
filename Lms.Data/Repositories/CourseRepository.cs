using Lms.Core.Entities;
using Lms.Core.Repositories;
using Lms.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly LmsApiContext db;

        public CourseRepository(LmsApiContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await db.Course.ToListAsync();
        }

        public async Task<Course> GetCourse(int? id)
        {
            if(id == null) throw new ArgumentNullException("id");
            return await db.Course.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course> FindAsync(int? id)
        {
            if (id == null) throw new ArgumentNullException("id");
            return await db.Course.FindAsync(id);
        }

        public void Add(Course course)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(int? id)
        {
            throw new NotImplementedException();
        }

        


        public void Remove(Course course)
        {
            throw new NotImplementedException();
        }

        public void Update(Course course)
        {
            throw new NotImplementedException();
        }
    }
}
