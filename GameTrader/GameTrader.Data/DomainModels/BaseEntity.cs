using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Data.DomainModels
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public BaseEntity()
        {
            Id = Guid.NewGuid();
            UpdatedAt = DateTime.UtcNow;
            IsDeleted = false;
        }
    }
}
