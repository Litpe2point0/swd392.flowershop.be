using BusinessObject.Model;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Bouquet> BouquetRepository { get; }
        IRepository<BouquetType> BouquetTypeRepository { get; }
        IRepository<Order> OrderRepository { get; }
        IRepository<Payment> PaymentRepository { get; }
        IRepository<Role> RoleRepository { get; }
        IRepository<Subscription> SubscriptionRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<SubscriptionPackage> SubscriptionPackageRepository { get; }
        IRepository<SubscriptionBouquetType> SubscriptionBouquetTypeRepository { get; }

        //Task BeginTransactionAsync();
        Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> operation);
        Task ExecuteInTransactionAsync(Func<Task> operation);

        Task CommitTransactionAsync();
        void Dispose();
        Task RollbackTransactionAsync();
        Task<int> SaveChangesAsync();

        Task CommitAsync();

    }
}
