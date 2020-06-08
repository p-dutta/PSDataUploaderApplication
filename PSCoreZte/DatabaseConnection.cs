using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace PSCoreZte
{
    public class DatabaseConnection
    {

        static string conf = ConfigurationManager.ConnectionStrings["connectionString"].ToString();

        static MySqlConnection db;

        public static MySqlConnection CreateConnection(){
            var db = new MySqlConnection(conf);
            db.Open();
            return db;
        }

        
        public static void CloseConnection(){
            if(db != null) {
                db.Close();
                db.Dispose();
                db = null;
            }
        }

    }


}






