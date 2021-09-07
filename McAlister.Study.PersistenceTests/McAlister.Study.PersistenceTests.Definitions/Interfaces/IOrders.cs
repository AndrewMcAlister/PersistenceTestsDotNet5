using McAlister.Study.PersistenceTests.Definitions.Models;
using System.Collections.Generic;
using System.Data;

namespace McAlister.Study.PersistenceTests.Definitions.Interfaces
{
    public interface IOrders
    {
        void Delete(int id);
        Order GetOrder(int orderId);
        List<Order> GetListOrdersFromContext(int customerId);
        List<Order> GetListOrdersFromDapper(int customerId);
        DataTable GetOrdersTableFromDataAdapter(int customerId);
        List<Order> GetListOrdersFromEF(int customerId);
        List<Order> GetListOrdersFromDataAdapter(int customerId);
        void Insert(Order order);
        bool IsValid(Order model, ref string msg);
        void SaveChanges();
        void SaveChangesAsync();
        void Update(Order order);
    }
}