using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class GetStudents
    {
        public string Id { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string ClassArmId { get; set; }
        public string Country { get; set; }
        public int StudentNo { get; set; }
    }
}
