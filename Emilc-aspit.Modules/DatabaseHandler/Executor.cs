using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FunkyUnitTest")]
namespace DatabaseHandler
{
    /// <summary>
    /// The class handles all the callings to the database
    /// </summary>
    public class Executor
    {
        /// <summary>
        /// Connectionstring for the database
        /// </summary>
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
        /// <returns>Datset from DB</returns>
        public DataSet Execute(string storedProcedureName, params string[] procedureParameters)
        {
            try
            {
                string sqlQuery = $"select * from information_schema.parameters where specific_name = '{storedProcedureName}'";
                List<string> test = new List<string>();
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
                            DataTableReader reader = set.CreateDataReader();
                            while (reader.Read())
                            {
                                string paramName = reader["PARAMETER_NAME"].ToString();
                                test.Add(paramName);
                            }
                        }

                    }
                }
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(storedProcedureName, conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            if (test.Count < procedureParameters.Length)
                            {
                                throw new DontBeStupidException("you have to many parameters");
                            }
                            if (test.Count > procedureParameters.Length)
                            {
                                throw new DontBeStupidException("you have to few parameters");
                            }
                            if (test.Count == procedureParameters.Length)
                            {
                                for (int i = 0; i < test.Count; i++)
                                {
                                    command.Parameters.AddWithValue(test[i], procedureParameters[i]);
                                }
                            }

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
            }
            catch(SqlException)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// A constructor that sends the name to the GetConnectionString function
        /// </summary>
        internal Executor(string connectionString)
        {
            this.connectionString = connectionString;
            try
            {
                SqlConnection conn = new SqlConnection(this.connectionString);
                conn.Open();
                conn.Close();
            }
            catch(SqlException)
            {
                throw;
            }
            
        }
    }
}
