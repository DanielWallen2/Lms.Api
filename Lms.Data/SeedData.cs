using Bogus;
using Lms.Core.Entities;
using Lms.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data
{
    public class SeedData
    {
        private static LmsApiContext db = default!;

        public static async Task InitAsync(LmsApiContext context)
        {
            ArgumentNullException.ThrowIfNull(nameof(context));
            db = context;

            if(await db.Course.AnyAsync()) return;

            var courses = GetFakeCourses(10);
            await db.AddRangeAsync(courses);
            await db.SaveChangesAsync();
        }

        private static ICollection<Course> GetFakeCourses(int quantity)
        {
            var faker = new Faker("sv");
            var courses = new List<Course>();
            Random rnd = new Random();

            for (int i = 0; i < quantity; i++)
            {
                var course = new Course
                {
                    Name = faker.Company.CatchPhrase(),
                    StartDate = DateTime.Now.AddDays(rnd.Next(-2, 3)),
                    Modules = GetFakeModules(2)

                };

                courses.Add(course);
            }

            return courses;
        }

        private static ICollection<Module> GetFakeModules(int quantity)
        {
            var faker = new Faker("sv");
            var modules = new List<Module>();
            Random rnd = new Random();

            for (int i = 0; i < quantity; i++)
            {
                var module = new Module
                {
                    Title = faker.Hacker.IngVerb(),
                    StartDate = DateTime.Now.AddDays(rnd.Next(-2, 3))
                };

                modules.Add(module);
            }

            return modules;

        }
    }
}
