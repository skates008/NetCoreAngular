using System;

namespace NetCoreAngular.Domain
{
    public class Customer : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
    }
}
