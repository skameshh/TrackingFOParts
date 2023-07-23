using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingFOParts.Global;

//
namespace TrackingFOParts.DB
{
   public class DBUtils
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public static FOStatusDao getPartCurrentStatus(String confNum)
        {
            FOStatusDao dao = null;
            string sql = " select * from t_fo_current_status WITH (NOLOCK) where  CON_NUMBER='" + confNum +"'";

            using (SqlConnection cnn = new SqlConnection(MYGlobal.getCString()))
            {
                cnn.Open();
                log.Info("getPartCurrentStatus(), = " + confNum + " sql = " + sql);
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    //cmd.Parameters.AddWithValue("@CON_NUMBER", confNum);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        try
                        {
                            while (reader.Read())
                            {
                                dao = new FOStatusDao();
                                dao.Id = (int)reader["ID"];
                                dao.VendorId = (string)reader["VENDOR_ID"];
                                dao.ConfNumber = confNum;// (string)reader["CON_NUMBER"];
                                dao.CurrentStatus = (string)reader["CURRENT_STATUS"];
                                dao.LastUpdOn = (DateTime)reader["LAST_UPD_ON"];
                            }
                        }catch(Exception ee)
                        {
                            log.Info("getPartCurrentStatus()v Error " + ee.Message);
                        }
                    }
                }
            }
            return dao;
        }

        public static int doInsertCurrentStatus(FOStatusDao dao)
        {
            int affRows = 0;
            String SQL = "INSERT INTO t_fo_current_status (VENDOR_ID ,CON_NUMBER, CURRENT_STATUS , LAST_UPD_ON ) VALUES" +
                "(@VENDOR_ID , @CON_NUMBER, @CURRENT_STATUS , @LAST_UPD_ON )";
            try
            {
                using (SqlConnection cnn = new SqlConnection(MYGlobal.getCString()))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(SQL, cnn))
                    {
                        cmd.CommandType = CommandType.Text;

                        //cmd.Parameters.AddWithValue("@ID", dao.Id);
                        cmd.Parameters.AddWithValue("@VENDOR_ID", dao.VendorId);
                        cmd.Parameters.AddWithValue("@CON_NUMBER", dao.ConfNumber);
                        cmd.Parameters.AddWithValue("@CURRENT_STATUS", dao.CurrentStatus);
                        cmd.Parameters.AddWithValue("@LAST_UPD_ON", dao.LastUpdOn);
                         
                        //exe
                        affRows = cmd.ExecuteNonQuery();
                        log.Info("Affrows = " + affRows);
                    }
                }
            }
            catch (Exception ee)
            {
                log.Info("doInsertCurrentStatus() Error " + ee.Message);
            }

            return affRows;
        }


        public static int doUpdateCurrentStatus(FOStatusDao dao)
        {
            int affRows = 0;
            String SQL = "update t_fo_current_status set VENDOR_ID=@VENDOR_ID , CURRENT_STATUS=@CURRENT_STATUS , " +
                "LAST_UPD_ON=@LAST_UPD_ON  where CON_NUMBER=@CON_NUMBER";
                
            try
            {
                using (SqlConnection cnn = new SqlConnection(MYGlobal.getCString()))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(SQL, cnn))
                    {
                        cmd.CommandType = CommandType.Text;

                        //cmd.Parameters.AddWithValue("@ID", dao.Id);
                        cmd.Parameters.AddWithValue("@VENDOR_ID", dao.VendorId);
                        cmd.Parameters.AddWithValue("@CON_NUMBER", dao.ConfNumber);
                        cmd.Parameters.AddWithValue("@CURRENT_STATUS", dao.CurrentStatus);
                        cmd.Parameters.AddWithValue("@LAST_UPD_ON", dao.LastUpdOn);

                        //exe
                        affRows = cmd.ExecuteNonQuery();
                        log.Info("Affrows = " + affRows);
                    }
                }
            }
            catch (Exception ee)
            {
                log.Info("doUpdateCurrentStatus() Error " + ee.Message);
            }

            return affRows;
        }




        public static int doInsertFOHistory(FOHistoryDao dao)
        {
            int affRows = 0;
            String SQL = "INSERT INTO t_fo_status_history(VENDOR_ID,CONF_NUMBER,PART_RECVD_ON, PART_PROC_STARTED_ON, " +
                " PART_PROC_END_ON, PART_QC_DONE_ON,PART_SENT_DELV_ON, LAST_UPD_ON)" +
                "  VALUES(@VENDOR_ID, @CONF_NUMBER, @PART_RECVD_ON, @PART_PROC_STARTED_ON, @PART_PROC_END_ON, @PART_QC_DONE_ON, @PART_SENT_DELV_ON, " +
                "  @LAST_UPD_ON)";
            try
            {
                using (SqlConnection cnn = new SqlConnection(MYGlobal.getCString()))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(SQL, cnn))
                    {
                        cmd.CommandType = CommandType.Text;

                        //cmd.Parameters.AddWithValue("@ID", dao.Id);
                        cmd.Parameters.AddWithValue("@VENDOR_ID", dao.VendorId);
                        cmd.Parameters.AddWithValue("@CONF_NUMBER", dao.ConfNumber);
                        cmd.Parameters.AddWithValue("@PART_RECVD_ON", dao.PartRecdOn);
                        cmd.Parameters.AddWithValue("@PART_PROC_STARTED_ON", dao.PartProcStartedOn);
                        cmd.Parameters.AddWithValue("@PART_PROC_END_ON", dao.PartProcEndOnOn);
                        cmd.Parameters.AddWithValue("@PART_QC_DONE_ON", dao.PartQCDoneOnOn);
                        cmd.Parameters.AddWithValue("@PART_SENT_DELV_ON", dao.PartSentDelvOn);
                        cmd.Parameters.AddWithValue("@LAST_UPD_ON", dao.LastUpdOn);
                        //exe
                        affRows = cmd.ExecuteNonQuery();
                        log.Info("Affrows = " + affRows);
                    }
                }
            }
            catch (Exception ee)
            {
                log.Info("doInsertFOHistory() Error " + ee.Message);
            }

            return affRows;
        }


        public static int doInsertStep1(FOHistoryDao dao)
        {
            int affRows = 0;
            String SQL = "INSERT INTO t_fo_status_history(VENDOR_ID, CONF_NUMBER,  " +
                "  PART_RECVD_ON, LAST_UPD_ON)" +
                "  VALUES(@VENDOR_ID, @CONF_NUMBER, @PART_RECVD_ON , " +
                "  @LAST_UPD_ON)";
            try
            {
                using (SqlConnection cnn = new SqlConnection(MYGlobal.getCString()))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(SQL, cnn))
                    {
                        cmd.CommandType = CommandType.Text;

                        //cmd.Parameters.AddWithValue("@ID", dao.Id);
                        cmd.Parameters.AddWithValue("@VENDOR_ID", dao.VendorId);
                        cmd.Parameters.AddWithValue("@CONF_NUMBER", dao.ConfNumber);
                        cmd.Parameters.AddWithValue("@PART_RECVD_ON", dao.PartRecdOn);
                        cmd.Parameters.AddWithValue("@LAST_UPD_ON", dao.LastUpdOn);
                        //exe
                        affRows = cmd.ExecuteNonQuery();
                        log.Info("Affrows = " + affRows);
                    }
                }
            }
            catch (Exception ee)
            {
                log.Info("doInsertStep1() Error " + ee.Message);
            }

            return affRows;
        }


        public static int doInsertStep2(FOHistoryDao dao)
        {
            int affRows = 0;
            String SQL = "INSERT INTO t_fo_status_history(VENDOR_ID, CONF_NUMBER,  " +
                "  PART_PROC_STARTED_ON, LAST_UPD_ON)" +
                "  VALUES(@VENDOR_ID, @CONF_NUMBER, @PART_PROC_STARTED_ON , " +
                "  @LAST_UPD_ON)";
            try
            {
                using (SqlConnection cnn = new SqlConnection(MYGlobal.getCString()))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(SQL, cnn))
                    {
                        cmd.CommandType = CommandType.Text;

                        //cmd.Parameters.AddWithValue("@ID", dao.Id);
                        cmd.Parameters.AddWithValue("@VENDOR_ID", dao.VendorId);
                        cmd.Parameters.AddWithValue("@CONF_NUMBER", dao.ConfNumber);
                        cmd.Parameters.AddWithValue("@PART_PROC_STARTED_ON", dao.PartProcStartedOn);
                        cmd.Parameters.AddWithValue("@LAST_UPD_ON", dao.LastUpdOn);
                        //exe
                        affRows = cmd.ExecuteNonQuery();
                        log.Info("Affrows = " + affRows);
                    }
                }
            }
            catch (Exception ee)
            {
                log.Info("doInsertStep2() Error " + ee.Message);
            }

            return affRows;
        }



        public static int doInsertStep3(FOHistoryDao dao)
        {
            int affRows = 0;
            String SQL = "INSERT INTO t_fo_status_history(VENDOR_ID, CONF_NUMBER,  " +
                "  PART_PROC_END_ON, LAST_UPD_ON)" +
                "  VALUES(@VENDOR_ID, @CONF_NUMBER, @PART_PROC_END_ON , " +
                "  @LAST_UPD_ON)";
            try
            {
                using (SqlConnection cnn = new SqlConnection(MYGlobal.getCString()))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(SQL, cnn))
                    {
                        cmd.CommandType = CommandType.Text;

                        //cmd.Parameters.AddWithValue("@ID", dao.Id);
                        cmd.Parameters.AddWithValue("@VENDOR_ID", dao.VendorId);
                        cmd.Parameters.AddWithValue("@CONF_NUMBER", dao.ConfNumber);
                        cmd.Parameters.AddWithValue("@PART_PROC_END_ON", dao.PartProcEndOnOn);
                        cmd.Parameters.AddWithValue("@LAST_UPD_ON", dao.LastUpdOn);
                        //exe
                        affRows = cmd.ExecuteNonQuery();
                        log.Info("Affrows = " + affRows);
                    }
                }
            }
            catch (Exception ee)
            {
                log.Info("doInsertStep3() Error " + ee.Message);
            }

            return affRows;
        }

        public static int doInsertStep4(FOHistoryDao dao)
        {
            int affRows = 0;
            String SQL = "INSERT INTO t_fo_status_history(VENDOR_ID, CONF_NUMBER,  " +
                "  PART_QC_DONE_ON, LAST_UPD_ON)" +
                "  VALUES(@VENDOR_ID, @CONF_NUMBER, @PART_QC_DONE_ON , " +
                "  @LAST_UPD_ON)";
            try
            {
                using (SqlConnection cnn = new SqlConnection(MYGlobal.getCString()))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(SQL, cnn))
                    {
                        cmd.CommandType = CommandType.Text;

                        //cmd.Parameters.AddWithValue("@ID", dao.Id);
                        cmd.Parameters.AddWithValue("@VENDOR_ID", dao.VendorId);
                        cmd.Parameters.AddWithValue("@CONF_NUMBER", dao.ConfNumber);
                        cmd.Parameters.AddWithValue("@PART_QC_DONE_ON", dao.PartQCDoneOnOn);
                        cmd.Parameters.AddWithValue("@LAST_UPD_ON", dao.LastUpdOn);
                        //exe
                        affRows = cmd.ExecuteNonQuery();
                        log.Info("Affrows = " + affRows);
                    }
                }
            }
            catch (Exception ee)
            {
                log.Info("doInsertStep4() Error " + ee.Message);
            }

            return affRows;
        }


        public static int doInsertStep5(FOHistoryDao dao)
        {
            int affRows = 0;
            String SQL = "INSERT INTO t_fo_status_history(VENDOR_ID, CONF_NUMBER,  " +
                "  PART_SENT_DELV_ON, LAST_UPD_ON)" +
                "  VALUES(@VENDOR_ID, @CONF_NUMBER, @PART_SENT_DELV_ON , " +
                "  @LAST_UPD_ON)";
            try
            {
                using (SqlConnection cnn = new SqlConnection(MYGlobal.getCString()))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(SQL, cnn))
                    {
                        cmd.CommandType = CommandType.Text;

                        //cmd.Parameters.AddWithValue("@ID", dao.Id);
                        cmd.Parameters.AddWithValue("@VENDOR_ID", dao.VendorId);
                        cmd.Parameters.AddWithValue("@CONF_NUMBER", dao.ConfNumber);                        
                        cmd.Parameters.AddWithValue("@PART_SENT_DELV_ON", dao.PartSentDelvOn);
                        cmd.Parameters.AddWithValue("@LAST_UPD_ON", dao.LastUpdOn);
                        //exe
                        affRows = cmd.ExecuteNonQuery();
                        log.Info("Affrows = " + affRows);
                    }
                }
            }
            catch (Exception ee)
            {
                log.Info("doInsertStep5() Error " + ee.Message);
            }

            return affRows;
        }



    }
}
