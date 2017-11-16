using Project.Core;

namespace SecurityAccess.Contracts.Contracts
{

    public class GetUserForAuthenticateResponse
    {
        public UserAccountDetail UserAccountDetail { get; set; }

        public ValidationResults ValidationResults { get; set; }
    }
}
