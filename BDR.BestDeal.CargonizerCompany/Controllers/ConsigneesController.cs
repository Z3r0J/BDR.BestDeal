using BDR.BestDeal.Application.Dtos.Cargonizer;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BDR.BestDeal.CargonizerCompany.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsigneesController(IValidator<ConsigneeRequest> validator) : ControllerBase
    {
        private readonly IValidator<ConsigneeRequest> _validator = validator;

        /// <summary>
        /// Processes the ConsigneeRequest to calculate total amounts based on provided details.
        /// </summary>
        /// <param name="request">The consignee request details.</param>
        /// <returns>A successful response with calculated amount or an error message if validation fails.</returns>
        /// <response code="200">Returns the total amount calculated from the request</response>
        /// <response code="400">Returned when validation fails with error messages</response>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ConsigneeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAmountAsync(ConsigneeRequest request)
        {
            var validation = await request.Validate(_validator);

            if (validation is { IsValid: true })
            {
                return Ok(ConsigneeResponse.GetAmount(request));
            }

            var errorMessages = string.Join(", ", validation?.Errors.Select(x => x.ErrorMessage) ?? Array.Empty<string>());
            return Problem(detail: errorMessages, title: "Validation Error", statusCode: StatusCodes.Status400BadRequest);
        }
    }
}
