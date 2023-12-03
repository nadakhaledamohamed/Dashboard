using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class test : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter adr;
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Int64 Id = 0;
           
            try
            {
              Id = Int64.Parse(Session["DetailId"].ToString());
               // Id = Int64.Parse(Request.QueryString["detid"].ToString());
                // type = int.Parse(Request.QueryString["type"].ToString()); ;

                Session["DetailId"] = Id;                
                GetComments(Id);
            }
            catch (Exception ex)
            {
                Id = 0;
                // throw;

                // Response.Redirect("Student_Affairs_Requests.aspx",false);
            }


        }

    }
    public void GetComments(Int64 id)
    {
        if (id != 0)
        {
            int usertype = 0;
            usertype = getUsertype();
            using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
            {
                using (cmd = new SqlCommand("GetComments", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DetailId", id);
                    cmd.Parameters.AddWithValue("@Type", usertype);
                    adr = new SqlDataAdapter(cmd);
                    adr = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adr.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        HistoryComment.Controls
                                  .Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            System.Web.UI.HtmlControls.HtmlGenericControl createDiv =
       new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                            //  createDiv.ID = "createDiv";
                            createDiv.InnerHtml = " <b> From :</b>" + dt.Rows[i]["CommentBy"] + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Date:</b>" + dt.Rows[i]["CommentDate"] + "<p>" + dt.Rows[i]["CommentText"] + "<hr class='hrDiv'>";
                            HistoryComment.Controls
                                .Add(createDiv);
                        }
                    }
                    else
                    {
                        System.Web.UI.HtmlControls.HtmlGenericControl createDiv =
new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                        createDiv.InnerHtml = "previous comments Exist";
                        HistoryComment.Controls
                               .Add(createDiv);

                    }
                }

            }


        }

    }

    protected void SendCommentBtn_Click(object sender, EventArgs e)
    {      
        SendCommentBtn.Visible = false;
        ExceptionLog l = new ExceptionLog();
        int type = 0;
        try
        {
            Int64 Id = Int64.Parse(Session["DetailId"].ToString());
            type = getUsertype();

            if (type != 0 && Id != 0)
            {
                using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
                {
                    using (cmd = new SqlCommand("AddComment", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DetailId", Id);
                        cmd.Parameters.AddWithValue("@Comment", txtcomment.Text);
                        cmd.Parameters.AddWithValue("@type", type);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        con.Dispose();
                        GetComments(Id);
                        try
                        {
                            l.SendComment(txtcomment.Text, type, Id);
                        }
                        catch (Exception ex)
                        {


                          ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Failed to send notification" + "');", true);
                            ExceptionLog log = new ExceptionLog();
                            log.SendExcepToDB(ex, System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
                        }
                       
                        txtcomment.Text = "";
                        SendCommentBtn.Visible = true;


                    }
                }
            }
        }
        catch (Exception ex)
        {
            l.SendExcepToDB(ex, System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
        }


    }
    [WebMethod]
    public static void SetSessions(string Deatail)
    {

        Page objPage = new Page();
        objPage.Session["DetailId"] = Deatail;

    }

    protected int getUsertype()
    {
        int type = 0;
        if (!string.IsNullOrEmpty(Session["StuSmallId"].ToString()))
        {
            type = 1;
        }
        else if (!string.IsNullOrEmpty(Session["EmployeeId"].ToString()))
        {
            using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
            {
                using (cmd = new SqlCommand("GetEmployeeCommentType", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeID", int.Parse(Session["EmployeeId"].ToString()));
                    SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);
                    type = int.Parse(dt.Rows[0]["UserType"].ToString());
                }
            }
        }

        if (!string.IsNullOrEmpty(Session["EmployeeId"].ToString()) && ((int.Parse(Session["EmployeeId"].ToString()) == 10105) || (int.Parse(Session["EmployeeId"].ToString()) == 10058)))  // for test case only 
        {
            type = 2;
        }

        return type;

    }
}
