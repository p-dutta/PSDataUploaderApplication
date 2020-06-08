
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
    class S1ModeIntraMMECombinedTauSucRate
    {

        string file_to_parse_gz = @"F:\pscore\zte_extracted\BDCL_uMAC Daily export  performace_GZ_Num_" + DateTime.Now.ToString("yyyyMMddHH") + "05.csv";
        string file_to_parse_kt = @"F:\pscore\zte_extracted\BDCL_uMAC Daily export  performace_KT_Num_" + DateTime.Now.ToString("yyyyMMddHH") + "05.csv";

        List<string> FilesToParse = new List<string>();

        List<S1ModeIntraMMECombinedTauSucRate_Model> dataList = new List<S1ModeIntraMMECombinedTauSucRate_Model>();

        public int parseS1ModeIntraMMECombinedTauSucRate4GFile()
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

                        S1ModeIntraMMECombinedTauSucRate_Model data = new S1ModeIntraMMECombinedTauSucRate_Model();
                        data.timesOfIntraMmeTauWithSgwChangeAttempt = Convert.ToInt32(tokens[32]);
                        data.timesOfIntraMmeTauWithoutSgwChangeAttempt = Convert.ToInt32(tokens[33]);
                        data.SuccessfulTimesOfIntraMmeTauWithSgwChangeAttempt = Convert.ToInt32(tokens[30]);
                        data.SuccessfulTimesOfIntraMmeTauWithoutSgwChangeAttempt = Convert.ToInt32(tokens[31]);
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

                queryString += "INSERT into ps_sgsn_4g_s1_intra_mme_com_tau_suc_rate ( tau_request_times_sgw_change,tau_request_times_sgw_not_change,tau_suc_times_for_eps_non_eps_sgw_change,tau_suc_times_for_eps_non_eps_sgw_not_change,tau_suc_times_for_eps_sms_sgw_change,tau_suc_times_for_eps_sms_sgw_not_change,node_name,vendor,result_time) values ('" + data.timesOfIntraMmeTauWithSgwChangeAttempt + "','" + data.timesOfIntraMmeTauWithoutSgwChangeAttempt + "',0,0,'" + data.SuccessfulTimesOfIntraMmeTauWithSgwChangeAttempt + "','" + data.SuccessfulTimesOfIntraMmeTauWithoutSgwChangeAttempt + "','" + data.nodeName + "','" + data.vendor + "','" + data.resultTime.ToString("yyyy-MM-dd HH:mm:ss") + "');";
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

    class S1ModeIntraMMECombinedTauSucRate_Model
    {
        public int timesOfIntraMmeTauWithSgwChangeAttempt { get; set; }
        public int timesOfIntraMmeTauWithoutSgwChangeAttempt { get; set; }
        public int SuccessfulTimesOfIntraMmeTauWithSgwChangeAttempt { get; set; }
        public int SuccessfulTimesOfIntraMmeTauWithoutSgwChangeAttempt { get; set; }
        public DateTime resultTime { get; set; }
        public string vendor { get; set; }
        public string nodeName { get; set; }

        public S1ModeIntraMMECombinedTauSucRate_Model()
        {
            vendor = "zte";
        }


    }
}




