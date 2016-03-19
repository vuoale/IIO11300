using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tehtava9
{
    public class Tietokanta
    {
        public static DataTable GetData()
        {
            try
            {
                using (SqlConnection conn =
                  new SqlConnection(Tehtava9.Properties.Settings.Default.Tietokanta))
                {
                    string sql = "SELECT * FROM customer";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt;
                    dt = new DataTable("dt");
                    da.Fill(dt);
                    conn.Close();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void InsertData(string firstname, string lastname, string address, string zip, string city)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Tehtava9.Properties.Settings.Default.Tietokanta);
                conn.Open();
                string sql = string.Format("INSERT INTO customer (firstname, lastname, address, zip, city) VALUES ('{0}','{1}','{2}','{3}','{4}')", firstname, lastname, address, zip, city);
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void DeleteData(string id)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Tehtava9.Properties.Settings.Default.Tietokanta);
                conn.Open();
                string sql = string.Format("DELETE FROM customer WHERE id = '{0}'", id);
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
