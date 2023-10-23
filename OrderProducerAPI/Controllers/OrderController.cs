using Microsoft.AspNetCore.Mvc;
using OrderProducerAPI.Messages;

namespace OrderProducerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private IOrderRepository _rabbitMQMessageSender;

        public OrderController(IOrderRepository rabbitMQMessageSender)
        {
            _rabbitMQMessageSender = rabbitMQMessageSender
                ?? throw new ArgumentNullException(nameof(rabbitMQMessageSender));
        }
        [HttpPost]
        public async Task<ActionResult<Order>> PostProd(Order ord)
        {
            if (ord?.Id == null) return BadRequest();

            //  Integração com RabbitMQ.
            _rabbitMQMessageSender.SendMessage(ord, "Orderqueue");

            return Ok(ord);
        }
    }
}