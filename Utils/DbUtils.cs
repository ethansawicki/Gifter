using System;
using Microsoft.Data.SqlClient;

namespace Gifter.Utils
{
    // A set of useful functions for interacting with ADO.NET
    public static class DbUtils
    {
        // Get a string from a data reader object and gracefully handle NULL values
        // <param name="reader">A SqlDataReader that has not exhausted it's result set.</param>
        // <param name="column">The name of the column from the result set refereed to by the reader.</param>
        // <returns>The value of the given column or null.</returns>
        public static string GetString(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            if (reader.IsDBNull(ordinal))
            {
                return null;
            }

            return reader.GetString(ordinal);
        }

        // Get an int from a data reader object.
        // This method assumes the value is not NULL.
        // <param name="reader">A SqlDataReader that has not exhausted it's result set.</param>
        // <param name="column">The name of the column from the result set refereed to by the reader.</param>
        // <returns>The value of the given column.</returns>
        public static int GetInt(SqlDataReader reader, string column)
        {
            return reader.GetInt32(reader.GetOrdinal(column));
        }

        // Get a DateTime from a data reader object.
        // This method assumes the value is not NULL.
        public static DateTime GetDateTime(SqlDataReader reader, string column)
        {
            return reader.GetDateTime(reader.GetOrdinal(column));
        }

        // Get an int? (nullable int) from a data reader object and gracefully handle NULL values

        public static int? GetNullableInt(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            if (reader.IsDBNull(ordinal))
            {
                return null;
            }
            return reader.GetInt32(ordinal);
        }

        public static bool IsDbNull(SqlDataReader reader, string column)
        {
            return reader.IsDBNull(reader.GetOrdinal(column));
        }

        //Determine if the value of a given column is NULL
        public static bool IsNotDBNull(SqlDataReader reader, string column) 
        {
            return !IsDbNull(reader, column);
        }

        // Add a parameter to the given sqlCommand object and gracefully handle null values

        public static void AddParameter(SqlCommand cmd, string name, object value)
        {
            if (value == null)
            {
                cmd.Parameters.AddWithValue(name, DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue(name, value);
            }
        }
    }
}
