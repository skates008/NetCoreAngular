using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreAngular.Domain
{
    public class BaseEntity<TKey>
    {
        public virtual TKey Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

    }
}
