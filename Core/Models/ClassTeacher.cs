using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ClassTeacher
    {
        public Guid Id { get; set; }
        public int TeacherId { get; set; }
        public int ClassArmId { get; set; }
    }
}
