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

        /// <summary>
        /// Processes the WareHouseRequest to calculate total dimensions based on provided details.
        /// </summary>
        /// <param name="request">The warehouse request details.</param>
        /// <returns>A successful response with calculated total or an error message if validation fails.</returns>
        /// <response code="200">Returns the total calculated from the request</response>
        /// <response code="400">Returned when validation fails with error messages</response>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WareHouseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetTotalAsync(WareHouseRequest request)
        {
            var validation = await request.Validate(_validator);

            if (validation is { IsValid: true }) return Ok(WareHouseResponse.GetTotal(request));
            
            var errorMessages = string.Join(", ", validation?.Errors.Select(x => x.ErrorMessage) ?? Array.Empty<string>());
            return Problem(detail: errorMessages, title: "Validation Error", statusCode: StatusCodes.Status400BadRequest);
        }

    }
}
