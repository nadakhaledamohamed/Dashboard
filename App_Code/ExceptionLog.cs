using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for ExceptionLog
/// </summary>
public class SessionData
{
    public long SessionId;
    public string CurentPW;

}
public class AttendanceParam
{
    public DateTime AttDate;
    public Guid RecordID;
}
public static class Attendance
{
    public static List<SessionData> Sessions = new List<SessionData>();

}
public class ExceptionLog
{
    protected static string FormRequestConStr = ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString;
    protected static string FinanceMail = "finance@ngu.edu.eg";
    protected static string RegistrarEmail = "Onlinerequest@ngu.edu.eg";
    protected static string credentialUserName = "Onlinerequest@ngu.edu.eg";
    protected static string credentialPassword = "96nR7TnW";
    protected static string onlineFormApplication = "Onlinerequest@ngu.edu.eg";
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter adr;
    DataTable dt;
    public ExceptionLog()
    {
        //
        // TODO: Add constructor logic here
        //
    }





    public void SendDBNotificationEmail(Int64 RequestId = 0)
    {

        MailMessage mail;
        if (RequestId != 0)
        {
            using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["dashboard"].ConnectionString))
            {
                using (cmd = new SqlCommand("GeDBtRequestNotificationData", con))
                {
                    // SqlDependency.Start(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString);
                    cmd.Connection = con;
                    // cmd.CommandText = "GetRequestData";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Requestid", RequestId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    adr = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adr.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        string htmlString = @"<html>
                                            <body>
                                            <p>Dear  ,</p>
                                            <p>Kindly ,  the status of the " + dt.Rows[0]["FormName"] + " for the student : " + dt.Rows[0]["StudentId"] + "-" + dt.Rows[0]["StudentName"] + " is " + dt.Rows[0]["Currentstatus"] + "</p>" +
                                            @"<p>
                                             <strong> For your action . </strong> .
                                            </p>
                                            </body>
                                            </html>
                                           ";
                        mail = new MailMessage();
                        mail.From = new MailAddress(onlineFormApplication);
                        mail.IsBodyHtml = true;

                        mail.Subject = "Dashboard Notification";

                        mail.Body = htmlString;
                        foreach (DataRow r in dt.Rows)
                        {

                            mail.To.Add(r["Email"].ToString());

                        }
                        SendEmail(mail, "R" + RequestId);

                    }

                }
            }
        }

    }
    public void SendExcepToDB(Exception exdb, string exepurl = "")
    {

        if (exepurl == "")
        {
            exepurl = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
        }
        using (con = new SqlConnection(FormRequestConStr))
        {
            using (cmd = new SqlCommand("ExceptionLoggingToDataBase", con))
            {
                cmd.Connection = con;
                cmd.CommandText = "ExceptionLoggingToDataBase";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExceptionMsg", exdb.Message.ToString());
                cmd.Parameters.AddWithValue("@ExceptionType", exdb.GetType().Name.ToString());
                cmd.Parameters.AddWithValue("@ExceptionURL", exepurl);
                cmd.Parameters.AddWithValue("@ExceptionSource", exdb.StackTrace.ToString());
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();

            }
        }

    }
    public void SendEmailToResponsible(Int64 DetailId = 0, Int64 Request = 0, int SchoolId = 0)
    {
        string FormName = "";
        string NewStatus = "";
        string studentName = "";
        string StudentMail = "";
        MailMessage mail;
        if (Request != 0)
        {
            using (con = new SqlConnection(FormRequestConStr))
            {
                using (cmd = new SqlCommand("GetRequestData", con))
                {
                    // SqlDependency.Start(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString);
                    cmd.Connection = con;
                    cmd.CommandText = "GetRequestData";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Requestid", Request);
                    cmd.CommandType = CommandType.StoredProcedure;
                    adr = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adr.Fill(dt);
                    StudentMail = dt.Rows[0]["Email"].ToString();
                    string htmlString = @"<html>
                                        <body>
                                        <p>Dear Student ,</p>
                                        <p>Kindly , Your  online request has been revised by registrar , refer to online request page for details :.</p>
                                        </body>
                                        </html>
                                       ";
                    mail = new MailMessage();
                    mail.From = new MailAddress(onlineFormApplication);
                    mail.IsBodyHtml = true;

                    mail.Subject = "Student Notification";

                    mail.Body = htmlString;
                    mail.To.Add(StudentMail);

                    SendEmail(mail, "R" + Request.ToString());

                }
            }
        }
        else
        {
            using (con = new SqlConnection(FormRequestConStr))
            {
                using (cmd = new SqlCommand("GetNotificationData", con))
                {
                    // SqlDependency.Start(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString);
                    cmd.Connection = con;
                    cmd.CommandText = "GetNotificationData";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DetailId", DetailId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    adr = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adr.Fill(dt);
                    FormName = dt.Rows[0]["FormTitle"].ToString();
                    NewStatus = dt.Rows[0]["Status_Name"].ToString();
                    StudentMail = dt.Rows[0]["Email"].ToString();


                    if (dt.Rows[0]["SendNotificationToStudent"].ToString().ToLower() == "true")
                    {

                        string htmlString = @"<html>
                                        <body>
                                        <p>Dear Student ,</p>
                                        <p>Kindly , Your  online request for" + FormName + @" is Now " + NewStatus + @" :.</p>
                                        </body>
                                        </html>
                                       ";
                        mail = new MailMessage();

                        mail.To.Add(StudentMail);


                        mail.From = new MailAddress(onlineFormApplication);
                        mail.IsBodyHtml = true;

                        mail.Subject = "Student Notification";

                        mail.Body = htmlString;

                        SendEmail(mail, "D" + DetailId.ToString());
                    }
                    if (dt.Rows[0]["SendNotificationToRegistrar"].ToString().ToLower() == "true")
                    {
                        studentName = dt.Rows[0]["StudentName"].ToString();
                        string htmlString = @"<html>
                                        <body>
                                        <p>Dear Sir / Ms.  ,</p>
                                        <p>Kindly Note that The status of the  Online  request of Student " + studentName + @" For  " + FormName + " Form  is Now " + NewStatus + @" :.</p>
                                        </body>
                                        </html>
                                       ";
                        mail = new MailMessage();

                        mail.To.Add(RegistrarEmail);


                        mail.From = new MailAddress(onlineFormApplication);
                        mail.IsBodyHtml = true;

                        mail.Subject = "Registrar Notification";

                        mail.Body = htmlString;


                        SendEmail(mail, "D" + DetailId.ToString());
                    }
                    if (dt.Rows[0]["SendNotificationToFinance"].ToString().ToLower() == "true")
                    {
                        studentName = dt.Rows[0]["StudentName"].ToString();
                        string htmlString = @"<html>
                                        <body>
                                        <p>Dear Sir / Ms.  ,</p>
                                          <p>Kindly Note that The status of the  Online  request of Student " + studentName + @" For  " + FormName + " Form  is Now " + NewStatus + @" :.</p>
                                        </body>
                                        </html>
                                       ";
                        mail = new MailMessage();

                        mail.To.Add(FinanceMail);


                        mail.From = new MailAddress(onlineFormApplication);
                        mail.IsBodyHtml = true;

                        mail.Subject = "Finance  Notification";

                        mail.Body = htmlString;

                        SendEmail(mail, "D" + DetailId.ToString());
                    }
                    if (dt.Rows[0]["SendNotificationToSchool"].ToString().ToLower() == "true")
                        if (SchoolId != 0)
                        {
                            {
                                studentName = dt.Rows[0]["StudentName"].ToString();
                                string htmlString = @"<html>
                                        <body>
                                        <p>Dear Sir / Ms.  ,</p>
                                          <p>Kindly Note that The status of the  Online  request of Student " + studentName + @" For  " + FormName + " Form  is Now " + NewStatus + @" :.</p>
                                        </body>
                                        </html>
                                       ";

                                List<string> Emails = getSchooldata(SchoolId);

                                if (Emails.Count > 0)
                                {

                                    mail = new MailMessage();
                                    mail.From = new MailAddress(onlineFormApplication);
                                    mail.IsBodyHtml = true;

                                    mail.Subject = "Medical Excuse request";

                                    mail.Body = htmlString;
                                    foreach (string ml in Emails)
                                    {
                                        mail.To.Add(ml);

                                    }

                                    SendEmail(mail, "D" + DetailId.ToString());

                                }
                            }
                        }

                }
            }

        }
    }
    public void SendComment(string Comment, int type, Int64 DetailId)
    {
        string FormName = "";
        string studentName = "";
        string StudentEmail = "";
        string Subject = "New Comment has been added to online form application";
        string body = "";
        MailMessage mail = new MailMessage();

        mail.From = new MailAddress(onlineFormApplication);
        mail.IsBodyHtml = true;
        using (con = new SqlConnection(FormRequestConStr))
        {
            using (cmd = new SqlCommand("GetNotificationData", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DetailId", DetailId);
                cmd.CommandType = CommandType.StoredProcedure;
                adr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adr.Fill(dt);
                FormName = dt.Rows[0]["FormTitle"].ToString();
                studentName = dt.Rows[0]["StudentName"].ToString();
                StudentEmail = dt.Rows[0]["Email"].ToString();
                int formid = int.Parse(dt.Rows[0]["formid"].ToString());
                int schoolid = int.Parse(dt.Rows[0]["Schoolid"].ToString());

                if (formid != 4)
                {

                    switch (type)
                    {
                        case 1:
                            body = @"<html>
                                        <body>
                                        <p>Dear ,</p>
                                        <p>Kindly , New online Comment has been sent by student " + studentName + @" For  " + FormName + " as Follow :.</p> " + Comment + @" 
                                        </body>
                                        </html>
                                       ";
                            mail.To.Add(RegistrarEmail);
                            mail.To.Add(FinanceMail);


                            break;
                        case 2:
                            body = @"<html>
                                        <body>
                                        <p>Dear Sir / Ms.  ,</p>
                                        <p>Kindly , New online Comment has been sent by Registrar department to " + studentName + @" For  " + FormName + " as Follow :.</p> " + Comment + @" 
                                        </body>
                                        </html>
                                       ";
                            mail.To.Add(StudentEmail);
                            mail.To.Add(FinanceMail);


                            break;
                        case 3:
                            body = @"<html>
                                        <body>
                                        <p>Dear Sir / Ms.  ,</p>
                                        <p>Kindly , New online Comment has been sent by Finance department to " + studentName + @" For  " + FormName + " as Follow :.</p> " + Comment + @" 
                                        </body>
                                        </html>
                                       ";
                            mail.To.Add(StudentEmail);
                            mail.To.Add(RegistrarEmail);


                            break;


                    }
                    mail.Subject = Subject;

                    mail.Body = body;
                    SendEmail(mail, "D" + DetailId.ToString());
                }
                else
                {
                    switch (type)
                    {
                        case 1:
                            body = @"<html>
                                        <body>
                                        <p>Dear ,</p>
                                        <p>Kindly , New online Comment has been sent by student " + studentName + @" For  " + FormName + " as Follow :.</p> " + Comment + @" 
                                        </body>
                                        </html>
                                       ";
                            List<string> Emails = getSchooldata(schoolid);
                            foreach (string ml in Emails)
                            {

                                mail.To.Add(ml);

                            }

                            break;
                        case 4:
                            body = @"<html>
                                        <body>
                                        <p>Dear Sir / Ms.  ,</p>
                                        <p>Kindly , New online Comment has been sent by School  to " + studentName + @" For  " + FormName + " as Follow :.</p> " + Comment + @" 
                                        </body>
                                        </html>
                                       ";
                            mail.To.Add(StudentEmail);
                            break;
                    }
                    mail.Subject = Subject;

                    mail.Body = body;
                    SendEmail(mail, "D" + DetailId.ToString());

                }
            }
        }
    }
    protected List<string> getSchooldata(int schoolid)
    {
        List<string> Emails = new List<string>();
        using (con = new SqlConnection(FormRequestConStr))
        {
            using (cmd = new SqlCommand("GetSchoolNotificationData", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolId", schoolid);
                cmd.CommandType = CommandType.StoredProcedure;
                adr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adr.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Emails.Add(dt.Rows[i]["Email"].ToString());

                    }



                }

            }
        }
        return Emails;
    }
    private void SendEmail(MailMessage EMail, string RefID = "" , string DBName="FormRequest")
    {
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.office365.com";

        smtp.Port = 587;

        smtp.UseDefaultCredentials = false;

        smtp.EnableSsl = true;
        smtp.Credentials = new System.Net.NetworkCredential(credentialUserName, credentialPassword);
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        try
        {
            smtp.Send(EMail);

        }
        catch (Exception)
        {

            try
            {
                smtp.Send(EMail);
            }
            catch (Exception)
            {

                try
                {
                    smtp.Send(EMail);
                }
                catch (Exception)
                {

                    using (con = new SqlConnection(FormRequestConStr))
                    {
                        using (cmd = new SqlCommand("AddPendingMail", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (!string.IsNullOrEmpty(RefID))
                            {
                                cmd.Parameters.AddWithValue("@RefID", RefID);
                            }
                            // cmd.Parameters.AddWithValue("@FromAccount", EMail.From);
                            cmd.Parameters.AddWithValue("@ToAccount", EMail.To);
                            cmd.Parameters.AddWithValue("@Subject", EMail.Subject);
                            cmd.Parameters.AddWithValue("@Body", EMail.Body);
                            cmd.Parameters.AddWithValue("@DBName", DBName);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            con.Dispose();
                        }
                    }
                }
            }
        }

    }
    public void SendDBCommentEmail(string Comment, int type, Int64 RequestId)
    {
        string FormName = "";
        string studentName = "";
        string StudentEmail = "";
        string Subject = "New Comment has been added to Dashboard form application";
        string body = "";
        string CommentBy = "";
        MailMessage mail = new MailMessage();

        mail.From = new MailAddress(onlineFormApplication);
        mail.IsBodyHtml = true;
        dt = new DataTable();
        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["dashboard"].ConnectionString))
        {
            using (cmd = new SqlCommand("GeDBtCommentNotificationData", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RequestId", RequestId);
                cmd.Parameters.AddWithValue("@Type", type);
                cmd.CommandType = CommandType.StoredProcedure;
                adr = new SqlDataAdapter(cmd);              
                adr.Fill(dt);
            }
        }
        if(dt.Rows.Count > 0)
        {
            FormName = dt.Rows[0]["FormName"].ToString();
            studentName = dt.Rows[0]["StudentName"].ToString();
            StudentEmail = dt.Rows[0]["StudentEmail"].ToString();
            List<string> Emails =new List<string>();
            switch (type)
            {
                case 0:
                    CommentBy = " Student " + studentName;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        mail.To.Add(dt.Rows[i]["Email"].ToString());
                    }

                    break;
                default:
                    CommentBy = dt.Rows[0]["CommentBy"].ToString();
                     mail.To.Add(StudentEmail);
                    for (int i = 0; i < dt.Rows.Count;i++)
                    {
                        mail.To.Add(dt.Rows[i]["Email"].ToString());

                    }
                    break; 
            }
            body = @"<html>
                                        <body>
                                        <p>Dear,</p>
                                        <p>Kindly , New online Comment has been sent by " + CommentBy + @" For  " + FormName + " Requested by student : " + dt.Rows[0]["StudentId"].ToString() +" - " + studentName + " as Follow : </p> " + Comment + @" 
                                        </body>
                                        </html>
                                       ";
            mail.Subject = Subject;

             mail.Body = body;
            SendEmail(mail, "R" + RequestId.ToString(),"DashBoard");


        }

    }
}

