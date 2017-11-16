namespace SecurityAccess.Contracts.Contracts
{
    public  class SearchServiceRequest
    {
        public int UserId { get; set; }

        public int CategoryId { get; set; }

        public decimal Range { get; set; }
    }
}
