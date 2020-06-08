using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSCoreZte
{
    class Util
    {
        public static void writeLog(string counter_name, Exception ex)
        {
  
            using (StreamWriter writer = new StreamWriter("error.log", true))
            {
                writer.WriteLine(DateTime.Now.ToString("yyMMddHHmmss\t3\t") + counter_name + "\t" + ex.Message + "\n");
            }

        }


    }
}
