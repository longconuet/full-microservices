using AutoMapper;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Ordering.Domain.Entities;
using Serilog;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, ApiResult<OrderDto>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger _logger;

        public UpdateOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository, ILogger logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ApiResult<OrderDto>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.Information($"BEGIN: {nameof(UpdateOrderCommand)} - Username: {request.UserName}");

            var result = await _orderRepository.UpdateOrder(_mapper.Map<Order>(request));
            await _orderRepository.SaveChangesAsync();

            _logger.Information($"END: {nameof(UpdateOrderCommand)} - Username: {request.UserName}");

            return new ApiSuccessResult<OrderDto>(_mapper.Map<OrderDto>(result));
        }
    }
}
