using Microsoft.Win32;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLFormat.Common
{
    public class DAS
    {
        private static readonly Lazy<DAS> lazy = new Lazy<DAS>(() => new DAS());
        public static DAS Instance { get { return lazy.Value; } }
        private DAS() { }


        OracleConnection connection = null;

        public string OracleSql(string ip, string port, string serviceName, string userID, string password, string sql)
        {
            string conn = $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL = TCP)(HOST = {ip})(PORT = {port}))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = {serviceName})));User ID={userID};Password={password}";
            string text3 = "";
            try
            {
                if (connection == null)
                    connection = new OracleConnection(conn);

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                 
                using (OracleCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    using (OracleDataReader dataReader = command.ExecuteReader())
                    {
                        string rowData = string.Empty;
                        int fieldCount = dataReader.FieldCount;
                        for (int i = 0; i < fieldCount; i++)
                        {
                            string name = dataReader.GetName(i);
                            if (i == 0)
                            {
                                rowData += (name + ":{" + i + "}");
                            }
                            else
                            {
                                rowData += (","+name + ":{" + i + "}");
                            } 
                        }

                        //name:value,name:value,name:value,name:value
                        do
                        {
                            while (dataReader.Read())
                            {
                                List<object> list = new List<object>();
                                for (int j = 0; j < dataReader.FieldCount; j++)
                                {
                                    list.Add(Convert.ToString(dataReader[j]));
                                }
                                text3 += string.Format(rowData, list.ToArray()) +",";
                            }
                        }
                        while (dataReader.NextResult());
                         
                        dataReader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                text3 = ex.Message;
            }
            finally
            {
                connection?.Close();
            }

            return text3;
        }

        public DataSet ExecDataSetByOracle(string ip, string port, string serviceName, string userID, string password, string sql)
        {
            string conn = $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL = TCP)(HOST = {ip})(PORT = {port}))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = {serviceName})));User ID={userID};Password={password}";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("exec_error_msg");
            DataRow dr = dt.NewRow();
            try
            {
                if (connection == null)
                    connection = new OracleConnection(conn);

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                using (OracleDataAdapter adapter = new OracleDataAdapter(sql, conn))
                {
                    adapter.Fill(ds);
                }    
            }
            catch (Exception ex)
            {
                dr[0] = ex.Message;
                dt.Rows.Add(dr);
                ds.Tables.Add(dt);  
            }
            finally
            {
                connection?.Close();
            }

            return ds;
        }


        OdbcConnection odbcConnection = null;
        public string GaussSql(string ip, string port, string userID, string password, string sql)
        {
            string text3 = "";
            string subkey;
            string key = string.Empty;
            try
            {
                bool in64bit = (IntPtr.Size == 8);

                //HKEY_LOCAL_MACHINE\SOFTWARE\ODBC\ODBCINST.INI\Gauss100_x64
                //HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\ODBC\ODBCINST.INI\Gauss100_x32
                if (in64bit)
                {
                    key = @"SOFTWARE\ODBC\ODBCINST.INI\Gauss100_x64";
                    subkey = "Gauss100_x64";
                }
                else
                {
                    key = @"SOFTWARE\WOW6432Node\ODBC\ODBCINST.INI\Gauss100_x32";
                    subkey = "Gauss100_x32";
                }
                RegistryKey winLogonKey = Registry.LocalMachine.OpenSubKey(key, false);
                if (winLogonKey == null)
                {
                    return $"FCM_ODBC: 没有配置odbc信息，请配置好注册表[HKEY_LOCAL_MACHINE\\{key}]";
                }
            }
            catch
            {
                return $"FCM_ODBC: 没有配置odbc信息，请配置好注册表[HKEY_LOCAL_MACHINE\\{key}]";
            }

            string conn = "Driver={" + subkey + "};server=" + ip + ";port=" + port + ";uid=" + userID + ";pwd=" + password + ";";

            try
            {
                if (odbcConnection == null)
                    odbcConnection = new OdbcConnection(conn);
                
                if (odbcConnection.State != ConnectionState.Open)
                {
                    odbcConnection.Open();
                }

                using (OdbcCommand odbcCommand = new OdbcCommand())
                {
                    odbcCommand.Connection = odbcConnection;
                    odbcCommand.CommandText = sql;
                    using (OdbcDataReader odbcDataReader = odbcCommand.ExecuteReader())
                    {
                        int fieldCount = odbcDataReader.FieldCount;
                        if (odbcDataReader.Read())
                        {
                            for (int i = 0; i < fieldCount; i++)
                            {
                                string name = odbcDataReader.GetName(i);
                                string @string = odbcDataReader.GetString(i);
                                //text3 = text3 + "fieldName:" + name + ",fieldValue:" + @string + " ";
                                text3 = @string;
                            }
                        }
                        odbcDataReader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                text3 = ex.Message;
            }
            finally
            {
                odbcConnection?.Close();
            }

            return text3;
        }

        public DataSet ExecDataSetByGauss(string ip, string port, string userID, string password, string sql)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("exec_error_msg");
            DataRow dr = dt.NewRow();
            string subkey;
            string key = string.Empty;
            try
            {
                bool in64bit = (IntPtr.Size == 8);

                //HKEY_LOCAL_MACHINE\SOFTWARE\ODBC\ODBCINST.INI\Gauss100_x64
                //HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\ODBC\ODBCINST.INI\Gauss100_x32
                if (in64bit)
                {
                    key = @"SOFTWARE\ODBC\ODBCINST.INI\Gauss100_x64";
                    subkey = "Gauss100_x64";
                }
                else
                {
                    key = @"SOFTWARE\WOW6432Node\ODBC\ODBCINST.INI\Gauss100_x32";
                    subkey = "Gauss100_x32";
                }
                RegistryKey winLogonKey = Registry.LocalMachine.OpenSubKey(key, false);
                if (winLogonKey == null)
                {
                    dr[0] = $"FCM_ODBC: 没有配置odbc信息，请配置好注册表[HKEY_LOCAL_MACHINE\\{key}]";
                    dt.Rows.Add(dr);
                    ds.Tables.Add(dt);
                    return ds;
                }
            }
            catch
            {
                dr[0] = $"FCM_ODBC: 没有配置odbc信息，请配置好注册表[HKEY_LOCAL_MACHINE\\{key}]";
                dt.Rows.Add(dr);
                ds.Tables.Add(dt);
                return ds;
            }

            string conn = "Driver={" + subkey + "};server=" + ip + ";port=" + port + ";uid=" + userID + ";pwd=" + password + ";";

            try
            {
                if (odbcConnection == null)
                    odbcConnection = new OdbcConnection(conn);

                if (odbcConnection.State != ConnectionState.Open)
                {
                    odbcConnection.Open();
                }

                using (OdbcDataAdapter adapter = new OdbcDataAdapter(sql, conn))
                {            
                    adapter.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                dr[0] = ex.Message;
                dt.Rows.Add(dr);
                ds.Tables.Add(dt);  
            }
            finally
            {
                odbcConnection?.Close();
            }

            return ds;
        }
    }
}
