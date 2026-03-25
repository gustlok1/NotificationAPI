namespace NotificationApi.Models
{
    public record NotificationRequest(string Recipient, string Message, string ChannelName);

    public record NotificationResponse(bool Success, string Message, DateTime SentAt);
}
