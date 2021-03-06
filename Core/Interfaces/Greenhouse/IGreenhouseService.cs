namespace Core.Interfaces.Greenhouse
{
    public interface IGreenhouseService
    {
        void Create(string id);
        Models.Greenhouse Get(string id);

        bool IsCreated(string id);
        void UpdateGreenhouse(Models.Greenhouse greenhouse);
    }
}