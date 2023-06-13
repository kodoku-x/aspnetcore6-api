using api_aspnetcore6.Models;
using api_aspnetcore6.Repositories.Interfaces;

namespace api_aspnetcore6.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _dbContext;
        private ICategoryRepository _categoryRepository;
        private IAuthenticationRepository _authenticationRepository;
        public UnitOfWork(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICategoryRepository Categories
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(_dbContext);
                }

                return _categoryRepository;
            }
        }

        public IAuthenticationRepository Users
        {
            get
            {
                if (_authenticationRepository == null)
                {
                    _authenticationRepository = new AuthenticationRepository(_dbContext);
                }

                return _authenticationRepository;
            }
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Rollback()
        {
            _dbContext.Dispose();
        }

        public async Task RollbackAsync()
        {
            await _dbContext.DisposeAsync();
        }
    }
}