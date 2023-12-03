using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _DBAPPList : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter adr;
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Int64 Id = 0;
            int faid = 0;
           
            try
            {
              Id = Int64.Parse(Session["DetailId"].ToString());
             

                Session["DetailId"] = Id;
                //    GetComments(Id,faid);
                FillGV(Id);
            }
            catch (Exception ex)
            {
                Id = 0;
                // throw;

                // Response.Redirect("Student_Affairs_Requests.aspx",false);
            }


        }

    }
    public void FillGV(Int64 id)
    {
        if (id != 0)
        {
         
         //   usertype = getUsertype();
            using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["Dashboard"].ConnectionString))
            {
                using (cmd = new SqlCommand("GetApprovalDetForList", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@requetid", id);
                   
                    adr = new SqlDataAdapter(cmd);
                    adr = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adr.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        gvForms.DataSource = dt;
                        gvForms.DataBind();


                    }
                   
                }

            }


        }

    }
    [WebMethod]
    public static void SetSessions(string Deatail)
    {

        Page objPage = new Page();
        objPage.Session["DetailId"] = Deatail;
      

    }


}
