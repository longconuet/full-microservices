using AutoMapper;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using Serilog;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResult<long>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger _logger;

        public CreateOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository, ILogger logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ApiResult<long>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.Information($"BEGIN: {nameof(CreateOrderCommand)} - Username: {request.UserName}");

            var result = await _orderRepository.CreateOrder(_mapper.Map<Order>(request));
            await _orderRepository.SaveChangesAsync();

            _logger.Information($"END: {nameof(CreateOrderCommand)} - Username: {request.UserName}");

            return new ApiSuccessResult<long>(result);
        }
    }
}
