using DataLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class BaseService<T> where T : class
    {
        readonly public IUnitOfWork _uow;
        readonly public DbSet<T> _TEntity;

        //private readonly IMapper _mapper;
        public BaseService(IUnitOfWork uow)
        {
            _uow = uow;
            _TEntity = _uow.Set<T>();
        }
    }
}