<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"  CodeFile="RequestReport_Org.aspx.cs" Inherits="ReuestReportt" Theme="Default" StylesheetTheme="Default" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" Visible="true">
    <asp:HiddenField runat="server" ID="txtStatus" />
    <div class="row">
        <div class="col-xs-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-list"></i>
                        <span>Scholarship</span>
                    </div> 
                    <div class="box-icons">
                        <a id="hlPrint" runat="server" onclick="javascript:printDiv('PrintArea')">
                            <i class="fa fa-print"></i>
                        </a>
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>
                        <a class="expand-link">
                            <i class="fa fa-expand"></i>
                        </a>
                    </div>
                    <div class="no-move"></div>
                </div>
                <div class="box-content">
                    <div class="row" style="margin-bottom: 10px;">
                        <div class="col-lg-12">                          
                        </div>
                    </div>
                     
                    <div class="row" style="margin-bottom: 10px;">
                        <div class="col-lg-12">
                            <div class="col-md-5">
                            </div>
                           <%-- <div class="col-md-2">
                                <asp:Button ID="btnView" runat="server" Text="View Report" CssClass="btn btn-primary" Width="100%" OnClick="btnView_Click" />
                            </div>--%>
                            <div class="col-md-5">
                            </div>
                        </div>
                    </div>
                

                    <div class="row">
                        <div class="col-lg-12 col-sm-6">
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="600px" Width="90%">
                                <LocalReport ReportPath="scholarship\scholarshipRpt1.rdlc">
           
                                </LocalReport>
                            </rsweb:ReportViewer>
                            </div>
                        </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        function pageLoad(sender, args) {

        };
        $(document).ready(function () {
         
            
        });
    </script>

</asp:Content>
