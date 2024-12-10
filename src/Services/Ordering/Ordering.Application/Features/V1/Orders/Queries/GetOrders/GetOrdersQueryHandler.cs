using AutoMapper;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Serilog;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, ApiResult<List<OrderDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger _logger;

        public GetOrdersQueryHandler(IMapper mapper, IOrderRepository orderRepository, ILogger logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ApiResult<List<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            _logger.Information($"BEGIN: {nameof(GetOrdersQueryHandler)} - Username: {request.UserName}");

            var orders = await _orderRepository.GetOrdersByUserName(request.UserName);
            var orderDtos = _mapper.Map<List<OrderDto>>(orders);

            _logger.Information($"END: {nameof(GetOrdersQueryHandler)} - Username: {request.UserName}");

            return new ApiSuccessResult<List<OrderDto>>(orderDtos);
        }
    }
}
