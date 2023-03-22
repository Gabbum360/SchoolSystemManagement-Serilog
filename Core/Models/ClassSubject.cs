using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ClassSubject
    {
        public Guid Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int ClassArmId { get; set; }
    }
}
