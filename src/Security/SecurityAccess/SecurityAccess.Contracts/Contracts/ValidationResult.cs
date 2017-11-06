using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityAccess.Contracts
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }

        public string Code { get; set; }

        public string ValidationMessage { get; set; }

        public string StackTrace { get; set; }
    }

    public class ValidationResults
    {
        public bool IsValid { get; set; }
        public ICollection<ValidationResult> validationResults;

        public ValidationResults()
        {
            IsValid = true;
            validationResults = new List<ValidationResult>();
        }
        public void Add(ValidationResult validationResult)
        {
            IsValid = false;
            validationResults.Add(validationResult);
        }
    }
}
