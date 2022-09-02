using Lms.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Core.Repositories
{
    public interface IModuleRepository
    {
        public Task<IEnumerable<Module>> GetAllModules();
        public Task<Module> GetModule(int? id);
        public Task<Module> FindAsync(int? id);
        public Task<bool> AnyAsync(int? id);
        public Task AddAsync(Module course);
        public void Update(Module course);
        public Task RemoveAsync(Module course);
    }
}
