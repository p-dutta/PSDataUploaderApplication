
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
    class GGSNThroughput2G3G
    {
        string file_to_parse_gz = @"F:\pscore\zte_extracted\BDCL_vGW Daily export  performace_GZ_Num_" + DateTime.Now.ToString("yyyyMMddHH") + "05.csv";
        string file_to_parse_kt = @"F:\pscore\zte_extracted\BDCL_vGW Daily export  performace_KT_Num_" + DateTime.Now.ToString("yyyyMMddHH") + "05.csv";

        List<string> FilesToParse = new List<string>();

        List<GGSNThroughput2G3G_Model> dataList = new List<GGSNThroughput2G3G_Model>();

        public int parseGgsnThroughput2G3GFile()
        {
            int line_count = 0;

            FilesToParse.Add(file_to_parse_gz);
            FilesToParse.Add(file_to_parse_kt);

            foreach (string file_to_parse in FilesToParse)
            {
                string nodeName = "", st_time = "";

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

                    while ((input = sr.ReadLine()) != null)
                    {

                        delimiterChars[0] = ',';
                        tokens = input.Split(delimiterChars[0]);
                        st_time = tokens[1];
                        DateTime oDate = DateTime.ParseExact(st_time, "yyyy-MM-dd HH:mm:ss", null);

                        GGSNThroughput2G3G_Model data = new GGSNThroughput2G3G_Model();
                        data.peakThroughputUmts_in_Gbps = Convert.ToDouble(tokens[8]);
                        data.peakThroughputGsm_in_Gbps = Convert.ToDouble(tokens[7]);
                        data.resultTime = oDate;
                        data.nodeName = nodeName;
                        dataList.Add(data);
                        line_count++;
                    }
                    sr.Close();
                }

            }

            string queryString = "";
            foreach (var data in dataList)
            {

                queryString += "INSERT into ps_ggsn_2g_3g_gi_throughput ( peak_throughput_umts_in_Gbps_zte,peak_throughput_gsm_in_Gbps_zte,node_name,vendor,result_time) values ('" + data.peakThroughputUmts_in_Gbps + "','" + data.peakThroughputGsm_in_Gbps + "','" + data.nodeName + "','" + data.vendor + "','" + data.resultTime.ToString("yyyy-MM-dd HH:mm:ss") + "');";
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

    class GGSNThroughput2G3G_Model
    {
        public double peakThroughputUmts_in_Gbps { get; set; }
        public double peakThroughputGsm_in_Gbps { get; set; }
        public DateTime resultTime { get; set; }
        public string vendor { get; set; }
        public string nodeName { get; set; }

        public GGSNThroughput2G3G_Model()
        {
            vendor = "zte";
        }


    }
}




