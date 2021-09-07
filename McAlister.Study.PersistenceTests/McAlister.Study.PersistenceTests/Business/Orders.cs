using AutoMapper;
using McAlister.Study.PersistenceTests.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using dfe = McAlister.Study.PersistenceTests.Definitions.Entities;
using dfi = McAlister.Study.PersistenceTests.Definitions.Interfaces;
using dfm = McAlister.Study.PersistenceTests.Definitions.Models;

namespace McAlister.Study.PersistenceTests.Business
{
    /// <summary>
    /// Application model saves and retrieves data - gateway to business libraries
    /// </summary>
    public class Orders : BusinessBase<dfm.Order>, dfi.IOrders
    {
        private IMapper mapper;
        private dfi.IOrderRepository repo;
        private WideWorldImportersContext db;

        public Orders(dfi.IOrderRepository repo, WideWorldImportersContext ctx, IMapper mapper)
        {
            this.mapper = mapper;
            this.repo = repo;
            db = ctx;
        }

        public List<dfm.Order> GetListOrdersFromDataAdapter(int customerId)
        {
            var lstModel = new List<dfm.Order>();
            try
            {
                var lstEntity = repo.GetListOrdersFromDataAdapter(customerId).ToList();
                lstModel = mapper.Map<List<dfm.Order>>(lstEntity);
            }
            catch (Exception ex)
            {

            }
            return lstModel;
        }

        public List<dfm.Order> GetListOrdersFromEF(int customerId)
        {
            var lstModel = new List<dfm.Order>();
            try
            {
                lstModel = repo.GetListOrdersFromEF(customerId).ToList();
            }
            catch (Exception ex)
            {

            }
            return lstModel;
        }

        public List<dfm.Order> GetListOrdersFromContext(int customerId)
        {
            var lstModel = new List<dfm.Order>();
            try
            {
                var lstEntity = db.Orders.Where(o => o.CustomerId == customerId).ToList();
                lstModel = mapper.Map<List<dfe.Order>, List<dfm.Order>>(lstEntity);
            }
            catch (Exception ex)
            {

            }
            return lstModel;
        }

        public List<dfm.Order> GetListOrdersFromDapper(int customerId)
        {
            var lstModel = new List<dfm.Order>();
            try
            {
                lstModel = repo.GetListOrdersFromDapper(customerId).ToList();
            }
            catch (Exception ex)
            {

            }
            return lstModel;
        }

        public DataTable GetOrdersTableFromDataAdapter(int customerId)
        {
            return repo.GetOrdersTableFromDataAdapter(customerId);
        }

        public dfm.Order GetOrder(int orderId)
        {
            dfm.Order m = null;
            try
            {
                m = repo.Get<dfm.Order>(o => o.OrderId == orderId);
            }
            catch (Exception ex)
            {

            }
            return m;
        }

        public void Insert(dfm.Order order)
        {
            var eOrder = mapper.Map<dfm.Order, dfe.Order>(order);
            repo.Insert(eOrder);
        }

        public void Update(dfm.Order order)
        {
            var eOrder = mapper.Map<dfm.Order, dfe.Order>(order);
            repo.Update(eOrder);
        }

        public void Delete(int id)
        {
            var ent = repo.Get<dfe.Order>(p => p.OrderId == id);
            if (ent != null) //as its a delete, no error id doesn't exist
                repo.Delete(ent);
        }

        public void SaveChanges()
        {
            repo.SubmitChanges();
        }

        public void SaveChangesAsync()
        {
            repo.SubmitChangesAsync();
        }

        public override bool IsValid(dfm.Order model, ref string msg)
        {
            Boolean valid = true;
            if (model.CustomerId == 0)
            {
                msg += $"No customer {Environment.NewLine}";
                valid = false;
            }

            return valid;
        }
    }
}
