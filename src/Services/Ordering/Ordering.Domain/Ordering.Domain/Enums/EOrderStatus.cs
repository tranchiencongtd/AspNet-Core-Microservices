﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Enums
{
  public enum EOrderStatus
  {
    New = 1, // Start with 1, 0 is used for filter All: 0
    Pending,  // order is pending, not any activities for a period time.
    Paid, // order is paid
    Shipping, // order is on the shipping
    Fulfilled, // order is fulfilled
  }
}
