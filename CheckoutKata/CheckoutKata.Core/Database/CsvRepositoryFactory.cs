namespace CheckoutKata.Core.Database
{
    public class CsvRepositoryFactory : IRepositoryFactory
    {
        public IRepository<T> Create<T>() where T : class, new()
        {
            return new CsvRepository<T>();
        }
    }
}