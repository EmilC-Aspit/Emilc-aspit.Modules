using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseHandler;
using System.Data;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_StoredProcedure_1Param()
        {
            RepositoryBase repository = new RepositoryBase("Tst");
            DataSet expected = new DataSet();

            
            var RealAmount  = repository.Execute("createPerson", "INSERT INTO Memes(Name) VALUES ('Jek')");

            Assert.AreNotEqual(expected, repository);
        }
        [TestMethod]
        public void Test_StoredProcedure_2Param()
        {
            RepositoryBase repository = new RepositoryBase("Tst");
            DataSet expected = new DataSet();

            DataSet realamount = repository.Execute("TestPerson", "Morten", "Jens");

            Assert.AreNotEqual(expected, realamount);
        }

        [TestMethod]
        public void Test_SqlQuery()
        {
            RepositoryBase repository = new RepositoryBase("Tst");
            DataSet exptected = new DataSet();

            DataSet realAmount = repository.Execute("SELECT * FROM Memes");

            Assert.AreNotEqual(exptected, realAmount);
        }
    }
}
