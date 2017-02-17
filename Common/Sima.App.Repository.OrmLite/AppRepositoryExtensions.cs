using ServiceStack.Text;
namespace Sima.App.Repository.OrmLite
{
    public static class AppRepositoryExtensions
    {
        public static string CreateLog(this IApp record)
        {
            return JsonSerializer.SerializeToString(record);
        }
    }
}
