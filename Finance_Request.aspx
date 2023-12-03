<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" Theme="Default" StylesheetTheme="Default" CodeFile="Finance_Request.aspx.cs" Inherits="Finance_Request" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>  
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
         <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" />
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

    <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" style = "display:none" >  
    <iframe style=" width: 100%; height: 100%;" id="irm1" src="about:blank" runat="server"></iframe>  
   <br/>  
    <asp:Button ID="Button2" runat="server" Text="Close" OnClientClick="UpdateComment()"/>  
</asp:Panel>
    <%--    <div id="SearchDiv" runat="server">

        <div class="row">
            <section class="col col-lg-12">
                <table>
                    <tr style="height: 20%">
                        <td>
                            <label style="width: 20%">ID Number</label>
                            <asp:TextBox ID="txtStudentID" runat="server" Type="Number" CssClass="form-control" ValidationGroup="NewCompany" Font-Bold="true" MaxLength="9" AutoPostBack="true" OnTextChanged="txtStudentID_TextChanged"></asp:TextBox></td>                        
                         <td style="width: 5%"></td>
                        <td>
                            <label style="width: 20%">Student Name</label>
                            <asp:TextBox ID="txtStudentName" runat="server" CssClass="form-control" ValidationGroup="NewCompany" Enabled="false" Font-Bold="true" Width="100%"></asp:TextBox></td>
                    </tr>

                    <tr style="height: 20%">
                        <td>
                            <label style="width: 20%">From : </label>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" Type="Date"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>
                            <label style="width: 20%">To :</label>
                            <asp:TextBox ID="txtTodate" runat="server" CssClass="form-control" Type="Date"></asp:TextBox>
                        </td>
                    </tr>

                    <tr style="height: 20%">
                        <td>
                            <label style="width: 20%">Payment Status : </label>
                        </td>
                        <td colspan="2">

                            <asp:RadioButtonList ID="PaymentList" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="-1" Selected="True">All</asp:ListItem>
                                <asp:ListItem Value="0">Not Paid</asp:ListItem>
                                <asp:ListItem Value="1">Paid</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                   
                </table>

            </section>
        </div>
        <br />


    </div>
    <div runat="server" style="align-items:center" >

        <asp:Button runat="server" CssClass="btn btn-primary" Style="margin-left: 5px;" ValidationGroup="NewCompany" ID="SearchBt" Text="Search" CausesValidation="false" OnClick="SearchBt_Click" ></asp:Button>


    </div>--%>
    <div id="SearchDiv" runat="server">
        <div class="row">
            <div class="col col-lg-12">
                <div class="col-lg-1">
                    <label>Student:</label>
                </div>
                <div class="col-lg-11">
                    <asp:DropDownList ID="ddlst_student" runat="server" CssClass="form-control"></asp:DropDownList>
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
                    <label>Payment Status:</label>
                </div>
                <div class="col-lg-5">
                    <asp:RadioButtonList ID="PaymentList" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="-1" Selected="True">All</asp:ListItem>
                        <asp:ListItem Value="0">Not Paid</asp:ListItem>
                        <asp:ListItem Value="1">Paid</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
        </div>
        <br />

        <div class="row">
            <div class="col col-lg-12">
                <asp:Button runat="server" CssClass="btn btn-primary" ID="SearchBt" Text="Search" OnClick="SearchBt_Click"></asp:Button>
            </div>
        </div>

    </div>
    <br />


    <div id="RequestDiv" runat="server" visible="false">
        <div class="box-content" style="overflow-x: scroll; overflow-y: scroll;">
            <asp:GridView runat="server" ID="gvForms" AutoGenerateColumns="false"
                CssClass="table table-bordered table-striped table-hover table-heading table-datatable" Width="100%"
                OnRowCommand="gvTypes_RowCommand" OnRowDataBound="gvForms_RowDataBound">
                <Columns>
                    <asp:BoundField HeaderText="Request Id" DataField="RequestId" />
                    <asp:BoundField HeaderText="StudentId" DataField="StudentId" />
                    <asp:BoundField HeaderText="Student Name" DataField="StudentName" />
					<asp:BoundField HeaderText="School" DataField="SchoolName" />
                    <asp:BoundField HeaderText="Request Form" DataField="Title" />
                    <asp:BoundField HeaderText="Periority" DataField="Periority" />
                    <asp:BoundField HeaderText="Unit Cost" DataField="UnitCost" />
                    <asp:BoundField HeaderText="No. of forms required" DataField="NOOFCopies" />
                    <asp:BoundField HeaderText="Total Cost" DataField="TotalCost" />
                    <asp:BoundField HeaderText="Payement Status" DataField="Payment_Status" />
                    <asp:TemplateField HeaderText="Payment Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtPaymentDate" Type="Date" runat="server" Text='<%# Bind("Payment_date") %>'></asp:TextBox>
                        </ItemTemplate>

                        <ItemStyle Width="75px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Save">
                        <ItemTemplate>
                            <asp:LinkButton CommandName="save" CommandArgument='<%# Eval("DetailId") %>' runat="server" ID="btnSave"> <i class="fa fa-save"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="75px" />
                    </asp:TemplateField>
                           <asp:TemplateField HeaderText="Payment Form">
                        <ItemTemplate>
                            <asp:LinkButton CommandName="downloadform" CommandArgument='<%# Eval("DetailId") %>' runat="server" ID="btndownloadform" Visible="true"> <i class="fa fa-download" title="download"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="75px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Receipt">
                        <ItemTemplate>
                            <asp:LinkButton CommandName="download" CommandArgument='<%# Eval("DetailId") %>' runat="server" ID="btndownload" Visible="false"> <i class="fa fa-download"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="75px" />
                    </asp:TemplateField>
                           <asp:TemplateField HeaderText="Suspend">
                        <ItemTemplate>
                            <asp:LinkButton CommandName="suspend" CommandArgument='<%# Eval("DetailId") %>' runat="server" ID="btnSuspend" Visible="false"> <i class="fa fa-ban"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="75px" />
                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Comments">
                        <ItemTemplate>                         
                            <asp:LinkButton CommandName="comment" CommandArgument='<%# Eval("DetailId") %>' runat="server" ID="btnComment" OnClientClick="clickbtn2(this);"  data-Detailid='<%# Eval("DetailId") %>'> <i class="fas fa-comment-alt"></i>
                            </asp:LinkButton>
                             <asp:label ID="txtNotification"  runat="server" ForeColor="Green" ></asp:label>
                            <cc1:ModalPopupExtender ID="mp2" runat="server" PopupControlID="Panl1" TargetControlID="btnComment"  
    CancelControlID="Button2" BackgroundCssClass="Background">  
</cc1:ModalPopupExtender>  
                                         
                        </ItemTemplate>
                        <ItemStyle Width="75px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <script>
        function clickbtn2(ctrl) {

            //debugger;
            var Det = { Deatail: ctrl.getAttribute("data-Detailid") };
            $.ajax({
                type: "POST",
                url: "Comments.aspx/SetSessions",
                data: JSON.stringify(Det),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var src = location.href.substring(0, location.href.lastIndexOf("/") + 1) + "DBComments.aspx"
                    $('iframe').attr('src', src);
                }
            });


        }
        function UpdateComment() {
            location.reload();

        }
    </script>
</asp:Content>

