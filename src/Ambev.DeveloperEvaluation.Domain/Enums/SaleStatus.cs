using System.ComponentModel;

namespace Ambev.DeveloperEvaluation.Domain.Enums
{
    public enum SaleStatus
    {
        [Description("Pending")]
        Pending = 1,
        [Description("Completed")]
        Completed = 2,
        [Description("Cancelled")]
        Cancelled = 3,
        [Description("NotCancelled")]
        NotCancelled = 4
    }
}
