using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected static string DashBoard = ConfigurationManager.ConnectionStrings["Dashboard"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private string GetIpAddress()
    {
        var context = HttpContext.Current;
        var ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (string.IsNullOrEmpty(ipAddress)) return context.Request.ServerVariables["REMOTE_ADDR"];
        var addresses = ipAddress.Split(',');
        return addresses.Length != 0 ? addresses[0] : context.Request.ServerVariables["REMOTE_ADDR"];
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        var userIp = GetIpAddress();
        var priv = new Privilages();

        bool isAuthenticate = priv.IsAuthenticated(txtUsername.Value.ToLower().Replace("@ngu.edu.eg", ""), txtPassword.Value);
        if (isAuthenticate)
        {
            SqlConnection con;
            SqlCommand cmd;
            SqlDataAdapter adr;
            DataTable dt;

            using (con = new SqlConnection(DashBoard))
            {
                using (cmd = new SqlCommand("GetUsrRole", con))
                {
                   cmd.Parameters.AddWithValue("@UserName", txtUsername.Value.ToLower().Replace("@ngu.edu.eg", ""));
                  //  cmd.Parameters.AddWithValue("@UserName", "mohamed.fawzy");
                    cmd.CommandType = CommandType.StoredProcedure;
                    adr = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adr.Fill(dt);

                    if (dt.Rows.Count != 0)
                    {
                       Session["EmployeeId"] = dt.Rows[0][1].ToString();
                      // Session["EmployeeId"] = 10257;
                        Session["EmployeeName"] = dt.Rows[0][0].ToString();
                        List<int> Approvals = new List<int>();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Approvals.Add(int.Parse(dt.Rows[i]["ApprovalId"].ToString()));

                        }
                        Session["Approvals"] = Approvals;

                        Response.Redirect("~/RequesterHome.aspx");
                    }
                    else
                    {
                        lblMessage.Visible = true;
                        priv.AddLogToDatabase(0, 25, txtUsername.Value, userIp);

                    }
                }

            }
            //else
            {
                lblMessage.Visible = true;
                priv.AddLogToDatabase(0, 25, txtUsername.Value, userIp);
            }
        }
    }
}