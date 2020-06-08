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
    class AttachSuccessRate2G
    {

        string file_to_parse_gz = @"F:\pscore\zte_extracted\BDCL_uMAC Daily export  performace_GZ_Num_" + DateTime.Now.ToString("yyyyMMddHH") + "05.csv";
        string file_to_parse_kt = @"F:\pscore\zte_extracted\BDCL_uMAC Daily export  performace_KT_Num_" + DateTime.Now.ToString("yyyyMMddHH") + "05.csv";

        List<string> FilesToParse = new List<string>();

        List<ASR2G_Model> asr2GList = new List<ASR2G_Model>();

        public int parseAttaSuccRFile()
        {
            int line_count = 0;

            FilesToParse.Add(file_to_parse_gz);
            FilesToParse.Add(file_to_parse_kt);

            

            foreach (string file_to_parse in FilesToParse)
            {
                string nodeName="",st_time="";
                //Console.WriteLine("{0} does not exist.", file_to_parse);

                if(file_to_parse.Contains("_GZ_")) {
                    nodeName = "GZ";
                } else if(file_to_parse.Contains("_KT_")) {
                    nodeName = "KT";
                }


                //int j = 0;
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

                /*if (!File.Exists(file_to_parse))
                {
                    Console.WriteLine("{0} does not exist.", file_to_parse);
                    return 0;
                }*/

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
                        //Console.WriteLine(oDate.ToString());
                        /*EmailData data = new EmailData();
                        data.FirstName = "JOhn";
                        data.LastName = "Smith";
                        data.Location = "Los Angeles";

                        lstemail.Add(data);*/

                        asr2GList.Add(new ASR2G_Model { attemptedTimesGPRSAttachProcedure = Convert.ToInt32(tokens[6]), successfulTimesGPRSAttachProcedure = Convert.ToInt32(tokens[7]), resultTime = oDate, nodeName = nodeName });          
                        line_count++;
                    }
                    sr.Close();
                }
   
            }

            string queryString = "";
            foreach (var asr2g in asr2GList)

            {
                //Console.WriteLine(asr2g.resultTime.ToString("yyyy-MM-dd HH:mm:ss"));
                queryString += "INSERT into ps_sgsn_2g_attach_success_rate ( gb_mode_gprs_attach_request_times,gb_mode_gprs_attach_accept_times,node_name,vendor,result_time) values ('" + asr2g.attemptedTimesGPRSAttachProcedure + "','" + asr2g.successfulTimesGPRSAttachProcedure + "','" + asr2g.nodeName + "','" + asr2g.vendor + "','" + asr2g.resultTime.ToString("yyyy-MM-dd HH:mm:ss") + "');";
            }

            //Console.WriteLine(queryString);

            try
            {
                
                MySqlConnection cn = DatabaseConnection.CreateConnection();
                MySqlCommand cmd = new MySqlCommand(queryString, cn);
                //MySqlDataReader rdr = cmd.ExecuteReader();
                int inserted_rows = cmd.ExecuteNonQuery();

                /*while (rdr.Read())
                {
                    Console.WriteLine(rdr[0] + " -- " + rdr[1]);
                }*/
                
            }
            catch (Exception ex)
            {
                DatabaseConnection.CloseConnection();
                Console.WriteLine(ex.ToString());
                Util.writeLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return 0;
            }
            DatabaseConnection.CloseConnection();
            
            return line_count;
        }



    }

    class ASR2G_Model
    {
        public int attemptedTimesGPRSAttachProcedure { get; set; }
        public int successfulTimesGPRSAttachProcedure { get; set; }
        public DateTime resultTime { get; set; }
        public string vendor { get; set; }
        public string nodeName { get; set; }

        public ASR2G_Model()
        {
            vendor = "zte";
        }


    }
}
