using Microsoft.AspNetCore.Mvc;
using TransactionApi.Services;

namespace TransactionApi.Controllers;

[ApiController]
[Route("kafka")]
public class KafkaController(IKafkaProducerService kafkaProducer) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Publish(string message)
    {
        await kafkaProducer.PublishAsync("transactions", message);

        return Ok(new
        {
            message
        });
    }
}