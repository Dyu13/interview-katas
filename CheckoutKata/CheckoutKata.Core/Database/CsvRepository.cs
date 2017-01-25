using System.Collections.Generic;

namespace CheckoutKata.Core.Database
{
    public class CsvRepository<T> : IRepository<T> where T : class, new()
    {
        private bool _isRemoved = false;

        public IEnumerable<T> GetDataList()
        {
            return new List<T>();
        }

        public T GetData(string name)
        {
            return _isRemoved ? null : new T();
        }

        public void Insert(T entry)
        {
            
        }

        public void Update(T entry)
        {
            
        }

        public void Delete(T entry)
        {
            _isRemoved = true;
        }
    }
}