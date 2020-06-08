using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSCoreZte
{
    class DemoMysqlOperation
    {

         //Console.WriteLine("Welcome to the C# Station Tutorial!");
            //Console.ReadLine();
            //string connStr = "server=localhost;user=root;database=world;port=3306;password=******";
            //string connStr = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
            //MySqlConnection conn = new MySqlConnection(connStr);
           
        public void demoOperations() {
            try
            {
                //DatabaseConnection.CreateConnection();
                /*Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                Console.WriteLine("opened...");*/
                MySqlConnection cn = DatabaseConnection.CreateConnection();
                // Perform database operations
                string sql = "SELECT * FROM ps_sgsn_4g_s1_inter_mme_com_tau_suc_rate";
                MySqlCommand cmd = new MySqlCommand(sql, cn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine(rdr[0] + " -- " + rdr[1]);
                }
                //rdr.Close();
                /*string sql = "INSERT INTO plabon (Name, HeadOfState, Continent) VALUES ('Disneyland','Mickey Mouse', 'North America')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            DatabaseConnection.CloseConnection();
            Console.WriteLine("Done.");
            Console.ReadLine();
        }
            

    }
}
