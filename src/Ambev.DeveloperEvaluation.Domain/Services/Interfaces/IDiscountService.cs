using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Services.Interfaces
{
    public interface IDiscountService
    {
        decimal CalculateDiscount(int quantity, decimal unitPrice);
        decimal GetDiscount(int quantity);
    }
}
