using NotificationApi.Interfaces;

namespace NotificationApi.Factories
{
    public class NotificationChannelFactory
    {
        private readonly Dictionary<string, INotificationChannel> _channels;

        public NotificationChannelFactory(IEnumerable<INotificationChannel> channels)
        {
            _channels = channels.ToDictionary(c => c.ChannelName);
        }


        public INotificationChannel Get(string channelName)
        {
            if(!_channels.TryGetValue(channelName.ToLower(), out var channel))
                throw new ArgumentException($"Canal '{channelName}' não existe.");

            return channel;
        }

    }
}
