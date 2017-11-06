using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityHostProject.Contracts
{
    public class SignUpUserResponse
    {
        public int UserId { get; set; }

        public string VerficationCode { get; set; }

        public ICollection<ValidationResult> ValidationResults { get; set; }
    }
}
