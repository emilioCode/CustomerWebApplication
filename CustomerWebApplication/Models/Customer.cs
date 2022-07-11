using System;
using System.Collections.Generic;

namespace CustomerWebApplication.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? Phone { get; set; }

        public virtual Address Address { get; set; } = null!;
    }
}
