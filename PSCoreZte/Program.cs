using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace PSCoreZte
{
    public class Program
    {
        

        

        static void Main(string[] args)
        {
            file_rename();

            AttachSuccessRate2G attach_suc_rate2g = new AttachSuccessRate2G();
            Sau2G sau_2g = new Sau2G();
            Traffic2G traffic_2g = new Traffic2G();
            InterSgsnRauSuccessRate2G inter_sgsn_ra_suc_rate_2g = new InterSgsnRauSuccessRate2G();
            IntraSgsnRauSuccessRate2G intra_sgsn_ra_suc_rate_2g = new IntraSgsnRauSuccessRate2G();
            PagingSuccessRate2G paging_suc_rate_2g = new PagingSuccessRate2G();
            PdpActivationSuccessRate2G pdp_activation_suc_rate_2g = new PdpActivationSuccessRate2G();
            Pdp2G pdp_2g = new Pdp2G();
            AttachSuccessRate3G attach_suc_rate3g = new AttachSuccessRate3G();
            Sau3G sau_3g = new Sau3G();
            InterSgsnRauSuccessRate3G inter_sgsn_ra_suc_rate_3g = new InterSgsnRauSuccessRate3G();
            IntraSgsnRauSuccessRate3G intra_sgsn_ra_suc_rate_3g = new IntraSgsnRauSuccessRate3G();
            PagingSuccessRate3G paging_suc_rate_3g = new PagingSuccessRate3G();
            PdpActivationSuccessRate3G pdp_activation_suc_rate_3g = new PdpActivationSuccessRate3G();
            Pdp3G pdp_3g = new Pdp3G();
            S1ModeCombinedAttachSuccessRate4G s1_mode_com_attach_suc_rate_4g = new S1ModeCombinedAttachSuccessRate4G();
            MaxSauInS1Mode4G max_sau_s1_4g = new MaxSauInS1Mode4G();
            PacketPagingSuccessRate4G paging_suc_rate_4g = new PacketPagingSuccessRate4G();
            MaxBearerNumber4G max_bearer_number_4g = new MaxBearerNumber4G();
            DefaultBearerActivationSucRate4G activation_success_rate_4g = new DefaultBearerActivationSucRate4G();
            GGSNPdpActivationSuccessRate2G3G ggsn_pdp_activation_suc_rate = new GGSNPdpActivationSuccessRate2G3G();
            S1ModeInterMMECombinedTauSucRate s1_mode_inter_mme_tau_suc_rate = new S1ModeInterMMECombinedTauSucRate();
            S1ModeIntraMMECombinedTauSucRate s1_mode_intra_mme_tau_suc_rate = new S1ModeIntraMMECombinedTauSucRate();
            GGSNGiTraffic ggsn_gi_traffic = new GGSNGiTraffic();
            GGSNThroughput2G3G ggsn_throughput_2g_3g = new GGSNThroughput2G3G();
            GGSNSGITraffic4G ggsn_sgi_traffic_4g = new GGSNSGITraffic4G();
            GGSNSGIThroughput4G ggsn_sgi_throughput_4g = new GGSNSGIThroughput4G();
            GGSNTotalTraffic ggsn_total_traffic = new GGSNTotalTraffic();

          
            
            attach_suc_rate2g.parseAttaSuccRFile();
            sau_2g.parseSauFile();
            traffic_2g.parseTraffic2GFile();
            inter_sgsn_ra_suc_rate_2g.parseInterSgsnRauSucRateFile();
            intra_sgsn_ra_suc_rate_2g.parseIntraSgsnRauSucRateFile();
            paging_suc_rate_2g.parsePagingSucRate2GFile();
            pdp_activation_suc_rate_2g.parsePdpActiSucRate2GFile();
            pdp_2g.parsePdp2GFile();
            attach_suc_rate3g.parseAttaSuccR3GFile();
            sau_3g.parseSau3GFile();
            inter_sgsn_ra_suc_rate_3g.parseInterSgsnRauSucRate3GFile();
            intra_sgsn_ra_suc_rate_3g.parseIntraSgsnRauSucRate3GFile();
            paging_suc_rate_3g.parsePagingSucRate3GFile();
            pdp_activation_suc_rate_3g.parsePdpActiSucRate3GFile();
            pdp_3g.parsePdp3GFile();
            s1_mode_com_attach_suc_rate_4g.parseS1CombAttachSucRate4GFile();
            max_sau_s1_4g.parseMaxSauInS1Mode4GFile();
            paging_suc_rate_4g.parsePagingSucRate4GFile();
            max_bearer_number_4g.parseMaxBearerNumber4GFile();
            activation_success_rate_4g.parseBearerActiSucRate4GFile();
            ggsn_pdp_activation_suc_rate.parseGgsnPdpActiSucRate2G3GFile();
            s1_mode_inter_mme_tau_suc_rate.parseS1ModeInterMMECombinedTauSucRate4GFile();
            s1_mode_intra_mme_tau_suc_rate.parseS1ModeIntraMMECombinedTauSucRate4GFile();
            ggsn_gi_traffic.parseGgsnGiTrafficFile();
            ggsn_throughput_2g_3g.parseGgsnThroughput2G3GFile();
            ggsn_sgi_traffic_4g.parseGgsnSgiTraffic4GFile();
            ggsn_sgi_throughput_4g.parseGgsnSgiThroughput4GFile();
            ggsn_total_traffic.parseGGSNTotalTrafficFile();

        }

       

        public static void file_rename()
        {
            int i = 0;
            DirectoryInfo dInfo = new DirectoryInfo(@"F:\pscore\zte_extracted\");
            var regex = new Regex(".*_(.*)_.*");
            string[] csvFiles = Directory.GetFiles(@"F:\pscore\zte_extracted\", "*.csv");
            foreach (FileInfo fName in dInfo.GetFiles("*.csv"))
            {

                var to_del = regex.Match(csvFiles[i]).Groups[1].Value;
                //Console.WriteLine(regex.Match(csvFiles[i]).Groups[1].Value);
                string new_file_name = csvFiles[i].Replace(to_del, "Num");
                System.IO.File.Move(csvFiles[i], new_file_name);
                i = i + 1;

            }
              
        }

    }
}
