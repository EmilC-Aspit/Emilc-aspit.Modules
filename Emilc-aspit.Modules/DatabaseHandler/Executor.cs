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

        public Executor(string name)
        {
            connectionString = ConfigurationManager.GetConnectionString(@"config\ConnectionPath.config",  $"{name}");
        }
    }
}
