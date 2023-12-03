using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;

public partial class contactRep : System.Web.UI.Page
{
	string connection = System.Configuration.ConfigurationManager.ConnectionStrings["ContactConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string id = Request.QueryString["id"].ToString();

            ReportViewer1.LocalReport.DataSources.Clear();
            DataTable parm = new DataTable("dt");
            parm.Columns.Add("name"); parm.Columns.Add("value");
            parm.Rows.Add("@stuID", id);

            DataTable dt = new DataManager2().ExecuteDataTable(connection,"SP_SearchContactByID", CommandType.StoredProcedure, parm);
            dt.TableName = "SP_SearchContactByID";

            ReportViewer1.LocalReport.ReportPath = @"rdlc_contact.rdlc";
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ds_SearchContactByID", dt));
            ReportViewer1.LocalReport.Refresh();

            
        }
    }
}