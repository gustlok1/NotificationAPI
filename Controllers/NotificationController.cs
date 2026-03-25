using Microsoft.AspNetCore.Mvc;
using NotificationApi.Models;
using NotificationApi.Services;

namespace NotificationApi.Controllers
{
    [ApiController]              
    [Route("api/[controller]")]  
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] NotificationRequest request)
        {
            var result = await _notificationService.NotifyAsync(request.Recipient, request.Message, request.ChannelName);
            return Ok(result);
        }
    }
}
