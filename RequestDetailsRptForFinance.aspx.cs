using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Configuration;

public partial class ReqDet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           

            int Id = 0;
            Id = int.Parse(Session["RequestId"].ToString());  

            FillGV(Id);
           
        }
    }
    
    public void FillGV(int id)
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adr;
        DataTable dt;
        
        using (con = new SqlConnection(  ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
        {
            using (cmd = new SqlCommand("GetStudentRequestDataRpt", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RequestId", id);
                adr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adr.Fill(dt);
            }
            dt.TableName = "DataSet1";
            DataTable d2;
            using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
            {
                using (cmd = new SqlCommand("GetBankData", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    adr = new SqlDataAdapter(cmd);
                    d2 = new DataTable();
                    adr.Fill(d2);
                }


            }
            ////  dt2.TableName = "Overall_Attendance_Report_Graph2";
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.ReportPath = @"Reports/RequestDetailsRpt.rdlc";
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", d2));
            ReportViewer1.LocalReport.Refresh();
        }

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        //FillGV();
    }
}
