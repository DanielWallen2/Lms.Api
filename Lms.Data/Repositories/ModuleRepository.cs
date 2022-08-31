using Lms.Core.Entities;
using Lms.Core.Repositories;
using Lms.Data.Data;
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
        public void Add(Module course)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<Module> FindAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Module>> GetAllModules()
        {
            return null; 
        }

        public Task<Module> GetModule(int? id)
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
