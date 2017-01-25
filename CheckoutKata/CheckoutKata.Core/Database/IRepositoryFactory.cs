namespace CheckoutKata.Core.Database
{
    public interface IRepositoryFactory
    {
        IRepository<T> Create<T>() where T : class, new();
    }
}