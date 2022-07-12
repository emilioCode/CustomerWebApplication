using System;
using System.Collections.Generic;

namespace CustomerWebApplication.Models
{
    public partial class Address
    {
        public int Id { get; set; }
        public string Address0 { get; set; } = null!;
        public string? Type0 { get; set; }
        public int Customerid { get; set; }

        public virtual Customer Customer { get; set; } = null!;
    }
}
