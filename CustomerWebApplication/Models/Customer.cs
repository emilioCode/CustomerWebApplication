using System;
using System.Collections.Generic;

namespace CustomerWebApplication.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Addresses = new HashSet<Address>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? Phone { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
