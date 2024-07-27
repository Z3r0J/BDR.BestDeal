using BDR.BestDeal.Application.Dtos.DimAddress;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BDR.BestDeal.DimAddressCompany.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController(IValidator<WareHouseRequest> validator) : ControllerBase
    {
        private readonly IValidator<WareHouseRequest> _validator = validator;

        [HttpPost]
        public async Task<IActionResult> GetAmountAsync(WareHouseRequest request)
        {
            var validation = await request.Validate(_validator);

            if (validation is { IsValid: true }) return Ok(WareHouseResponse.GetAmount(request));
            
            var errorMessages = string.Join(", ", validation?.Errors.Select(x => x.ErrorMessage) ?? Array.Empty<string>());
            return Problem(detail: errorMessages, title: "Validation Error", statusCode: StatusCodes.Status400BadRequest);
        }

    }
}
