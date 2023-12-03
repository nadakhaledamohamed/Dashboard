using AjaxControlToolkit;
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
            FillDD();
            FillGv();
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
        if (e.CommandName == "save")
        {
            try
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                DropDownList chck = (DropDownList)row.FindControl("ddrevised");
                DropDownList ddstat = (DropDownList)row.FindControl("ddStatus");

                if (chck != null && ddstat.Enabled == false && chck.SelectedValue != "0" && chck.Enabled == true)
                {
                    using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
                    {
                        using (cmd = new SqlCommand("ChangeApprovedStatus", con))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@DetailId", id);
                            //cmd.Parameters.AddWithValue("@PaymentDate", text);
                            cmd.Parameters.AddWithValue("@EmployeeId", Int64.Parse(Session["EmployeeId"].ToString()));
                            cmd.Parameters.AddWithValue("@RevisedStatus", int.Parse(chck.SelectedValue));
                            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                            DataTable d = new DataTable();
                            adpt.Fill(d);
                            try
                            {
                                if (d.Rows[0]["sendnotification"].ToString().ToLower() == "true")
                                {
                                    ExceptionLog l = new ExceptionLog();
                                    l.SendEmailToResponsible(0, Int64.Parse(d.Rows[0]["RequestId"].ToString()));
                                }

                            }
                            catch (Exception ex)
                            {
                                 ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Failed to send notification" + "');", true);
                                ExceptionLog log = new ExceptionLog();
                                log.SendExcepToDB(ex, System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
                            }
                           
                            FillGv();
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Data Saved Succesfully" + "');", true);
                        }

                    }
                }
                else
                {

                    string text = ddstat.SelectedValue; ;
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
                                    //cmd.Parameters.AddWithValue("@PaymentDate", text);
                                    cmd.Parameters.AddWithValue("@EmployeeId", Int64.Parse(Session["EmployeeId"].ToString()));
                                    cmd.Parameters.AddWithValue("@NewStatus", text);
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                    ExceptionLog l = new ExceptionLog();
                                    try
                                    {
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
            }
            catch (Exception)
            {

                throw;
            }
        }
        //if (e.CommandName == "save")
        //{
        //    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
        //    CheckBox chck = (CheckBox)row.FindControl("ddStatus");
        //    if (chck != null && chck.Enabled == true && chck.Checked == true)
        //    {
        //        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
        //        {
        //            using (cmd = new SqlCommand("ChangeApprovedStatus", con))
        //            {

        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@DetailId", id);
        //                //cmd.Parameters.AddWithValue("@PaymentDate", text);
        //                cmd.Parameters.AddWithValue("@EmployeeId", Int64.Parse(Session["EmployeeId"].ToString()));
        //                //con.Open();
        //                //cmd.ExecuteNonQuery();
        //                //con.Close();
        //                ExceptionLog l = new ExceptionLog();
        //                l.SendEmailToResponsible(id);
        //                FillGv();

        //            }

        //        }


        //    }
        //    else
        //    {
        //        DropDownList ddstat = (DropDownList)row.FindControl("ddStatus");
        //        string text = ddstat.SelectedValue; ;
        //        if (text.Trim() != "")
        //        {
        //            try
        //            {
        //                using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
        //                {
        //                    using (cmd = new SqlCommand("ChangeRequestStatus", con))
        //                    {

        //                        cmd.CommandType = CommandType.StoredProcedure;
        //                        cmd.Parameters.AddWithValue("@DetailId", id);
        //                        //cmd.Parameters.AddWithValue("@PaymentDate", text);
        //                        cmd.Parameters.AddWithValue("@EmployeeId", Int64.Parse(Session["EmployeeId"].ToString()));
        //                        cmd.Parameters.AddWithValue("@NewStatus", text);
        //                        con.Open();
        //                        cmd.ExecuteNonQuery();
        //                        con.Close();
        //                        ExceptionLog l = new ExceptionLog();
        //                        l.SendEmailToResponsible(id);
        //                        FillGv();

        //                    }

        //                }
        //                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Data Saved Succesfully" + "');", true);
        //            }
        //            catch (Exception ex)
        //            {


        //                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Error Occured during Save, Try Again" + "');", true);
        //                ExceptionLog log = new ExceptionLog();
        //                log.SendExcepToDB(ex, System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
        //            }

        //        }
        //    }
        //}
        else if (e.CommandName == "print")
        {

            Session["DetailId"] = id;
            //irm1.Src = "test.aspx";
            // irm1.Attributes.Add("src", "test.aspx");
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            // ModalPopupExtender ddstat = (ModalPopupExtender)row.FindControl("ddStatus");
            //ddstat.Show();
            //mp1.Show();
        }
        else if (e.CommandName == "download")
        {

            DownLoad_Documents(id);
        }
    }

    private void FillGv()
    {
        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
        {
            using (cmd = new SqlCommand("GetRequestDetailsForEmployess", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeType", 2);
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
                if (int.Parse(myDataRowView["DocumentCount"].ToString()) > 0)
                {
                    LinkButton download = (LinkButton)e.Row.FindControl("btndownload");
                    if (download != null)
                    {
                        download.Visible = true;
                    }
                }
                if (myDataRowView["StatusID"].ToString().ToLower() == "4")
                {
                    LinkButton download = (LinkButton)e.Row.FindControl("btnSave");
                    if (download != null)
                    {
                        download.Enabled = false;
                    }
                }
                DropDownList revise = (DropDownList)e.Row.FindControl("ddrevised");
                if (revise != null)
                {
                    revise.Items.Insert(0, new ListItem { Value = "0", Text = "Not revised" });
                    revise.Items.Insert(1, new ListItem { Value = "1", Text = "Approved" });
                    revise.Items.Insert(2, new ListItem { Value = "2", Text = "Canceled" });
                    if (myDataRowView["RevisedStatus"].ToString() != "")
                    {
                        revise.SelectedValue = myDataRowView["RevisedStatus"].ToString();
                        if ((myDataRowView["RevisedStatus"].ToString() != "0"))
                        {
                            revise.Enabled = false;
                        }
                    }


                }
                DropDownList status = (DropDownList)e.Row.FindControl("ddStatus");
                if (status != null)
                {
                    int paystatus = int.Parse(myDataRowView["Processing_Status"].ToString()) > 0 ? int.Parse(myDataRowView["Processing_Status"].ToString()) : 0;
                    int Approved = int.Parse(myDataRowView["RevisedStatus"].ToString());
                    using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
                    {
                        using (cmd = new SqlCommand("GetRequestStatus", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@StatusId", paystatus);
                            cmd.Parameters.AddWithValue("@Approved", Approved);
                            adr = new SqlDataAdapter(cmd);
                            dt = new DataTable();
                            adr.Fill(dt);
                        }
                    }
                    status.DataValueField = "id";
                    status.DataTextField = "title";
                    status.DataSource = dt;
                    status.DataBind();
                    status.SelectedValue = myDataRowView["Processing_Status"].ToString();
                    if (int.Parse(myDataRowView["RevisedStatus"].ToString()) != 1)
                    {
                        status.Enabled = false;

                    }
                }
                Label not = (Label)e.Row.FindControl("txtNotification");
                if (not != null)
                {
                    not.Text = "(" + myDataRowView["NewNotification"].ToString() + ")";
                }
                break;
        }

    }

    protected void txtStudentID_TextChanged(object sender, EventArgs e)
    {

        //if (txtStudentID.Text.Trim()!="")
        //{
        //    int studentid = int.Parse(txtStudentID.Text);
        //    using (con = new SqlConnection(FormRequestConStr))
        //    {
        //        using (cmd = new SqlCommand("GetStudentData", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@LongId", studentid);
        //            adr = new SqlDataAdapter(cmd);
        //            dt = new DataTable();
        //            adr.Fill(dt);
        //            if (dt.Rows.Count > 0)
        //            {
        //                txtStudentName.Text = dt.Rows[0][1].ToString();

        //            }
        //            else
        //            {
        //                txtStudentID.Text = "";
        //            }
        //        }
        //    }


        //}
        //else
        //{
        //    txtStudentName.Text ="";
        //}



    }

    protected void SearchBt_Click(object sender, EventArgs e)
    {
        FillGv();


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
            using (cmd = new SqlCommand("GetRequestStatus", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                adr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adr.Fill(dt);
            }
        }
        ddRequestStatus.DataTextField = "title";
        ddRequestStatus.DataValueField = "id";
        ddRequestStatus.DataSource = dt;
        ddRequestStatus.DataBind();
        ddRequestStatus.Items.Insert(0, new ListItem { Value = "0", Text = "Select Status" });
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
        ddForms.Items.Insert(0, new ListItem { Value = "0", Text = "Select Form" });
        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
        {
            using (cmd = new SqlCommand("GetSchools", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

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
    private void DownLoad_Documents(Int64 id)
    {
        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
        {
            using (cmd = new SqlCommand("GetAttachments", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DetailId", id);
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
                filePath = System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/ExcusesDocument/"), dt.Rows[i]["File_Name"].ToString());
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

    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvForms.PageIndex = e.NewPageIndex;
        this.FillGv();
    }

}
