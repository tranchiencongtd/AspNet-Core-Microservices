using Contracts.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Common.Models;
using Ordering.Application.Features.V1.Orders.Queries.GetOrders;
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

    public OrdersController(IMediator mediator, ISmtpEmailService smtpEmailService)
    {
      _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
      _smtpEmailService = smtpEmailService ?? throw new ArgumentNullException(nameof(smtpEmailService));
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
  }
}
