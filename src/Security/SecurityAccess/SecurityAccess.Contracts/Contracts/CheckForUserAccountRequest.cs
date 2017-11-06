namespace SecurityAccess.Contracts.Contracts
{
    public class CheckForUserAccountRequest
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
