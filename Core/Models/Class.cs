using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Models
{
    public class Class
    {
        public Class(Guid armId)
        {
            //ClassName = GenerateClassName(classname);

            if (ArmId == default)
                throw new ArgumentNullException("armId can not be null");
            ArmId = armId;

        }
         public Guid Id { get; set; }
         public string ClassName { get; private set; }
         public Guid ArmId { get; set; }

        public static class ClassFactory
        {
            public static Class Create(Guid armId, string classname)
            {
                return new Class(armId);
            }
        }

        private string GenerateClassName(string classname)
        {
            if (string.IsNullOrEmpty(classname))
                ClassName = classname;
            return classname;
        }
    }
}
