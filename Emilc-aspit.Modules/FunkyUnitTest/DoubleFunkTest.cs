using System;
using System.Data;
using DatabaseHandler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
namespace FunkyUnitTest
{
    [TestClass]
    public class DoubleFunkTest
    {
        [TestMethod]
        public void TestIfConnectionStringWorks()
        {
            try
            {
                DummyClass dummy = new DummyClass("Tst", @"C:\Users\emil055f\Documents\Config\Memem.config");
            }
            catch (SqlException ex)
            {
                Assert.Fail("Connection to database failed", ex.Message);
            }

        }


        [TestMethod]
        public void GetData()
        {
            //Arrange
            Executor e = new Executor(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Tst; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            
            //Act
            DataSet set = e.Execute("SELECT * FROM Memes");

            //Assert
            Assert.AreNotEqual(0, set.Tables.Count);
        }


        [TestMethod]
        [ExpectedException(typeof(System.Data.SqlClient.SqlException))]
        public void TestIfConnectionStringFail()
        {
            //Arrange
            Executor e = new Executor(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Tqt; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
        }


        [TestMethod]
        public void TestStoredProcedureGetData()
        {
            //Arrange
            Executor e = new Executor(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Tst; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");

            //Act
            DataSet set = e.Execute("CreatePerson", "SELECT * FROM Memes");

            //Assert
            Assert.AreNotEqual(0, set.Tables.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseHandler.DontBeStupidException))]
        public void TestIfStoredProcedureFail()
        {
            //Arrange
            Executor e = new Executor(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Tst; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");

            //Act
            DataSet set = e.Execute("CreatePerson", "SELECT * FROM Memes", "Morten er grøn");
        }
    }

    public class DummyClass : RepositoryBase
    {
        public DummyClass(string databaseName,string filepath): base(databaseName,filepath)
        {

        }
    }
}
