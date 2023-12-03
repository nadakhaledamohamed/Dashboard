<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" Theme="Default" StylesheetTheme="Default" CodeFile="SCU_Approval.aspx.cs" Inherits="_DBSCU" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" />

    <style type="text/css">
        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 50%;
            height: 80%;
        }

        .lbl {
            font-size: 16px;
            font-style: italic;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" Visible="true">
    <%--<asp:ScriptManager EnablePageMethods="true" ID="ScriptManager" runat="server" ScriptMode="Release" LoadScriptsBeforeUI="true"></asp:ScriptManager>--%>
    <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" Style="display: none" Width="80%">
        <iframe style="width: 100%; height: 100%;" id="irm1" src="about:blank" runat="server"></iframe>
        <br />
        <asp:Button ID="Button2" runat="server" Text="Close" OnClientClick="UpdateComment()" />
    </asp:Panel>
    <div id="SearchDiv" runat="server">
        <div class="row">

            <div class="col-lg-1">
                <label>Student::</label>
            </div>
            <div class="col-lg-5">
                <asp:DropDownList ID="ddlst_student" runat="server" CssClass="form-control" Width="80%"></asp:DropDownList>
            </div>
            <div class="col-lg-1">
                <label>School:</label>
            </div>
            <div class="col-lg-5">
                <asp:DropDownList ID="ddschool" runat="server" CssClass="form-control" Width="80%"></asp:DropDownList>

            </div>

        </div>

        <br />
        <div class="row">

            <div class="col-lg-1">
                <label>From:</label>
            </div>
            <div class="col-lg-5">
                <asp:TextBox runat="server" ID="txtFromDate" CssClass="form-control" Type="Date" Width="50%"></asp:TextBox>
            </div>
            <div class="col-lg-1">
                <label>To:</label>
            </div>
            <div class="col-lg-5">
                <asp:TextBox runat="server" ID="txtTodate" CssClass="form-control" Type="Date" Width="50%"></asp:TextBox>
            </div>
        </div>
        <br />
        <div class="row">

            <div class="col-lg-1">
                <label>Form Type:</label>
            </div>
            <div class="col-lg-5">
                <asp:DropDownList ID="ddForms" runat="server" CssClass="form-control" Width="80%"></asp:DropDownList>
            </div>
            <div class="col-lg-1">
                <label>Request Status:</label>
            </div>
            <div class="col-lg-5">
                <asp:DropDownList ID="ddRequestStatus" runat="server" CssClass="form-control" Width="80%"></asp:DropDownList>

            </div>

        </div>
        <br />

        <div class="row">
            <div class="col col-lg-12">
                <asp:Button runat="server" ID="SearchBt" Text="Search" OnClick="SearchBt_Click" CssClass="btn btn-primary" Style="margin-left: 5px;"></asp:Button>
            </div>
        </div>

    </div>
    <div id="RequestDiv" runat="server" visible="true">

        <div class="box-content" style="overflow-x: scroll; overflow-y: scroll;">
            <asp:GridView runat="server" ID="gvForms" AutoGenerateColumns="false"
                CssClass="table table-bordered table- striped table-hover table-heading table-datatable" Width="100%"
                OnRowDataBound="gvForms_RowDataBound" OnRowCommand="gvTypes_RowCommand">
                <Columns>
                    <asp:BoundField HeaderText="Request Name" DataField="formName" />
                    <asp:BoundField HeaderText="Student Id" DataField="StudentId" />
                    <asp:BoundField HeaderText="Student Name" DataField="StudentName" />
                    
                    <asp:BoundField HeaderText="Year" DataField="student_current_year" />
                    <asp:BoundField HeaderText="Created Date" DataField="CreatedDate" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}" />
                               <asp:TemplateField HeaderText="SCU Decision">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddStatus"  runat="server" ></asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle Width="75px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                                     <asp:LinkButton CssClass="loadiframe" CommandName="save" CommandArgument='<%# Eval("RequestId") %>' runat="server" ID="btnSave"> <i class="fa fa-save fa-lg" title="Save"></i>
                            </asp:LinkButton>&nbsp;&nbsp;
                            <asp:LinkButton CommandName="print" CommandArgument='<%# Eval("RequestId") %>' runat="server" ID="btnprint" OnClientClick="clickbtn(this);" data-Detailid='<%# Eval("RequestId") %>'> <i class="fa fa-print fa-lg" title="Print"></i>
                            </asp:LinkButton>&nbsp;&nbsp;
                            <asp:LinkButton CommandName="download" CommandArgument='<%# Eval("RequestId") %>' runat="server" ID="btndownload" Visible="false"> <i class="fa fa-download fa-lg"  title="Download"></i>
                            </asp:LinkButton>&nbsp;&nbsp;
                             <asp:LinkButton CommandName="appdet" CommandArgument='<%# Eval("RequestId") %>' runat="server"  data-Detailid='<%# Eval("RequestId") %>' ID="btnappdet" Visible="true" OnClientClick="clickbtn3(this);"> <i class="fa fa-list-alt fa-lg" title="Approval Details"></i>
                            </asp:LinkButton>

                            <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnprint"
                                CancelControlID="Button2" BackgroundCssClass="Background">
                            </cc1:ModalPopupExtender>
                             <cc1:ModalPopupExtender ID="mp3" runat="server" PopupControlID="Panl1" TargetControlID="btnappdet"
                                CancelControlID="Button2" BackgroundCssClass="Background">
                            </cc1:ModalPopupExtender>
                        </ItemTemplate>
                        <ItemStyle Width="200px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Comments">
                        <ItemTemplate>
                            <asp:LinkButton CommandName="comment" CommandArgument='<%# Eval("RequestId") %>' runat="server" ID="btnComment" OnClientClick="clickbtn2(this);" data-Detailid='<%# Eval("RequestId") %>' data-faid='<%# Eval("FormApprovalId") %>'> <i class="fas fa-comment-alt"></i>
                            </asp:LinkButton>
                             <asp:HiddenField ID="hfform_approval" runat="server" Value='<%# Eval("FormApprovalId") %>' />                         
                            <asp:Label ID="txtNotification" runat="server" ForeColor="Green"></asp:Label>
                            <cc1:ModalPopupExtender ID="mp2" runat="server" PopupControlID="Panl1" TargetControlID="btnComment"
                                CancelControlID="Button2" BackgroundCssClass="Background">
                            </cc1:ModalPopupExtender>
                            <%--                          <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnprint"  
    CancelControlID="Button2" BackgroundCssClass="Background">  
</cc1:ModalPopupExtender>  --%>
                        </ItemTemplate>
                        <ItemStyle Width="75px" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Request Status" DataField="Currentstatus" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <script>
        function clickbtn(ctrl) {
            debugger;
            var Detail = ctrl.getAttribute("data-Detailid");
            debugger;
            //var src = "http://localhost:64132//RequestReport.aspx?detid=" + Detail
            //var src = "http://localhost:64132//DBRequestReport.aspx?detid=" + Detail
            var src = location.href.substring(0, location.href.lastIndexOf("/") + 1) + "DBRequestReport.aspx?detid=" + Detail
            $('iframe').attr('src', src);
            //$('#irm1').attr('src', src);           
            // $('#irm1').attr('src', src);
        }
        function clickbtn2(ctrl) {          
            debugger;
            var Det = { Deatail: ctrl.getAttribute("data-Detailid"), Approval: ctrl.getAttribute("data-faid") };
            $.ajax({
                type: "POST",
                url: "DBComments.aspx/SetSessions",
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
        function clickbtn3(ctrl) {

            debugger;
            // alert("hellooo")
            var Det = { Deatail: ctrl.getAttribute("data-Detailid") };
            $.ajax({
                type: "POST",
                url: "DBApprovalList.aspx/SetSessions",
                data: JSON.stringify(Det),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var src = location.href.substring(0, location.href.lastIndexOf("/") + 1) + "DBApprovalList.aspx"
                    $('iframe').attr('src', src);
                }
            });
        }
    </script>

</asp:Content>

