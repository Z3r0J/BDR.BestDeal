using System.Text.Json;
using BDR.BestDeal.Application.Client.Entities;
using BDR.BestDeal.Application.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace BDR.BestDeal.Application.Dtos.Cargonizer;

public record struct ConsigneeRequest(
    string Consignee,
    string Consignor,
    List<int> Cartons)
{
    public Task<ValidationResult?> Validate(IValidator<ConsigneeRequest> validator)
    {
        return validator.ValidateAsync(this);
    }
}