using AjaxControlToolkit;
using Microsoft.Reporting.WebForms;
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
public partial  class Finance_Request : System.Web.UI.Page
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


    
    private void FillGv()
    {
        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
        {
            using (cmd = new SqlCommand("RequestDetailsForEmployessRpt", con))
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
                adr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adr.Fill(dt);
                dt.TableName = "DataSet1";
                ////  dt2.TableName = "Overall_Attendance_Report_Graph2";
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.ReportPath = @"Reports/RequestDetailsStatisticRpt.rdlc";
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                ReportViewer1.LocalReport.Refresh();

            }
        }
      


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
        ddRequestStatus.Items.Insert(0,new ListItem { Value="0",Text="Select Status"});
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

    }
   

}
