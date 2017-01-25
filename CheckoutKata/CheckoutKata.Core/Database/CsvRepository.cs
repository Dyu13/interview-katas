using System.Collections.Generic;

namespace CheckoutKata.Core.Database
{
    public class CsvRepository<T> : IRepository<T> where T : class, new()
    {
        public IEnumerable<T> GetDataList()
        {
            throw new System.NotImplementedException();
        }

        public T GetData()
        {
            throw new System.NotImplementedException();
        }

        public void Insert(T entry)
        {
            throw new System.NotImplementedException();
        }

        public void Update(T entry)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(T entry)
        {
            throw new System.NotImplementedException();
        }
    }
}