using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lms.Data.Data;
using Lms.Core.Entities;
using Lms.Data.Repositories;
using AutoMapper;
using Lms.Core.Dto;
using Lms.Core.Repositories;
using Microsoft.AspNetCore.JsonPatch;

namespace Lms.Api.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        //private readonly LmsApiContext db;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public CoursesController(/*LmsApiContext context,*/ IMapper mapper, IUnitOfWork uow)
        {
            //db = context;
            this.uow = uow;
            this.mapper = mapper;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourse(bool includeModules = false)
        {
            if(uow.CourseRepository.CheckIfDbIsNull()) return NotFound();

            var coursesDto = mapper.Map<IEnumerable<CourseDto>>(await uow.CourseRepository.GetAllCourses(includeModules));
            return Ok(coursesDto);
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id, bool includeModules = false)
        {
            if (uow.CourseRepository.CheckIfDbIsNull()) return NotFound();

            var coursesDto = mapper.Map<CourseDto>(await uow.CourseRepository.GetCourse(id, includeModules));
            if (coursesDto == null) return NotFound();

            return Ok(coursesDto);
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CourseDto>> PostCourse(CourseDto courseDto)
        {
            // Varför är en tom db ett problem vid post?
            if (uow.CourseRepository.CheckIfDbIsNull()) 
                return Problem("Entity set 'LmsApiContext.Course' is null.");

            var course = mapper.Map<Course>(courseDto);
            await uow.CourseRepository.AddAsync(course);
            await uow.CompleteAsync();

            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, courseDto);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if(uow.CourseRepository.CheckIfDbIsNull()) return NotFound();

            var course = await uow.CourseRepository.FindAsync(id);
            if (course == null) return NotFound();
            
            await uow.CourseRepository.RemoveAsync(course);
            await uow.CompleteAsync();

            return NoContent();
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, CourseDto courseDto)
        {
            var course = await uow.CourseRepository.FindAsync(id);
            if(course == null) return NotFound();

            mapper.Map(courseDto, course);
            await uow.CompleteAsync();

            return Ok(mapper.Map<CourseDto>(course));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<CourseDto>> PatchCourse(int id, JsonPatchDocument<CourseDto> patchDocument)
        {
            if (uow.CourseRepository.CheckIfDbIsNull()) return NotFound();

            var course = await uow.CourseRepository.FindAsync(id);
            if (course == null) return NotFound();

            var courseDto = mapper.Map<CourseDto>(course);

            patchDocument.ApplyTo(courseDto, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            mapper.Map(courseDto, course);
            await uow.CompleteAsync();

            return Ok(mapper.Map<CourseDto>(course));       // varför Map? Varför inte courseDto
        }



        //private bool CourseExists(int id)
        //{
        //    return (db.Course?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
