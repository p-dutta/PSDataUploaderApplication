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
    class PagingSuccessRate3G
    {

        string file_to_parse_gz = @"F:\pscore\zte_extracted\BDCL_uMAC Daily export  performace_GZ_Num_" + DateTime.Now.ToString("yyyyMMddHH") + "05.csv";
        string file_to_parse_kt = @"F:\pscore\zte_extracted\BDCL_uMAC Daily export  performace_KT_Num_" + DateTime.Now.ToString("yyyyMMddHH") + "05.csv";

        List<string> FilesToParse = new List<string>();

        List<PagingSuccessRate3G_Model> dataList = new List<PagingSuccessRate3G_Model>();

        public int parsePagingSucRate3GFile()
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
                    line_count = 0;
                    //int i = 0;

                    while ((input = sr.ReadLine()) != null)
                    {

                        delimiterChars[0] = ',';
                        tokens = input.Split(delimiterChars[0]);
                        st_time = tokens[1];
                        DateTime oDate = DateTime.ParseExact(st_time, "yyyy-MM-dd HH:mm:ss", null);

                        PagingSuccessRate3G_Model data = new PagingSuccessRate3G_Model();
                        if (file_to_parse.Contains("_GZ_"))
                        {
                            data.timesOfPsPagingSentToIu = Convert.ToInt32(tokens[56]);
                            data.timesOfSuccessfulPsPaging = Convert.ToInt32(tokens[55]);
                        }
                        else if (file_to_parse.Contains("_KT_"))
                        {
                            data.timesOfPsPagingSentToIu = Convert.ToInt32(tokens[57]);
                            data.timesOfSuccessfulPsPaging = Convert.ToInt32(tokens[58]);
                        }
                        
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

                queryString += "INSERT into ps_sgsn_3g_paging_success_rate ( iu_mode_packet_paging_request_times,iu_mode_packet_paging_response_by_service_request_times,node_name,vendor,result_time) values ('" + data.timesOfPsPagingSentToIu + "','" + data.timesOfSuccessfulPsPaging + "','" + data.nodeName + "','" + data.vendor + "','" + data.resultTime.ToString("yyyy-MM-dd HH:mm:ss") + "');";
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

    class PagingSuccessRate3G_Model
    {
        public int timesOfPsPagingSentToIu { get; set; }
        public int timesOfSuccessfulPsPaging { get; set; }
        public DateTime resultTime { get; set; }
        public string vendor { get; set; }
        public string nodeName { get; set; }


        public PagingSuccessRate3G_Model()
        {
            vendor = "zte";
        }


    }
}



