<%@ Page Language="C#" AutoEventWireup="true" CodeFile="contactRep.aspx.cs" Inherits="contactRep" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            font-size: xx-large;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
        <br />
        <a href="How to Print your contact form.pdf" target="_blank" ><strong><span class="auto-style1">How to Print your contact form</span></strong></a>
        <br />
        <a href="How to Print your contact form.pdf" target="_blank" ><strong><span class="auto-style1"><img alt="" src="step.jpg" /><br /></span></strong></a>
        
        
<br />
        <br />
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="1057px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1092px" HyperlinkTarget="_blank">
            <LocalReport ReportPath="rdlc_contact.rdlc" EnableHyperlinks="True">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="ds_SearchContactByID" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="ds_contactTableAdapters.SP_SearchContactByIDTableAdapter"></asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
