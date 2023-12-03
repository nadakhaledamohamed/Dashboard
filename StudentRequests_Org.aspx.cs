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




public partial class _RequestForm : System.Web.UI.Page
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
                ((HtmlAnchor)Page.Master.FindControl("hlforms")).Attributes["class"] = "dropdown-toggle active-parent active";
                ((HtmlControl)Page.Master.FindControl("menu5")).Attributes["style"] = "display: block;";
                ((HtmlAnchor)Page.Master.FindControl("hlApp22")).Attributes["class"] = "ajax-link active-parent active";
            }

            #endregion
            FillGv(int.Parse(Session["StuSmallId"].ToString()));
        }
    }


    protected void cancelType_Click(object sender, EventArgs e)
    {
        RequestDiv.Visible = true;
        DetailsDiv.Visible = false;
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
        Session["RequestId"] = id;
        if (e.CommandName == "view")
        {
            using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
            {
                using (cmd = new SqlCommand("GetStudentRequestData", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RequestId", id);
                    adr = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adr.Fill(dt);
                }
            }
            gvDetails.DataSource = dt;
            gvDetails.DataBind();
            DetailsDiv.Visible = true;
            PrevBtn.Visible = true;
            RequestDiv.Visible = false;
        }
        //Response.Redirect("RequestDetailsRpt.aspx", false);
        else if (e.CommandName == "receipt")
        {
            Session["RequestId"] = id;
            Response.Redirect("RequestDetailsRpt.aspx", false);

        }
        else if (e.CommandName == "save")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            FileUpload fileuploader = (FileUpload)row.FindControl("fileuploadReceipt");
            Label lbl = (Label)row.FindControl("StatusLabel");
            string FName = "";
            List<string> strfiles = new List<string>();
            if (fileuploader.HasFiles && fileuploader.PostedFile.ContentLength < 30000000)
            {
                try
                {
                    foreach (HttpPostedFile uploadedFile in fileuploader.PostedFiles)
                    {
                        FName = Session["StuSmallId"].ToString() + "_" + uploadedFile.FileName;
                        strfiles.Add(FName);
                        uploadedFile.SaveAs(System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/FinanceReceipt/"), FName));
                        using (con = new SqlConnection(FormRequestConStr))
                        {
                            using (cmd = new SqlCommand("AddRequestRecipt", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@RequestId", id);
                                cmd.Parameters.AddWithValue("@Attachments", string.Join(";", strfiles.ToArray()));
                                adr = new SqlDataAdapter(cmd);
                                dt = new DataTable();
                                adr.Fill(dt);
                                try
                                {
                                    ExceptionLog l;
                                    if (dt.Rows.Count > 0)
                                    {
                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {
                                            l = new ExceptionLog();
                                            l.SendEmailToResponsible(long.Parse(dt.Rows[i]["DetailId"].ToString()));

                                        }
                                    }

                                }
                                catch (Exception ex)
                                {

                                    // ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Failed to send notification" + "');", true);
                                    ExceptionLog log = new ExceptionLog();
                                    log.SendExcepToDB(ex, System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
                                }

                            }
                        }


                    }
                    lbl.Text = "Upload status: Files uploaded!";
                    FillGv(int.Parse(Session["StuSmallId"].ToString()));
                }
               

                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Error Occured during Save, Try Again" + "');", true);
                    ExceptionLog log = new ExceptionLog();
                    log.SendExcepToDB(ex, System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
                }
            }
            else
            {

                CustomValidator ct = (CustomValidator)row.FindControl("FileUploadCustomValidator");
                ct.IsValid = false;
                return;
            }
        }
        else if (e.CommandName == "clear")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            FileUpload fileuploader = (FileUpload)row.FindControl("fileuploadReceipt");
            fileuploader.PostedFile.InputStream.Dispose();

        }
        else if (e.CommandName == "download")
        {
            using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
            {
                using (cmd = new SqlCommand("GetReciepts", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RequestId", id);
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

            FillGv(int.Parse(Session["StuSmallId"].ToString()));
        }

    }
    private void FillGv(Int64 id)
    {
        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
        {
            using (cmd = new SqlCommand("GetStudentRequestForGrid", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SmallId", id);
                adr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adr.Fill(dt);
            }


        }
        gvForms.DataSource = dt;
        gvForms.DataBind();
        RequestDiv.Visible = true;


    }
    public void FillDetailsGridView(Int64 id)
    {


    }

    protected void PrevBtn_Click(object sender, EventArgs e)
    {
        RequestDiv.Visible = true;
        DetailsDiv.Visible = false;
        PrevBtn.Visible = false;
    }

    protected void NewType_Click(object sender, EventArgs e)
    {
        Response.Redirect("Form_Request.aspx", false);
    }
    protected void gvForms_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                DataRowView myDataRowView = (DataRowView)e.Row.DataItem;

                if (myDataRowView["IsRevisedByRegistrar"].ToString().ToLower() == "true")
                {
                    LinkButton rcp = (LinkButton)e.Row.FindControl("btnreceipt");
                    if (rcp != null)
                    {
                        rcp.Visible = true;
                    }
                    if (int.Parse(myDataRowView["AttachNo"].ToString()) > 0)
                    {
                        LinkButton download = (LinkButton)e.Row.FindControl("btndownload");
                        if (download != null)
                        {
                            download.Visible = true;
                        }

                    }
                    else
                    {

                        LinkButton save = (LinkButton)e.Row.FindControl("btSave");
                        if (save != null)
                        {
                            save.Visible = true;
                        }
                        LinkButton del = (LinkButton)e.Row.FindControl("btnDelete");
                        if (del != null)
                        {
                            del.Visible = true;
                        }
                        FileUpload fl = (FileUpload)e.Row.FindControl("fileuploadReceipt");
                        if (fl != null)
                        {
                            fl.Visible = true;
                        }
                        Label lbl = (Label)e.Row.FindControl("StatusLabel");
                        if (lbl != null)
                        {
                            fl.Visible = true;
                        }
                    }

                }
                //IsRevisedByRegistrar
                break;
        }

    }

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                DataRowView myDataRowView = (DataRowView)e.Row.DataItem;
                LinkButton download = (LinkButton)e.Row.FindControl("btnprint");
                if (download != null)
                {
                    if (myDataRowView["SubmittedInPerson"].ToString().ToLower() == "true")
                    {

                        download.Visible = true;

                    }
                    else
                    {
                        download.Visible = false;
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

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var id = Convert.ToInt32(e.CommandArgument.ToString());

    }
    [System.Web.Services.WebMethod]
    public static void SetUserName(Int64 Detid)
    {
        Page objPage = new Page();
        objPage.Session["DetailId"] = Detid;

    }




    protected void FileUploadCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        //args.IsValid = false;
        CustomValidator cv = (CustomValidator)source;
        GridViewRow grdrow = (GridViewRow)((CustomValidator)source).NamingContainer;
        FileUpload fl = (FileUpload)grdrow.FindControl("fileuploadReceipt");
        if (fl.PostedFile.ContentLength > 30000000)
        {
            args.IsValid = false;

        }
    }
}
