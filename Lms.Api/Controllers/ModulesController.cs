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

namespace Lms.Api.Controllers
{
    [Route("api/modules")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly LmsApiContext db;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public ModulesController(LmsApiContext context, IMapper mapper, IUnitOfWork uow)
        {
            db = context;
            this.uow = uow;
            this.mapper = mapper;
        }

        // GET: api/Modules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Module>>> GetModule()
        {
            if(db.Module == null) return NotFound();

            var modulesDto = mapper.Map<IEnumerable<ModuleDto>>(await uow.ModuleRepository.GetAllModules());
            return Ok(modulesDto);
        }

        // GET: api/Modules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModuleDto>> GetModule(int id)
        {
            if(db.Module == null) return NotFound();

            var moduleDto = mapper.Map<ModuleDto>(await uow.ModuleRepository.GetModule(id));
            if (moduleDto == null) return NotFound();

            return Ok(moduleDto);
        }

        // POST: api/Modules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ModuleDto>> PostModule(ModuleDto moduleDto)
        {
            if (db.Module == null) return Problem("Entity set 'LmsApiContext.Module'  is null.");

            var module = mapper.Map<Module>(moduleDto);
            uow.ModuleRepository.Add(module);
            await uow.CompleteAsync();

            return CreatedAtAction(nameof(GetModule), new { id = module.Id }, moduleDto);
        }

        // DELETE: api/Modules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            if (db.Module == null)
            {
                return NotFound();
            }
            var @module = await db.Module.FindAsync(id);
            if (@module == null)
            {
                return NotFound();
            }

            db.Module.Remove(@module);
            await db.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Modules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(int id, Module @module)
        {
            if (id != @module.Id)
            {
                return BadRequest();
            }

            db.Entry(@module).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        private bool ModuleExists(int id)
        {
            return (db.Module?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
