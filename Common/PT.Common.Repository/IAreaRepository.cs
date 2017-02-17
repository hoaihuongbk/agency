using System.Collections.Generic;

namespace PT.Common.Repository
{
    public interface IAreaRepository
    {
        IArea CreateArea(IArea newArea);
        void DeleteArea(int id);
        IArea GetArea(int id);
        IArea GetArea(string name, int type);
        List<IArea> GetAreaByBaseId(int baseId);
        List<IArea> GetAreaByType(int type);
        IArea UpdateArea(IArea existingArea, IArea newArea);
    }
}
