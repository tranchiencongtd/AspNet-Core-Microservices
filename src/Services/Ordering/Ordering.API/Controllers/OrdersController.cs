using AutoMapper;
using Contracts.Messages;
using Contracts.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Ordering.Application.Features.V1.Orders.Queries.GetOrders;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Ordering.API.Controllers
{
  [Route("api/v1/[controller]")]
  [ApiController]
  public class OrdersController : ControllerBase
  {
    private readonly IMediator _mediator;
    private readonly ISmtpEmailService _smtpEmailService;
    private readonly IMessageProducer _messageProducer;
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;



    public OrdersController(IMediator mediator, ISmtpEmailService smtpEmailService, IMessageProducer messageProducer, IOrderRepository orderRepository, IMapper mapper)
    {
      _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
      _smtpEmailService = smtpEmailService ?? throw new ArgumentNullException(nameof(smtpEmailService));
      _messageProducer = messageProducer ?? throw new ArgumentNullException(nameof(messageProducer)); ;
      _orderRepository = orderRepository;
      _mapper = mapper;
    }

    public static class RouteNames
    {
      public const string GetOrders = nameof(GetOrders);
    }

    [HttpGet("{userName}", Name = RouteNames.GetOrders)]
    [ProducesResponseType(typeof(IEnumerable<OrderDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByUserName([Required] string userName)
    {
      var query = new GetOrdersQuery(userName);
      var result = await _mediator.Send(query);
      return Ok(result);  
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(OrderDto orderDto)
    {
      var order = _mapper.Map<Order>(orderDto);
      var addOrder = _orderRepository.CreateOrder(order);
      await _orderRepository.SaveChangesAsync();
      var result = _mapper.Map<OrderDto>(addOrder);
      _messageProducer.SendMessage(result);
      return Ok(result);  
    }
  }
}
