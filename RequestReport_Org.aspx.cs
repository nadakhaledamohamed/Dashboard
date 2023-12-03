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

public partial class ReuestReportt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           

            int Id = 0;
            Id = 28832;
          //  Id = int.Parse(Session["DetailId"].ToString());  

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
            using (cmd = new SqlCommand("Sp_MilitaryRpt", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DetailId", id);
                adr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adr.Fill(dt);
            }
            

        }
        dt.TableName = "DataSet1";
        ReportViewer1.LocalReport.DataSources.Clear();
        int FormId =int.Parse( dt.Rows[0]["FormId"].ToString());
        string ReportName = "";
        switch (FormId)
        {
            case 3:
                ReportName = @"Reports/MilitaryRpt.rdlc";
                break;
            default:
                break;

        }
        ReportViewer1.LocalReport.ReportPath = ReportName;
        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
        ReportViewer1.LocalReport.Refresh();

    }

 
}
