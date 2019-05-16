using System.Collections.Generic;

namespace Exercise.Domain.Models
{
    public class PagedResult<TEntity, TIdType> where TEntity : IEntity<TIdType>
    {
        public PagedResult(IReadOnlyCollection<TEntity> data, int currentPage, int pageSize, double lastPage,
            double total)
        {
            Data = data;
            CurrentPage = currentPage;
            PageSize = pageSize;
            Total = total;
            LastPage = lastPage;
        }

        public IReadOnlyCollection<TEntity> Data { get; }
        public int CurrentPage { get; }
        public int PageSize { get; }
        public double Total { get; }
        public double LastPage { get; }
    }

    public class PagedResult<TEntity>: PagedResult<TEntity, string> where TEntity : IEntity
    {
        public PagedResult(IReadOnlyCollection<TEntity> data, int currentPage, int pageSize, double lastPage, double total) 
            : base(data, currentPage, pageSize, lastPage, total)
        {
        }
    }
}
