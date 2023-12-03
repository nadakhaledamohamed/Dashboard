using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI.HtmlControls;

public partial class Pass_Default : System.Web.UI.Page
{
    readonly SqlConnection _connection =
        new SqlConnection(ConfigurationManager.ConnectionStrings["NGUAuthentication"].ConnectionString);

    private UserPrincipal GetDomainUser(string logonName)
    {
        var principalContext = GetPrincipalContext();
        var userPrincipal = UserPrincipal.FindByIdentity(principalContext, IdentityType.SamAccountName, logonName.Trim());
        return userPrincipal;
    }

    private PrincipalContext GetPrincipalContext()
    {
        //var principalContext = new PrincipalContext(
        //   ContextType.Domain,
        //   "ngu",
        //   "DC=ngu,DC=edu,DC=eg",
        //   ContextOptions.Negotiate,
        //   "reset@ngu.edu.eg",
        //   "reset@ngu");
        //return principalContext;

        var principalContext = new PrincipalContext(
           ContextType.Domain,
           "ngu",
           "DC=ngu,DC=local",
           ContextOptions.Negotiate,
           "reset@ngu.local",
           "Zb2CNokWxNXz");
        return principalContext;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        #region Master

        //if (Page.Master != null)
        //{
        //    ((HtmlAnchor)Page.Master.FindControl("hluser")).Attributes["class"] =
        //        "dropdown-toggle active-parent active";
        //    ((HtmlControl)Page.Master.FindControl("menu3")).Attributes["style"] = "display: block;";
        //    ((HtmlAnchor)Page.Master.FindControl("A7")).Attributes["class"] = "ajax-link active-parent active";
        //}

        #endregion

        if (!IsPostBack)
        {
            div_error.Visible = false;
            lbl_error.Text = "";

            GetUserData(Session["EmploNum"].ToString());
        }
    }

    protected void btn_change_Click(object sender, EventArgs e)
    {
        try
        {
            lbl_error.Text = "";
            div_error.Visible = false;

            if (CheckCurrentPass(txt_oldPass.Text))
            {
                if (CheckPassPolicy(txt_newPass.Text))
                {
                    if (UpdateUserPassword(hdf_userName.Value, txt_newPass.Text))
                    {
                        int res = AddLog();

                        string strMsg = "Password Chang Done.";
                        string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
                        Response.Write(script);
                    }
                    else
                    {
                        lbl_error.Text = "Change Error.";
                        div_error.Visible = true;
                    }
                }
                else
                {
                    lbl_error.Text = "Password at least 8 chars included at least 1 integer and any special char (~ ! @ # $ % & _)";
                    div_error.Visible = true;
                }
            }
            else
            {
                lbl_error.Text = "Wrong Old password";
                div_error.Visible = true;
            }
        }
        catch (Exception ex)
        {

            lbl_error.Text = "Change Error "+ ex.Message.ToString();
            div_error.Visible = true;
        }
    }

    private void GetUserData(string userCode)
    {
        SqlCommand cmd = new SqlCommand("SP_GetUserData", _connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.Add("@userCode", SqlDbType.Int).Value = userCode;
        if (_connection.State == ConnectionState.Closed)
            _connection.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            hdf_userID.Value = reader["Id"].ToString();
            txt_userID.Text = reader["Id"].ToString();

            hdf_userName.Value = reader["UserName"].ToString();
            txt_username.Text = reader["UserName"].ToString();

            txt_name.Text = reader["Namee"].ToString();

            txt_oldPass.Text = "";
            txt_newPass.Text = "";
            txt_newPassConfirm.Text = "";

            reader.Close();
            _connection.Close();
        }
        else
        {
            div_error.Visible = true;
            lbl_error.Text = "Data Display Error";

            reader.Close();
            _connection.Close();
        }
    }

    private bool CheckCurrentPass(string pass)
    {
        Privilages priv = new Privilages();
        return priv.IsAuthenticated(hdf_userName.Value,pass);
    }

    private bool CheckPassPolicy(string pass)
    {
        //return true;

        bool ok = false;
        if (
            (pass.Contains("~") || pass.Contains("!") || pass.Contains("@") || pass.Contains("#") || pass.Contains("$") || pass.Contains("&") || pass.Contains("_"))
            && (pass.Contains("1") || pass.Contains("2") || pass.Contains("3") || pass.Contains("4") || pass.Contains("5") || pass.Contains("6") || pass.Contains("7") || pass.Contains("8") || pass.Contains("9") || pass.Contains("0"))
            && (pass.Length >= 8)
            )
        {
            ok = true;
        }
        return ok;
    }

    private bool ResetMailPassword(string user, string pass)
    {
        bool isok = true;
        isok = UpdateUserPassword(user, pass);

        return isok;
    }

    public bool UpdateUserPassword(string logonName, string newPassword)
    {
        try
        {
            var userPrincipal = GetDomainUser(logonName);
            userPrincipal.SetPassword(newPassword);
            userPrincipal.Save();
            return true;
        }
        catch (Exception ex)
        { return false; }
    }

    private void DomainUser(string code)
    {
        //DirectoryEntry entry = new DirectoryEntry("LDAP://ngu.edu.eg");
        //DirectorySearcher dsearch = new DirectorySearcher(entry);
        //dsearch.Filter = "(&(objectClass=user)(office=" + code + "))";

        //foreach (SearchResult sResultSet in dsearch.FindAll())
        //{
        //    txt_universityMail.Text = GetProperty(sResultSet, "userPrincipalName");
        //    txt_netuser.Text = GetProperty(sResultSet, "userPrincipalName").Split('@')[0].ToString();
        //}

    }

    public string GetProperty(SearchResult searchResult, string PropertyName)
    {
        if (searchResult.Properties.Contains(PropertyName))
        {
            return searchResult.Properties[PropertyName][0].ToString();
        }
        else
        {
            return string.Empty;
        }
    }

    private string GetIpAddress()
    {
        var context = HttpContext.Current;
        var ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (string.IsNullOrEmpty(ipAddress)) return context.Request.ServerVariables["REMOTE_ADDR"];
        var addresses = ipAddress.Split(',');
        return addresses.Length != 0 ? addresses[0] : context.Request.ServerVariables["REMOTE_ADDR"];
    }

    private int AddLog()
    {
        SqlCommand cmd = new SqlCommand("SP_AddChangePasswordLog", _connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.Add("@userCode", SqlDbType.Int).Value = hdf_userID.Value;
        cmd.Parameters.Add("@userName", SqlDbType.NVarChar,50).Value = hdf_userName.Value;
        cmd.Parameters.Add("@updatedBy", SqlDbType.Int).Value = hdf_userID.Value;
        cmd.Parameters.Add("@updatedFrom", SqlDbType.NVarChar,50).Value = GetIpAddress();
        if (_connection.State == ConnectionState.Closed)
            _connection.Open();


        int res = cmd.ExecuteNonQuery();
        _connection.Close();

        return res;
    }
}