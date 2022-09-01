using Lms.Core.Entities;
using Lms.Core.Repositories;
using Lms.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private LmsApiContext db;

        public ModuleRepository(LmsApiContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<Module>> GetAllModules()
        {
            return await db.Module.ToListAsync();
        }

        public async Task<Module> GetModule(int? id)
        {
            if (id == null) throw new ArgumentNullException("id");
            return await db.Module.FirstOrDefaultAsync(c => c.Id == id);
        }
        
        public void Add(Module course)
        {
            db.Module.Add(course);
        }

        public Task<Module> FindAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(int? id)
        {
            throw new NotImplementedException();
        }



        public void Remove(Module course)
        {
            throw new NotImplementedException();
        }

        public void Update(Module course)
        {
            throw new NotImplementedException();
        }
    }
}
