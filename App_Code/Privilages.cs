using System;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Configuration;
using System.Data;
using System.Runtime.CompilerServices;

/// <summary>
/// Summary description for Privilages
/// </summary>
public class Privilages
{
    //string Connection = new Connection().ConnercionString;
    readonly SqlConnection _connection =
        new SqlConnection(ConfigurationManager.ConnectionStrings["Authentication"].ConnectionString);

    readonly SqlConnection _attendconnection =
          new SqlConnection(ConfigurationManager.ConnectionStrings["MyString"].ConnectionString);
    readonly SqlConnection _Dashboard =
         new SqlConnection(ConfigurationManager.ConnectionStrings["Dashboard"].ConnectionString);


    string _displayName = "",
        _mail = "",
        _sAmAccountName = "",
        _company = "",
        _department = "",
        _physicalDeliveryOfficeName = "",
        _telephoneNumber = "",
        _title = "",
        _employeeId = "",
        _description = "";

     public bool IsAuthenticated(string username, string pwd)
    {
       // return true;
        var isTrue = false;
        const string ldapPath = "LDAP://DC=ngu,DC=local";

        var entry = new DirectoryEntry(ldapPath, username+"@ngu.edu.eg", pwd);
        try
        {
            //Bind to the native AdsObject to force authentication.
            Object obj ;
            DirectorySearcher search;
            try{
                obj = entry.NativeObject;
            }
            catch {
                obj = null;
            }
            try{
                search = new DirectorySearcher(entry) { Filter = "(SAMAccountName=" + username + ")" };
            }
            catch {
                search = new DirectorySearcher(entry) { Filter = "(mail=" + username + "@ngu.edu.eg)" };
            }

            search.PropertiesToLoad.Add("cn");

            search.PropertiesToLoad.Add("displayName");
            search.PropertiesToLoad.Add("mail");
            search.PropertiesToLoad.Add("sAMAccountName");
            search.PropertiesToLoad.Add("COMPANY");
            search.PropertiesToLoad.Add("DEPARTMENT");
            search.PropertiesToLoad.Add("physicalDeliveryOfficeName");
            search.PropertiesToLoad.Add("telephoneNumber");
            search.PropertiesToLoad.Add("title");
            search.PropertiesToLoad.Add("employeeID");
            search.PropertiesToLoad.Add("description");

            SearchResult result = null ;
            result = search.FindOne();

            if (result==null)
            {
                search = new DirectorySearcher(entry) { Filter = "(mail=" + username + "@ngu.edu.eg)" };
                search.PropertiesToLoad.Add("cn");

                search.PropertiesToLoad.Add("displayName");
                search.PropertiesToLoad.Add("mail");
                search.PropertiesToLoad.Add("sAMAccountName");
                search.PropertiesToLoad.Add("COMPANY");
                search.PropertiesToLoad.Add("DEPARTMENT");
                search.PropertiesToLoad.Add("physicalDeliveryOfficeName");
                search.PropertiesToLoad.Add("telephoneNumber");
                search.PropertiesToLoad.Add("title");
                search.PropertiesToLoad.Add("employeeID");
                search.PropertiesToLoad.Add("description");

                result = search.FindOne();
            }
            var filterAttribute = (string)result.Properties["cn"][0];

            try
            {
                _displayName = Convert.ToString(result.Properties["displayName"][0]);
            }
            catch
            {
                // ignored
            }
            try
            {
                _mail = Convert.ToString(result.Properties["mail"][0]);
            }
            catch
            {
                // ignored
            }
            try
            {
                _sAmAccountName = Convert.ToString(result.Properties["sAMAccountName"][0]);
            }
            catch
            {
                // ignored
            }
            try
            {
                _company = Convert.ToString(result.Properties["COMPANY"][0]);
            }
            catch
            {
                // ignored
            }
            try
            {
                _department = Convert.ToString(result.Properties["DEPARTMENT"][0]);
            }
            catch
            {
                // ignored
            }
            try
            {
                _physicalDeliveryOfficeName = Convert.ToString(result.Properties["physicalDeliveryOfficeName"][0]);
            }
            catch
            {
                // ignored
            }
            try
            {
                _telephoneNumber = Convert.ToString(result.Properties["telephoneNumber"][0]);
            }
            catch
            {
                // ignored
            }
            try
            {
                _title = Convert.ToString(result.Properties["title"][0]);
            }
            catch
            {
                // ignored
            }
            try
            {
                _employeeId = Convert.ToString(result.Properties["employeeID"][0]);
            }
            catch
            {
                // ignored
            }
            try
            {
                _description = Convert.ToString(result.Properties["description"][0]);
            }
            catch
            {
                // ignored
            }


            isTrue = true;
        }
        catch (Exception ex)
        {
            isTrue = false;
        }

       return isTrue;
  
       
    }

    public bool IsApplicationAllowed(int applicationId, string username, string password, ref string displayname,
        ref int userid)
    {
        var isAllowed = false;
        if (IsAuthenticated(username, password))
        {
            bool x = isAllowedInDB(applicationId, username, ref displayname, ref userid);

            isAllowed = x;
        }
        else
        {

        }
        return isAllowed;
    }


    public bool isAllowedInDB(int applicationId, string username, ref string userNameEn, ref int userid)
    {
        bool isAllowedInDb = false;
     
        return isAllowedInDb;
    }

    public bool isHasPermission(int actionID, int userid)
    {
        bool isAllowedInDb = false;
        SqlCommand cmd = new SqlCommand("CheckUserPermissionAllow", _connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.Add("@pageActionID", SqlDbType.Int).Value = actionID;
        cmd.Parameters.Add("@userID", SqlDbType.NVarChar).Value = userid;
        if (_connection.State == ConnectionState.Closed)
            _connection.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            isAllowedInDb = true;

            reader.Close();
            _connection.Close();
        }
        else
        {
            isAllowedInDb = false;

            reader.Close();
            _connection.Close();
        }
        return isAllowedInDb;
    }

    public void AddLogToDatabase(int userId, int pageActionId, string pageActionDetailsOptional, string logIp)
    {
        try
        {



            var cmd = new SqlCommand("InsertLog", _connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            var pUserId = cmd.Parameters.Add("@UserId", SqlDbType.Int);
            var pPageActionId = cmd.Parameters.Add("@PageActionId", SqlDbType.Int);
            var ppageActionDetailsOptional = cmd.Parameters.Add("@PageActionDetails", SqlDbType.NVarChar, 500);
            var pLogIp = cmd.Parameters.Add("@LogIp", SqlDbType.NVarChar, 500);


            pUserId.Value = userId;
            pPageActionId.Value = pageActionId;
            ppageActionDetailsOptional.Value = pageActionDetailsOptional;
            pLogIp.Value = logIp;

            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
            var strRowAffect = cmd.ExecuteNonQuery().ToString();
            _connection.Close();
        }
        catch (Exception e)
        {
            //Console.WriteLine(e);
            //throw;
        }
    }

   
   

   

}