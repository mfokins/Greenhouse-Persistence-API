using Core.Services.Interfaces;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace Core.Services
{
    public class NotificationService : INotificationService
    {
        private readonly FirebaseMessaging firebaseMessaging;
        private bool notificationServiceSetUp = false;
        public NotificationService()
        {


            try
            {
                var app = FirebaseApp.Create(new AppOptions() { Credential = GoogleCredential.FromFile("appsettings.json").CreateScoped("https://www.googleapis.com/auth/firebase.messaging") });
                firebaseMessaging = FirebaseMessaging.GetMessaging(app);
                notificationServiceSetUp = true;
                Console.WriteLine("Notification serivice is running! ");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Notification serivice not set up! ");
                Console.WriteLine(ex.Message);
                notificationServiceSetUp = false;
            }
        }

        public async Task SendMoistureThreshold(string potname, string greenhouseId)
        {
            if (notificationServiceSetUp)
            {
                var msg = new Message();
                msg.Topic = greenhouseId;
                msg.Notification = new Notification()
                {
                    Title = "Low water warning !!!",
                    Body = $"{potname} is  running low on water :/, time to water it."
                };
                await firebaseMessaging.SendAsync(msg);
                Console.WriteLine("Notification sent about Moisture threshold! ");

            }
            else
            {
                Console.WriteLine("Notification serivice not set up! ");

            }

        }
    }
}
