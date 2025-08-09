using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Data.DomainModels
{
    public class PaymentVerification : BaseEntity
    {
        public Guid RequestId { get; set; }
        public Guid ProveId { get; set; }
        public bool IsVerified { get; set; }
        public virtual Request Request { get; set; }
        public virtual PaymentProfe PaymentProfe { get; set; }
    }
}
