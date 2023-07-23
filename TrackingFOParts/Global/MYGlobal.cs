using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingFOParts.Global
{
    class MYGlobal
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static string VERSION = "v3.8.5";

        public static String USE_DB = "";
        public static String USE_DB_NAME = "";

        public static string STEP1_ = "PART_REVD";
        public static string STEP2_ = "PROC_STARD";
        public static string STEP3_ = "PROC_END";
        public static string STEP4_ = "QC_DONE";
        public static string STEP5_ = "SENT_DELV";


        public static String getCString()
        {
            USE_DB = MYGlobal.GetSettingValue("DB");
            USE_DB_NAME = MYGlobal.GetSettingValue("DB_NAME");

            log.Info("use_db=" + MYGlobal.USE_DB);


            if (MYGlobal.USE_DB.Equals("APMFG"))
            {
                return @"Data Source=APMFGDBS001;Initial Catalog=" + USE_DB_NAME + ";User ID=CIMS_USER;Password=D2c?Z7w^E8e!";
            }

            else if (MYGlobal.USE_DB.Equals("Sing3"))
            {
                return @"Data Source=DKTP611587\SQLEXPRESS;Initial Catalog=" + USE_DB_NAME + ";User ID=sa;Password=ABCD7890&*()";
            }

            else if (MYGlobal.USE_DB.Equals("Dev"))
            {
                return @"Data Source=DKTP611587\SQLEXPRESS;Initial Catalog=" + USE_DB_NAME + ";User ID=sa;Password=ABCD7890&*()";
            }

            else if (MYGlobal.USE_DB.Equals("Kam"))
            {
                return @"Data Source=NTBK616741\SQLEXPRESS;Initial Catalog=" + USE_DB_NAME + ";User ID=sa;Password=ABCD7890&*();";
            }

            return null;

        }




        public static String getCurretnDate()
        {
            DateTime dateTime = DateTime.Now;
            return dateTime.ToString("yyyy-MM-dd");
        }

        public static String getCurretnDateTime()
        {
            DateTime dateTime = DateTime.Now;
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static String getCurretnTime()
        {
            DateTime dateTime = DateTime.Now;
            return dateTime.ToString("HH:mm:ss");
        }

        public static String getPromisedDate()
        {
            //curretn date add 56days (8weeks * 7)
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddDays(56);
            return dateTime.ToString("yyyy-MM-dd");
        }

        public static string GetSettingValue(string paramName)
        {
            return String.Format(ConfigurationManager.AppSettings[paramName]);
        }

    }
}
