using AutoMapper;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Shared.SeedWork;
using Serilog;

namespace Ordering.Application.Features.V1.Orders.Queries.GetOrders
{
  public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, ApiResult<List<OrderDto>>>
  {
    private readonly IMapper _mapper;
    private readonly IOrderRepository _repository;
    private readonly ILogger _logger;
    private const string MethodName = "GetOrdersQueryHandler";

    public GetOrdersQueryHandler(IMapper mapper, IOrderRepository repository, ILogger logger)
    {
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _repository = repository ?? throw new ArgumentNullException(nameof(repository));
      _logger = logger;
    }

    public async Task<ApiResult<List<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
      _logger.Information($"BEGIN: {MethodName} - UserName: {request.UserName}");

      var orderEntities = await _repository.GetOrdersByUsername(request.UserName);
      var orderList = _mapper.Map<List<OrderDto>>(orderEntities);

      _logger.Information($"END: {MethodName} - UserName: {request.UserName}");

      return new ApiSuccessResult<List<OrderDto>>(orderList);
    }
  }
}
