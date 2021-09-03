using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHunger.Domain.Enumeration
{
    public enum TypeOrderStatus
    {
        WaitingForPayment = 1,
        Paid = 2,
        Preparing = 3,
        OutForDelivery = 4,
        Delivered = 5
    }
}
