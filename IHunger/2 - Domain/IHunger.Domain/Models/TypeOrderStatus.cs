using System;
using System.Collections.Generic;
using System.Text;

namespace IHunger.Domain.Models
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
