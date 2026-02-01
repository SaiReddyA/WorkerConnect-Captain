using System;
using System.Collections.Generic;
using System.Text;

namespace SR.HomeServices.Domain.Enums
{
    /// <summary>
    /// Booking status enumeration
    /// </summary>
    [Serializable]
    public enum BookingStatus : byte
    {
        Pending = 1,
        InProgress = 2,
        Completed = 3
    }

}
