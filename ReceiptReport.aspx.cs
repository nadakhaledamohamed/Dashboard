using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Int64 Id = 0;
            try
            {
                //Id = Int64.Parse(Session["DetailId"].ToString());

                Id = Int64.Parse(Request.QueryString["detid"].ToString());
            }
            catch (Exception)
            {
                Id = 0;
                // throw;

                // Response.Redirect("Student_Affairs_Requests.aspx",false);
            }
            FillGV(Id);

        }

    }
    public void FillGV(Int64 id)
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adr;
        DataTable dt;

        if (id != 0)
        {
            using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
            {
                using (cmd = new SqlCommand("GetFormId", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DetailId", id);
                    adr = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adr.Fill(dt);
                }
                int FormId = int.Parse(dt.Rows[0]["FormId"].ToString());
                string ReportName = "";
                switch (FormId)
                {
                  

                    case 1:
                        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
                        {
                            using (cmd = new SqlCommand("Sp_OfficialTranscriptRpt", con))
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
                        ReportName = @"Reports/OfficialTranscriptRpt.rdlc";
                        ReportViewer1.LocalReport.ReportPath = ReportName;
                        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;
                        break;
                    case 2:
                        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
                        {
                            using (cmd = new SqlCommand("Sp_UnOfficialTranscriptRpt", con))
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
                        ReportName = @"Reports/UnOfficialTranscriptRpt.rdlc";
                        ReportViewer1.LocalReport.ReportPath = ReportName;
                        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;
                        break;
                    case 3:
                        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
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
                        ReportName = @"Reports/MilitaryRpt.rdlc";
                        ReportViewer1.LocalReport.ReportPath = ReportName;
                        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;
                        break;
                    case 4:
                        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
                        {
                            using (cmd = new SqlCommand("Sp_AbsenceExcuseRequestRpt", con))
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
                        ReportName = @"Reports/AbsenceExcuseRpt.rdlc";
                        ReportViewer1.LocalReport.ReportPath = ReportName;
                        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;
                        break;
                    case 5:
                        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
                        {
                            using (cmd = new SqlCommand("Sp_EnglishEnrollmentCertRpt", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@DetailId", id);
                                adr = new SqlDataAdapter(cmd);
                                dt = new DataTable();
                                adr.Fill(dt);
                            }


                        }
                        dt.TableName = "DataSet1";
                        DataTable d2 ;
                        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
                        {
                            using (cmd = new SqlCommand("Sp_EnglishEnrollmentCertDetRpt", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@DetailId", id);
                                adr = new SqlDataAdapter(cmd);
                                d2 = new DataTable();
                                adr.Fill(d2);
                            }


                        }
                        // dt.TableName = "DataSet2";
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportName = @"Reports/EngEnrollmentCertRpt.rdlc";
                        ReportViewer1.LocalReport.ReportPath = ReportName;
                        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                         ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", d2));
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;
                        break;
                    case 6:
                        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
                        {
                            using (cmd = new SqlCommand("Sp_ArabicEnrollmentCertRpt", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@DetailId", id);
                                adr = new SqlDataAdapter(cmd);
                                dt = new DataTable();
                                adr.Fill(dt);
                            }


                        }
                        dt.TableName = "DataSet1";
                        DataTable d3;
                        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
                        {
                            using (cmd = new SqlCommand("Sp_EnglishEnrollmentCertDetRpt", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@DetailId", id);
                                cmd.Parameters.AddWithValue("@IsArabic", true);
                                adr = new SqlDataAdapter(cmd);
                                d3 = new DataTable();
                                adr.Fill(d3);
                            }


                        }
                        // dt.TableName = "DataSet2";
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportName = @"Reports/ArabicEnroolmentCertRpt.rdlc";
                        ReportViewer1.LocalReport.ReportPath = ReportName;
                        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", d3));
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;
                        break;
                    case 7:
                        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
                        {
                            using (cmd = new SqlCommand("Sp_AssessmentPetitionRpt", con))
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
                        ReportName = @"Reports/AssessmentPetitonRpt.rdlc";
                        ReportViewer1.LocalReport.ReportPath = ReportName;
                        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;
                        break;
                    case 8:
                        using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString))
                        {
                            using (cmd = new SqlCommand("Sp_RegisterartionBreakRpt", con))
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
                        ReportName = @"Reports/RegistrerationBreakRpt.rdlc";
                        ReportViewer1.LocalReport.ReportPath = ReportName;
                        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;
                        break;
                    default:
                        break;
                }
            }


        }

    }
}