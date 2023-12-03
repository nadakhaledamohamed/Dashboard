<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" Theme="Default" StylesheetTheme="Default" CodeFile="Medical_Excuse.aspx.cs" Inherits="Finance_Request" %>
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
<%--<asp:ScriptManager ID="ScriptManager1" runat="server">  
</asp:ScriptManager> --%> 
<%--<asp:Button ID="Button1" runat="server" Text="Fill Form in Popup"  />  --%>
 
<asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" style = "display:none">  
    <iframe style=" width: 100%; height: 100%;" id="irm1" src="about:blank" runat="server"></iframe>  
   <br/>  
    <asp:Button ID="Button2" runat="server" Text="Close"   OnClientClick="UpdateComment()"/>  
</asp:Panel>

    <div id="SearchDiv" runat="server">
        <div class="row">
                <div class="col col-lg-12">
                <div class="col-lg-1">
<label>Student::</label>
                </div>
                <div class="col-lg-5">
                   <asp:DropDownList ID="ddlst_student" runat="server" CssClass="form-control" ></asp:DropDownList>
                </div>
                <div class="col-lg-1">
<label>School:</label>
                </div>
                <div class="col-lg-5">
                                                <asp:DropDownList ID="ddschool" runat="server" CssClass="form-control"></asp:DropDownList>

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
                <asp:Button runat="server"  ID="SearchBt" Text="Search"  OnClick="SearchBt_Click" ></asp:Button>
                </div>
</div>

</div>


    <br />


    <div id="RequestDiv" runat="server" visible="false">
        <div class="box-content" style="overflow-x: scroll; overflow-y: scroll;">
            <asp:GridView runat="server" ID="gvForms" AutoGenerateColumns="false"
                CssClass="table table-bordered table-striped table-hover table-heading table-datatable" Width="100%"
                OnRowCommand="gvTypes_RowCommand" OnRowDataBound="gvForms_RowDataBound" allowpaging="true"  emptydatatext="No data available."   OnPageIndexChanging="OnPageIndexChanging" PageSize="10">
                <Columns>
                    <asp:BoundField HeaderText="Request Id" DataField="RequestId" />
                    <asp:BoundField HeaderText="StudentId" DataField="StudentId" />
                    <asp:BoundField HeaderText="Student Name" DataField="StudentName" />
					<asp:BoundField HeaderText="School" DataField="SchoolName" />
                     <asp:BoundField HeaderText="Request Date" DataField="RequestDate" />
                    <asp:BoundField HeaderText="No Of Copies" DataField="NoOFCopies" />
                           <asp:BoundField HeaderText="Request Form" DataField="Title" />
                     <asp:BoundField HeaderText="Last Action By" DataField="Createdbyuser" />
                    <asp:TemplateField HeaderText="Request Status">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddStatus"  runat="server" ></asp:DropDownList>
                        </ItemTemplate>

                        <ItemStyle Width="75px" />
                    </asp:TemplateField>
                          <%--<asp:TemplateField HeaderText="Apprved">
                        <ItemTemplate>
                           <asp:DropDownList ID="ddrevised"  runat="server" ></asp:DropDownList>
                        </ItemTemplate>

                        <ItemStyle Width="75px" />
                    </asp:TemplateField>            --%>
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:LinkButton CssClass="loadiframe" CommandName="save" CommandArgument='<%# Eval("DetailId") %>' runat="server" ID="btnSave"  > <i class="fa fa-save"></i>
                            </asp:LinkButton>
                             <asp:LinkButton CommandName="print" CommandArgument='<%# Eval("DetailId") %>' runat="server" ID="btnprint" OnClientClick="clickbtn(this);"  data-Detailid='<%# Eval("DetailId") %>'> <i class="fa fa-print"></i>
                            </asp:LinkButton>
                             <asp:LinkButton CommandName="download" CommandArgument='<%# Eval("DetailId") %>' runat="server" ID="btndownload" Visible="false"> <i class="fa fa-download"></i>
                            </asp:LinkButton>
                            <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnprint"  
    CancelControlID="Button2" BackgroundCssClass="Background">  
</cc1:ModalPopupExtender> 
                            <%--<asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" style = "display:none">  
    <iframe style=" width: 100%; height: 100%;" id="irm1" src='<%# "test.aspx?id="+Eval("DetailId") %>'  runat="server"></iframe>  
   <br/>  
    <asp:Button ID="Button2" runat="server" Text="Close" />  
</asp:Panel>--%>
                        </ItemTemplate>
                        <ItemStyle Width="200px" />
                    </asp:TemplateField>
                         <asp:TemplateField HeaderText="Comments">
                        <ItemTemplate>                         
                            <asp:LinkButton CommandName="comment" CommandArgument='<%# Eval("DetailId") %>' runat="server" ID="btnComment" OnClientClick="clickbtn2(this);"  data-Detailid='<%# Eval("DetailId") %>'> <i class="fas fa-comment-alt"></i>
                            </asp:LinkButton>
                            <asp:label ID="txtNotification"  runat="server" ForeColor="Green" ></asp:label>
                                                   <cc1:ModalPopupExtender ID="mp2" runat="server" PopupControlID="Panl1" TargetControlID="btnComment"  
    CancelControlID="Button2" BackgroundCssClass="Background">  
</cc1:ModalPopupExtender>  
   <%--                          <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnprint"  
    CancelControlID="Button2" BackgroundCssClass="Background">  
</cc1:ModalPopupExtender>  --%>              
                        </ItemTemplate>
                        <ItemStyle Width="75px" />
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
    </div>
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
                    var src = location.href.substring(0, location.href.lastIndexOf("/") + 1) + "Comments.aspx"
                    $('iframe').attr('src', src);
                }
            });


        }
        function UpdateComment() {
         location.reload();
          
        }
    </script>

</asp:Content>

