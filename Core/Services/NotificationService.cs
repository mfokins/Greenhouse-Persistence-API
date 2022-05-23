using Core.Services.Interfaces;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace Core.Services
{
    public class NotificationService : INotificationService
    {
        private readonly FirebaseMessaging firebaseMessaging;
        public NotificationService()
        {
            var app = FirebaseApp.Create(new AppOptions() { Credential = GoogleCredential.FromFile("FirebaseAuth.json").CreateScoped("https://www.googleapis.com/auth/firebase.messaging") });
            firebaseMessaging = FirebaseMessaging.GetMessaging(app);
        }

        public async Task SendMoistureThreshold(string potname, string greenhouseId)
        {
            var msg = new Message();
            msg.Topic = greenhouseId;
            msg.Notification = new Notification()
            {
                Title = "Low water warning !!!",
                Body = $"{potname} is  running low on water :/, time to water it."
            };
            await firebaseMessaging.SendAsync(msg);
        }
    }
}
