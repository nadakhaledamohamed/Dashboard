using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
            //  FillDD();
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
        GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
        DropDownList ddforms = (DropDownList)row.FindControl("ddForms");
        int formId = int.Parse(ddforms.SelectedValue);
        DropDownList ddtypes = (DropDownList)row.FindControl("ddTypes");
        int TypeId = int.Parse(ddtypes.SelectedValue);
        TextBox txtdur = (TextBox)row.FindControl("txtDurarion");
        string strDuration = txtdur.Text;
        TextBox txtFees = (TextBox)row.FindControl("txtfees");
        string strFees = txtFees.Text;
        CheckBox check = (CheckBox)row.FindControl("checkActive");
        bool Active = check.Checked ? true : false;
        if (e.CommandName == "save")
        {

            if (Page.IsValid)
            {
                try
                {
                    using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
                    {
                        using (cmd = new SqlCommand("AddFormType", con))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@FormId", formId);
                            cmd.Parameters.AddWithValue("@TypeId", TypeId);
                            if (strFees != "")
                            {
                                cmd.Parameters.AddWithValue("@Cost", int.Parse(strFees));
                            }
                            if (strDuration != "")
                            {
                                cmd.Parameters.AddWithValue("@Duration", int.Parse(strDuration));
                            }

                            cmd.Parameters.AddWithValue("@IsActive", Active);
                            cmd.Parameters.AddWithValue("@TableId", id);
                            cmd.Parameters.AddWithValue("@EmployeeId", Int64.Parse(Session["EmployeeId"].ToString()));

                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
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
        else if (e.CommandName == "modify")
        {

            if (Page.IsValid)
            {
                try
                {
                    using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
                    {
                        using (cmd = new SqlCommand("AddFormType", con))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@FormId", formId);
                            cmd.Parameters.AddWithValue("@TypeId", TypeId);
                            if (strFees != "")
                            {
                                cmd.Parameters.AddWithValue("@Cost", int.Parse(strFees));
                            }
                            if (strDuration != "")
                            {
                                cmd.Parameters.AddWithValue("@Duration", int.Parse(strDuration));
                            }

                            cmd.Parameters.AddWithValue("@IsActive", Active);
                            cmd.Parameters.AddWithValue("@TableId", id);
                            cmd.Parameters.AddWithValue("@EmployeeId", Int64.Parse(Session["EmployeeId"].ToString()));

                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Data Saved Succesfully" + "');", true);
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

    private void FillGv()
    {
        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
        {
            using (cmd = new SqlCommand("GetFormTypeForSettingGrid", con))
            {
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
                CheckBox chkactive = (CheckBox)e.Row.FindControl("checkActive");
                if (int.Parse(myDataRowView["FormTypeId"].ToString()) == 0)
                {
                    e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='orange'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E56E94'");
                    e.Row.BackColor = Color.FromName("#E56E94");
                }
                if (chkactive != null)
                {
                    chkactive.Checked = myDataRowView["IsActive"].ToString() == "1" ? true : false;
                }

                if (int.Parse(myDataRowView["UsedNo"].ToString()) > 0)
                {
                    
                    LinkButton edit = (LinkButton)e.Row.FindControl("btnEdit");
                    if (edit != null)
                    {
                        edit.Visible = false;
                    }
                }
                else
                {
                    if (int.Parse(myDataRowView["FormTypeId"].ToString()) > 0)
                    {
                        LinkButton Save = (LinkButton)e.Row.FindControl("btnSave");
                        if (Save != null)
                        {
                            Save.Visible = false;
                        }
                        LinkButton edit = (LinkButton)e.Row.FindControl("btnEdit");
                        if (edit != null)
                        {
                            edit.Visible = true;
                        }

                    }
                    else
                    {
                        e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#b3d7ff'");
                        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#cce5ff'");
                        e.Row.BackColor = Color.FromName("#cce5ff");
                        LinkButton Save = (LinkButton)e.Row.FindControl("btnSave");
                        if (Save != null)
                        {
                            Save.Visible = true;
                        }
                        LinkButton edit = (LinkButton)e.Row.FindControl("btnEdit");
                        if (edit != null)
                        {
                            edit.Visible = false;
                        }

                    }


                }
                DropDownList types = (DropDownList)e.Row.FindControl("ddTypes");
                if (types != null)
                {
                    using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
                    {
                        using (cmd = new SqlCommand("GetTypes", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            adr = new SqlDataAdapter(cmd);
                            dt = new DataTable();
                            adr.Fill(dt);
                        }
                    }
                    types.DataValueField = "id";
                    types.DataTextField = "title";
                    types.DataSource = dt;
                    types.DataBind();
                    types.SelectedValue = myDataRowView["TypeId"].ToString();
                }
                DropDownList Forms = (DropDownList)e.Row.FindControl("ddForms");
                if (Forms != null)
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
                    Forms.DataValueField = "id";
                    Forms.DataTextField = "title";
                    Forms.DataSource = dt;
                    Forms.DataBind();
                    Forms.SelectedValue = myDataRowView["FormId"].ToString();
                }
                break;
        }

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

}
