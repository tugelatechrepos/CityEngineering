using Project.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAccess.Contracts.Contracts
{
    public class GetUserProfileRequest
    {
        public int UserId { get; set; }
    }

    public class GetUserProfileResponse
    {
        public UserInfo UserProfile { get; set; }

        public ValidationResults ValidationResults { get; set; }
    }
}
