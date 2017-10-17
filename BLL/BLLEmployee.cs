using System;
using Microsoft.ApplicationBlocks.Data;
using System.Data;

namespace BLL
{
    public static class BLLEmployee 
    {
        public static int InsertEmployee(string Name, int Age, int Salary)
        {
            if (Age < 20 || Name == "")
            {
                return -1;
            }

            try
            {
                int resultQuery = SqlHelper.ExecuteNonQuery(CommonMethods.ConnectionString, "USP_InsEmployee", Name, Age, Salary);
                return resultQuery;
            }
            catch (Exception ex)
            {
                throw new Exceptions.DatabaseOperationException("Unable to insert employee details due to a database error.", ex);
            }
        }

        public static DataSet GetEmployee(int Id)
        {
            try
            {
                DataSet ds = SqlHelper.ExecuteDataset(CommonMethods.ConnectionString, "USP_GetEmployee", Id);
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exceptions.DatabaseOperationException("Unable to insert employee details due to a database error.", ex);
            }
        }
    }
}
