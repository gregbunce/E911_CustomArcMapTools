using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E911_Tools
{
    class clsStGeorge
    {

        // this method formats the STREET field for St George specifics 
        public static string StGeorgeSTREET(string strSTREET)
        {
            try
            {
                // check for the word "HIGHWAY" and replace it with "SR-"
                strSTREET = strSTREET.Replace("HIGHWAY ", "SR-");
                strSTREET = strSTREET.Replace("OLD SR-", "OLD HWY ");
                // trim the whole street concatination
                strSTREET = strSTREET.Trim();

                return strSTREET;
            }
            catch (Exception)
            {
                return "Error StGerogeSTREET";
            }
        }


        // this method formats the SALIAS1 field for St George specifics 
        public static string StGeorgeSALIAS1(string strSALIAS1)
        {
            try
            {
                // check for the word "HIGHWAY" and replace it with "SR-"
                strSALIAS1 = strSALIAS1.Replace("HIGHWAY ", "SR-");
                strSALIAS1 = strSALIAS1.Replace("OLD SR-", "OLD HWY ");
                strSALIAS1 = strSALIAS1.Trim();

                return strSALIAS1;
            }
            catch (Exception)
            {
                return "Error StGeorgeSALIAS1";
            }
        }


        // this method formats the SALILAS2 field for St George specifics 
        public static string StGeorgeSALIAS2(string strSALIAS2)
        {
            try
            {
                // check for the word "HIGHWAY" and replace it with "SR-"
                strSALIAS2 = strSALIAS2.Replace("HIGHWAY ", "SR-");
                strSALIAS2 = strSALIAS2.Replace("OLD SR-", "OLD HWY ");
                strSALIAS2 = strSALIAS2.Trim();

                return strSALIAS2;
            }
            catch (Exception)
            {
                return "Error StGeorgeSALIAS2";
            }
        }


    }
}
