using System.ComponentModel.DataAnnotations;

namespace ASP.NEThwMain.DTO
{
    public class OrderCreateDTO
    {
        [Required]
        public string? CustomerId { get; set; }

        public string Status { get; set; } = "Pending";
    }
}
