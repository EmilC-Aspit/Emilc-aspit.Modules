using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace DatabaseHandler
{
    /// <summary>
    /// The class handles all the callings to the database
    /// </summary>
    public class Executor
    {
        /// <summary>
        /// the connection string
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
                        if (test.Count > 1)
                        {
                            for (int i = 0; i < test.Count; i++)
                            {
                                command.Parameters.AddWithValue(test[i], procedureParameters[i]);
                            }
                        }
                        else
                        {
                            command.Parameters.AddWithValue(test[0], procedureParameters[0]);
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
