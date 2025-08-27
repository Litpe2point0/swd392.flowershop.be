using BusinessObject.Model;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccessLayer.Repositories
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly FlowerShopDBContext _dbContext;
        private IDbContextTransaction? _transaction = null;

        IRepository<Bouquet>? _bouquetRepository;
        IRepository<BouquetType>? _bouquetTypeRepository;
        IRepository<Order>? _orderRepository;
        IRepository<Payment>? _paymentRepository;
        IRepository<Role>? _roleRepository;
        IRepository<Subscription>? _subscriptionRepository;
        IRepository<User>? _userRepository;
        IRepository<SubscriptionPackage>? _subscriptionPackageRepository;
        IRepository<SubscriptionBouquetType>? _subscriptionBouquetTypeRepository;


        public UnitOfWork(
            FlowerShopDBContext dbContext,
            IRepository<Bouquet> bouquetRepository,
            IRepository<BouquetType> bouquetTypeRepository,
            IRepository<Order> orderRepository,
            IRepository<Payment> paymentRepository,
            IRepository<Role> roleRepository,
            IRepository<Subscription> subscriptionRepository,
            IRepository<User> userRepository,
            IRepository<SubscriptionPackage> subscriptionPackageRepository,
            IRepository<SubscriptionBouquetType> subscriptionBouquetTypeRepository
        )
        {
            _dbContext = dbContext;
            _bouquetRepository = bouquetRepository;
            _bouquetTypeRepository = bouquetTypeRepository;
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _roleRepository = roleRepository;
            _subscriptionRepository = subscriptionRepository;
            _userRepository = userRepository;
            _subscriptionPackageRepository = subscriptionPackageRepository;
            _subscriptionBouquetTypeRepository = subscriptionBouquetTypeRepository;
        }

        public IRepository<Bouquet> BouquetRepository => _bouquetRepository;
        public IRepository<BouquetType> BouquetTypeRepository => _bouquetTypeRepository;
        public IRepository<Order> OrderRepository => _orderRepository;
        public IRepository<Payment> PaymentRepository => _paymentRepository;
        public IRepository<Role> RoleRepository => _roleRepository;
        public IRepository<Subscription> SubscriptionRepository => _subscriptionRepository;
        public IRepository<User> UserRepository => _userRepository;
        public IRepository<SubscriptionPackage> SubscriptionPackageRepository => _subscriptionPackageRepository;
        public IRepository<SubscriptionBouquetType> SubscriptionBouquetTypeRepository => _subscriptionBouquetTypeRepository;

        // 🔹 Transaction - Dùng async để tránh block luồng
        //public async Task BeginTransactionAsync()
        //{
        //    _transaction = await _dbContext.Database.BeginTransactionAsync();
        //}

        public async Task ExecuteInTransactionAsync(Func<Task> operation)
        {
            var strategy = _dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                // Bắt đầu transaction
                await using var transaction = await _dbContext.Database.BeginTransactionAsync();

                await operation(); // Gọi logic chính (thường là service gọi SaveChanges)

                await transaction.CommitAsync();
            });
        }

        public async Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> operation)
        {
            var strategy = _dbContext.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _dbContext.Database.BeginTransactionAsync();
                var result = await operation();
                await transaction.CommitAsync();
                return result;
            });
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        // 🔹 Lưu thay đổi vào DB - Thêm async để dùng trong môi trường bất đồng bộ
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
