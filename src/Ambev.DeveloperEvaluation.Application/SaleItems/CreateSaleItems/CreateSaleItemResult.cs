﻿namespace Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItems
{
    public class CreateSaleItemResult
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
