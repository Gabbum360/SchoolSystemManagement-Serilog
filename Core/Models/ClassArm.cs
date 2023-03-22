using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ClassArm
    {
        public Guid Id { get; set; }
        public int Department { get; set; }
        public int ClassId { get; set; }
        public int ArmId { get; set; }
        public int LABooks { get; set; }
        public int SubjectId { get; set; }
        public int StudentId { get; set; }
    }
}
