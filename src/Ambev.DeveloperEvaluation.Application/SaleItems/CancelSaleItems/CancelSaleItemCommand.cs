﻿using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.CancelSaleItems
{
    public class CancelSaleItemCommand : IRequest<CancelSaleItemResult>
    {
        public Guid SaleId { get; set; }
        public Guid ItemId { get; set; }
    }
}
