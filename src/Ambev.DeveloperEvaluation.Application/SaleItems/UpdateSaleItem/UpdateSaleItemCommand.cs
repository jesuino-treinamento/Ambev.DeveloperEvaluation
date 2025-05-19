﻿namespace Ambev.DeveloperEvaluation.Application.SaleItems.UpdateSaleItem
{
    public class UpdateSaleItemCommand
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }

    }
}
