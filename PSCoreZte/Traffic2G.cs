using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSCoreZte
{
    class Traffic2G
    {
        string file_to_parse_gz = @"F:\pscore\zte_extracted\BDCL_uMAC Daily export  performace_GZ_Num_" + DateTime.Now.ToString("yyyyMMddHH") + "05.csv";
        string file_to_parse_kt = @"F:\pscore\zte_extracted\BDCL_uMAC Daily export  performace_KT_Num_" + DateTime.Now.ToString("yyyyMMddHH") + "05.csv";

        List<string> FilesToParse = new List<string>();

        List<Traffic2G_Model> traffic2GList = new List<Traffic2G_Model>();

        public int parseTraffic2GFile()
        {
            int line_count = 0;

            FilesToParse.Add(file_to_parse_gz);
            FilesToParse.Add(file_to_parse_kt);



            foreach (string file_to_parse in FilesToParse)
            {
                string nodeName = "", st_time = "";
                long gbSent, gbReceived;

                if (file_to_parse.Contains("_GZ_"))
                {
                    nodeName = "GZ";
                }
                else if (file_to_parse.Contains("_KT_"))
                {
                    nodeName = "KT";
                }

                char[] delimiterChars = new char[5];

                try
                {
                    if (!File.Exists(file_to_parse))
                        throw new Exception();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Util.writeLog(new StackTrace(1).GetFrame(0).GetMethod().Name, e);
                    return 0;
                }


                using (StreamReader sr = File.OpenText(@file_to_parse))
                {
                    String input;
                    string[] tokens;
                    sr.ReadLine();
                    line_count = 0;

                    while ((input = sr.ReadLine()) != null)
                    {

                        delimiterChars[0] = ',';
                        tokens = input.Split(delimiterChars[0]);
                        st_time = tokens[1];
                        gbReceived = Convert.ToInt64(tokens[12]);
                        gbSent = Convert.ToInt64(tokens[13]);
                        
                        //Console.WriteLine((double)gbSent/1024);
                       
                        DateTime oDate = DateTime.ParseExact(st_time, "yyyy-MM-dd HH:mm:ss", null);
                        traffic2GList.Add(new Traffic2G_Model { sentToGbOverIpInKB = Math.Round((double)gbSent/1024, 2), rcvdFrmGbOverIpInKB = Math.Round((double)gbReceived/1024, 2), resultTime = oDate, nodeName = nodeName });
                        line_count++;
                    }
                    sr.Close();
                }

            }

            string queryString = "";
            foreach (var traffic2g in traffic2GList)
            {
                queryString += "INSERT into ps_sgsn_2g_traffic ( gb_mode_downlink_kbytes,gb_mode_uplink_kbytes,node_name,vendor,result_time) values ('" + traffic2g.rcvdFrmGbOverIpInKB + "','" + traffic2g.sentToGbOverIpInKB + "','" + traffic2g.nodeName + "','" + traffic2g.vendor + "','" + traffic2g.resultTime.ToString("yyyy-MM-dd HH:mm:ss") + "');";
            }

            try
            {

                MySqlConnection cn = DatabaseConnection.CreateConnection();
                MySqlCommand cmd = new MySqlCommand(queryString, cn);
                int inserted_rows = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                DatabaseConnection.CloseConnection();
                Console.WriteLine(ex.ToString());
                Util.writeLog(new StackTrace(1).GetFrame(0).GetMethod().Name, ex);
                return 0;
            }
            DatabaseConnection.CloseConnection();

            return line_count;
        }
    }

    class Traffic2G_Model
    {
        public double sentToGbOverIpInKB { get; set; }
        public double rcvdFrmGbOverIpInKB { get; set; }
        public DateTime resultTime { get; set; }
        public string vendor { get; set; }
        public string nodeName { get; set; }

        public Traffic2G_Model()
        {
            vendor = "zte";
        }


    }


}
