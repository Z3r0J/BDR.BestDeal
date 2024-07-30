using BDR.BestDeal.Application.Dtos.XMLLogistics;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BDR.BestDeal.XMLLogisticsCompany.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PackagesController(IValidator<PackageRequest> validator) : ControllerBase
{
    private readonly IValidator<PackageRequest> _validator = validator;

    /// <summary>
    /// Processes the PackageRequest to calculate a quote based on the package details provided in XML format.
    /// </summary>
    /// <param name="request">The package request details.</param>
    /// <returns>A successful response with the calculated quote or an error message if validation fails.</returns>
    /// <response code="200">Returns the calculated quote as XML</response>
    /// <response code="400">Returned when validation fails with detailed error messages</response>
    [HttpPost]
    [Consumes("application/xml")]
    [Produces("application/xml")]
    [ProducesResponseType(typeof(PackageResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetQuoteAsync([FromBody]PackageRequest request)
    {
        var validation = await request.Validate(_validator);
        var response = PackageResponse.GetQuote(request);

        if (validation is { IsValid: true }) return Ok(response);
        
        var errorMessages = string.Join(", ", validation?.Errors.Select(x => x.ErrorMessage) ?? Array.Empty<string>());
        return Problem(detail: errorMessages, title: "Validation Error", statusCode: StatusCodes.Status400BadRequest);
    }

}