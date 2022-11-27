
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ETLAPIFramework.ConsoleApp.Data.Common
{
    public class LoadDataTableToSQL
    {
       
        SqlBulkCopy bulkCopy = null;

        public void BulkInsert(DataTable jsonData, string DestinationTableName)
        {

             string ConnectionString =@"Data Source=SUBBUCHAND19\SQLEXPRESS;" +"Initial Catalog=AdventureWorks2019;" +"Integrated Security=SSPI;";

            Console.WriteLine("BulkInsert - Saving  Records into " + DestinationTableName + " begin...");
            try
            {
                string Connnection = ConnectionString;
                using (SqlConnection sqlCon = new SqlConnection(Connnection))
                {
                    sqlCon.Open();
                    if (bulkCopy != null)
                    {
                        bulkCopy.Close();
                        ((IDisposable)bulkCopy).Dispose();
                        bulkCopy = null;
                    }
                    bulkCopy = null;
                    GC.Collect();
                    GC.WaitForFullGCComplete();

                    bulkCopy = new SqlBulkCopy(sqlCon, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction, null);
                    bulkCopy.BulkCopyTimeout = 0;
                    bulkCopy.DestinationTableName = DestinationTableName;
                    foreach (DataColumn column in jsonData.Columns)
                    {

                        bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(column.ColumnName, column.ColumnName));
                    }
                    bulkCopy.BatchSize = 1000;
                    bulkCopy.WriteToServer(jsonData);
                    bulkCopy.Close();

                    jsonData = null;
                    GC.Collect();
                    GC.WaitForFullGCComplete();
                   
                    Console.WriteLine("BulkInsert - Saving  Records into " + DestinationTableName + " end...");
                }
            }
            catch (Exception ex)
            {
               Console.WriteLine("BulkInsert - Error Occured while Saving  Records into " + DestinationTableName + ". Error " + ex.InnerException.Message);
                throw;
            }
            finally
            {
            }
        }

        public void DeleteRecords(string destinationTableName)
        {
            string ConnectionString = @"Data Source=SUBBUCHAND19\SQLEXPRESS;" + "Initial Catalog=AdventureWorks2019;" + "Integrated Security=SSPI;";
            Console.WriteLine("DeleteRecords - Deleting Records from Table  " + destinationTableName + " Begin...");
            string sqlQuery = string.Empty;
            try
            {
                string Connnection = ConnectionString;
                using (SqlConnection sqlCon = new SqlConnection(Connnection))
                {
                    sqlQuery = "truncate table " + destinationTableName;
                    using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.CommandTimeout = 0;
                        sqlCon.Open();
                        sqlCmd.ExecuteNonQuery();

                        
                        Console.WriteLine("DeleteRecords - Deleting Records from Table  " + destinationTableName + " End...");
                    }
                }
            }
            catch (Exception ex)
            {
               Console.WriteLine("Delete Records - Error Occured while Deleting Records from Table  " + destinationTableName + ". Error: " + ex.Message + ". Details: " + ex.StackTrace);
                throw;
            }
            finally
            {
            }
        }

    }


}