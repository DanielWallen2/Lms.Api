using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Core.Dto
{
    public class CourseDto
    {
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime
        {
            get { return StartTime.AddMonths(3); }
        }
    }
}
