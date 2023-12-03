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

public partial class _StuContact : System.Web.UI.Page
{
    readonly SqlConnection _connection =
        new SqlConnection(ConfigurationManager.ConnectionStrings["ContactConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            div_data.Visible = false;
            div_noData.Visible = false;
        }
    }

    protected void btn_load_Click(object sender, EventArgs e)
    {
        SqlCommand command = new SqlCommand("SP_SearchContactByID", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Clear();

        command.Parameters.AddWithValue("@stuID", txt_stuID.Text);

        if (_connection.State == ConnectionState.Closed)
            _connection.Open();

        SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            div_noData.Visible = false;
            div_data.Visible = true;
            hdf_stuID.Value = reader["studentID"].ToString();
            txt_stuCode.Text = reader["studentID"].ToString();

            txt_stuNameEn.Text = reader["studentFullNameEn"].ToString();
            ddlst_school.SelectedValue = reader["schooID"].ToString();
            txt_countryEn.Text = reader["pers_countryEn"].ToString();
            txt_cityEn.Text = reader["pers_cityEn"].ToString();
            txt_districtEn.Text = reader["pers_districtEn"].ToString();
            txt_streetEn.Text = reader["pers_streetEn"].ToString();
            txt_buildingEn.Text = reader["pers_buildingEn"].ToString();
            txt_flatEn.Text = reader["pers_flatEn"].ToString();
            txt_perLandLine.Text = reader["pers_landLine"].ToString();
            txt_perCellphone.Text = reader["pers_cellPhone"].ToString();
            txt_permail.Text = reader["pers_mail"].ToString();
            txt_stuNameAr.Text = reader["studentFullNamAr"].ToString();
            txt_countryAr.Text = reader["pers_countryAr"].ToString();
            txt_cityAr.Text = reader["pers_cityAr"].ToString();
            txt_districtAr.Text = reader["pers_districtAr"].ToString();
            txt_streetAr.Text = reader["pers_streetAr"].ToString();
            txt_buildingAr.Text = reader["pers_buildingAr"].ToString();
            txt_flatAr.Text = reader["pers_flatAr"].ToString();
            ddlst_guardRelation.SelectedValue = reader["gur_type"].ToString();
            txt_gurNameEn.Text = reader["gur_nameEn"].ToString();
            txt_gurJobEn.Text = reader["gur_jobEn"].ToString();
            txt_gurAddressEn.Text = reader["gur_addEn"].ToString();
            txt_gurNameAr.Text = reader["gur_nameAr"].ToString();
            txt_gurJobAr.Text = reader["gur_jobAr"].ToString();
            txt_gurAddressAr.Text = reader["gur_addAr"].ToString();
            txt_gurLandLine.Text = reader["gur_landLine"].ToString();
            txt_gurCellphone.Text = reader["gur_cellPhone"].ToString();
            txt_gurmail.Text = reader["gur_mail"].ToString();
            ddlst_emergincyRelation.SelectedValue = reader["emr_type"].ToString();
            txt_emrNameEn.Text = reader["emr_nameEn"].ToString();
            txt_emrNameAr.Text = reader["emr_nameAr"].ToString();
            txt_emrCellphone.Text = reader["emr_cellPhone"].ToString();
            txt_emrmail.Text = reader["emr_mail"].ToString();

            txt_gurWorkAddressEn.Text = reader["gur_workAddEn"].ToString();
            txt_gurWorkAddressAr.Text = reader["gur_workAddAr"].ToString();
            txt_emrJobEn.Text = reader["emr_JobEn"].ToString();
            txt_emrJobAr.Text = reader["emr_JobAr"].ToString();
            txt_emrAddressEn.Text = reader["emr_AddEn"].ToString();
            txt_emrAddressAr.Text = reader["emr_AddAr"].ToString();
            txt_emrLandline.Text = reader["emr_landLine"].ToString();

            ddlst_reqGender.SelectedValue = reader["req_gender"].ToString();
            txt_reqDOB.SelectedDate = Convert.ToDateTime(reader["req_DOB"].ToString());
            txt_reqnationalty1En.Text = reader["req_national1En"].ToString();
            txt_reqnationalty1Ar.Text = reader["req_national1Ar"].ToString();
            txt_reqnationalty12En.Text = Convert.ToString(reader["req_national2En"]);
            txt_reqnationalty12Ar.Text = Convert.ToString(reader["req_national2Ar"]);
            txt_reqplaceOBEn.Text = reader["req_placeOBEn"].ToString();
            txt_reqplaceOBAr.Text = reader["req_placeOBAr"].ToString();
            txt_reqnationalID.Text = reader["req_nationalID"].ToString();
            ddlst_reqNatinalIDType.SelectedValue = reader["req_nationalIDType"].ToString();
            txt_reqschoolNameEn.Text = reader["req_secondrySchoolEn"].ToString();
            txt_reqschoolNameAr.Text = reader["req_secondrySchoolAr"].ToString();
            txt_reqcertificateNameEn.Text = reader["req_secondryCertificateEn"].ToString();
            txt_reqcertificateNameAr.Text = reader["req_secondryCertificateAr"].ToString();

            img_gurID.ImageUrl = "~/NationalIDs/" + reader["NationalIDPath"].ToString();

            hdf_gurIDPic.Value = reader["NationalIDPath"].ToString();
            req_pic.Enabled = false;

            reader.Close();
            _connection.Close();
        }
        else
        {
            div_noData.Visible = true;
            div_data.Visible = true;

            hdf_stuID.Value = "";
            txt_stuCode.Text = txt_stuID.Text;

            img_gurID.ImageUrl = "";
            hdf_gurIDPic.Value = "";

            txt_stuNameEn.Text = "";
            ddlst_school.SelectedValue = "0";
            txt_countryEn.Text = "";
            txt_cityEn.Text = "";
            txt_districtEn.Text = "";
            txt_streetEn.Text = "";
            txt_buildingEn.Text = "";
            txt_flatEn.Text = "";
            txt_perLandLine.Text = "";
            txt_perCellphone.Text = "";
            txt_permail.Text = "";
            txt_stuNameAr.Text = "";
            txt_countryAr.Text = "";
            txt_cityAr.Text = "";
            txt_districtAr.Text = "";
            txt_streetAr.Text = "";
            txt_buildingAr.Text = "";
            txt_flatAr.Text = "";
            ddlst_guardRelation.SelectedValue = "0";
            txt_gurNameEn.Text = "";
            txt_gurJobEn.Text = "";
            txt_gurAddressEn.Text = "";
            txt_gurNameAr.Text = "";
            txt_gurJobAr.Text = "";
            txt_gurAddressAr.Text = "";
            txt_gurLandLine.Text = "";
            txt_gurCellphone.Text = "";
            txt_gurmail.Text = "";
            ddlst_emergincyRelation.SelectedValue = "0";
            txt_emrNameEn.Text = "";
            txt_emrNameAr.Text = "";
            txt_emrCellphone.Text = "";
            txt_emrmail.Text = "";
            img_gurID.ImageUrl = "";

            txt_gurWorkAddressEn.Text = "";
            txt_gurWorkAddressAr.Text = "";
            txt_emrJobEn.Text = "";
            txt_emrJobAr.Text = "";
            txt_emrAddressEn.Text = "";
            txt_emrAddressAr.Text = "";
            txt_emrLandline.Text = "";

            ddlst_reqGender.SelectedValue = "0";
            txt_reqDOB.SelectedDate = null;
            txt_reqnationalty1En.Text = "";
            txt_reqnationalty1Ar.Text = "";
            txt_reqnationalty12En.Text = "";
            txt_reqnationalty12Ar.Text = "";
            txt_reqplaceOBEn.Text = "";
            txt_reqplaceOBAr.Text = "";
            txt_reqnationalID.Text = "";
            ddlst_reqNatinalIDType.SelectedValue = "0";
            txt_reqschoolNameEn.Text = "";
            txt_reqschoolNameAr.Text = "";
            txt_reqcertificateNameEn.Text = "";
            txt_reqcertificateNameAr.Text = "";


            hdf_gurIDPic.Value = "";
            req_pic.Enabled = true;

            reader.Close();
            _connection.Close();
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        string IDpath = "";
        if (fil_gurIdPic.HasFile)
        {
            IDpath = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + txt_stuCode.Text + ".jpg";

            FileUpload(IDpath);
        }
        else
        {
            IDpath = hdf_gurIDPic.Value;
        }

        SqlCommand command = new SqlCommand("SaveContact", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Clear();

        command.Parameters.AddWithValue("@studentID", txt_stuCode.Text);
        command.Parameters.AddWithValue("@studentFullNameEn", txt_stuNameEn.Text);
        command.Parameters.AddWithValue("@schooID", ddlst_school.SelectedValue);
        command.Parameters.AddWithValue("@pers_countryEn", txt_countryEn.Text);
        command.Parameters.AddWithValue("@pers_cityEn", txt_cityEn.Text);
        command.Parameters.AddWithValue("@pers_districtEn", txt_districtEn.Text);
        command.Parameters.AddWithValue("@pers_streetEn", txt_streetEn.Text);
        command.Parameters.AddWithValue("@pers_buildingEn", txt_buildingEn.Text);
        command.Parameters.AddWithValue("@pers_flatEn", txt_flatEn.Text);
        command.Parameters.AddWithValue("@pers_landLine", txt_perLandLine.Text);
        command.Parameters.AddWithValue("@pers_cellPhone", txt_perCellphone.Text);
        command.Parameters.AddWithValue("@pers_mail", txt_permail.Text);
        command.Parameters.AddWithValue("@studentFullNamAr", txt_stuNameAr.Text);
        command.Parameters.AddWithValue("@pers_countryAr", txt_countryAr.Text);
        command.Parameters.AddWithValue("@pers_cityAr", txt_cityAr.Text);
        command.Parameters.AddWithValue("@pers_districtAr", txt_districtAr.Text);
        command.Parameters.AddWithValue("@pers_streetAr", txt_streetAr.Text);
        command.Parameters.AddWithValue("@pers_buildingAr", txt_buildingAr.Text);
        command.Parameters.AddWithValue("@pers_flatAr", txt_flatAr.Text);
        command.Parameters.AddWithValue("@gur_type", ddlst_guardRelation.SelectedValue);
        command.Parameters.AddWithValue("@gur_nameEn", txt_gurNameEn.Text);
        command.Parameters.AddWithValue("@gur_jobEn", txt_gurJobEn.Text);
        command.Parameters.AddWithValue("@gur_addEn", txt_gurAddressEn.Text);
        command.Parameters.AddWithValue("@gur_nameAr", txt_gurNameAr.Text);
        command.Parameters.AddWithValue("@gur_jobAr", txt_gurJobAr.Text);
        command.Parameters.AddWithValue("@gur_addAr", txt_gurAddressAr.Text);
        command.Parameters.AddWithValue("@gur_landLine", txt_gurLandLine.Text);
        command.Parameters.AddWithValue("@gur_cellPhone", txt_gurCellphone.Text);
        command.Parameters.AddWithValue("@gur_mail", txt_gurmail.Text);
        command.Parameters.AddWithValue("@emr_type", ddlst_emergincyRelation.SelectedValue);
        command.Parameters.AddWithValue("@emr_nameEn", txt_emrNameEn.Text);
        command.Parameters.AddWithValue("@emr_nameAr", txt_emrNameAr.Text);
        command.Parameters.AddWithValue("@emr_cellPhone", txt_emrCellphone.Text);
        command.Parameters.AddWithValue("@emr_mail", txt_emrmail.Text);

        command.Parameters.AddWithValue("@gur_workAddEn", txt_gurWorkAddressEn.Text);
        command.Parameters.AddWithValue("@gur_workAddAr", txt_gurWorkAddressAr.Text);
        command.Parameters.AddWithValue("@emr_JobEn", txt_emrJobEn.Text);
        command.Parameters.AddWithValue("@emr_JobAr", txt_emrJobAr.Text);
        command.Parameters.AddWithValue("@emr_AddEn", txt_emrAddressEn.Text);
        command.Parameters.AddWithValue("@emr_AddAr", txt_emrAddressAr.Text);
        command.Parameters.AddWithValue("@emr_landLine", txt_emrLandline.Text);

        command.Parameters.AddWithValue("@req_gender", ddlst_reqGender.SelectedValue);
        command.Parameters.AddWithValue("@req_DOB", txt_reqDOB.SelectedDate);
        command.Parameters.AddWithValue("@req_national1En", txt_reqnationalty1En.Text);
        command.Parameters.AddWithValue("@req_national1Ar", txt_reqnationalty1Ar.Text);
        command.Parameters.AddWithValue("@req_national2En", txt_reqnationalty12En.Text);
        command.Parameters.AddWithValue("@req_national2Ar", txt_reqnationalty12Ar.Text);
        command.Parameters.AddWithValue("@req_placeOBEn", txt_reqplaceOBEn.Text);
        command.Parameters.AddWithValue("@req_placeOBAr", txt_reqplaceOBAr.Text);
        command.Parameters.AddWithValue("@req_nationalID", txt_reqnationalID.Text);
        command.Parameters.AddWithValue("@req_nationalIDType", ddlst_reqNatinalIDType.SelectedValue);
        command.Parameters.AddWithValue("@req_secondrySchoolEn", txt_reqschoolNameEn.Text);
        command.Parameters.AddWithValue("@req_secondrySchoolAr", txt_reqschoolNameAr.Text);
        command.Parameters.AddWithValue("@req_secondryCertificateEn", txt_reqcertificateNameEn.Text);
        command.Parameters.AddWithValue("@req_secondryCertificateAr", txt_reqcertificateNameAr.Text);

        command.Parameters.AddWithValue("@NationalIDPath", IDpath);
        command.Parameters.AddWithValue("@IP", GetIpAddress());

        if (_connection.State == ConnectionState.Closed)
            _connection.Open();
        try
        {

            command.ExecuteNonQuery();
            _connection.Close();           

            hdf_stuID.Value = txt_stuCode.Text;
            hdf_gurIDPic.Value = IDpath;
            img_gurID.ImageUrl = "~/NationalIDs/" + IDpath;


            string strMsg = "Save Done.";
            string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
            Response.Write(script);

            Response.Redirect("~/contactRep.aspx?id=" + txt_stuCode.Text);

        }
        catch (Exception)
        {

            string strMsg = "Save Error.";
            string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
            Response.Write(script);

            _connection.Close();

            
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

    private void FileUpload(string url)
    {
                fil_gurIdPic.SaveAs(Server.MapPath("~/NationalIDs/") + url);
    }
}