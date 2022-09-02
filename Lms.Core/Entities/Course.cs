using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Core.Entities
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "Length must be 6 to 40 characters.", MinimumLength = 6)]
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        
        // Navigation Properties
        public ICollection<Module>? Modules { get; set; }
    }
}
