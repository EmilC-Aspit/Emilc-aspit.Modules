using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace DatabaseHandler
{
    public class Executor
    {
        protected readonly string connectionString = "";

        /// <summary>
        /// Return data from table with a sqlQuery
        /// </summary>
        /// <param name="sqlQuery">the command you want</param>
        /// <returns>DataSet</returns>
        public DataSet Execute(string sqlQuery)
        {
            try
            {


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataSet set = new DataSet();
                            adapter.Fill(set);
                            connection.Close();
                            return set;
                        }
                    }
                }
            }
            catch (SqlException) { throw; }
        }

        /// <summary>
        /// Executes a stored proecedure with parameters
        /// </summary>
        /// <param name="storedProcedureName">The name of the stored procedure </param>
        /// <param name="procedureParameters">The parameters for the stored procedure</param>
        /// <returns></returns>
        public DataSet Execute(string storedProcedureName, params string[] procedureParameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(storedProcedureName, conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(procedureParameters);
                        conn.Open();
                        using (SqlDataAdapter adpater = new SqlDataAdapter())
                        {
                            adpater.SelectCommand = command;
                            DataSet set = new DataSet();
                            adpater.Fill(set);
                            conn.Close();
                            return set;
                        }
                    }
                }
            }
            catch(SqlException)
            {
                throw;
            }
        }
        /// <summary>
        /// Takes the name and sends if to the getConnectionString Function
        /// </summary>
        /// <param name="name"></param>
        public Executor(string name)
        {
            string mapname = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Config";
            try
            {
                connectionString = ConfigurationManager.GetConnectionString(mapname, name);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
