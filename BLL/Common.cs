using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using Microsoft.ApplicationBlocks.Data;
using System.Security.Principal;
using System.IO;
using System.Security.AccessControl;

namespace BLL
{
    public class CommonMethods
    {
        private static string _connectionString;
        private static OleDbConnection _excelConn;


        /// <summary>Gets the ComTech connection string.</summary>
        internal static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    _connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
                }

                return _connectionString;
            }
        }

        // QUERY: Why is this called ExcelConnection when it returns a Command?
        /// <summary>
        /// Method to read the excel sheet data
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        internal static OleDbCommand ExcelConnection(string file)
        {
            string excelConnectionString = string.Empty;

            if (file.EndsWith(@".xlsx"))
            {
                excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + file + ";" + "Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text\"";
            }
            else if (file.EndsWith(@".xls"))
            {
                excelConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + file + ";" + "Extended Properties=\"Excel 8.0;HDR=True;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text\"";
            }

            using (_excelConn = new OleDbConnection(excelConnectionString))
            {
                _excelConn.Open();
                DataTable excelSheets = _excelConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                string spreadSheetName = "[" + excelSheets.Rows[0]["TABLE_NAME"].ToString() + "]";

                //use a SQL Select command to retrieve the data from the Excel Spreadsheet
                using (OleDbCommand objCommand = new OleDbCommand("SELECT * FROM " + spreadSheetName, _excelConn))
                {
                    //objCommand.ExecuteReader();
                    return objCommand;
                }
            }
        }


        /// <summary>Gets the stored proc flag for the specified upsert action</summary>
        /// <param name="upsertAction">The upsert action.</param>
        /// <returns>char</returns>
        public static char GetUpsertValue(UpsertAction upsertAction)
        {
            // ASSUME: There is only update 'U' and insert 'I'
            return (upsertAction == UpsertAction.Insert) ? 'I' : 'U';
        }

        /// <summary>Gets the stored proc flag for the specified save changes action</summary>
        /// <param name="saveChangesAction"></param>
        /// <returns></returns>
        public static char GetSaveChangesValue(SaveChangesAction saveChangesAction)
        {
            char saveChangesFlag;

            switch (saveChangesAction)
            {
                case SaveChangesAction.Insert:
                    saveChangesFlag = 'I'; break;
                case SaveChangesAction.Update:
                    saveChangesFlag = 'U'; break;
                case SaveChangesAction.Delete:
                    saveChangesFlag = 'D'; break;
                default:
                    throw new NotSupportedException(String.Format("Save changes action '{0}' is not supported", saveChangesAction));
            }

            return saveChangesFlag;
        }

        /// <summary>Gets the operation title after save changes performed</summary>
        /// <param name="saveChangesAction"></param>
        /// <returns></returns>
        public static string GetSaveChangesPostOperationTitle(SaveChangesAction saveChangesAction)
        {
            string postOperationTitle = string.Empty;

            switch (saveChangesAction)
            {
                case SaveChangesAction.Insert:
                    postOperationTitle = "inserted"; break;
                case SaveChangesAction.Update:
                    postOperationTitle = "updated"; break;
                case SaveChangesAction.Delete:
                    postOperationTitle = "deleted"; break;
                default:
                    throw new NotSupportedException(String.Format("Save changes action '{0}' is not supported", saveChangesAction));
            }

            return postOperationTitle;
        }

        /// <summary>
        /// Gets database server details from connection string
        /// </summary>
        public static string DatabaseServerDetails
        {
            get
            {
                string dbConnString = CommonMethods.ConnectionString;

                var match = System.Text.RegularExpressions.Regex.Match(dbConnString, @"Source\s?=\s?(.*?);");
                if (match.Success)
                {
                    return "Server : " + match.Groups[1].Value;
                }

                return String.Empty;
            }
        }

        /// <summary>
        /// Validates whether user has delete permission or not
        /// </summary>
        /// <param name="userPermissionLevel"></param>
        /// <returns></returns>
        public static bool IsUserHasDeletePermissionLevel(int userPermissionLevel)
        {
            return ((userPermissionLevel == PermissionLevel.Administrator) || (userPermissionLevel == PermissionLevel.SuperUser));
        }


        /// <summary>
        /// To fetch the available service list
        /// </summary>
        /// <returns></returns>
        public DataSet GetServicesList()
        {
            try
            {
                return SqlHelper.ExecuteDataset(CommonMethods.ConnectionString, "USP_GetServices");
            }
            catch (Exception ex)
            {
                throw new Exceptions.DatabaseOperationException("Unable to get the services due to a database error.", ex);
            }
        }

        /// <summary>
        /// method to get the report list data
        /// </summary>
        /// <returns></returns>
        public static DataSet GetReportsList()
        {
            try
            {
                return SqlHelper.ExecuteDataset(CommonMethods.ConnectionString, "getReportList");
            }
            catch (Exception ex)
            {
                throw new Exceptions.DatabaseOperationException("Unable to get the report due to a database error.", ex);
            }
        }

        /// <summary>
        /// to get the menu items to display in side of the screen based on the service
        /// </summary>
        public static DataSet GetMenuItems(int serviceId)
        {
            try
            {
                return SqlHelper.ExecuteDataset(CommonMethods.ConnectionString, "USP_GetMenuItems", serviceId);
            }
            catch (Exception ex)
            {
                throw new Exceptions.DatabaseOperationException("Unable to get the menu items due to a database error.", ex);
            }
        }

        /// <summary>
        /// Gets Monthly Report for a selected country
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public DataSet GetCountryMonthlyReport(string countryCode, out int result)
        {
            result = -1;
            try
            {
                SqlParameter[] param = { new SqlParameter("@CountryCode", countryCode), new SqlParameter("@Output", result) };
                param[1].Direction = ParameterDirection.Output;

                DataSet ds = SqlHelper.ExecuteDataset(CommonMethods.ConnectionString, CommandType.StoredProcedure, "GetCountryMonthlyReport", param);
                result = Convert.ToInt32(param[1].Value);

                return ds;
            }
            catch (Exception ex)
            {
                throw new Exceptions.DatabaseOperationException("Unable to get the monthly report for country due to a database error.", ex);
            }
        }

        /// <summary>
        /// Method to get the service description for the selected serviceid
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public static string FetchServiceName(int serviceId)
        {
            string serviceName = string.Empty;
            try
            {
                SqlParameter[] sqlParams = { new SqlParameter("@ServiceId", serviceId) };
                DataSet dsServices = SqlHelper.ExecuteDataset(CommonMethods.ConnectionString, CommandType.StoredProcedure, "USP_GetMasterServiceByServiceId", sqlParams);

                if (dsServices.Tables[0].Rows.Count > 0)
                {
                    serviceName = dsServices.Tables[0].Rows[0][0].ToString();
                }

                return serviceName;
            }
            catch (Exception ex)
            {
                throw new Exceptions.DatabaseOperationException("Unable to get the services by serviceid due to a database error.", ex);
            }
        }

        /// <summary>
        /// Get the table names from the master_tables_variables_service
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public static DataSet GetTableName(int serviceId)
        {
            try
            {
                return SqlHelper.ExecuteDataset(CommonMethods.ConnectionString, "USP_Gettablename", serviceId);
            }
            catch (Exception ex)
            {
                throw new Exceptions.DatabaseOperationException("Unable to get the tablename by serviceid due to a database error.", ex);
            }
        }

        /// <summary>
        /// Fetch the variables for the spec'd table name
        /// </summary>
        /// <returns></returns>
        public DataSet GetVariablesFromTable()
        {
            try
            {
                return SqlHelper.ExecuteDataset(CommonMethods.ConnectionString, CommandType.Text, "SELECT * FROM Master_Tables_Variables_Service t INNER JOIN master_service s ON t.Service_Number=s.Service_Number ");
            }
            catch (Exception ex)
            {
                throw new Exceptions.DatabaseOperationException("Unable to get the master tables variable services due to a database error.", ex);
            }
        }

        /// <summary>
        /// Gets variable details
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public static DataSet GetVariablesFromTable(int serviceId)
        {
            string sqlQuery = "SELECT * FROM Master_Tables_Variables_Service t INNER JOIN master_service s ON t.Service_Number=s.Service_Number WHERE t.Service_Number=@ServiceId";

            SqlParameter[] parameters = new SqlParameter[1];

            parameters.SetValue(new SqlParameter("@ServiceId", serviceId), 0);

            try
            {
                return SqlHelper.ExecuteDataset(CommonMethods.ConnectionString, CommandType.Text, sqlQuery, parameters);
            }
            catch (Exception ex)
            {
                throw new Exceptions.DatabaseOperationException("Unable to get the variables from table due to a database error.", ex);
            }
        }

        /// <summary>
        /// Gets variable details based on table name given
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet GetVariablesFromTable(string tableName)
        {
            string sqlQuery = "Select * FROM Master_Tables_Variables_Service t INNER JOIN master_service s ON t.Service_Number=s.Service_Number WHERE table_name =@TableName";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters.SetValue(new SqlParameter("@TableName", tableName), 0);

            try
            {
                return SqlHelper.ExecuteDataset(CommonMethods.ConnectionString, CommandType.Text, sqlQuery, parameters);
            }
            catch (Exception ex)
            {
                throw new Exceptions.DatabaseOperationException("Unable to get the variables from table using table name due to a database error.", ex);
            }
        }

        /// <summary>
        /// Gets table variables
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public static DataSet GetVariablesFromTable(string tableName, int serviceId)
        {
            string sqlQuery = "SELECT * FROM Master_Tables_Variables_Service t INNER JOIN master_service s ON t.Service_Number=s.Service_Number WHERE t.Service_Number=@ServiceId AND table_name=@TableName";

            SqlParameter[] parameters = new SqlParameter[2];
            parameters.SetValue(new SqlParameter("@ServiceId", serviceId), 0);
            parameters.SetValue(new SqlParameter("@TableName", tableName), 1);

            try
            {
                return SqlHelper.ExecuteDataset(CommonMethods.ConnectionString, CommandType.Text, sqlQuery, parameters);
            }
            catch (Exception ex)
            {
                throw new Exceptions.DatabaseOperationException("Unable to get the varables from table using serviceid and table name due to a database error.", ex);
            }
        }

        /// <summary>
        /// Gets the known as names for the spec'd table
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="variableName"></param>
        /// <param name="variableId"></param>
        /// <returns></returns>
        public static DataTable FetchKnownsByVariable(string tableName, string variableName, int variableId)
        {
            try
            {
                DataSet dsTableVariables = SqlHelper.ExecuteDataset(CommonMethods.ConnectionString, "USP_FetchVariable", tableName, variableName, variableId);
                if (dsTableVariables != null)
                {
                    if (dsTableVariables.Tables.Count > 0)
                    {
                        return dsTableVariables.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exceptions.DatabaseOperationException("Unable to get the variables using table table name, variable name, variableid due to a database error.", ex);
            }
            return null;
        }

        /// <summary>
        /// Gets countries list for country consumer pulse
        /// </summary>
        /// <returns></returns>
        public DataSet GetCountriesListCP()
        {
            try
            {
                return SqlHelper.ExecuteDataset(CommonMethods.ConnectionString, "getCountriesList_CP");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///  Get the variable and knownas names for the spec'd table
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public static DataSet GetVariables(string tableName, int serviceId)
        {
            string sqlQuery = "SELECT DISTINCT variable, knownas, display FROM Master_Tables_Variables_Service WHERE display='y' AND Service_Number=@ServiceId AND table_name=@TableName";

            SqlParameter[] parameters = new SqlParameter[2];

            parameters.SetValue(new SqlParameter("@ServiceId", serviceId), 0);
            parameters.SetValue(new SqlParameter("@TableName", tableName), 1);

            try
            {
                return SqlHelper.ExecuteDataset(CommonMethods.ConnectionString, CommandType.Text, sqlQuery, parameters);
            }
            catch (Exception ex)
            {
                throw new Exceptions.DatabaseOperationException("Unable to get the varaibles by using table name and serviceid due to a database error.", ex);
            }
        }

        /// <summary>
        /// Get the variables of a given table based on service
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public static DataSet GetTableVariables(string tableName, int serviceId)
        {
            try
            {
                return SqlHelper.ExecuteDataset(CommonMethods.ConnectionString, "USP_GetTableVariables", tableName, serviceId, DBNull.Value);
            }
            catch (Exception ex)
            {
                throw new Exceptions.DatabaseOperationException("Unable to get the table variables due to a database error.", ex);
            }
        }

        /// <summary>
        /// Get the variables of a given table to display the field labels
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public static DataSet GetTableVariablesToDisplay(string tableName, int serviceId)
        {
            try
            {
                char displayFlag = 'y';
                return SqlHelper.ExecuteDataset(CommonMethods.ConnectionString, "USP_GetTableVariables", tableName, serviceId, displayFlag);
            }
            catch (Exception ex)
            {
                throw new Exceptions.DatabaseOperationException("Unable to get the table variable to display due to a database error.", ex);
            }
        }

        /// <summary>
        /// Checks the columns in excel with the db table
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i) == columnName)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Update/Insert menu items
        /// </summary>
        /// <param name="menuItems"></param>
        /// <param name="strServices"></param>
        /// <returns></returns>
        public static bool UpsertMenuItems(string menuItems, string strServices)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(CommonMethods.ConnectionString, "USP_InsUpdMenuItems", menuItems, strServices);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exceptions.DatabaseOperationException("Unable to upsert the menu items due to a database error.", ex);
            }
        }

        /// <summary>
        /// Gets database table columns
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataSet GetDatabaseTableColumns(string tableName)
        {
            string sqlQuery = "SELECT column_name FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME=@TableName ORDER BY column_name";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters.SetValue(new SqlParameter("@TableName", tableName), 0);

            try
            {
                return SqlHelper.ExecuteDataset(CommonMethods.ConnectionString, CommandType.Text, sqlQuery, parameters);
            }
            catch (Exception ex)
            {
                throw new Exceptions.DatabaseOperationException("Unable to get the databse columns using table name due to a database error.", ex);
            }
        }

        /// <summary>
        /// Method to check whther user has the permission to read/write the file or not.
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="userNetworkLoginName"></param>
        /// <returns></returns>
        public static bool HasWriteAccessToFolder(string folderName, string userNetworkLoginName)
        {
            object obj = new object();

            NTAccount acc = new NTAccount(userNetworkLoginName);
            SecurityIdentifier secId = acc.Translate(typeof(SecurityIdentifier)) as SecurityIdentifier;
            DirectoryInfo dInfo = new DirectoryInfo(folderName);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            AuthorizationRuleCollection rules = dSecurity.GetAccessRules(true, true, typeof(SecurityIdentifier));
            foreach (FileSystemAccessRule ar in rules)
            {
                if (secId.CompareTo(ar.IdentityReference as SecurityIdentifier) == 0)
                {
                    if (ar.FileSystemRights.ToString().Contains("Write") || ar.FileSystemRights.ToString().Contains("Modify") || ar.FileSystemRights.ToString().Contains("FullControl"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Deletes a file from its temporary location.
        /// </summary>
        /// <param name="targetLocation"></param>
        /// <param name="fileName"></param>
        public static bool DeleteTempFile(string targetLocation, string fileName)
        {
            // do nothing, if target location is not temporary location
            if (targetLocation.ToLower().EndsWith("tempfiles")) return false;

            // do nothing if file name is empty
            if (string.IsNullOrEmpty(fileName)) return false;

            try
            {
                DirectoryInfo targetDir = new DirectoryInfo(targetLocation);

                foreach (FileInfo file in targetDir.GetFiles())
                {
                    if ((file.Attributes & FileAttributes.ReadOnly) == 0 && file.Name == fileName)
                    {
                        file.Delete();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }
    }
}
