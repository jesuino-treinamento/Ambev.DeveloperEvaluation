using Ambev.DeveloperEvaluation.Application.SaleItems.CancelSaleItems;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSales;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSales;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Application.Sales.ListSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSales;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.CancelSaleItems;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSales;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public SalesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var validator = new CreateSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);


            if (!validationResult.IsValid)
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Validation errors occurred",
                    Errors = validationResult.Errors.Select(e => new ValidationErrorDetail
                    {
                        Error = e.ErrorMessage
                    })
                });

            var command = _mapper.Map<CreateSaleCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);
            var response = _mapper.Map<CreateSaleResponse>(result);

            return CreatedAtAction(
                actionName: nameof(GetSale),
                routeValues: new { id = response.Id },
                value: new ApiResponseWithData<CreateSaleResponse>
                {
                    Success = true,
                    Message = "Sale created successfully",
                    Data = response
                });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSale(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetSaleByIdQueryCommand
            {
                SaleId = id
            };
            var result = await _mediator.Send(query, cancellationToken);


            var response = _mapper.Map<GetSaleResponse>(result);

            return Ok(new ApiResponseWithData<GetSaleResponse>
            {
                Success = true,
                Data = response
            });
        }
        catch (Exception ex)
        {
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<GetSaleResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PaginatedResponse<GetSaleResult>>> GetAllSales(
     [FromQuery] int page = 1,
     [FromQuery] int size = 10,
     [FromQuery] string order = "saledate",
     CancellationToken cancellationToken = default)
    {
        try
        {
            var query = new GetAllSalesQuery
            {
                Page = page,
                Size = size,
                Order = order
            };

            var result = await _mediator.Send(query, cancellationToken);

            return OkPaginated(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred" });
        }
    }


    [HttpPut("{id}/cancel")]
    [ProducesResponseType(typeof(ApiResponseWithData<CancelSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CancelSale(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new CancelSaleCommand { SaleId = id };
            var result = await _mediator.Send(command, cancellationToken);
            var response = _mapper.Map<CancelSaleResponse>(result);

            return Ok(new ApiResponseWithData<CancelSaleResponse>
            {
                Success = true,
                Message = "Sale cancelled successfully",
                Data = response
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    [HttpPut("{saleId}/items/{itemId}/cancel")]
    [ProducesResponseType(typeof(ApiResponseWithData<CancelSaleItemResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CancelSaleItem(
        Guid saleId,
        Guid itemId,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new CancelSaleItemCommand
            {
                SaleId = saleId,
                ItemId = itemId
            };

            var result = await _mediator.Send(command, cancellationToken);
            var response = _mapper.Map<CancelSaleItemResponse>(result);

            return Ok(new ApiResponseWithData<CancelSaleItemResponse>
            {
                Success = true,
                Message = "Sale item cancelled successfully",
                Data = response
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSale([FromRoute] Guid id, [FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
    {
        try
        {
            request.Id = id;

            var validator = new UpdateSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Validation errors occurred",
                    Errors = validationResult.Errors.Select(e => new ValidationErrorDetail { Error = e.ErrorMessage })
                });

            var command = _mapper.Map<UpdateSaleCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);
            var response = _mapper.Map<UpdateSaleResponse>(result);

            return Ok(new ApiResponseWithData<UpdateSaleResponse>
            {
                Success = true,
                Message = "Sale updated successfully",
                Data = response
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse { Success = false, Message = ex.Message });
        }
    }


    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponseWithData<DeleteSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteSaleCommand(id);

            var validator = new DeleteSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Validation errors occurred",
                    Errors = validationResult.Errors.Select(e => new ValidationErrorDetail { Error = e.ErrorMessage })
                });
            }

            var result = await _mediator.Send(command, cancellationToken);
            var response = _mapper.Map<DeleteSaleResponse>(result);

            return Ok(new ApiResponseWithData<DeleteSaleResponse>
            {
                Success = true,
                Message = "Sale deleted successfully",
                Data = response
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse { Success = false, Message = ex.Message });
        }
    }


}
