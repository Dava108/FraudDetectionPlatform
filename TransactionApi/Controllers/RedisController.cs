using Microsoft.AspNetCore.Mvc;
using TransactionApi.Services;

namespace TransactionApi.Controllers;

[ApiController]
[Route("redis")]
public class RedisController(IRedisService redisService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Set(string key, string value)
    {
        await redisService.SetAsync(key, value, TimeSpan.FromMinutes(5));

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get(string key)
    {
        var value = await redisService.GetAsync(key);

        if (value is null)
        {
            return NotFound();
        }

        return Ok(value);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string key)
    {
        await redisService.DeleteAsync(key);

        return NoContent();
    }
}