using MediatR;
using Maggsoft.Core.Model.Pagination;
using MinimalAirbnb.Application.Properties.DTOs;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.Properties.Queries.GetProperties;

/// <summary>
/// Properties listesi query'si
/// </summary>
public class GetPropertiesQuery : IRequest<Result<PagedList<PropertyDto>>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? City { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? PropertyType { get; set; }
} 