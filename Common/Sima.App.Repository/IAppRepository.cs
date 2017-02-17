namespace Sima.App.Repository
{
    public interface IAppRepository
    {
        IApp CreateApp(IApp newApp);
        void DeleteApp(int id);
        IApp GetApp(int id);
        IApp GetApp(string name);
        IApp GetAppByCode(string code);
        IApp UpdateApp(IApp existingApp, IApp newApp);
    }
}
