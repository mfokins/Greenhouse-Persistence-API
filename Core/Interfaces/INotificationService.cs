namespace Core.Services.Interfaces
{
    public interface INotificationService
    {
        Task SendMoistureThreshold(string potname, string greenhouseId);
        
    }
}