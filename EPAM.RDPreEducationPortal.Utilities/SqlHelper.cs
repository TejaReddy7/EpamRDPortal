using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.RDPreEducationPortal.Utilities
{
    public static class SqlHelper
    {
        #region "FILL DATA TABLE"
        public static DataTable Fill(string connectionString, string procedureName)
        {
            var dataTable = new DataTable();
            var oConnection = new SqlConnection(connectionString);
            var oCommand = new SqlCommand(procedureName, oConnection)
            {
                CommandType = CommandType.StoredProcedure
            };
            var oAdapter = new SqlDataAdapter
            {
                SelectCommand = oCommand
            };
            oConnection.Open();
            using (var oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    oAdapter.SelectCommand.Transaction = oTransaction;
                    oAdapter.Fill(dataTable);
                    oTransaction.Commit();
                }
                catch
                {
                    oTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oAdapter.Dispose();
                }
            }
            return dataTable;
        }
        public static DataTable Fill(string connectionString, string procedureName, SqlParameter[] parameters)
        {
            var dataTable = new DataTable();
            var oConnection = new SqlConnection(connectionString);
            var oCommand = new SqlCommand(procedureName, oConnection)
            {
                CommandType = CommandType.StoredProcedure
            };
            if (parameters != null)
                oCommand.Parameters.AddRange(parameters);
            var oAdapter = new SqlDataAdapter
            {
                SelectCommand = oCommand
            };
            oConnection.Open();
            using (var oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    oAdapter.SelectCommand.Transaction = oTransaction;
                    oAdapter.Fill(dataTable);
                    oTransaction.Commit();
                }
                catch
                {
                    oTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oAdapter.Dispose();
                }
            }
            return dataTable;
        }
        #endregion
        #region "FILL DATASET"
        public static DataSet FillDataSet(string connectionString, string procedureName)
        {
            var dataSet = new DataSet();
            var oConnection = new SqlConnection(connectionString);
            var oCommand = new SqlCommand(procedureName, oConnection)
            {
                CommandType = CommandType.StoredProcedure
            };
            var oAdapter = new SqlDataAdapter
            {
                SelectCommand = oCommand
            };
            oConnection.Open();
            using (var oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    oAdapter.SelectCommand.Transaction = oTransaction;
                    oAdapter.Fill(dataSet);
                    oTransaction.Commit();
                }
                catch
                {
                    oTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oAdapter.Dispose();
                }
            }
            return dataSet;
        }
        public static DataSet FillDataSet(string connectionString, string procedureName, SqlParameter[] parameters)
        {
            var dataSet = new DataSet();
            var oConnection = new SqlConnection(connectionString);
            var oCommand = new SqlCommand(procedureName, oConnection)
            {
                CommandType = CommandType.StoredProcedure
            };
            if (parameters != null)
                oCommand.Parameters.AddRange(parameters);
            var oAdapter = new SqlDataAdapter
            {
                SelectCommand = oCommand
            };
            oConnection.Open();
            using (var oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    oAdapter.SelectCommand.Transaction = oTransaction;
                    oAdapter.Fill(dataSet);
                    oTransaction.Commit();
                }
                catch
                {
                    oTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oAdapter.Dispose();
                }
            }
            return dataSet;
        }
        #endregion
        #region "EXECUTE SCALAR"
        public static object ExecuteScalar(string connectionString, string procedureName)
        {
            var oConnection = new SqlConnection(connectionString);
            var oCommand = new SqlCommand(procedureName, oConnection)
            {
                CommandType = CommandType.StoredProcedure
            };
            object oReturnValue;
            oConnection.Open();
            using (var oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    oCommand.Transaction = oTransaction;
                    oReturnValue = oCommand.ExecuteScalar();
                    oTransaction.Commit();
                }
                catch
                {
                    oTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oCommand.Dispose();
                }
            }
            return oReturnValue;
        }
        public static object ExecuteScalar(string connectionString, string procedureName, SqlParameter[] parameters)
        {
            var oConnection = new SqlConnection(connectionString);
            var oCommand = new SqlCommand(procedureName, oConnection)
            {
                CommandType = CommandType.StoredProcedure
            };
            object oReturnValue;
            oConnection.Open();
            using (var oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    if (parameters != null)
                        oCommand.Parameters.AddRange(parameters);
                    oCommand.Transaction = oTransaction;
                    oReturnValue = oCommand.ExecuteScalar();
                    oTransaction.Commit();
                }
                catch
                {
                    oTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oCommand.Dispose();
                }
            }
            return oReturnValue;
        }
        #endregion
        #region "EXECUTE NON QUERY"
        public static int ExecuteNonQuery(string connectionString, string procedureName)
        {
            var oConnection = new SqlConnection(connectionString);
            var oCommand = new SqlCommand(procedureName, oConnection)
            {
                CommandType = CommandType.StoredProcedure
            };
            int iReturnValue;
            oConnection.Open();
            using (var oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    oCommand.Transaction = oTransaction;
                    iReturnValue = oCommand.ExecuteNonQuery();
                    oTransaction.Commit();
                }
                catch
                {
                    oTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oCommand.Dispose();
                }
            }
            return iReturnValue;
        }
        public static int ExecuteNonQuery(string connectionString, string procedureName, SqlParameter[] parameters)
        {
            var oConnection = new SqlConnection(connectionString);
            var oCommand = new SqlCommand(procedureName, oConnection)
            {
                CommandType = CommandType.StoredProcedure
            };
            int iReturnValue;
            oConnection.Open();
            using (var oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    if (parameters != null)
                        oCommand.Parameters.AddRange(parameters);
                    oCommand.Transaction = oTransaction;
                    iReturnValue = oCommand.ExecuteNonQuery();
                    oTransaction.Commit();
                }
                catch
                {
                    oTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oCommand.Dispose();
                }
            }
            return iReturnValue;
        }
        #endregion
    }
}
