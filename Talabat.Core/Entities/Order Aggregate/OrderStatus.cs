using System.Runtime.Serialization;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Confirmed")]
        Confirmed,
        [EnumMember(Value = "Canceled")]
        Canceled,
        [EnumMember(Value = "Delevired")]
        Delevired,
    }
}
