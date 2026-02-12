namespace ASP.NEThwMain.DTO
{
    public class OrderReadDTO
    {
        public Guid Id { get; set; }
        public string? CustomerId { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public decimal Total { get; set; }
    }
}
