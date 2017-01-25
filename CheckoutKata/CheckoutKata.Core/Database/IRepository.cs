using System.Collections.Generic;

namespace CheckoutKata.Core.Database
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetDataList();

        T GetData(string name);

        void Insert(T entry);

        void Update(T entry);

        void Delete(T entry);
    }
}