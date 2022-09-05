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
using Lms.Core.Repositories;
using Lms.Core.Dto;
using Microsoft.AspNetCore.JsonPatch;

namespace Lms.Api.Controllers
{
    [Route("api/modules")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        //private readonly LmsApiContext db;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public ModulesController(/*LmsApiContext context,*/ IMapper mapper, IUnitOfWork uow)
        {
            //db = context;
            this.uow = uow;
            this.mapper = mapper;
        }

        // GET: api/Modules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Module>>> GetModule()
        {
            if(uow.ModuleRepository.CheckIfDbIsNull()) return NotFound();

            var modulesDto = mapper.Map<IEnumerable<ModuleDto>>(await uow.ModuleRepository.GetAllModules());
            return Ok(modulesDto);
        }

        // GET: api/Modules/Title
        [HttpGet("{title}")]
        public async Task<ActionResult<ModuleDto>> GetModule(string title)
        {
            if (uow.ModuleRepository.CheckIfDbIsNull()) return NotFound();

            var moduleDto = mapper.Map<ModuleDto>(await uow.ModuleRepository.GetModuleByTitle(title));
            if (moduleDto == null) return NotFound();

            return Ok(moduleDto);
        }

        //// GET: api/Modules/Name
        //[HttpGet("title/{title:string}")]
        //[HttpGet("id/{id:int}")]
        //public async Task<ActionResult<ModuleDto>> GetModule(string title, int id)
        //{
        //    if (uow.ModuleRepository.CheckIfDbIsNull()) return NotFound();

        //    var moduleDto = mapper.Map<ModuleDto>(await uow.ModuleRepository.GetModuleByTitle(title));
        //    if (moduleDto == null) return NotFound();

        //    return Ok(moduleDto);
        //}

        // POST: api/Modules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ModuleDto>> PostModule(ModuleDto moduleDto)
        {
            if (uow.ModuleRepository.CheckIfDbIsNull()) return Problem("Entity set 'LmsApiContext.Module'  is null.");

            var module = mapper.Map<Module>(moduleDto);
            await uow.ModuleRepository.AddAsync(module);
            await uow.CompleteAsync();

            return CreatedAtAction(nameof(GetModule), new { id = module.Id }, moduleDto);
        }

        // DELETE: api/Modules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            if (uow.ModuleRepository.CheckIfDbIsNull()) return NotFound();

            var module = await uow.ModuleRepository.FindAsync(id);
            if (module == null) return NotFound();

            await uow.ModuleRepository.RemoveAsync(module);
            await uow.CompleteAsync();

            return NoContent();
        }

        // PUT: api/Modules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(int id, ModuleDto moduleDto)
        {
            var module = await uow.ModuleRepository.FindAsync(id);
            if (module == null) return NotFound();

            mapper.Map(moduleDto, module);
            await uow.CompleteAsync();

            return Ok(mapper.Map<ModuleDto>(module));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<CourseDto>> PatchCourse(int id, JsonPatchDocument<CourseDto> patchDocument)
        {
            if (uow.CourseRepository.CheckIfDbIsNull()) return NotFound();

            var module = await uow.ModuleRepository.FindAsync(id);
            if (module == null) return NotFound();

            var moduleDto = mapper.Map<CourseDto>(module);

            patchDocument.ApplyTo(moduleDto, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            mapper.Map(moduleDto, module);
            await uow.CompleteAsync();

            return Ok(mapper.Map<CourseDto>(module));       // varför Map? Varför inte courseDto
        }




        //private bool ModuleExists(int id)
        //{
        //    return (db.Module?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
