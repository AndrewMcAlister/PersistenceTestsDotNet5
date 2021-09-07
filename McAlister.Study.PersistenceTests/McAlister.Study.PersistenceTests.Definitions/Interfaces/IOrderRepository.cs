using McAlister.Study.PersistenceTests.Definitions.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace McAlister.Study.PersistenceTests.Definitions.Interfaces
{
    public interface IOrderRepository
    {
        ICollection<Order> GetListOrdersFromDapper(int customerId);
        DataTable GetOrdersTableFromDataAdapter(int customerId);
        ICollection<Order> GetListOrdersFromEF(int customerId);
        ICollection<Order> GetListOrdersFromDataAdapter(int customerId);
        T Get<T>(Expression<Func<T, bool>> predicate) where T : class;
        T Insert<T>(T entity) where T : class;
        T Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        int SubmitChanges();
        Task<int> SubmitChangesAsync();

    }
}