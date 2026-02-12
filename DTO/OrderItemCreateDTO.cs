using System.ComponentModel.DataAnnotations;

namespace ASP.NEThwMain.DTO
{
    public class OrderItemCreateDTO
    {
        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}