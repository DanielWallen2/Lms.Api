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

        public async Task<IEnumerable<Course>> GetAllCourses(bool includeModules)
        {
            return includeModules ? 
                await db.Course.Include(c => c.Modules).ToListAsync() :
                await db.Course.ToListAsync();
        }

        public async Task<Course> GetCourse(int? id, bool includeModules)
        {
            if(id == null) throw new ArgumentNullException("id");

            return includeModules ?
                await db.Course.Include(c => c.Modules).FirstOrDefaultAsync(c => c.Id == id) :
                await db.Course.FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Add(Course course)
        {
            db.Course.Add(course);
        }

        //public async Task AddAsync(Course course)
        //{
        //    //await db.Course.AddAsync(course);     ?
        //    await db.AddAsync(course);
        //}

        public async Task<Course> FindAsync(int? id)
        {
            if (id == null) throw new ArgumentNullException("id");
            return await db.Course.FindAsync(id);
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
