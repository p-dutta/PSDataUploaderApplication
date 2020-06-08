using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSCoreZte
{
    class DemoCodes
    {
        /*static void DirSearch(string sDir)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        Console.WriteLine(f);
                    }
                    DirSearch(d);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        public void demoFubc()
        {
            //DirectoryInfo d = new DirectoryInfo(@"D:\Test");//Assuming Test is your Folder
            FileInfo[] Files = dInfo.GetFiles("*.csv"); //Getting Text files
            string str = "";
            foreach (FileInfo file in Files)
            {
                using (StreamReader sr = file.OpenText())
                {
                    String input;
                    string[] tokens;

                    //sr.ReadLine();
                    //sr.ReadLine();

                    Console.WriteLine(sr.ReadLine());
                    Console.WriteLine(sr.ReadLine());

                    int line_count = 0;
                    int i = 0;
                    char[] delimiterChars = new char[5];

                    //while ((input = sr.ReadLine()) != null)
                    //{

                    //    delimiterChars[0] = ',';
                    //    tokens = input.Split(delimiterChars);

                    //    bs[line_count].Id = Convert.ToDouble(tokens[0]);
                    //    //bs[line_count].ST_DATES = tokens[1].Replace("-", "/");
                    //    bs[line_count].ST_DATES = tokens[1];//.Substring(0, 13);
                    //    bs[line_count].NE_Location = tokens[4];
                    //    bs[line_count].SUCC_GPRS_ATTACH_GSM = tokens[6];
                    //    bs[line_count].SUCC_GPRS_ATTACH_UMTS = tokens[7]; //Convert.ToDouble(tokens[6]);
                    //    bs[line_count].SUCC_EPS_ATTACH = tokens[8];
                    //    bs[line_count].FILE_TYPE = "BDCL 2-3-4G attach success rate";
                    //    i = i + 61;
                    //    line_count++;
                    //}
                    sr.Close();
                }
            }
        }



        /*string partialName = "171_s";
            DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(@"c:\");
            FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles("*" + partialName + "*.*");

            foreach (FileInfo foundFile in filesInDir)
            {
                string fullName = foundFile.FullName;
                Console.WriteLine(fullName);
            }


            string file_to_parse = @"F:\pscore\zte_extracted\BDCL_uMAC Daily export performance_Gz_Num_" + DateTime.Now.ToString("yyyyMMddHH") + "05.csv";

            */


        /*
         try
            {
                return uop.ConvertListToDataTable(list);
            }
            catch(Exception ex)
            {
                if(DateTime.Now.ToString("dd")!="17")
                SMSAlert.sendSmsAlert("2673", "008801962400740".Split(','), ConfigurationManager.AppSettings["SMSC_HOST"], "prone", "ronep", DateTime.Now.ToString("yyMMddHHmmss 3 ") + currentTask + " " + ex.Message);

                StreamWriter sw = new StreamWriter("error.log", true);
                sw.WriteLine(DateTime.Now.ToString("yyMMddHHmmss\t3\t") + currentTask+"\t"+ex.Message + "\n");
                sw.Close();
                sw.Dispose();

                DataTable errorTable = new DataTable();
                errorTable.Columns.Add("ERROR");
                return errorTable;
            }
         
         */

        //get the header names from csv file
        /*
         
                     public int parseGgsnGiTrafficFile()
        {
            int line_count = 0, peak_thr_umts_index=0, peak_thr_gsm_index=0;

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
                    return 0;
                }

                using (StreamReader sr = File.OpenText(@file_to_parse))
                {
                    string input;
                    string[] tokens;
                    string[] tokens_header;
                    int header_index = 0;
                    string header_input = sr.ReadLine();
                    tokens_header = header_input.Split(',');
                    foreach (string value in tokens_header)
                    {
                        Console.WriteLine("FOREACH ITEM: " + value.Trim()+"|index" + header_index);
                        if(value.Trim() == "Peak Throughput UMTS(Gbps)") {
                            peak_thr_umts_index = header_index;
                        }
 
                        else if(value.Trim() == "Peak Throughput GSM (Gbps)") {
                            peak_thr_gsm_index = header_index;
                        }
 


                        header_index++;
                    }
                    Console.WriteLine("{0} and {1}", peak_thr_umts_index, peak_thr_gsm_index);

                    return 0;
                   }
         
         */



    }
}
