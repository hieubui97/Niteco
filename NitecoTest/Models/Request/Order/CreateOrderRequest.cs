using System;
using System.ComponentModel.DataAnnotations;

namespace NitecoTest.Models.Request.Order
{
    public class CreateOrderRequest
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }
    }
}
