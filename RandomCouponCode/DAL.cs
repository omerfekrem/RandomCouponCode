using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace RandomCouponCode
{
    public class DAL
    {

        public static void BulkInsert(DataTable dt, string tableName)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Example;Integrated Security=True;");
                SqlBulkCopy bulkCopy =
                new SqlBulkCopy
                (
                con,
                SqlBulkCopyOptions.TableLock |
                SqlBulkCopyOptions.FireTriggers |
                SqlBulkCopyOptions.UseInternalTransaction,
                null
                );
                bulkCopy.DestinationTableName = tableName;
                if (tableName == "CouponCodes")
                {
                    bulkCopy.ColumnMappings.Add("CouponCode", "RandomCouponCode");
                }
                con.Open();
                bulkCopy.WriteToServer(dt);
                con.Close();
            }
            catch (Exception ex)
            {


            }
        }
    }
}
