using BDR.BestDeal.Application.Dtos.XMLLogistics;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BDR.BestDeal.XMLLogisticsCompany.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PackagesController(IValidator<PackageRequest> validator) : ControllerBase
{
    private readonly IValidator<PackageRequest> _validator = validator;

    [HttpPost]
    [Consumes("application/xml")]
    [Produces("application/xml")]
    public async Task<IActionResult> GetQuoteAsync([FromBody]PackageRequest request)
    {
        var validation = await request.Validate(_validator);
        var response = PackageResponse.GetQuote(request);

        if (validation is { IsValid: true }) return Ok(response);
        
        var errorMessages = string.Join(", ", validation?.Errors.Select(x => x.ErrorMessage) ?? Array.Empty<string>());
        return Problem(detail: errorMessages, title: "Validation Error", statusCode: StatusCodes.Status400BadRequest);
    }

}