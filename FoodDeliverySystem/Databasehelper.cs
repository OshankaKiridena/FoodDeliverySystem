using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FoodDeliverySystem
{
    public static class DatabaseHelper
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;

        // Execute a query that returns data (SELECT)
        public static DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            var dataTable = new DataTable();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddRange(parameters);

                try
                {
                    connection.Open();
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Database query failed: {ex.Message}", ex);
                }
            }

            return dataTable;
        }

        // Execute a command that doesn't return data (INSERT/UPDATE/DELETE)
        public static int ExecuteNonQuery(string commandText, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(commandText, connection))
            {
                command.Parameters.AddRange(parameters);

                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Database operation failed: {ex.Message}", ex);
                }
            }
        }

        // Execute a command and return a single value
        public static object ExecuteScalar(string commandText, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(commandText, connection))
            {
                command.Parameters.AddRange(parameters);

                try
                {
                    connection.Open();
                    return command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Database operation failed: {ex.Message}", ex);
                }
            }
        }

        // Async version of ExecuteNonQuery
        public static async Task<int> ExecuteNonQueryAsync(string commandText, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(commandText, connection))
            {
                command.Parameters.AddRange(parameters);

                try
                {
                    await connection.OpenAsync();
                    return await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Database operation failed: {ex.Message}", ex);
                }
            }
        }

        // Helper method to create SqlParameter
        public static SqlParameter CreateParameter(string name, object value, SqlDbType dbType)
        {
            return new SqlParameter
            {
                ParameterName = name,
                Value = value ?? DBNull.Value,
                SqlDbType = dbType
            };
        }
    }
}