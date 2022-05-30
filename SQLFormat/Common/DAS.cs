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
                        int fieldCount = dataReader.FieldCount;
                        if (dataReader.Read())
                        {
                            for (int i = 0; i < fieldCount; i++)
                            {
                                string name = dataReader.GetName(i);
                                string @string = Convert.ToString(dataReader.GetOracleDate(i));
                                text3 = text3 + "fieldName:" + name + ",fieldValue:" + @string + "\t\n";
                            }
                        }
                        dataReader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                text3 = ex.Message;
            }
            return text3;
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

            return text3;
        }
    }
}
