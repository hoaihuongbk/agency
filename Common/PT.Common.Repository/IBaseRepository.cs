using System.Collections.Generic;

namespace PT.Common.Repository
{
    public interface IBaseRepository<T>
    {
        T Get(int id);
        T Create(T item);
        T Update(T oldItem, T newItem);
        void Delete(T item);
        void Delete(int id);
        IEnumerable<T> GetAll();
    }

    public interface IBaseHasCodeRepository<T>
    {
        T Get(string code);
        void Delete(string code);
        string GenerateCode();
        string GenerateCode(int len);
        string GenerateCode(int len, string prefix = null, string delimeter = "-");
    }

    public interface IBaseHasConfirmCodeRepository<T>
    {
        string GenerateConfirmCode();
        string GenerateConfirmCode(int len);
    }
}
