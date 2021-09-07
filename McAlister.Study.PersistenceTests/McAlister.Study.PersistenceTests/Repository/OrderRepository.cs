using AutoMapper;
using Dapper;
using McAlister.Study.PersistenceTests.Definitions.Interfaces;
using McAlister.Study.PersistenceTests.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using dfe = McAlister.Study.PersistenceTests.Definitions.Entities;
using dfm = McAlister.Study.PersistenceTests.Definitions.Models;

namespace McAlister.Study.PersistenceTests.Repository
{
    ///This is the place to put any complex linq queries.  No where else!
    ///Entity Container Name must match line below, and there must be a connection string of same name in ProdAdd even though we want to overwrite connection string.
    ///Do not return IQueryable ! https://blog.ploeh.dk/2012/03/26/IQueryableTisTightCoupling/
    ///Note .AsNoTracking().ToList() quicker if readonly.
    public class OrderRepository : GenericRepository<dfe.Order>, IDisposable, IOrderRepository
    {
        private IMapper mapper;

        public OrderRepository(IMapper mapper, WideWorldImportersContext context) : base(context)
        {
            this.mapper = mapper;
        }

        public ICollection<dfm.Order> GetListOrdersFromDataAdapter(int customerId)
        {
            var sql = $"Select * from Sales.Orders where customerId={customerId}";
            var dt = GetData(sql, "Orders");
            var results = Utility.ConvertDataTableToList<dfm.Order>(dt);
            return results;
        }

        public ICollection<dfm.Order> GetListOrdersFromEF(int customerId)
        {
            var ents = base.GetList<dfe.Order>(p => p.CustomerId == customerId).ToList();
            var results = mapper.Map<List<dfm.Order>>(ents);
            return results;
        }

        /// <summary>
        /// Using Dapper sometimes, its faster
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public ICollection<dfm.Order> GetListOrdersFromDapper(int customerId)
        {
            List<dfm.Order> results;
            var sql = $"Select * from Sales.Orders where CustomerId = @CustomerId";
            using (var con = new SqlConnection(this.ConnStr))
            {
                var entities = con.Query<dfe.Order>(sql, new { CustomerId = customerId }, null, true, 0, CommandType.Text);
                results = mapper.Map<List<dfm.Order>>(entities);
            }
            return results;
        }

        public DataTable GetOrdersTableFromDataAdapter(int customerId)
        {
            var sql = $"Select * from Sales.Orders where customerId={customerId}";
            var dt = GetData(sql, "Orders");
            return dt;
        }

        public DataTable GetData(string SQL, string Tablename)
        {
            var myDT = new DataTable(Tablename);
            myDT.TableName = "Orders";
            using (var con = new SqlConnection(this.ConnStr))
            {
                var myDA = new SqlDataAdapter(SQL, con);
                myDA.Fill(myDT);
                myDA.Dispose();
            }
            return myDT;
        }
    }

}
