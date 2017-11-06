using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityHostProject.Contracts
{
    public class ValidationResult
    {
        public string Code { get; set; }

        public string ValidationMessage { get; set; }

        public string StackTrace { get; set; }
    }
}
