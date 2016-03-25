using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tehtava10
{
    public class DBPelaajat
    {
        public static DataTable GetPlayers(string connStr)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connStr))
                {
                    string sql = "SELECT id, etunimi, sukunimi, seura, arvo FROM Pelaajat";
                    OleDbCommand cmd = new OleDbCommand(sql, conn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataTable dt = new DataTable("Pelaajat");
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int UpdatePelaaja(string connStr, int id, string etunimi, string sukunimi, string seura, int arvo)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connStr))
                {
                    conn.Open();
                    string sql = string.Format("UPDATE Pelaajat SET etunimi='{1}', sukunimi='{2}', seura='{3}', arvo={4} WHERE id={0}", id, etunimi, sukunimi, seura, arvo);
                    OleDbCommand cmd = new OleDbCommand(sql, conn);
                    int lkm = cmd.ExecuteNonQuery();
                    return lkm;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
