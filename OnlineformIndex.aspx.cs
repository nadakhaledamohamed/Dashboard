using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class NotAllow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void downloadDocumentation()
    {
        if (!IsPostBack)
        {
            #region Master

            if (Page.Master != null)
            {
                ((HtmlAnchor)Page.Master.FindControl("hlforms")).Attributes["class"] = "dropdown-toggle active-parent active";
                ((HtmlControl)Page.Master.FindControl("menu5")).Attributes["style"] = "display: block;";
                ((HtmlAnchor)Page.Master.FindControl("hlApp")).Attributes["class"] = "ajax-link active-parent active";
            }

            #endregion
            using (var client = new WebClient())
            {
                client.DownloadFile("http://localhost:64132/Online_form.pdf.mpeg", "How_To_use.mpeg");
            }
        }
    }

    protected void helppdf_Click(object sender, EventArgs e)
    {
        string filePath = "";
        filePath = System.IO.Path.Combine(Server.MapPath("Online_form.pdf"));
        FileInfo file = new FileInfo(filePath);
        if (file.Exists)
        {
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "text/plain";
            Response.Flush();
            Response.TransmitFile(file.FullName);
            Response.End();
        }

    }
}