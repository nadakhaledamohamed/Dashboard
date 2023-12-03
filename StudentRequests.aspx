<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Theme="Default" StylesheetTheme="Default" CodeFile="StudentRequests.aspx.cs" Inherits="_RequestForm" %>

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
<%--<asp:ScriptManager EnablePageMethods="true" ID="ScriptManager" runat="server" ScriptMode="Release" LoadScriptsBeforeUI="true"></asp:ScriptManager>--%>
<asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" style = "display:none" Width="100%">  
    <iframe style=" width: 100%; height: 100%;" id="irm1" src="about:blank" runat="server"></iframe>  
   <br/>  
    <asp:Button ID="Button2" runat="server" Text="Close"   OnClientClick="UpdateComment()"/>  
</asp:Panel>
    <div id="RequestDiv" runat="server" visible="false">
         <div class="row">
                        <div class="col-md-12">
                            <asp:Button runat="server" CssClass="btn btn-primary" Style="margin-left: 5px;"
                                ID="NewType" OnClick="NewType_Click" Text="New"></asp:Button>
                        </div>
                    </div>
        <div class="box-content"  style="overflow-x: scroll; overflow-y: scroll;">
            <asp:GridView runat="server" ID="gvForms" AutoGenerateColumns="false"
                CssClass="table table-bordered table- striped table-hover table-heading table-datatable" Width="100%"
                OnRowDataBound="gvForms_RowDataBound"  OnRowCommand="gvTypes_RowCommand">
                <Columns>
            
                    <asp:TemplateField HeaderText="Details">
                        <ItemTemplate>
                            <asp:LinkButton CommandName="view" CommandArgument='<%# Eval("ReqId") %>' runat="server" ID="lkbDelete" > <i class="fa fa-list-alt"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="75px" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Request Date" DataField="RequestDate" ItemStyle-Width="15%" />
                    <asp:BoundField HeaderText="No. of forms" DataField="FormCount" ItemStyle-Width="10%" />
                        <asp:TemplateField HeaderText="Payment permission receipt">
                        <ItemTemplate>                         
                             <asp:LinkButton CommandName="receipt" CommandArgument='<%# Eval("ReqId") %>' runat="server" ID="btnreceipt" Visible="false"><i class="fa fa-download"></i>
                            </asp:LinkButton>                        
                        </ItemTemplate>
                        <ItemStyle Width="75px" />
                    </asp:TemplateField>
                            
                        <asp:TemplateField HeaderText="Upload Receipt" ItemStyle-Width="50%">
                        <ItemTemplate>
                            <asp:FileUpload ID="fileuploadReceipt"  runat="server"  Visible="false"></asp:FileUpload>
                           <%-- <asp:RequiredFieldValidator runat="server" Display="Dynamic" ErrorMessage="*"  Font-Bold="True" ForeColor="Red" ControlToValidate="fileuploadReceipt" ValidationGroup="NewCompany2">
    </asp:RequiredFieldValidator>--%>
                              <asp:RegularExpressionValidator ID="RegExValFileUploadFileType" runat="server"
                        ControlToValidate="fileuploadReceipt" ForeColor="Red"
                        ErrorMessage="Only .jpg,.png,.jpeg,.gif , .pdf Files are allowed"    Font-Bold="True" ValidationGroup="NewCompany2"
                        Font-Size="Medium"
                        ValidationExpression="(.*?)\.(pdf|jpg|jpeg|png|gif|JPG|JPEG|PNG|GIF)$"></asp:RegularExpressionValidator>         
                               <asp:Label runat="server" ID="StatusLabel" Font-Bold="True" ForeColor="Green" Text=" Select Required files and press save " Visible="false" />  
                              <asp:CustomValidator ID="FileUploadCustomValidator" runat="server" ControlToValidate="fileuploadReceipt"
					OnServerValidate="FileUploadCustomValidator_ServerValidate" ErrorMessage="*File must be less then 3mb" ForeColor="Red" Display="Dynamic" ValidationGroup="NewCompany2" ></asp:CustomValidator>                                                
                        </ItemTemplate>

                        <ItemStyle Width="200px" />
                    </asp:TemplateField>
                          <asp:TemplateField HeaderText="Save Files">
                        <ItemTemplate>                         
                             <asp:LinkButton CommandName="save" CommandArgument='<%# Eval("ReqId") %>' runat="server" ID="btSave" Visible="false" ValidationGroup="NewCompany2" CausesValidation="true"> <i class="fa fa-save"></i>
                            </asp:LinkButton>   
                               
                        </ItemTemplate>
                        <ItemStyle Width="75px" />
                    </asp:TemplateField>
                             <asp:TemplateField HeaderText="Remove Files">
                        <ItemTemplate>                         
                             <asp:LinkButton CommandName="clear" CommandArgument='<%# Eval("ReqId") %>' runat="server" ID="btnDelete" Visible="false"> <i class="fa fa-trash"></i>
                            </asp:LinkButton>                        
                        </ItemTemplate>
                        <ItemStyle Width="75px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DownLoad Receipt">
                        <ItemTemplate>                         
                             <asp:LinkButton CommandName="download" CommandArgument='<%# Eval("ReqId") %>' runat="server" ID="btndownload" Visible="false"> <i class="fa fa-download"></i>
                            </asp:LinkButton>                        
                        </ItemTemplate>
                        <ItemStyle Width="75px" />
                    </asp:TemplateField>
                   
                                   
                </Columns>
            </asp:GridView>
        </div>
    </div>
        <div id="DetailsDiv" runat="server" visible="false">
        <div class="box-content"  style="overflow-x: scroll; overflow-y: scroll;">
            <asp:GridView runat="server" ID="gvDetails" AutoGenerateColumns="false"
                CssClass="table table-bordered table-striped table-hover table-heading table-datatable" Width="100%"
               OnRowDataBound="gvDetails_RowDataBound">
                <Columns>
                     <asp:BoundField HeaderText="ID" DataField="DetailId" Visible="false" />
                    <asp:BoundField HeaderText="Form" DataField="Title" />
                     <asp:BoundField HeaderText="Duration" DataField="Duration" />
                    <asp:BoundField HeaderText="Fees / L.E" DataField="UnitCost" />
                    <asp:BoundField HeaderText="No. Of Copies" DataField="NoOFCopies" />
                    <asp:BoundField HeaderText="Total Fees" DataField="TotalCost" />
                    <asp:BoundField HeaderText="Request Status" DataField="ReqStatus" />
                      <asp:TemplateField HeaderText="Print  Request">
                        <ItemTemplate>                         
                            <asp:LinkButton CommandName="print" CommandArgument='<%# Eval("DetailId") %>' runat="server" ID="btnprint" OnClientClick="clickbtn(this);"  data-Detailid='<%# Eval("DetailId") %>'> <i class="fa fa-print"></i>
                            </asp:LinkButton>
                             <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnprint"  
    CancelControlID="Button2" BackgroundCssClass="Background">  
</cc1:ModalPopupExtender>                
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
    <footer class="modal-footer" id="divfooter" runat="server">
        
        <asp:Button runat="server" CssClass="btn btn-primary" Style="margin-left: 5px;" ValidationGroup="NewCompany" ID="PrevBtn" Text="Return" OnClick="PrevBtn_Click" CausesValidation="false"   Visible="false"></asp:Button>
     

    </footer>
    <script>
        function clickbtn(ctrl)
        {
            debugger;
            var Detail = ctrl.getAttribute("data-Detailid");
            debugger;
          //var src = "http://localhost:64132//RequestReport.aspx?detid=" + Detail
            var src = location.href.substring(0, location.href.lastIndexOf("/") + 1)+"RequestReport.aspx?detid=" + Detail
            $('iframe').attr('src', src);
            //$('#irm1').attr('src', src);           
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

