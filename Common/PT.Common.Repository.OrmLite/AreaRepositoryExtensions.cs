using ServiceStack;
using System;

namespace PT.Common.Repository.OrmLite
{
    public static class AreaRepositoryExtensions
    {
        public static void ValidateNewArea(this IArea newArea)
        {
            if (newArea.Name.IsNullOrEmpty())
                throw new ArgumentNullException("Name is required");
        }
    }
}
