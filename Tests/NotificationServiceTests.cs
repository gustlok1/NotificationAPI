using NotificationApi.Factories;
using NotificationApi.Services;
using NotificationApi.Tests.Fakes;

namespace Tests
{
    public class NotificationServiceTests
    {
        [Fact]
        public async Task Should_Send_To_Email_When_Channel_Is_Email()
        {
            // ARRANGE
            var fakeEmail = new FakeNotificationChannel("email");
            var fakeSms = new FakeNotificationChannel("sms");
            var factory = new NotificationChannelFactory(new[] { fakeEmail, fakeSms });
            var service = new NotificationService(factory);

            // ACT
            await service.NotifyAsync("joao@email.com", "Olá!", "email");

            // ASSERT
            Assert.Single(fakeEmail.SentMessages);  // email should recive one meessage
            Assert.Empty(fakeSms.SentMessages);     // sms not recive
        }

        [Fact]
        public async Task Should_Throw_An_Error_When_Channel_Does_Not_Exist()
        {
            var factory = new NotificationChannelFactory(new[] { new FakeNotificationChannel("email") });
            var service = new NotificationService(factory);

            await Assert.ThrowsAsync<ArgumentException>(() =>
                service.NotifyAsync("joao@email.com", "Olá!", "telegrama")
            );
        }
    }

}