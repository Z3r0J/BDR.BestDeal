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

        [HttpPost]
        public async Task<IActionResult> GetTotal(ConsigneeRequest request)
        {
            var validation = await request.Validate(_validator);

            if (validation is { IsValid: true }) return Ok(ConsigneeResponse.GetTotal(request));

            var errorMessages = string.Join(", ", validation?.Errors.Select(x => x.ErrorMessage) ?? Array.Empty<string>());
                return Problem(detail: errorMessages, title: "Validation Error", statusCode: StatusCodes.Status400BadRequest);

        }
    }
}
