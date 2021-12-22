using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class AdoContext
    {
        
        private SqlConnection _connection = default!;
        public SqlCommand _command = default!;

        private bool isOpen = false;

        public enum CommandType
        {
            QUERY,
            PROCEDURE
        }

        public AdoContext()
        {
     
        }

        public SqlDataReader DataReadQuery(String query)
        {
            if (!isOpen)
            {
                _connection.Open();
                isOpen = true;
            }
            _command.CommandText = query;
            SqlDataReader dataReader = _command.ExecuteReader();
            return dataReader;
        }

        public SqlDataReader DataReadQuery()
        {
            if (!isOpen)

            {
                _connection.Open();
                isOpen = true;
            }
            SqlDataReader dataReader = _command.ExecuteReader();
            return dataReader;
        }

        public int DataWriteQuery(String query)
        {
            if (!isOpen)
            {
                _connection.Open();
                isOpen = true;
            }
            _command.CommandText = query;
            return _command.ExecuteNonQuery();

        }

        public int DataWriteQuery()
        {
            if (!isOpen)
            {
                _connection.Open();
                isOpen = true;
            }

            return _command.ExecuteNonQuery();

        }


        public bool OpenConnection()
        {
            return false;
        }

        public void AddQueryParam(string paramName, object? paramValue)
        {
            this._command.Parameters.AddWithValue(paramName, paramValue);

        }

        public SqlParameter AddOutputParam(string paramName, System.Data.SqlDbType type)
        {
            SqlParameter sqlParam = new SqlParameter();
            sqlParam.ParameterName = paramName;
            sqlParam.SqlDbType = type;
            sqlParam.Direction = System.Data.ParameterDirection.Output;
            _command.Parameters.Add(sqlParam);
            return sqlParam;
        }

        public void Clean()
        {
            if (isOpen)
            {
                _connection.Close();
                _command.Dispose();
                isOpen = false;

            }
            else throw new Exception("Connection is not open!");
        }

        public void Dispose() {

            if (isOpen) {
                _command.Dispose();
            }
            else throw new Exception("Connection is not open!");
        }

        public void SetQuery(string query)
        {

            this._command.CommandText = query;
        }

        public void SetCommandType(CommandType type)
        {

            switch (type)
            {

                case CommandType.PROCEDURE:
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    break;
                case CommandType.QUERY:
                    _command.CommandType = System.Data.CommandType.Text;
                    break;
                default: throw new Exception("Wrong type.");
            }

        }
    }
}
