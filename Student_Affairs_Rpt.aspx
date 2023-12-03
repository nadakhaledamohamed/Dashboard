<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" Theme="Default" StylesheetTheme="Default" CodeFile="Student_Affairs_Rpt.aspx.cs" Inherits="Finance_Request" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>  
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">  
        .Background  
        {  
            background-color: Black;  
            filter: alpha(opacity=90);  
            opacity: 0.8;  
        }  
        .Popup  
        {  
            background-color: #FFFFFF;  
            border-width: 3px;  
            border-style: solid;  
            border-color: black;  
            padding-top: 10px;  
            padding-left: 10px;  
            width: 50%;  
            height: 80%;  
        }  
        .lbl  
        {  
            font-size:16px;  
            font-style:italic;  
            font-weight:bold;  
        }  
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" Visible="true">
<%--<asp:ScriptManager ID="ScriptManager1" runat="server">  
</asp:ScriptManager> --%> 
<%--<asp:Button ID="Button1" runat="server" Text="Fill Form in Popup"  />  --%>
 
<asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" style = "display:none">  
    <iframe style=" width: 100%; height: 100%;" id="irm1" src="RequestReport.aspx?detid=0" runat="server"></iframe>  
   <br/>  
    <asp:Button ID="Button2" runat="server" Text="Close" />  
</asp:Panel>

    <div id="SearchDiv" runat="server">
        <div class="row">
            <div class="col col-lg-12">
                <div class="col-lg-1">
<label>Student:</label>
                </div>
                <div class="col-lg-11">
<asp:DropDownList ID="ddlst_student" runat="server" CssClass="form-control" ></asp:DropDownList>
                </div>
            </div>
        </div>
        <br />
<div class="row">
            <div class="col col-lg-12">
                <div class="col-lg-1">
<label>From:</label>
                </div>
                <div class="col-lg-5">
<asp:TextBox runat="server" ID="txtFromDate" CssClass="form-control" Type="Date"></asp:TextBox>
                </div>
                <div class="col-lg-1">
<label>To:</label>
                </div>
                <div class="col-lg-5">
<asp:TextBox runat="server" ID="txtTodate" CssClass="form-control" Type="Date"></asp:TextBox>
                </div>
            </div>
</div>
        <br />
        <div class="row">
                <div class="col col-lg-12">
                <div class="col-lg-1">
<label>Form Type:</label>
                </div>
                <div class="col-lg-5">
                    <asp:DropDownList ID="ddForms" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-lg-1">
<label>Request Status:</label>
                </div>
                <div class="col-lg-5">
                                                <asp:DropDownList ID="ddRequestStatus" runat="server" CssClass="form-control"></asp:DropDownList>

                </div>
            </div>
</div>
        <br />

                <div class="row">
                <div class="col col-lg-12">
                <asp:Button runat="server" CssClass="btn btn-primary"   ID="SearchBt" Text="Search"  OnClick="SearchBt_Click" ></asp:Button>
                </div>
</div>

</div>


    <br />


        <rsweb:ReportViewer ID="ReportViewer1"   runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="600px" Width="90%" Visible="true">
                                <LocalReport ReportPath="Reports\MilitaryRpt.rdlc">
           
                                </LocalReport>
                            </rsweb:ReportViewer>

    <script>
        function clickbtn(ctrl)
        {
            debugger;
            var Detail = ctrl.getAttribute("data-Detailid");
            debugger;
          //var src = "http://localhost:64132//RequestReport.aspx?detid=" + Detail
            var src = location.href.substring(0, location.href.lastIndexOf("/") + 1)+"RequestReport.aspx?detid=" + Detail
            $('iframe').attr('src',src);
           // $('#irm1').attr('src', src);
        }
    </script>

</asp:Content>

