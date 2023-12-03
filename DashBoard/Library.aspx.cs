using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;




public partial class _DBlibrary : System.Web.UI.Page
{


    protected static string FormRequestConStr = ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString;
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter adr;
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {

        //AssesmentPetiton_Div
        if (!IsPostBack)
        {
            #region Master

            if (Page.Master != null)
            {
                //  ((HtmlAnchor)Page.Master.FindControl("DBMenu")).Attributes["class"] = "dropdown-toggle active-parent active";
                // ((HtmlControl)Page.Master.FindControl("menu5")).Attributes["style"] = "display: block;";
                //  ((HtmlAnchor)Page.Master.FindControl("regapp")).Attributes["class"] = "ajax-link active-parent active";
            }

            #endregion

            FillDD();
            FillStudents();
            FillGv();
        }
    }
    protected void gvForms_PreRender(object sender, EventArgs e)
    {

    }
    protected void GridView_PreRender(object sender, EventArgs e)
    {
        if (((GridView)sender).HeaderRow != null)
        {
            ((GridView)sender).HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (((GridView)sender).FooterRow != null)
        {
            ((GridView)sender).FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }
    protected void gvTypes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var id = Convert.ToInt32(e.CommandArgument.ToString());
        if (e.CommandName == "save")
        {
            try
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                HiddenField formapproval = (HiddenField)row.FindControl("hfform_approval");
                DropDownList ddstat = (DropDownList)row.FindControl("ddStatus");
                string statusvalue = ddstat.SelectedValue; ;
                if (statusvalue.Trim() != "" && formapproval.Value != "0")
                {
                    using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["Dashboard"].ConnectionString))
                    {
                        using (cmd = new SqlCommand("Add_Request_Approval", con))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@RequestId", id);
                            cmd.Parameters.AddWithValue("@UserId", Int64.Parse(Session["EmployeeId"].ToString()));
                            cmd.Parameters.AddWithValue("@Status", statusvalue);
                            cmd.Parameters.AddWithValue("@RequestApprovalId", formapproval.Value);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            ExceptionLog l = new ExceptionLog();
                            try
                            {
                                l.SendDBNotificationEmail(id);
                            }
                            catch (Exception ex)
                            {

                                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Failed to send notification" + "');", true);
                                ExceptionLog log = new ExceptionLog();
                                log.SendExcepToDB(ex, System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
                            }

                            FillGv();

                        }


                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Data Saved Succesfully" + "');", true);
                    }
                }
            }


            catch (Exception ex)
            {


                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Error Occured during Save, Try Again" + "');", true);
                ExceptionLog log = new ExceptionLog();
                log.SendExcepToDB(ex, System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
            }

        }

    }





    private void FillGv()
    {
        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["Dashboard"].ConnectionString))
        {
            using (cmd = new SqlCommand("GetRequestsForApproval", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ApprovalId", 4);
                if (ddlst_student.SelectedValue != "0")
                {
                    cmd.Parameters.AddWithValue("@StudentId", ddlst_student.SelectedValue);
                }
                if (txtFromDate.Text.Trim() != "")
                {
                    cmd.Parameters.AddWithValue("@FromDate", txtFromDate.Text);
                }
                if (txtTodate.Text.Trim() != "")
                {
                    cmd.Parameters.AddWithValue("@ToDate", txtTodate.Text);
                }
                if (ddForms.SelectedValue != "0")
                {
                    cmd.Parameters.AddWithValue("@FormId", int.Parse(ddForms.SelectedValue));
                }
                if (ddRequestStatus.SelectedValue != "0")
                {
                    cmd.Parameters.AddWithValue("@Status", int.Parse(ddRequestStatus.SelectedValue));
                }
                if (ddschool.SelectedValue != "0")
                {
                    cmd.Parameters.AddWithValue("@SchoolId", int.Parse(ddschool.SelectedValue));
                }
                adr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adr.Fill(dt);
                gvForms.DataSource = dt;
                gvForms.DataBind();


                // RequestDiv.Visible = true;
            }


        }



    }




    protected void NewType_Click(object sender, EventArgs e)
    {
        Response.Redirect("DB_Requests.aspx", false);
    }
    protected void gvForms_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                DropDownList status = (DropDownList)e.Row.FindControl("ddStatus");
                DataRowView myDataRowView = (DataRowView)e.Row.DataItem;
                if (status != null)
                {
                    bool finalDecisiin = myDataRowView["IsFinalApproval"].ToString() == "False" ? false : true;
                    status.Items.Insert(0, new ListItem { Value = "2", Text = "Hold" });
                    if (finalDecisiin == false)
                    {
                        status.Items.Insert(0, new ListItem { Value = "3", Text = "Approve" });
                        status.Items.Insert(0, new ListItem { Value = "4", Text = "Decline" });

                    }
                    else
                    {
                        status.Items.Insert(0, new ListItem { Value = "5", Text = "Cancel" });
                        status.Items.Insert(0, new ListItem { Value = "6", Text = "Process" });
                    }
                    status.SelectedValue = myDataRowView["ApprovalStatus"].ToString();
                }
             
            
                if (myDataRowView["ApprovalStatus"].ToString().ToLower()!= "2")
                {
                    LinkButton savebtn = (LinkButton)e.Row.FindControl("btnSave");                   
                    if (savebtn != null)
                    {
                        savebtn.Visible = false;
                        status.Enabled = false;
                    }
                }             
                Label not = (Label)e.Row.FindControl("txtNotification");
                if (not != null)
                {
                    if (myDataRowView["UnReadComment"].ToString() != "0")
                    {
                        not.Text = "(" + myDataRowView["UnReadComment"].ToString() + ")";
                    }
                }
                break;
        }

    }


    [System.Web.Services.WebMethod]
    public static void SetUserName(Int64 Detid)
    {
        //Page objPage = new Page();
        //objPage.Session["DetailId"] = Detid;

    }

    protected void SearchBt_Click(object sender, EventArgs e)
    {
        FillGv();


    }
    private void FillDD()
    {
        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["Dashboard"].ConnectionString))
        {
            using (cmd = new SqlCommand("GetDBRequestStatus", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                adr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adr.Fill(dt);
            }
        }
        ddRequestStatus.DataTextField = "StatusName";
        ddRequestStatus.DataValueField = "StatusId";
        ddRequestStatus.DataSource = dt;
        ddRequestStatus.DataBind();
        ddRequestStatus.Items.Insert(0, new ListItem { Value = "0", Text = "Select Status" });
        ddRequestStatus.SelectedValue = "2";
        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["Dashboard"].ConnectionString))
        {
            using (cmd = new SqlCommand("GetDBForms", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ApprovalId", 6);
                adr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adr.Fill(dt);
            }
        }
        ddForms.DataTextField = "FormName";
        ddForms.DataValueField = "FormId";
        ddForms.DataSource = dt;
        ddForms.DataBind();
        ddForms.Items.Insert(0, new ListItem { Value = "0", Text = "Select Form" });
        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["Dashboard"].ConnectionString))
        {
            using (cmd = new SqlCommand("GetSchools", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", Session["EmployeeId"].ToString());
                cmd.Parameters.AddWithValue("@ApprovalId", 6);
                adr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adr.Fill(dt);
            }
        }
        ddschool.DataTextField = "SchoolName";
        ddschool.DataValueField = "SchoolId";
        ddschool.DataSource = dt;
        ddschool.DataBind();
        ddschool.Items.Insert(0, new ListItem { Value = "0", Text = "Select School" });


    }
    private void FillStudents()
    {
        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["Dashboard"].ConnectionString))
        {
            using (cmd = new SqlCommand("SPFillStudentForList", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", Session["EmployeeId"].ToString());
                cmd.Parameters.AddWithValue("@ApprovalId", 6);
                adr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adr.Fill(dt);
            }
        }
        ddlst_student.DataTextField = "Namee";
        ddlst_student.DataValueField = "Id";
        ddlst_student.DataSource = dt;
        ddlst_student.DataBind();

        con.Close();
    }




}
