using MediatR;
using Sellify.Application.Features.Orders.Vms;
using Sellify.Application.Features.Shared.Queries;

namespace Sellify.Application.Features.Orders.Queries.PaginationOrders;

public class PaginationOrdersQuery : PaginationBaseQuery, IRequest<PaginationVm<OrderVm>>
{
    public int? Id { get; set; }
    public string? Username { get; set; }
}