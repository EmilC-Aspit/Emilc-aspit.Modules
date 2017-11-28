using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseHandler;
using System.Data;
namespace FunkyUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_StoredProcedure_1Param()
        {
            RepositoryBase repository = new RepositoryBase("Tst", @"C:\Users\emil055f\Documents\Config\ConnectionPath.config");
            DataSet expected = new DataSet();

            DataSet real = repository.Execute("SELECT * FROM Memes");

            Assert.AreNotEqual(expected, real);
        }
        [TestMethod]
        public void Test_StoredProcedure_2Param()
        {
            RepositoryBase repository = new RepositoryBase("Tst", @"C:\Users\emil055f\Documents\Config\ConnectionPath.config");
            DataSet expected = new DataSet();

            DataSet realamount = repository.executor.Execute("TestPerson", "Morten", "Jens");

            Assert.AreNotEqual(expected, realamount);
        }

        [TestMethod]
        public void Test_SqlQuery()
        {
            RepositoryBase repository = new RepositoryBase("Tst", @"C:\Users\emil055f\Documents\Config\ConnectionPath.config");
            DataSet exptected = new DataSet();

            DataSet realAmount = repository.executor.Execute("SELECT * FROM Memes");

            Assert.AreNotEqual(exptected, realAmount);
        }
    }
}
