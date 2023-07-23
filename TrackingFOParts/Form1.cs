using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;
using System.Runtime.InteropServices;
using System.Collections;
using TrackingFOParts.App;
using TrackingFOParts.DB;
using TrackingFOParts.Global;

namespace TrackingFOParts
{
    public partial class Form1 : Form
    {
        private static string DEF_VENDOR = "888";
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Form1()
        {
            InitializeComponent();
        }


        private void doClicke()
        {

            Pop3Client pop3Client = new Pop3Client();//create an object for pop3client

            try
            {

                //smtp.corp.halliburton.com

                var username = ConfigurationManager.AppSettings["Username"];
                var Password = ConfigurationManager.AppSettings["Password"];

                //Outlook
                pop3Client.Connect("outlook.office365.com", 995, true);
                //Gmail
                //pop3Client.Connect("pop.gmail.com", 995, true);
                pop3Client.Authenticate(username, Password, AuthenticationMethod.UsernameAndPassword);//, AuthenticationMethod.UsernameAndPassword)
            }
            catch(System.Exception ee)
            {
                log.Error("Error = "+ee.Message);
                return;
            }

            int count = pop3Client.GetMessageCount(); //total count of email in MessageBox  ie our inbox
            var Emails = new List<POPClientEmail>(); //object for list POPClientEmail class which we already created. 

            log.Info("Total email messages " + count);

            int counter = 0;
            for (int i = count; i >= 1; i--)//going to read mails from last till total number of mails received
            {
                OpenPop.Mime.Message message = pop3Client.GetMessage(i);//assigning messagenumber to get detailed mail.//each mail having messagenumber

                POPClientEmail email = new POPClientEmail()
                {
                    MessageNumber = i,
                    Subject = message.Headers.Subject,
                    DateSent = message.Headers.DateSent,
                    From = message.Headers.From.Address,


                };

                log.Info("email.Subject =  " + email.Subject +", From "+ email.From);

                MessagePart body = message.FindFirstHtmlVersion();
                if (body != null)
                {
                    email.Body = body.GetBodyAsText();
                   
                }
                else
                {
                    body = message.FindFirstPlainTextVersion();
                    if (body != null)
                    {
                        email.Body = body.GetBodyAsText();
                    }
                }

                log.Info("email.Body =  " +  email.Body.Substring(0,100));

                //Attachments
                List<MessagePart> attachments = message.FindAllAttachments();

                foreach (MessagePart attachment in attachments)
                {
                    email.Attachments.Add(new Attachment
                    {
                        FileName = attachment.FileName,
                        ContentType = attachment.ContentType.MediaType,
                        Content = attachment.Body
                    });
                }
                Emails.Add(email);
                counter++;

            }
            var emails = Emails;//You can filter mails by date from this list
        }


        private void button1_Click(object sender, EventArgs e)
        {
            doClicke();
        }

        private ArrayList doLocalOutlook()
        {
            ArrayList al = new ArrayList();
            Microsoft.Office.Interop.Outlook.Items oItems = null;
            Microsoft.Office.Interop.Outlook.Application oApp = null;
            Microsoft.Office.Interop.Outlook.NameSpace oNS = null;
            Microsoft.Office.Interop.Outlook.MAPIFolder oInbox = null;
            try { 
           
            oApp = new Microsoft.Office.Interop.Outlook.Application();
                            
            oNS = oApp.GetNamespace("mapi");
           
            oNS.Logon(Missing.Value, Missing.Value, false, true);

            oInbox = oNS.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);

            Microsoft.Office.Interop.Outlook.MAPIFolder destFolder = oInbox.Folders["halauto"];

                oItems = oInbox.Items;

                /* Microsoft.Office.Interop.Outlook.MailItem oMsg = (Microsoft.Office.Interop.Outlook.MailItem)oItems.GetFirst();

                 Console.WriteLine(oMsg.Subject);
                 Console.WriteLine(oMsg.SenderName);
                 Console.WriteLine(oMsg.ReceivedTime);
                 Console.WriteLine(oMsg.Body);


                 int AttachCnt = oMsg.Attachments.Count;
                 Console.WriteLine("Attachments: " + AttachCnt.ToString());


                 //Display the message.
                 //oMsg.Display(true); //modal */
                int count = 0;
                int count_all = 0;
                foreach (MailItem item in oItems)
                {
                    //Console.WriteLine("Now doing "+ item.Subject + ", count="+ count_all++);
                    if (item.UnRead == true)
                    {
                        bool can_move = false;
                        try
                        {
                            count++;
                            var stringBuilder = new StringBuilder();
                            stringBuilder.AppendLine("From: " + item.SenderEmailAddress);
                            stringBuilder.AppendLine("To: " + item.To);
                            stringBuilder.AppendLine("CC: " + item.CC);
                            stringBuilder.AppendLine("");
                            stringBuilder.AppendLine("Subject: " + item.Subject);
                            stringBuilder.AppendLine("Body: " + item.Body);

                            String subject = item.Subject;
                            if (subject.Contains("[FOParts]"))
                            {
                                Console.WriteLine("Found FO_PARTS = "+stringBuilder);
                                can_move = true;
                                FOObj fobj = new FOObj();
                                fobj.RecvdOn = DateTime.Now;

                                String body = item.Body;
                                string[] lines = body.Split('\n');
                                foreach(var line in lines)
                                {
                                    if (line.Length > 0)
                                    {
                                        if (line.Contains("confnum:"))
                                        {
                                            fobj.ConfNumber = line.Remove(0, 8 ).Trim();
                                        }                                        

                                        if (line.Contains("step"))
                                        {
                                            if (line.Contains("step1:"))
                                            {
                                                if (line.Contains("ON"))
                                                {
                                                    fobj.Step1 = "ON";
                                                }                                                   
                                            }
                                            else if (line.Contains("step2:"))
                                            {
                                                if (line.Contains("ON"))
                                                    fobj.Step2 = "ON";
                                            }
                                            else if (line.Contains("step3:"))
                                            {
                                                if (line.Contains("ON"))
                                                    fobj.Step3 = "ON";
                                            }
                                            else if (line.Contains("step4:"))
                                            {
                                                if (line.Contains("ON"))
                                                    fobj.Step4 = "ON";
                                            }
                                            else if (line.Contains("step5:"))
                                            {
                                                if (line.Contains("ON"))
                                                    fobj.Step5 = "ON";
                                            }

                                            Console.WriteLine("Body line = " + line);
                                        }
                                        
                                    }
                                    
                                }

                                al.Add(fobj);

                            }//IF
                        }catch(System.Exception ee)
                        {
                            Console.WriteLine("{0} Exception caught: ", ee);
                        }


                        if (can_move)
                        {
                            try
                            {
                                //Move folder
                                item.Move(destFolder);
                                log.Info("Moved email \n\n");
                            }
                            catch (System.Exception ee)
                            {
                                log.Info("Move error " + ee.Message);
                            }
                        }

                        Marshal.ReleaseComObject(item);

                    }//if



                }//

                Console.WriteLine("\n\n------------All email done ------ \n\n");

                //Log off.
                oNS.Logoff();

            //Explicitly release objects.
            //oMsg = null;
            oItems = null;
            oInbox = null;
            oNS = null;
            oApp = null;
            }

            //Error handler.
            catch (System.Exception e)
            {
            Console.WriteLine("{0} Exception caught: ", e);
            }
            finally
            {
                ReleaseComObject(oItems);
                 ReleaseComObject(oInbox);
                 ReleaseComObject(oNS);
                ReleaseComObject(oApp);
            }
            // Return value.
            //return null;

            return al;
        }

        private static void ReleaseComObject(object obj)
        {
            if (obj != null)
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
            }
        }

        private void btnLocalOutlook_Click(object sender, EventArgs e)
        {
            ArrayList al = doLocalOutlook();
            if (al.Count > 0)
            {
                for(int x = 0; x < al.Count; x++)
                {
                    FOObj fo = (FOObj)al[x];

                    FOHistoryDao foh = new FOHistoryDao();
                    foh.LastUpdOn = DateTime.Now;

                    FOStatusDao statusDao = DBUtils.getPartCurrentStatus(fo.ConfNumber);
                    bool is_new_conf = false;
                    if (statusDao == null)
                    {
                        is_new_conf = true;
                        statusDao = new FOStatusDao();
                        statusDao.VendorId = DEF_VENDOR;
                        statusDao.ConfNumber = fo.ConfNumber;
                        statusDao.LastUpdOn = DateTime.Now;
                    }



                    foh.ConfNumber = fo.ConfNumber.Trim();
                    foh.VendorId = DEF_VENDOR;
                    if (fo.Step5.Equals("ON"))
                    {
                        foh.PartSentDelvOn = fo.RecvdOn ;
                        statusDao.CurrentStatus = MYGlobal.STEP5_;
                        DBUtils.doInsertStep5(foh);
                    }
                    else if (fo.Step4.Equals("ON"))
                    {
                        foh.PartQCDoneOnOn = fo.RecvdOn ;
                        statusDao.CurrentStatus = MYGlobal.STEP4_;
                        DBUtils.doInsertStep4(foh);
                    }
                    else if (fo.Step3.Equals("ON"))
                    {
                        foh.PartProcEndOnOn = fo.RecvdOn;
                        statusDao.CurrentStatus = MYGlobal.STEP3_;
                        DBUtils.doInsertStep3(foh);
                    }
                    else if (fo.Step2.Equals("ON"))
                    {
                        foh.PartProcStartedOn = fo.RecvdOn;
                        statusDao.CurrentStatus = MYGlobal.STEP2_;
                        DBUtils.doInsertStep2(foh);
                    }
                    else if (fo.Step1.Equals("ON"))
                    {
                        foh.PartRecdOn = fo.RecvdOn;
                        statusDao.CurrentStatus = MYGlobal.STEP1_;
                        DBUtils.doInsertStep1(foh);
                    }

                    if (is_new_conf)
                    {
                        DBUtils.doInsertCurrentStatus(statusDao);
                    }
                    else
                    {
                        DBUtils.doUpdateCurrentStatus(statusDao);
                    }

                   
                }
            }
        }
    }
}
