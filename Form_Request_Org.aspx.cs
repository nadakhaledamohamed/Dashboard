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
public class RequestFormdata
{
    public int FormTypeId;
    public decimal Cost;
    public string FormTitle;
    public string TypeName;
    public int TypeId;
    public int FormId;


}

enum RequestStep
{
    SelectForm,     // 0
    FillData,    // 1
    Revise,  // 2
    Submit   // 3   
}
public partial class _RequestForm : System.Web.UI.Page
{
    public class FormBasicData
    {
        public int FormId;
        public HtmlGenericControl control;
    }

    public List<FormBasicData> FormData = new List<FormBasicData>();
    public static List<RequestFormdata> FormtypeList = new List<RequestFormdata>();
    protected static string FormRequestConStr = ConfigurationManager.ConnectionStrings["FormRequestConnectionString"].ConnectionString;
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter adr;
    DataTable dt;
    public List<UserControl> controls = new List<UserControl>();
    protected void Page_Load(object sender, EventArgs e)
    {
        
     
        FormData.Add(new FormBasicData { FormId = -1, control = InvalidFormsDiv });
        FormData.Add(new FormBasicData { FormId = 1, control = OFF_Transcriptdiv });
        FormData.Add(new FormBasicData { FormId = 3, control = Militartdiv });
        FormData.Add(new FormBasicData { FormId = 0, control = CommaanDataDiv });
        FormData.Add(new FormBasicData { FormId = 2, control = UnOFF_Transcriptdiv });
        FormData.Add(new FormBasicData { FormId = 4, control = ExcussesDiv });
        FormData.Add(new FormBasicData { FormId = 5, control = EnglishEnrollmentDiv });
        FormData.Add(new FormBasicData { FormId = 6, control = ArabicEnrollmentDiv });
        FormData.Add(new FormBasicData { FormId = 7, control = AssesmentPetiton_Div });
        FormData.Add(new FormBasicData { FormId = 8, control = RegisterationBreak_Div });
        FormData.Add(new FormBasicData { FormId = 9, control = Withdrawal_Div });
        FormData.Add(new FormBasicData { FormId = 11, control = supplementarycourses_Div });
        //AssesmentPetiton_Div
        if (!IsPostBack)
        {
            FillDD();
            Session["Requeststatus"] = RequestStep.SelectForm;
            Session["SelectedForm"] = null;
            Session["FileUpload"] = null;
            Excuse_ReasonDescribtion.Enabled = false;
        }
    }
    private void FillDD()
    {

        using (con = new SqlConnection(FormRequestConStr))
        {
            using (cmd = new SqlCommand("GetCourses", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                adr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adr.Fill(dt);
                supplementarycourses_list.DataTextField = "title";
                supplementarycourses_list.DataValueField = "id";
                supplementarycourses_list.DataSource = dt;
                supplementarycourses_list.DataBind();               
            }
           
        }
            using (con = new SqlConnection(FormRequestConStr))
        {
            using (cmd = new SqlCommand("GetFormRepresntedTo", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FormId", 5);
                adr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adr.Fill(dt);
            }
            EnglishEnrollmentRepeater.DataSource = dt;
            EnglishEnrollmentRepeater.DataBind();
            foreach (RepeaterItem item in EnglishEnrollmentRepeater.Items)
            {
                CheckBox chk = item.FindControl("chkSourceType") as CheckBox;
                if (chk.Checked == false)
                {
                    (item.FindControl("txtDescribtion") as TextBox).Text = "";
                    (item.FindControl("txtDescribtion") as TextBox).Enabled = false;
                }
                else
                {

                    (item.FindControl("txtDescribtion") as TextBox).Enabled = true;

                }



            }

        }
        using (con = new SqlConnection(FormRequestConStr))
        {
            using (cmd = new SqlCommand("GetFormRepresntedTo", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FormId", 6);
                adr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adr.Fill(dt);
            }
            ArabicEnrollment_Repeater.DataSource = dt;
            ArabicEnrollment_Repeater.DataBind();
            foreach (RepeaterItem item in ArabicEnrollment_Repeater.Items)
            {
                CheckBox chk = item.FindControl("ArchkSourceType") as CheckBox;
                HiddenField hf = item.FindControl("ArSourceTypeId") as HiddenField;
                TextBox txt = item.FindControl("ArtxtDescribtion") as TextBox;
                if (hf.Value == "4")
                {
                    txt.Enabled = false;
                }
                else
                {
                    txt.Visible = false;
                }



            }

        }
        using (con = new SqlConnection(FormRequestConStr))
        {
            using (cmd = new SqlCommand("GetYearSmester", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.AddWithValue("@FormId", 6);
                adr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adr.Fill(dt);
            }
            RegisterationBreak_startSemester.DataSource = dt;
            RegisterationBreak_startSemester.DataTextField = "title";
            RegisterationBreak_startSemester.DataValueField = "row_num";
            RegisterationBreak_startSemester.DataBind();
            RegisterationBreak_startSemester.Items.Insert(0, new ListItem { Value = "0", Text = "Select Semester" });
            RegisterationBreak_EndSemester.Items.Insert(0, new ListItem { Value = "0", Text = "Select Semester" });
            RegisterationBreak_EndSemester.Enabled = false;
        }
        List<int> invalidforms = new List<int>();
        long studentid = Convert.ToInt64(Session["StuSmallId"]);

        using (con = new SqlConnection(FormRequestConStr))
        {
            using (cmd = new SqlCommand("GetRequestForms", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SmallId", studentid);
                adr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adr.Fill(dt);
                ddForms.DataSource = dt;
                ddForms.DataTextField = "FormTitle";
                ddForms.DataValueField = "Id";
                ddForms.DataBind();

            }
        }
       

        FormtypeList.Clear();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i][5].ToString() == "0")
            {
                invalidforms.Add(int.Parse(dt.Rows[i]["Id"].ToString()));
            }
            FormtypeList.Add(new RequestFormdata
            {
                FormTypeId = int.Parse(dt.Rows[i]["Id"].ToString()),
                FormId = int.Parse(dt.Rows[i]["FormId"].ToString()),
                TypeId = int.Parse(dt.Rows[i]["TypeId"].ToString()),
                FormTitle = dt.Rows[i]["FormTitle"].ToString(),
                Cost = Convert.ToDecimal(dt.Rows[i]["Cost"].ToString())
            });
        }
        FormtypeList.Add(new RequestFormdata { FormTypeId = 0, FormId = 0, TypeId = 0 });
        FormtypeList.Add(new RequestFormdata { FormTypeId = -1, FormId = -1, TypeId = -1 });
        Session["InvalidForms"] = invalidforms;
        using (con = new SqlConnection(FormRequestConStr))
        {
            using (cmd = new SqlCommand("GetRelatedForm", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FormId", 9);
                adr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adr.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        ListItem itm = ddForms.Items.FindByValue(dt.Rows[i]["tableId"].ToString());
                        if(itm != null)
                        {
                            itm.Enabled = false;
                        }

                      


                    }
                }
            }
        }
    }



    protected void nextBtn_Click(object sender, EventArgs e)
    {
        cancelType.Visible = true;
        PrevBtn.Visible = true;

        if ((RequestStep)Session["Requeststatus"] == RequestStep.SelectForm)
        {
            FillDataStep();
            Session["Requeststatus"] = RequestStep.FillData;
        }
        else if ((RequestStep)Session["Requeststatus"] == RequestStep.FillData)
        {
            if (Page.IsValid)
            {
                FormsDiv.Visible = false;
                FillGv();
                ReviseDiv.Visible = true;
                NextBtn.Text = "Submit";
                Session["Requeststatus"] = RequestStep.Revise;
            }
        }
        else if ((RequestStep)Session["Requeststatus"] == RequestStep.Revise)
        {
            //FormsDiv.Visible = false;
            //FillGv();
            //ReviseDiv.Visible = true;
            NextBtn.Enabled = false;
            SaveRequest();

            Session["Requeststatus"] = RequestStep.Submit;
        }
    }

    private void Display_Forms(int FormTypeId, string HTMLError = "")
    {

        int FormId = (from p in FormtypeList where p.FormTypeId == FormTypeId select p.FormId).First();
        if (FormId != 10)
        {
            HtmlGenericControl cntrl = (HtmlGenericControl)(from p in FormData
                                                            where p.FormId == FormId
                                                            select p.control).First();
            if (cntrl != null)
            {
                cntrl.Visible = true;
            }
            if (FormId == -1)
            {
                ErrorLbl.InnerHtml = HTMLError;
            }
            else if (FormId == 0)
            {
                FillstudentData();
            }
        }
    }
    private RequestFormdata GetTypeCost(int FormId, int typeid = 0)
    {
        RequestFormdata d = new RequestFormdata();
        using (con = new SqlConnection(FormRequestConStr))
        {
            using (cmd = new SqlCommand("GetFormTypeCost", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FormId", FormId);
                cmd.Parameters.AddWithValue("@TypeId", typeid);
                adr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adr.Fill(dt);
            }
        }
        if (dt.Rows.Count > 0)
        {
            d.TypeId = int.Parse(dt.Rows[0][1].ToString());
            d.Cost = decimal.Parse(dt.Rows[0][2].ToString());
            d.FormTitle = dt.Rows[0][3].ToString();
            d.FormTypeId = int.Parse(dt.Rows[0][4].ToString());
            d.TypeName = dt.Rows[0][5].ToString();
            d.FormId = int.Parse(dt.Rows[0][0].ToString());


        }
        return d;


    }

    protected void cancelType_Click(object sender, EventArgs e)
    {
        Response.Redirect("StudentRequests.aspx", false);
    }



    private void FillDataStep()
    {
        List<int> Forms = new List<int>();
        List<String> invalidform = new List<string>();


        if (ddForms.SelectedItem != null)
        {
            foreach (ListItem liForm in ddForms.Items)
            {
                if (liForm.Selected)
                {
                    if (((List<int>)Session["InvalidForms"]).Contains(Convert.ToInt32(liForm.Value)))
                    {
                        invalidform.Add(liForm.Text);

                        //   liform.
                    }
                    else
                    {
                        Forms.Add(Convert.ToInt32(liForm.Value));
                    }
                }

            }

            Forms = Forms.Distinct().ToList();
        }
        if (Forms.Count > 0 || invalidform.Count > 0)
        {
            FormsDiv.Visible = true;
            FormRequestDiv.Visible = false;
            Session["SelectedForm"] = Forms;
            if (Forms.Count > 0)
            {

                Display_Forms(0);

                for (int i = 0; i < Forms.Count; i++)
                {
                    Display_Forms(Forms[i]);

                }

            }
            else
            {
                NextBtn.Visible = false;

            }
            if (invalidform.Count > 0)
            {
                string HTMLError = "<b>The Following Request is not available due to Financial stop , Please refere to finance .</b> <br/>";
                foreach (string st in invalidform)
                {
                    HTMLError = HTMLError + "* " + st + "<br/>";

                }
                Display_Forms(-1, HTMLError);
            }
        }
    }

    protected void ddForms_SelectedIndexChanged(object sender, EventArgs e)
    {

        int formId = 0;
        int FormtypeId = 0;
        foreach (ListItem item in ddForms.Items)
        {
            item.Enabled = true;
        }
        //int
        List<int> DisbaleItem = new List<int>();
        if (ddForms.SelectedItem != null)
        {

            NextBtn.Visible = true;
            if (ddForms.SelectedItem != null)
            {
                foreach (ListItem liForm in ddForms.Items)
                {
                    FormtypeId = int.Parse(liForm.Value);
                    formId = FormtypeList.Where(s => s.FormTypeId == FormtypeId).Select(t => t.FormId).First();
                    if (liForm.Selected)
                    {
                        DisbaleItem = FormtypeList.Where(f => f.FormId == formId && f.FormTypeId != FormtypeId).Select(tt => tt.FormTypeId).ToList();
                        foreach (int i in DisbaleItem)
                        {
                            ListItem srtItem = ddForms.Items.FindByValue(i.ToString());
                            if (srtItem != null)
                            {
                                srtItem.Enabled = false;
                            }

                        }
                      

                    }
                    if (formId == 9)
                    {
                        using (con = new SqlConnection(FormRequestConStr))
                        {
                            using (cmd = new SqlCommand("GetRelatedForm", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@FormId", formId);
                                adr = new SqlDataAdapter(cmd);
                                dt = new DataTable();
                                adr.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {

                                        ListItem itm = ddForms.Items.FindByValue(dt.Rows[i]["tableId"].ToString());
                                        if(itm != null)
                                        {
                                            if (liForm.Selected == true)
                                            {
                                                itm.Enabled = true;
                                            }
                                            else
                                            {
                                                itm.Selected = false;
                                                itm.Enabled = false;

                                            }

                                        }
                                        
                                            

                                        
                                    }
                                }
                            }
                        }



                    }

                }
            }

        }
        else
        {

            NextBtn.Visible = false;
            using (con = new SqlConnection(FormRequestConStr))
            {
                using (cmd = new SqlCommand("GetRelatedForm", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FormId", 9);
                    adr = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adr.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            ListItem itm = ddForms.Items.FindByValue(dt.Rows[i]["tableId"].ToString());

                            itm.Enabled = false;


                        }
                    }
                }
            }

        }
    }


    protected void PrevBtn_Click(object sender, EventArgs e)
    {
        cancelType.Visible = true;
        PrevBtn.Visible = true;

        if ((RequestStep)Session["Requeststatus"] == RequestStep.FillData)
        {
            FormsDiv.Visible = false;
            PrevBtn.Visible = false;
            cancelType.Visible = false;
            Session["Requeststatus"] = RequestStep.SelectForm;
            FormRequestDiv.Visible = true;

        }
        //else if ((RequestStep)Session["Requeststatus"] == RequestStep.FillData)       
        else if ((RequestStep)Session["Requeststatus"] == RequestStep.Revise)
        {
            ReviseDiv.Visible = false;
            List<int> selected = new List<int>();
            selected = (List<int>)Session["SelectedForm"];
            foreach (ListItem liForm in ddForms.Items)
            {
                if (selected.Contains(int.Parse(liForm.Value)))
                {
                    liForm.Selected = true;
                }
                else
                {

                    liForm.Selected = false;
                }
            }
            List<HtmlGenericControl> controls = new List<HtmlGenericControl>();
            controls = FormData.Select(s => s.control).ToList();
            foreach (HtmlGenericControl cntrl in controls)
            {
                cntrl.Visible = false;
            }


            //      FillDataStep();
            NextBtn.Text = "Next";
            Session["Requeststatus"] = RequestStep.FillData;
            FillDataStep();


        }
    }
    public void FillstudentData()
    {

        if (Session["StuSmallId"] != null)
        {
            int studentid = (int)Session["StuSmallId"];
            using (con = new SqlConnection(FormRequestConStr))
            {
                using (cmd = new SqlCommand("GetStudentData", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SmallId", studentid);
                    adr = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adr.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        txtFullName.Text = dt.Rows[0][1].ToString();
                        txtStudentID.Text = dt.Rows[0][0].ToString();
                        txtDOB.Text = dt.Rows[0][2].ToString();
                        txtSchoolName.Text = dt.Rows[0][8].ToString();
                        HFSchool.Value = dt.Rows[0][7].ToString();
                        txtMail.Text = dt.Rows[0][9].ToString()+"@ngu.edu.eg";
                        txtYear.Text = dt.Rows[0][10].ToString();
                    }
                }
            }


        }


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


    protected void gvTypes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var id = Convert.ToInt32(e.CommandArgument.ToString());
        if (e.CommandName == "del")
        {
            List<int> Forms = (List<int>)Session["SelectedForm"];
            Forms.Remove(id);
            Session["SelectedForm"] = Forms;
            if (Forms.Count() == 0)
            {
                NextBtn.Visible = false;
            }

            FillGv();

        }
    }
    private void FillGv()
    {
        List<RequestFormdata> selected = new List<RequestFormdata>();
        List<int> Forms = (List<int>)Session["SelectedForm"];
        FormtypeList = FormtypeList.Distinct().ToList();
        selected = FormtypeList.Where(t => Forms.Contains(t.FormTypeId)).Distinct().ToList();
        selected = selected.Distinct().ToList();
        if (selected != null)
        {
            int formId = 0;
            int NoOFCopies = 0;
            DataTable dt = new DataTable();

            dt.Columns.Add("FormTypeId");
            dt.Columns.Add("FormTitle");
            dt.Columns.Add("NoOFCopies");
            dt.Columns.Add("Cost");
            dt.Columns.Add("TotalCost");

            foreach (var item in selected)
            {

                formId = FormtypeList.Where(f => f.FormTypeId == item.FormTypeId).Select(d => d.FormId).First();
                switch (formId)
                {
                    case 1:
                        NoOFCopies = int.Parse(OFFTranscript_NoOfCopies.Text);
                        dt.Rows.Add(item.FormTypeId, Convert.ToString(item.FormTitle), NoOFCopies, item.Cost, NoOFCopies * item.Cost);
                        break;
                    case 2:
                        NoOFCopies = int.Parse(UnOFFTranscript_NoOfCopies.Text);
                        dt.Rows.Add(item.FormTypeId, Convert.ToString(item.FormTitle), NoOFCopies, item.Cost, NoOFCopies * item.Cost);
                        break;
                    case 3:
                        NoOFCopies = 1;
                        dt.Rows.Add(item.FormTypeId, Convert.ToString(item.FormTitle), 1, item.Cost, NoOFCopies * item.Cost);
                        break;
                    case 4:
                        //   NoOFCopies = int.Parse(UnOFFTranscript_NoOfCopies.Text);
                        dt.Rows.Add(item.FormTypeId, Convert.ToString(item.FormTitle), "", "", "");
                        break;
                    case 5:
                        NoOFCopies = int.Parse(EnglishEnrollment_txtNoOfCopies.Text);
                        dt.Rows.Add(item.FormTypeId, Convert.ToString(item.FormTitle), NoOFCopies, item.Cost, NoOFCopies * item.Cost);
                        break;
                    case 6:
                        NoOFCopies = int.Parse(ArabicEnrollment_txtNoOFCopies.Text);
                        dt.Rows.Add(item.FormTypeId, Convert.ToString(item.FormTitle), NoOFCopies, item.Cost, NoOFCopies * item.Cost);
                        break;
                    case 7:
                        NoOFCopies = 1;
                        dt.Rows.Add(item.FormTypeId, Convert.ToString(item.FormTitle), NoOFCopies, item.Cost, NoOFCopies * item.Cost);
                        break;
                    case 8:
                        NoOFCopies = 1;
                        dt.Rows.Add(item.FormTypeId, Convert.ToString(item.FormTitle), NoOFCopies, item.Cost, NoOFCopies * item.Cost);
                        break;
                    case 9:
                        NoOFCopies = 1;
                        dt.Rows.Add(item.FormTypeId, Convert.ToString(item.FormTitle), NoOFCopies, item.Cost, NoOFCopies * item.Cost);
                        break;
                    case 10:
                        NoOFCopies = 1;
                        dt.Rows.Add(item.FormTypeId, Convert.ToString(item.FormTitle), NoOFCopies, item.Cost, NoOFCopies * item.Cost);
                        break;
                    case 11:
                        NoOFCopies = 0;
                        foreach(ListItem itm in supplementarycourses_list.Items)
                        {
                            if (itm.Selected == true)
                            {
                                NoOFCopies += 1;
                            }
                        }
                        dt.Rows.Add(item.FormTypeId, Convert.ToString(item.FormTitle), NoOFCopies, item.Cost, NoOFCopies * item.Cost);
                        break;
                    default:
                        break;

                }


            }

            gvForms.DataSource = dt;
            gvForms.DataBind();
        }

    }
    private void SaveRequest()
    {

        if (Page.IsValid)
        {
            ExceptionLog l = new ExceptionLog();
            using (con = new SqlConnection(FormRequestConStr))
            {
                List<int> Selected = new List<int>();
                Selected = (List<int>)Session["SelectedForm"];
                Selected = Selected.Distinct().ToList();
                int Request_Id = 0;
                int WithDrawnId = 0;
                if (Selected.Count > 0)
                {

                    dt = new DataTable();
                    string FormTypeIds = string.Join(";", Selected.ToArray());
                    try
                    {

                        using (cmd = new SqlCommand("AddStudentRequest", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@SmallId", (int)(Session["StuSmallId"]));
                            cmd.Parameters.AddWithValue("@SchoolId", int.Parse(HFSchool.Value));
                            cmd.Parameters.AddWithValue("@PlaceOfBirth", txtPlaceOfBirth.Text);
                            cmd.Parameters.AddWithValue("@DateOfBirth", txtDOB.Text);
                            cmd.Parameters.AddWithValue("@MobileNo", txtMobile.Text);
                            if (txtFullName.Text.Trim() != "")
                            {
                                cmd.Parameters.AddWithValue("@StudentName", txtFullName.Text);
                            }
                            if (txtStudentArName.Text.Trim() != "")
                            {
                                cmd.Parameters.AddWithValue("@StudentArName", txtStudentArName.Text);
                            }
                            if (txtMail.Text.Trim() != "")
                            {
                                cmd.Parameters.AddWithValue("@Email", txtMail.Text);
                            }
                            if (txtYear.Text.Trim() != "")
                            {
                                cmd.Parameters.AddWithValue("@Year", txtYear.Text);
                            }
                            cmd.Parameters.AddWithValue("@FormType", FormTypeIds);
                            adr = new SqlDataAdapter(cmd);
                            adr.Fill(dt);
                        }
                        if (dt.Rows.Count > 0)
                        {
                            Request_Id = int.Parse(dt.Rows[0]["RequestId"].ToString());
                            Session["RequestId"] = Request_Id;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                switch (int.Parse(dt.Rows[i]["FormId"].ToString()))
                                {
                                    case 3:

                                        using (cmd = new SqlCommand("AddMilitaryformDetails", con))
                                        {

                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@DetailId", int.Parse(dt.Rows[i]["DetailId"].ToString()));
                                            cmd.Parameters.AddWithValue("@Remarks", MilitaryForm_txtRemarks.Text);
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                 

                                        }
                                        break;
                                    case 1:
                                        using (cmd = new SqlCommand("AddOfficialTranscriptDetails", con))
                                        {

                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@DetailId", int.Parse(dt.Rows[i]["DetailId"].ToString()));
                                            cmd.Parameters.AddWithValue("@Remarks", OFFTranscript_txtRemarks.Text);
                                            cmd.Parameters.AddWithValue("@NoOfCopies", int.Parse(OFFTranscript_NoOfCopies.Text));
                                            cmd.Parameters.AddWithValue("@Purpose", OFFTranscript_txtPurpose.Text);
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();


                                        }
                                        break;
                                    case 2:
                                        using (cmd = new SqlCommand("AddUnOfficialTranscriptDetails", con))
                                        {

                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@DetailId", int.Parse(dt.Rows[i]["DetailId"].ToString()));
                                            cmd.Parameters.AddWithValue("@Remarks", UnOFFTranscript_txtRemarks.Text);
                                            cmd.Parameters.AddWithValue("@NoOfCopies", int.Parse(UnOFFTranscript_NoOfCopies.Text));
                                            cmd.Parameters.AddWithValue("@Purpose", UnOFFTranscript_txtPurpose.Text);
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();


                                        }
                                        break;
                                    case 4:
                                        bool isMedical = Excuse_MedicalList.SelectedValue == "1" ? true : false;
                                        using (cmd = new SqlCommand("AddExcuseformDetails", con))
                                        {

                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@DetailId", int.Parse(dt.Rows[i]["DetailId"].ToString()));
                                            cmd.Parameters.AddWithValue("@ToDate", Excuse_txtTo.Text);
                                            cmd.Parameters.AddWithValue("@FromDate", Excuse_txtFrom.Text);
                                            cmd.Parameters.AddWithValue("@IsMedical", isMedical);
                                            cmd.Parameters.AddWithValue("@Remark", Excuse_txtRemarks.Text);
                                            if (Session["FileUpload"] != null && ((List<string>)Session["FileUpload"]).Count > 0)
                                            {
                                                cmd.Parameters.AddWithValue("@Attachments", string.Join(";", ((List<string>)Session["FileUpload"]).ToArray()));
                                            }

                                            cmd.Parameters.AddWithValue("@Reason", Excuse_ReasonDescribtion.Text);
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                            //   Clear_Unused_file();

                                        }
                                        break;
                                    case 5:

                                        using (cmd = new SqlCommand("AddEnglishEnrollmentDetails", con))
                                        {

                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@DetailId", int.Parse(dt.Rows[i]["DetailId"].ToString()));
                                            cmd.Parameters.AddWithValue("@NoOfCopies", int.Parse(EnglishEnrollment_txtNoOfCopies.Text));
                                            cmd.Parameters.AddWithValue("@Remarks", EnglishEnrollment_txtRemarks.Text);
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                            // Clear_Unused_file();

                                        }
                                        foreach (RepeaterItem item in EnglishEnrollmentRepeater.Items)
                                        {
                                            CheckBox chk = item.FindControl("chkSourceType") as CheckBox;
                                            TextBox txt = item.FindControl("txtDescribtion") as TextBox;
                                            HiddenField hf = item.FindControl("SourceTypeId") as HiddenField;
                                            if (chk.Checked && txt.Text != "")
                                            {
                                                using (cmd = new SqlCommand("AddEnrollment_To_Details", con))
                                                {

                                                    cmd.CommandType = CommandType.StoredProcedure;
                                                    cmd.Parameters.AddWithValue("@DetailId", int.Parse(dt.Rows[i]["DetailId"].ToString()));
                                                    cmd.Parameters.AddWithValue("@ToId", int.Parse(hf.Value));
                                                    cmd.Parameters.AddWithValue("@Describtion", txt.Text);
                                                    con.Open();
                                                    cmd.ExecuteNonQuery();
                                                    con.Close();
                                                    //  Clear_Unused_file();

                                                }
                                            }

                                        }
                                        break;
                                    case 6:

                                        using (cmd = new SqlCommand("AddArabicEnrollmentDetails", con))
                                        {

                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@DetailId", int.Parse(dt.Rows[i]["DetailId"].ToString()));
                                            cmd.Parameters.AddWithValue("@NoOfCopies", int.Parse(ArabicEnrollment_txtNoOFCopies.Text));
                                            cmd.Parameters.AddWithValue("@Remarks", ArabicEnrollmen_txtRemarks.Text);
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                            //  Clear_Unused_file();

                                        }
                                        foreach (RepeaterItem item in ArabicEnrollment_Repeater.Items)
                                        {
                                            CheckBox chk = item.FindControl("ArchkSourceType") as CheckBox;
                                            TextBox txt = item.FindControl("ArtxtDescribtion") as TextBox;
                                            HiddenField hf = item.FindControl("ArSourceTypeId") as HiddenField;
                                            if (chk.Checked && txt.Text != "")
                                            {
                                                using (cmd = new SqlCommand("AddEnrollment_To_Details", con))
                                                {

                                                    cmd.CommandType = CommandType.StoredProcedure;
                                                    cmd.Parameters.AddWithValue("@DetailId", int.Parse(dt.Rows[i]["DetailId"].ToString()));
                                                    cmd.Parameters.AddWithValue("@ToId", int.Parse(hf.Value));
                                                    cmd.Parameters.AddWithValue("@Describtion", txt.Text);
                                                    con.Open();
                                                    cmd.ExecuteNonQuery();
                                                    con.Close();
                                                    //  Clear_Unused_file();
                                                }
                                            }

                                        }

                                        break;
                                    case 7:
                                        using (cmd = new SqlCommand("AddAssesmnetPetitonDetails", con))
                                        {

                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@DetailId", int.Parse(dt.Rows[i]["DetailId"].ToString()));
                                            cmd.Parameters.AddWithValue("@Reason", AssesmentPetiton_txtReason.Text);
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();

                                        }
                                        // Response.Redirect("RequestDetails.aspx");
                                        break;
                                    case 8:
                                        using (cmd = new SqlCommand("AddRegisterationBreakDetails", con))
                                        {

                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@DetailId", int.Parse(dt.Rows[i]["DetailId"].ToString()));
                                            cmd.Parameters.AddWithValue("@StartSem", RegisterationBreak_startSemester.SelectedItem.Text);
                                            cmd.Parameters.AddWithValue("@EndSem", RegisterationBreak_EndSemester.SelectedItem.Text);
                                            cmd.Parameters.AddWithValue("@Reason", RegisterationBreak_txtReason.Text);
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();

                                        }
                                        break;
                                    case 9:
                                        using (cmd = new SqlCommand("AddWithdrawalForm", con))
                                        {
                                            WithDrawnId = int.Parse(dt.Rows[i]["DetailId"].ToString());
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@DetailId", int.Parse(dt.Rows[i]["DetailId"].ToString()));
                                            cmd.Parameters.AddWithValue("@StudentLegalGurdian", Withdrawal_txtGurdian.Text);
                                            cmd.Parameters.AddWithValue("@ReasonOFWitdrawal", withdrawal_txtReason.Text);
                                            cmd.Parameters.AddWithValue("@WithdrawalStarting", Withdrwal_StartingList.SelectedItem.Text);
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();

                                        }
                                        break;
                                    case 10:
                                        using (cmd = new SqlCommand("AddWithdrawalArabicForm", con))
                                        {

                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@DetailId", int.Parse(dt.Rows[i]["DetailId"].ToString()));
                                            cmd.Parameters.AddWithValue("@StudentLegalGurdian", Withdrawal_txtGurdian.Text);
                                            cmd.Parameters.AddWithValue("@ReasonOFWitdrawal", withdrawal_txtReason.Text);
                                            cmd.Parameters.AddWithValue("@WithdrawalStarting", Withdrwal_StartingList.SelectedItem.Text);
                                            cmd.Parameters.AddWithValue("@RelatedWithDrawn", WithDrawnId);
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();

                                        }
                                        break;
                                    case 11:
                                        List<string> crs = new List<string> ();
                                        using (cmd = new SqlCommand("AddSupplementaryExamsDetails", con))
                                        {
                                            int NoOFCourses = 0;
                                            foreach(ListItem itm in supplementarycourses_list.Items)
                                            {
                                                if (itm.Selected == true)
                                                {
                                                    NoOFCourses += 1;
                                                    crs.Add(itm.Value);
                                                }
                                            }
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@DetailId", int.Parse(dt.Rows[i]["DetailId"].ToString()));
                                            cmd.Parameters.AddWithValue("@NoOFCourses", NoOFCourses);
                                            cmd.Parameters.AddWithValue("@Courses", string.Join(";",crs.ToArray()));                                            
                                            cmd.Parameters.AddWithValue("@Remarks", supplementarycourses_txtremark.Text);
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();

                                        }
                                        break;
                                    default:
                                        break;
                                }
                                try
                                {
                                    l.SendEmailToResponsible(int.Parse(dt.Rows[i]["DetailId"].ToString()));
                                }
                                catch (Exception ex)
                                {

                                  // ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Failed to send notification" + "');", true);
                                    ExceptionLog log = new ExceptionLog();
                                    log.SendExcepToDB(ex, System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
                                }
                              
                                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Your request has been added succesfully , it will be revised by registrar" + "');", true);
                            }

                           
                        }                    
                        Clear_Unused_file();                     
                        Response.Redirect("StudentRequests.aspx", false);
                       


                    }
                    catch (Exception ex)
                    {
                        Clear_Unused_file();
                        if (Request_Id != 0)
                        {
                            using (cmd = new SqlCommand("DeleteRequest", con))
                            {

                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@RequestId", Request_Id);
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                cmd.ExecuteNonQuery();
                                con.Close();

                            }
                        }
                        NextBtn.Enabled = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Error Occured during Save, Try Again" + "');", true);
                        ExceptionLog log = new ExceptionLog();

                        log.SendExcepToDB(ex, System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
                    }

                }


            }
        }
    }


    protected void UploadButton_Click(object sender, EventArgs e)
    {
        Page.Validate("NewCompany2");
        if (Page.IsValid)
        {
            //Continue with your logic

            List<String> FilesName = new List<string>();
            string FName = "";
            if (Excuse_FileUploadControl.HasFiles)
            {
                try
                {
                    foreach (HttpPostedFile uploadedFile in Excuse_FileUploadControl.PostedFiles)
                    {
                        FName = Session["StuSmallId"].ToString() + "_" + uploadedFile.FileName;
                        FilesName.Add(FName);
                        uploadedFile.SaveAs(System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/ExcusesDocument/"), FName));
                        // filesName.Add(dt.Rows[i]["DetailId"].ToString() + "_" + uploadedFile.FileName);
                    }
                    StatusLabel.Text = "Upload status: File uploaded!";
                    Session["FileUpload"] = FilesName;
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
        }
        else
        {
            //Display errors, hide controls, etc.
        }
    }
    protected void NonMedical_ServerValidate(object sender, ServerValidateEventArgs e)
    {
        bool isvalid = true;

        if (Excuse_ReasonDescribtion.Text.Trim() == "" && Excuse_MedicalList.SelectedValue == "0")
        {

            isvalid = false;
        }


        e.IsValid = isvalid;

    }
    public void Clear_Unused_file()// use this function to delete the unused_file
    {
        try
        {
            if (Session["FileUpload"] != null && ((List<string>)Session["FileUpload"]).Count > 0)
            {
                string files = string.Join(";", ((List<string>)Session["FileUpload"]).ToArray());
                adr = new SqlDataAdapter();
                dt = new DataTable();
                using (cmd = new SqlCommand("GetUnUsedFile", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilesName", files);
                    adr = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adr.Fill(dt);
                }
                List<string> UsedFile = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UsedFile.Add(dt.Rows[i][0].ToString());

                }
                List<string> AllFiles = new List<string>();
                AllFiles = (List<string>)Session["FileUpload"];
                List<string> UnUsed = new List<string>();
                UnUsed = AllFiles.Where(p => UsedFile.All(p2 => p2 != p)).ToList();
                if (UnUsed.Count > 0)
                {
                    foreach (string f in UnUsed)
                    {
                        string strFileFullPath = System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/ExcusesDocument/"), f);

                        // string strFileFullPath = strPhysicalFolder + f;

                        if (System.IO.File.Exists(strFileFullPath))
                        {
                            System.IO.File.Delete(strFileFullPath);
                        }

                    }

                }

            }
            Session["FileUpload"] = null;
        }
        catch (Exception ex)
        {

            ExceptionLog log = new ExceptionLog();
            log.SendExcepToDB(ex, System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
        }

    }



    protected void Excuse_MedicalList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Excuse_MedicalList.SelectedValue == "1")
        {
            Excuse_ReasonDescribtion.Text = "";
            Excuse_ReasonDescribtion.Enabled = false;

        }
        else
        {

            Excuse_ReasonDescribtion.Enabled = true;


        }
    }

    protected void chkSourceType_CheckedChanged(object sender, EventArgs e)
    {
        foreach (RepeaterItem item in EnglishEnrollmentRepeater.Items)
        {
            CheckBox chk = item.FindControl("chkSourceType") as CheckBox;
            if (chk.Checked == false)
            {
                (item.FindControl("txtDescribtion") as TextBox).Text = "";
                (item.FindControl("txtDescribtion") as TextBox).Enabled = false;
            }
            else
            {

                (item.FindControl("txtDescribtion") as TextBox).Enabled = true;

            }



        }

    }

    protected void PresentedTo_CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        int Checked = 0;
        foreach (RepeaterItem item in EnglishEnrollmentRepeater.Items)
        {
            CheckBox chk = item.FindControl("chkSourceType") as CheckBox;
            TextBox txt = item.FindControl("txtDescribtion") as TextBox;
            CustomValidator cvalid = item.FindControl("Desc_CustomValidator") as CustomValidator;

            if (chk.Checked)
            {
                Checked += 1;

            }

        }

        if (Checked == 0)
        {
            args.IsValid = false;
        }
    }

    protected void Desc_CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {

        foreach (RepeaterItem item in EnglishEnrollmentRepeater.Items)
        {
            CheckBox chk = item.FindControl("chkSourceType") as CheckBox;
            TextBox txt = item.FindControl("txtDescribtion") as TextBox;
            CustomValidator cvalid = item.FindControl("Desc_CustomValidator") as CustomValidator;
            if (chk.Checked && (txt.Text).Trim() == "")
            {
                cvalid.IsValid = false;
            }
            else
            {

                cvalid.IsValid = true;
            }


        }
        // args.IsValid = false;

    }
    protected void ArabicPresentedTo_CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        int Checked = 0;
        foreach (RepeaterItem item in ArabicEnrollment_Repeater.Items)
        {
            CheckBox chk = item.FindControl("ArchkSourceType") as CheckBox;
            if (chk.Checked)
            {
                Checked += 1;
            }

        }

        if (Checked == 0)
        {
            args.IsValid = false;
        }
    }

    protected void Arabic_CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        //Here you can loop through each item
        foreach (RepeaterItem item in ArabicEnrollment_Repeater.Items)
        {
            CheckBox chk = item.FindControl("ArchkSourceType") as CheckBox;
            TextBox txt = item.FindControl("ArtxtDescribtion") as TextBox;
            HiddenField hf = item.FindControl("ArSourceTypeId") as HiddenField;
            CustomValidator cvalid = item.FindControl("ArDesc_CustomValidator") as CustomValidator;
            if (chk.Checked && txt.Text.Trim() == "" && hf.Value == "4" && txt.Enabled == true && PresentedTo_CustomValidator.IsValid == true)
            {
                cvalid.IsValid = false;
            }
            else
            {

                cvalid.IsValid = true;
            }


        }
        //args.IsValid = false;

    }
    protected void ArchkSourceType_CheckedChanged(object sender, EventArgs e)
    {
        foreach (RepeaterItem item in ArabicEnrollment_Repeater.Items)
        {
            CheckBox chk = item.FindControl("ArchkSourceType") as CheckBox;
            if (chk.Checked == false)
            {
                (item.FindControl("ArtxtDescribtion") as TextBox).Text = "";
                (item.FindControl("ArtxtDescribtion") as TextBox).Enabled = false;
            }
            else
            {

                (item.FindControl("ArtxtDescribtion") as TextBox).Enabled = true;

            }



        }

    }

    protected void RegisterationBreak_startSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RegisterationBreak_startSemester.SelectedValue != "0")
        {
            using (con = new SqlConnection(FormRequestConStr))
            {
                using (cmd = new SqlCommand("GetYearSmester", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Row_no", int.Parse(RegisterationBreak_startSemester.SelectedValue));
                    adr = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adr.Fill(dt);
                }
                RegisterationBreak_EndSemester.DataSource = dt;
                RegisterationBreak_EndSemester.DataTextField = "title";
                RegisterationBreak_EndSemester.DataValueField = "row_num";
                RegisterationBreak_EndSemester.DataBind();
                RegisterationBreak_EndSemester.Items.Insert(0, new ListItem { Value = "0", Text = "Select Semester" });
            }
            RegisterationBreak_EndSemester.Enabled = true;
        }
        else
        {
            RegisterationBreak_EndSemester.Items.Clear();
            RegisterationBreak_EndSemester.Items.Insert(0, new ListItem { Value = "0", Text = "Select Semester" });
            RegisterationBreak_EndSemester.Enabled = false;
        }
    }


    protected void Cust_NameValidation_ServerValidate(object source, ServerValidateEventArgs args)
    {

        List<int> Form_Type = (List<int>)Session["SelectedForm"];
        List<int> Forms = FormtypeList
                   .Where(x => Form_Type.Any(y => y == x.FormTypeId)).Select(f => f.FormId).ToList();
        string FormIds = string.Join(";", Forms.ToArray());
        using (con = new SqlConnection(FormRequestConStr))
        {
            using (cmd = new SqlCommand("GetFormsLanguage", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FormsId", FormIds);
                adr = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adr.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["IsArabic"].ToString().ToLower() == "true" && txtStudentArName.Text.Trim() == "")
                        {
                            Cust_arabicName.IsValid = false;
                        }
                        if (dt.Rows[i]["IsArabic"].ToString().ToLower() == "false" && txtFullName.Text.Trim() == "")
                        {
                            cust_EnglishName.IsValid = false;
                        }

                    }
                }
            }

        }
    }





    protected void Fileupload_CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        //if (Excuse_FileUploadControl.HasFiles)
        //{
        //    var supportedTypes = new[] { "pdf", "jpg", "jpeg", "png" };
        //   // string ErrorMessage = "";
        //    try
        //    {
        //        foreach (HttpPostedFile uploadedFile in Excuse_FileUploadControl.PostedFiles)
        //        {
        //          string Filetxt=  System.IO.Path.GetExtension(uploadedFile.FileName).Substring(1);
        //            if (!supportedTypes.Contains(Filetxt))
        //            {
        //               // ErrorMessage = "File Extension Is InValid - Only Upload WORD/PDF/EXCEL/TXT File";
        //                args.IsValid = false;
        //                Fileupload_CustomValidator.ErrorMessage= "File Extension Is InValid - Only Upload pdf, jpg, jpeg, png";


        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
        //    }
        //}

        //else
        //{
        //    args.IsValid = false;

        //}


    }

    protected void btnClearUpload_Click(object sender, EventArgs e)
    {

        if (Session["FileUpload"] != null)
        {

            List<string> strfiles = new List<string>();
            strfiles = (List<string>)Session["FileUpload"];
            if (strfiles.Count > 0)
            {
                foreach (string f in strfiles)
                {
                    string strFileFullPath = System.IO.Path.Combine(Server.MapPath("~/UploadedDocuments/ExcusesDocument/"), f);

                    // string strFileFullPath = strPhysicalFolder + f;

                    if (System.IO.File.Exists(strFileFullPath))
                    {
                        System.IO.File.Delete(strFileFullPath);
                    }

                }


            }
            Excuse_FileUploadControl.PostedFile.InputStream.Dispose();
            StatusLabel.Text = "Please attach excuse supporting documenst to the request form";


        }
    }


    protected void FileUploadCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {

        if (Excuse_FileUploadControl.HasFile)
        {
            if (Excuse_FileUploadControl.PostedFile.ContentLength > 20000000)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
    }
}