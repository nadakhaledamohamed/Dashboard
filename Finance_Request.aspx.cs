using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;




public partial class Finance_Request : System.Web.UI.Page
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
            FillStudents();
            FillGv();
            FillDD();
        }
    }


    protected void cancelType_Click(object sender, EventArgs e)
    {

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
        ExceptionLog l;
        if (e.CommandName == "save")
        {
            //int index = Convert.ToInt32(e.CommandArgument);
            //GridViewRow clickedRow = gvForms.Rows[index];
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            TextBox txtbox = (TextBox)row.FindControl("txtPaymentDate");
            string text = txtbox.Text;
            if (text.Trim() != "")
            {
                try
                {
                    using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
                    {
                        using (cmd = new SqlCommand("ChangeRequestStatus", con))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@DetailId", id);
                            cmd.Parameters.AddWithValue("@PaymentDate", text);
                            cmd.Parameters.AddWithValue("@EmployeeId", Int64.Parse(Session["EmployeeId"].ToString()));
                            cmd.Parameters.AddWithValue("@NewStatus", 3);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            try
                            {
                                l = new ExceptionLog();
                                l.SendEmailToResponsible(id);
                            }
                            catch (Exception ex)
                            {

                                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Failed to send notification" + "');", true);
                                ExceptionLog log = new ExceptionLog();
                                log.SendExcepToDB(ex, System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
                            }

                            FillGv();

                        }

                    }
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Data Saved Succesfully" + "');", true);
                }
                catch (Exception ex)
                {


                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Error Occured during Save, Try Again" + "');", true);
                    ExceptionLog log = new ExceptionLog();
                    log.SendExcepToDB(ex, System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
                }

            }
        }
        else if (e.CommandName == "download")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            int RequestId = int.Parse(row.Cells[0].Text);
            using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
            {
                using (cmd = new SqlCommand("GetReciepts", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RequestId", RequestId);
                    adr = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adr.Fill(dt);
                }
            }
            if (dt.Rows.Count > 0)
            {
                string filePath = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    filePath = System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/FinanceReceipt/"), dt.Rows[i]["File_Name"].ToString());
                    FileInfo file = new FileInfo(filePath);
                    if (file.Exists)
                    {
                        Response.Clear();
                        Response.ClearHeaders();
                        Response.ClearContent();
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                        Response.AddHeader("Content-Length", file.Length.ToString());
                        Response.ContentType = "text/plain";
                        Response.Flush();
                        Response.TransmitFile(file.FullName);
                        Response.End();
                    }


                }

            }

        }
        else if (e.CommandName == "suspend")
        {
            int newStatus = 6;
            LinkButton btn = (LinkButton)e.CommandSource;
            if (btn.Text == "<span class=\"fas fa-check-circle\"></span>&nbsp; Activate")
            {
                newStatus = 2;
            }
            using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
            {
                using (cmd = new SqlCommand("ChangeRequestStatus", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DetailId", id);
                    cmd.Parameters.AddWithValue("@EmployeeId", Int64.Parse(Session["EmployeeId"].ToString()));
                    cmd.Parameters.AddWithValue("@NewStatus", newStatus);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    try
                    {
                        l = new ExceptionLog();
                        l.SendEmailToResponsible(id);
                    }
                    catch (Exception ex)
                    {

                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Failed to send notification" + "');", true);
                        ExceptionLog log = new ExceptionLog();
                        log.SendExcepToDB(ex, System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
                    }

                    FillGv();

                }

            }
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Data Saved Succesfully" + "');", true);

        }
        else if (e.CommandName == "downloadform")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            int RequestId = int.Parse(row.Cells[0].Text);
            Session["RequestId"] = RequestId;
            Response.Redirect("RequestDetailsRptForFinance.aspx", false);

        }
    }

    private void FillGv()
    {
        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
        {
            using (cmd = new SqlCommand("GetRequestDetailsForEmployess", con))
            {
                cmd.Parameters.AddWithValue("@EmployeeType", 1);
                cmd.CommandType = CommandType.StoredProcedure;
                adr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adr.Fill(dt);
            }
        }
        gvForms.DataSource = dt;
        gvForms.DataBind();
        RequestDiv.Visible = true;


    }

    protected void gvForms_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                DataRowView myDataRowView = (DataRowView)e.Row.DataItem;
                TextBox txt = (TextBox)e.Row.FindControl("txtPaymentDate");
                if (txt != null)
                {
                    txt.Text = myDataRowView["payment_date"].ToString();
                }
                if (myDataRowView["payment_status"].ToString().ToLower() == "false" && myDataRowView["HasUploadedReceipt"].ToString().ToLower() == "true")
                {

                    LinkButton status = (LinkButton)e.Row.FindControl("btnSave");
                    if (status != null)
                    {
                        status.Visible = true;
                    }
                    txt.Enabled = true;

                }
                else
                {

                    LinkButton status = (LinkButton)e.Row.FindControl("btnSave");
                    if (status != null)
                    {
                        status.Visible = false;
                    }
                    txt.Enabled = false;
                }




                //    if (myDataRowView["payment_status"].ToString().ToLower() == "true" || myDataRowView["HasUploadedReceipt"].ToString().ToLower()=="false")
                //{
                //    LinkButton status = (LinkButton)e.Row.FindControl("btnSave");
                //    if (status != null)
                //    {
                //        status.Visible = false;
                //    }
                //    txt.Enabled = false;

                //}
                //else if (myDataRowView["payment_status"].ToString().ToLower() == "false")
                //{
                //  if(myDataRowView["HasUploadedReceipt"].ToString().ToLower() == "false")
                //    {

                //        LinkButton status = (LinkButton)e.Row.FindControl("btnSave");
                //        if (status != null)
                //        {
                //            status.Visible = false;
                //        }
                //        txt.Enabled = false;
                //    }
                //  else
                //    {
                //        LinkButton status = (LinkButton)e.Row.FindControl("btnSave");
                //        if (status != null)
                //        {
                //            status.Visible = true;
                //        }
                //        txt.Enabled = true;

                //    }
                //}
                if (myDataRowView["HasUploadedReceipt"].ToString().ToLower() == "true")
                {
                    LinkButton download = (LinkButton)e.Row.FindControl("btndownload");
                    if (download != null)
                    {
                        download.Visible = true;
                    }

                }
                if (myDataRowView["payment_status"].ToString().ToLower() == "false" && myDataRowView["HasUploadedReceipt"].ToString().ToLower() == "true")
                {


                    LinkButton suspend = (LinkButton)e.Row.FindControl("btnSuspend");
                    if (suspend != null)
                    {
                        suspend.Visible = true;
                        if (int.Parse(myDataRowView["Processing_Status"].ToString().ToLower()) == 6)
                        {
                            suspend.Text = "<span class=\"fas fa-check-circle\"></span>&nbsp; Activate";
                        }
                        else
                        {
                            suspend.Text = "<span class=\"fa fa-ban\"></span>&nbsp; Suspend";

                        }
                       

                    }

                }
                //else if (int.Parse(myDataRowView["Processing_Status"].ToString().ToLower()) == 6)
                //{

                //    LinkButton suspend = (LinkButton)e.Row.FindControl("btnSuspend");
                //    if (suspend != null)
                //    {
                //        suspend.Visible = true;
                //        suspend.Text = "<span class=\"fas fa-check-circle\"></span>&nbsp; Activate";

                //    }


                //}
                Label not = (Label)e.Row.FindControl("txtNotification");
                if (not != null)
                {
                    not.Text = "(" + myDataRowView["NewNotification"].ToString() + ")";
                }
                break;

        }

    }


    protected void SearchBt_Click(object sender, EventArgs e)
    {

        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
        {
            using (cmd = new SqlCommand("GetRequestDetailsForEmployess", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeType", 1);
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
                if (PaymentList.SelectedValue != "-1")
                {
                    cmd.Parameters.AddWithValue("@PaymentStatus", PaymentList.SelectedValue);
                }
                if (ddForms.SelectedValue != "0")
                {
                    cmd.Parameters.AddWithValue("@FormId", int.Parse(ddForms.SelectedValue));
                }
                adr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adr.Fill(dt);
            }
        }
        gvForms.DataSource = dt;
        gvForms.DataBind();
        RequestDiv.Visible = true;
    }
    private void FillStudents()
    {
        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
        {
            using (cmd = new SqlCommand("SPFillStudentForList", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

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
    private void FillDD()
    {
        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
        {
            using (cmd = new SqlCommand("GetForms", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                adr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adr.Fill(dt);
            }
        }
        ddForms.DataTextField = "title";
        ddForms.DataValueField = "id";
        ddForms.DataSource = dt;
        ddForms.DataBind();
        //  ddForms.Items.Insert(0, new ListItem { Value = "0", Text = "Select Form" });

    }

}
