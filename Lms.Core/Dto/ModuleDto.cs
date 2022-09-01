using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Core.Dto
{
    public class ModuleDto
    {
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime
        {
            get { return StartTime.AddMonths(1); }
        }
    }
}
