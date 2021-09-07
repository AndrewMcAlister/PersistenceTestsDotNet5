using AutoMapper;
using McAlister.Study.PersistenceTests.Definitions;
using McAlister.Study.PersistenceTests.Definitions.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using dfm = McAlister.Study.PersistenceTests.Definitions.Models;

namespace McAlister.Study.PersistenceTests.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrders orders;
        private ILogger<DebugLoggerProvider> loggerDebug;

        public OrdersController(IOrders orders, IMapper mapper, ILogger<DebugLoggerProvider> logger)
        {
            this.orders = orders;
            loggerDebug = logger;
        }

        [HttpGet]
        [Route("GetListOrdersFromDataAdapter/{customerId}")]
        public APIResponse GetListOrdersFromDataAdapter(int customerId)
        {
            HttpStatusCode status = HttpStatusCode.OK;
            List<dfm.Order> ords = null;
            Exception exForReponse = null;
            try
            {
                ords = orders.GetListOrdersFromDataAdapter(customerId);
                if (ords == null || !ords.Any())
                {
                    status = HttpStatusCode.NotFound;
                }
                //else if (other conditions)
                //status = HttpStatusCode.BadRequest; etc
            }
            catch (Exception ex)
            {
                exForReponse = ex;
            }
            var res = Utility.CreateAPIResponse(ords, status, loggerDebug, exForReponse);
            return res;
        }

        [HttpGet]
        [Route("GetListOrdersFromEF/{customerId}")]
        public APIResponse GetListOrdersFromEF(int customerId)
        {
            HttpStatusCode status = HttpStatusCode.OK;
            List<dfm.Order> ords = null;
            Exception exForReponse = null;
            try
            {
                ords = orders.GetListOrdersFromEF(customerId);
                if (ords == null || !ords.Any())
                {
                    status = HttpStatusCode.NotFound;
                }
                //else if (other conditions)
                //status = HttpStatusCode.BadRequest; etc
            }
            catch (Exception ex)
            {
                exForReponse = ex;
            }
            var res = Utility.CreateAPIResponse(ords, status, loggerDebug, exForReponse);
            return res;
        }

        [HttpGet]
        [Route("GetListOrdersFromContext/{customerId}")]
        public APIResponse GetListOrdersFromContext(int customerId)
        {
            HttpStatusCode status = HttpStatusCode.OK;
            List<dfm.Order> ords = null;
            Exception exForReponse = null;
            try
            {
                ords = orders.GetListOrdersFromContext(customerId);
                if (ords == null || !ords.Any())
                {
                    status = HttpStatusCode.NotFound;
                }
                //else if (other conditions)
                //status = HttpStatusCode.BadRequest; etc
            }
            catch (Exception ex)
            {
                exForReponse = ex;
            }
            var res = Utility.CreateAPIResponse(ords, status, loggerDebug, exForReponse);
            return res;
        }

        [HttpGet]
        [Route("GetListOrdersFromDapper/{customerId}")]
        public APIResponse GetListOrdersFromDapper(int customerId)
        {
            HttpStatusCode status = HttpStatusCode.OK;
            List<dfm.Order> ords = null;
            Exception exForReponse = null;
            try
            {
                ords = orders.GetListOrdersFromDapper(customerId);
                if (ords == null || !ords.Any())
                {
                    status = HttpStatusCode.NotFound;
                }
                //else if (other conditions)
                //status = HttpStatusCode.BadRequest; etc
            }
            catch (Exception ex)
            {
                exForReponse = ex;
            }
            var res = Utility.CreateAPIResponse(ords, status, loggerDebug, exForReponse);
            return res;
        }

        [HttpGet]
        [Route("GetOrdersTableFromDataAdapter/{customerId}")]
        public APIResponse GetOrdersTableFromDataAdapter(int customerId)
        {
            HttpStatusCode status = HttpStatusCode.OK;
            DataTable ords = null;
            Exception exForReponse = null;
            try
            {
                ords = orders.GetOrdersTableFromDataAdapter(customerId);
                if (ords == null || ords.Rows.Count == 0)
                {
                    status = HttpStatusCode.NotFound;
                }
                //else if (other conditions)
                //status = HttpStatusCode.BadRequest; etc
            }
            catch (Exception ex)
            {
                exForReponse = ex;
            }
            var res = Utility.CreateAPIResponse(ords, status, loggerDebug, exForReponse);
            return res;
        }

        // GET: api/orders/order
        [HttpGet]
        [Route("GetOrder/{orderId}")]
        public APIResponse GetOrder(int orderId)
        {
            HttpStatusCode status = HttpStatusCode.OK;
            dfm.Order ord = null;
            Exception exForReponse = null;
            try
            {
                ord = orders.GetOrder(orderId);
                if (ord == null)
                {
                    status = HttpStatusCode.NotFound;
                }
                //else if (other conditions)
            }
            catch (Exception ex)
            {
                exForReponse = ex;
                //status = HttpStatusCode.BadRequest; etc
            }
            var res = Utility.CreateAPIResponse(ord, status, loggerDebug, exForReponse);

            return res;
        }

        // POST: api/order
        [HttpPost]
        [Route("New")]
        public APIResponse Post([FromBody] dfm.Order value)
        {
            HttpStatusCode status = HttpStatusCode.OK;
            Exception exForReponse = null;
            try
            {
                orders.Insert(value);
                orders.SaveChanges();
            }
            catch (Exception ex)
            {
                exForReponse = ex;
                if (ex.Message.Contains("blah blah"))
                    status = HttpStatusCode.BadRequest;
                else
                    status = HttpStatusCode.InternalServerError;
            }
            var res = Utility.CreateAPIResponse(null, status, loggerDebug, exForReponse);
            return res;
        }

        [HttpPut]
        // PUT: api/order
        [Route("Update")]
        public APIResponse Put([FromBody] dfm.Order value)
        {
            HttpStatusCode status = HttpStatusCode.OK;
            Exception exForReponse = null;
            try
            {
                orders.Update(value);
                orders.SaveChanges();
            }
            catch (Exception ex)
            {
                exForReponse = ex;
                if (ex.Message.Contains("blah blah"))
                    status = HttpStatusCode.BadRequest;
                else
                    status = HttpStatusCode.InternalServerError;
            }
            var res = Utility.CreateAPIResponse(null, status, loggerDebug, exForReponse);
            return res;
        }

        // DELETE: api/orders
        [HttpDelete]
        [Route("Delete")]
        public APIResponse Delete(int id)
        {
            HttpStatusCode status = HttpStatusCode.OK;
            Exception exForReponse = null;
            try
            {
                orders.Delete(id);
                orders.SaveChanges();
            }
            catch (Exception ex)
            {
                exForReponse = ex;
                if (ex.Message.Contains("blah blah"))
                    status = HttpStatusCode.BadRequest;
                else
                    status = HttpStatusCode.InternalServerError;
            }
            var res = Utility.CreateAPIResponse(null, status, loggerDebug, exForReponse);
            return res;
        }
    }
}
