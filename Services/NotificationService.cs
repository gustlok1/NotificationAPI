using NotificationApi.Factories;
using NotificationApi.Interfaces;
using NotificationApi.Models;

namespace NotificationApi.Services
{
    public class NotificationService
    {
        private readonly NotificationChannelFactory _factory;

        public NotificationService(NotificationChannelFactory factory)
        {
            _factory = factory;
        }

        public async Task<NotificationResponse> NotifyAsync(string recipient, string message, string channelName)
        {
            //Factory found channel
            var channel = _factory.Get(channelName);
            return await channel.SendAsync(recipient, message);
        }
    }
}
