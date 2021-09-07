using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace McAlister.Study.PersistenceTests.Tests.Controllers
{
    [TestClass]
    [DoNotParallelize]
    public class OrderControllerTests
    {
        private const string baseURL = "http://localhost:82/api/Orders"; // "https://localhost:44344/api/Orders"

        [TestMethod]
        public void GetListOrdersFromDataAdapterTest()
        {
            GetData("GetListOrdersFromDataAdapter");
        }

        [TestMethod]
        public void GetListOrdersFromEFTest()
        {
            GetData("GetListOrdersFromEF");
        }

        [TestMethod]
        public void GetListOrdersFromContextTest()
        {
            GetData("GetListOrdersFromContext");
        }

        [TestMethod]
        public void GetListOrdersFromDapperTest()
        {
            GetData("GetListOrdersFromDapper");
        }

        [TestMethod]
        public void GetOrdersTableFromDataAdapterTest()
        {
            GetData("GetOrdersTableFromDataAdapter");
        }

        private static void GetData(string method)
        {
            for (int i = 1; i < 1062; i++)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{baseURL}/{method}/{i}");
                var r = (HttpWebResponse)request.GetResponse();
                Stream receiveStream = r.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                using (StreamReader sr = new StreamReader(receiveStream, encode))
                {
                    String str = sr.ReadToEnd();
                    //Console.Write(str);
                }
            }
        }
    }
}