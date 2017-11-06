using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAccess.Contracts.Contracts
{
    public class UserAccountDetail
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public bool IsUserAccount { get; set; }

        public bool IsSystemAccount { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }

    public class UserAccountVerificationDetail
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string VerificationText { get; set; }

        public DateTime VerificationTextDateTime { get; set; }

        public int VerificationTextExpiryInSeconds { get; set; }

    }

}
