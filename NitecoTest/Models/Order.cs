using System;

namespace NitecoTest.Models
{
    public class Order : Entity
    {
        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public int Amount { get; set; }

        public DateTime OrderDate { get; set; }

        public Customer Customer { get; set; }

        public Product Product { get; set; }
    }
}
